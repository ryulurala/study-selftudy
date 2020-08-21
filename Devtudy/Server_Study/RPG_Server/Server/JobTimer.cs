using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;

namespace Server
{
    struct JobTimerElem : IComparable<JobTimerElem>
    {
        public int execTick;    // 실행 시간
        public Action action;   // 할 Action

        public int CompareTo(JobTimerElem other)
        {
            return other.execTick - execTick;       // Tick이 작은 것이 먼저
        }
    }

    // 1) [연결리스트HEAD][연결리스트HEAD][...][...]...[]: list로 관리
    // 2) 우선 순위 큐 이용
    class JobTimer
    {
        PriorityQueue<JobTimerElem> _pq = new PriorityQueue<JobTimerElem>();
        object _lock = new object();

        public static JobTimer Instance { get; } = new JobTimer();

        public void Push(Action action, int tickAfter = 0)  // 몇 tick 후에 action을 시작
        {
            JobTimerElem job;
            job.execTick = System.Environment.TickCount + tickAfter;    // 현재 시간 + 몇 tick 후에
            job.action = action;        // Action 시작

            lock (_lock)
            {
                _pq.Push(job);
            }
        }

        public void Flush()
        {
            while (true)
            {
                int now = System.Environment.TickCount;

                JobTimerElem job;

                lock (_lock)
                {
                    if (_pq.Count == 0)
                        break;          // while문을 나감

                    job = _pq.Peek();       // top() 조회
                    if (job.execTick > now)     // 아직이다.
                        break;

                    _pq.Pop();
                }

                job.action.Invoke();    // 일감을 실행 명령
            }
        }
    }
}