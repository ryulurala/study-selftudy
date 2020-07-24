using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore
{
    public class RecvBuffer
    {
        // [rw][][][][][][][][][] : 비어있는 상태(커서위치 r:read, w:write)
        // [r][][][][][w][][][][] : 5 byte까지는 받았다.
        // [][][][][][rw][][][][] : 5 byte까지는 읽었다.
        // Read는 읽어서 처리, Write는 받아서 쓰기
        ArraySegment<byte> _buffer;
        int _readPos;       // Read cursor
        int _writePos;      // Write cursor
        public RecvBuffer(int bufferSize)
        {
            _buffer = new ArraySegment<byte>(new byte[bufferSize], 0, bufferSize);
        }

        public int DataSize { get { return (_writePos - _readPos); } }  // 쌓여있는(처리할) Data 크기
        public int FreeSize { get { return (_buffer.Count - _writePos); } }   // 버퍼 남은 공간

        // 읽어야할 세그먼트
        public ArraySegment<byte> ReadSegment
        {
            get { return new ArraySegment<byte>(_buffer.Array, _buffer.Offset + _readPos, DataSize); }
        }
        // 받을 수 있는 유효 범위
        public ArraySegment<byte> WriteSegment
        {
            get { return new ArraySegment<byte>(_buffer.Array, _buffer.Offset + _writePos, FreeSize); }
        }

        // [rw][][][][][][][][][] : rw를 앞으로 초기화
        public void Clean()
        {
            int dataSize = DataSize;
            if (dataSize == 0)  // r, w cursor가 겹침: cursor만 처음으로 옮겨줌
            {
                // [][][][][][rw][][][][]
                // 남은 데이터가 없으면 복사하지 않고 커서 위치만 리셋
                _readPos = _writePos = 0;
            }
            else
            {
                // [][][][][r][][][w][][]
                // 나머지가 있으면 시작 위치로 복사
                // Source(2), Destination(2), size(1)

                Array.Copy(_buffer.Array, _buffer.Offset + _readPos, _buffer.Array, _buffer.Offset, dataSize);
                // [r][][][][w][][][][][]
                _readPos = 0;
                _writePos = dataSize;
            }
        }

        public bool OnRead(int numOfBytes)      // Read를 했을 때(성공 처리)
        {
            if (numOfBytes > DataSize)
            {   // 비정상
                return false;
            }
            // Read Cursor를 옮겨줌
            _readPos += numOfBytes;
            return true;
        }

        public bool OnWrite(int numOfBytes)     // Recv를 했을 때(성공 처리)
        {
            if (numOfBytes > FreeSize)
            {   // 비정상
                return false;
            }
            // Write Cursor를 옮겨줌
            _writePos += numOfBytes;
            return true;
        }
    }
}