using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    // 정책: 재귀적 락을 허용할 지(No), 스핀락 정책(5000번 -> Yield)
    // 재귀적 락(Yes) : WriteLock->WriteLock Ok, WriteLock->ReadLock Ok, ReadLock->WriteLock No
    class ExReaderWriterLock
    {
        const int EMPTY_FLAG = 0x00000000;
        const int WRITE_MASK = 0x7FFF0000;
        const int READ_MASK = 0x0000FFFF;
        const int MAX_SPIN_COUNT = 5000;

        // int형 32bits = 
        //  [Unused(1)], : 음수가 되는 것을 방지
        //  [WriteThreadId(15)], : Write 한 번에 한 쓰레드만 잡을 때, 그 쓰레드 ID : 상호-배타적임
        //  [ReadCount(16)] : 여러 쓰레드들이 동시에 잡을 때, 쓰레드 개수
        int _flag = EMPTY_FLAG;
        int _writeCount = 0;    // 상호-배타적인 관계, 멀티쓰레드 문제가 없어서 별도의 변수 Ok
        public void WriteLock()
        {
            // 동일 쓰레드가 WriteLock을 이미 획득하고 있는지 확인
            int _lockThredId = (_flag & WRITE_MASK) >> 16;
            if (Thread.CurrentThread.ManagedThreadId == _lockThredId)
            {
                // 성공
                _writeCount++;  // 재귀적 락
                return;
            }

            int desired = (Thread.CurrentThread.ManagedThreadId << 16) & WRITE_MASK;
            // 아무도 WriteLock or ReadLock을 획득하고 있지 않을 때, 경합해서 소유권을 얻는다.
            while (true)
            {
                for (int i = 0; i < MAX_SPIN_COUNT; i++)
                {
                    // 시도를 해서 성공하면 return
                    // if (_flag == EMPTY_FLAG)    // 1단계
                    // {
                    //     _flag = desired;        // 2단계 로 구성되어 멀티쓰레드 환경에서 버그가 발생한다.
                    //      return;
                    // }
                    if (Interlocked.CompareExchange(ref _flag, desired, EMPTY_FLAG) == EMPTY_FLAG)
                    {
                        _writeCount = 1;    // 재귀적 락
                        return;
                    }
                }

                Thread.Yield();     // 실패했을 경우 양보
            }
        }

        public void WriteUnLock()
        {
            int lockCount = --_writeCount;
            if (lockCount == 0)
            {
                // WriteLock을 한 쓰레드만 WriteLock을 해제할 수 있다.
                Interlocked.Exchange(ref _flag, EMPTY_FLAG);    // 초기 상태로 바꿔줌
            }

        }

        public void ReadLock()
        {
            // 동일 쓰레드가 WriteLock을 이미 획득하고 있는지 확인
            int lockThredId = (_flag & WRITE_MASK) >> 16;
            if (Thread.CurrentThread.ManagedThreadId == lockThredId)
            {
                // 성공
                Interlocked.Increment(ref _flag);   // 재귀적 락
                return;
            }
            // 아무도 WriteLock을 획득하고 있지 않으면, ReadCount를 1 증가시킨다.
            while (true)
            {
                for (int i = 0; i < MAX_SPIN_COUNT; i++)
                {
                    // 아무도 WriteLock을 획득하고 있지 않다.
                    // if ((_flag & WRITE_MASK) == EMPTY_FLAG) // 1단계
                    // {
                    //     _flag = _flag + 1;                  // 2단계
                    //     return;
                    // }
                    int expected = (_flag & READ_MASK);     // WriteFlag를 0으로 만들어줌, A(0), B(0) 동시에 들어옴
                    if (Interlocked.CompareExchange(ref _flag, expected + 1, expected) == expected) // A(0->1), B(0->1) 둘 중에 먼저 한 사람이 1이 되면 한 명은 못함
                    {
                        return;
                    }
                }
                Thread.Yield();
            }
        }

        public void ReadUnLock()
        {
            // 1을 줄여줌.
            Interlocked.Decrement(ref _flag);
        }
    }
}