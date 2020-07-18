using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
  class Program
  {
    static int number = 0;
    static object _obj = new object();
    static void Thread_1()
    {
      for (int i = 0; i < 100000; i++)
      {
        lock (_obj)   // 내부적으로는 Monitor.Enter(), Monitor.Exit()을 사용한다(대부분 이걸 사용)
        {
          number++;
        }
        try
        {
          // 상호배제한 행위 Mutual-Exclusive
          Monitor.Enter(_obj);  // 문을 잠구는 행위 (_obj는 자물쇠 역할)
          number++;
          // return;            // 문을 열지 않고 return함, 대신 다른 곳에서는 먹통이 된다.(DeadLock 상태)
          // 싱글 쓰레드 환경

        }
        finally // 한 번은 무조건 실행
        {

          Monitor.Exit(_obj);   // 문을 여는 행위
        }
      }

    }
    static void Thread_2()
    {
      for (int i = 0; i < 100000; i++)
      {
        Monitor.Enter(_obj);

        number--;

        Monitor.Exit(_obj);
      }
    }
    static void Main(string[] args)
    {
      Task task1 = new Task(Thread_1);
      Task task2 = new Task(Thread_2);
      task1.Start();
      task2.Start();

      Task.WaitAll(task1, task2); // 끝날 때까지 기다림

      System.Console.WriteLine(number);
    }
  }
}