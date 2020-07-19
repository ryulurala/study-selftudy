using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static volatile int count = 0;
        static ExReaderWriterLock _lock = new ExReaderWriterLock();

        static void Main(string[] args)
        {
            Task t1 = new Task(delegate ()
            {
                for (int i = 0; i < 100000; i++)
                {
                    _lock.WriteLock();
                    count++;
                    _lock.WriteUnLock();
                }
            });

            Task t2 = new Task(delegate ()
            {
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        _lock.WriteLock();
                        count--;
                        _lock.WriteUnLock();
                    }
                }
            });

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            System.Console.WriteLine("최종 값 = " + count);
        }
    }
}