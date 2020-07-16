using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static void MainThread(object state)    // Thread 무슨 일할 지
        {
            for (int i = 0; i < 5; i++)
            {
                System.Console.WriteLine("Hello Thread");
            }
        }
        static void Main(string[] args)     // 사장
        {
            // ThreadPool로 인한 먹통을 방지.
            // 사실 WorkThreadPool에 포함되지만 옵션을 넣어서 별도 쓰레드로 생성하게 함. 옵션 없으면 먹통 상황 같음.
            // TaskCreationOptions.LongRunning : 오래 걸린다는 것을 알려줌.
            // WorkThreadPool보다 효율적이다.
            for (int i = 0; i < 5; i++)
            {
                Task t = new Task(() => { while (true) { } }, TaskCreationOptions.LongRunning);
                t.Start();
            }
            // 단기 알바(인원 수 설정 가능, 구체적인 것은 불가능)
            // 생성하여 대기하다가 필요할 때마다 사용하고 많이 안쓰이면 삭제
            // 인력 사무소같은 느낌
            // 짧은 일감으로 쓰인다. (긴 것은 직접 new 해서 만드는 것을 권장) 이유는 먹통이 된다.
            ThreadPool.SetMinThreads(1, 1);     // 첫 번째 인자 : 인원 수, 두 번째 인자 : IO와 관련된 Network를 기다린다.
            ThreadPool.SetMaxThreads(5, 5);

            // for (int i = 0; i < 4; i++)
            // {
            //     ThreadPool.QueueUserWorkItem(obj => { while (true) { } });  // object를 받아서 => ...일을 해라.
            // }

            ThreadPool.QueueUserWorkItem(MainThread);       // Call-Back, 파라미터가 있어야함

            // Thread t = new Thread(MainThread);      // 정직원 고용
            // t.Name = "Test thread";

            // // t.IsBackground = false;  // default 상태
            // // C# 에서는 기본적으로 Foreground에서 Thread가 실행된다.
            // t.IsBackground = true;  // BackGround에서 실행된다.
            // t.Start();                  // Thread 시작
            // System.Console.WriteLine("Waiting for thread");
            // t.Join();                   // Background Thread가 끝날 때까지 기다림. C++도 Join이다
            // Console.WriteLine("Hello World!");

            while (true) ;  // main 함수가 죽으면 thread도 죽는다.
        }
    }
}