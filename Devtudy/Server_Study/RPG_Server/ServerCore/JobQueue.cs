using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore
{
    public interface IJobQueue
    {
        void Push(Action job);
    }

    public class JobQueue : IJobQueue
    {
        Queue<Action> _jobQueue = new Queue<Action>();
        object _lock = new object();
        bool _flush = false;        // Queue에 쌓인 것을 실행할건지 말건지 정하는 변수

        public void Push(Action job)
        {
            bool flush = false;

            lock (_lock)
            {
                _jobQueue.Enqueue(job);
                if (_flush == false)
                    flush = _flush = true;
            }

            if (flush)
                Flush();        // 순차적으로 하나의 쓰레드만 처리
        }
        void Flush()
        {
            while (true)
            {
                Action action = Pop();
                if (action == null)
                    return;

                action.Invoke();
            }
        }
        Action Pop()
        {
            lock (_lock)
            {
                if (_jobQueue.Count == 0)
                {
                    _flush = false;
                    return null;
                }
                return _jobQueue.Dequeue();
            }
        }
    }

}