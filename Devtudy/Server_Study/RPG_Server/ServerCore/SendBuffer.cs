using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServerCore
{
    public class SendBufferHelper
    {       // 쓰레드끼리 경합을 없애기 위해서
        public static ThreadLocal<SendBuffer> CurrentBuffer = new ThreadLocal<SendBuffer>(() => { return null; });
        public static int ChunkSize { get; set; } = 4096 * 100;
        public static ArraySegment<byte> Open(int reserveSize)
        {
            if (CurrentBuffer.Value == null) // 한 번도 사용 안함
            {
                CurrentBuffer.Value = new SendBuffer(ChunkSize);
            }
            if (CurrentBuffer.Value.FreeSize < reserveSize)
            {
                CurrentBuffer.Value = new SendBuffer(ChunkSize);
            }
            return CurrentBuffer.Value.Open(reserveSize);
        }
        public static ArraySegment<byte> Close(int usedSize)
        {
            return CurrentBuffer.Value.Close(usedSize);
        }
    }

    public class SendBuffer // 일회용
    {
        // [u][][][][][][][][][]
        byte[] _buffer;
        int _usedSize = 0;  // 일종의 커서
        public SendBuffer(int chunkSize)        // SendBuffer가 어마어마하게 크게 잡을거라는 메세지
        {
            _buffer = new byte[chunkSize];
        }
        public int FreeSize { get { return _buffer.Length - _usedSize; } }  // 남은 공간

        public ArraySegment<byte> Open(int reserveSize)     // 예약 공간을 매개 변수
        {
            if (reserveSize > FreeSize)
            {
                return null;
            }
            return new ArraySegment<byte>(_buffer, _usedSize, reserveSize);
        }
        public ArraySegment<byte> Close(int usedSize)       // 실제로 사용한 사이즈가 매개 변수
        {
            ArraySegment<byte> segment = new ArraySegment<byte>(_buffer, _usedSize, usedSize);
            _usedSize += usedSize;      // 커서 옮김
            return segment;
        }
    }
}