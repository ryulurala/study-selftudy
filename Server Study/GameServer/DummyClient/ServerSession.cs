using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

namespace DummyClient
{
    // 패킷은 공통적으로 코드를 작성한다.
    // 기본적인 패킷 헤더
    public abstract class Packet    // 패킷은 최대한 압축해서 보내야된다.
    {   // 경우에 따라서 제거(size packetId)
        public ushort size;        // packet size를 모르므로   (ushort(2) vs uint(4))
        public ushort packetId;    // 무슨 패킷인지 구별       (ushort(2) vs uint(4))

        public abstract ArraySegment<byte> Write();
        public abstract void Read(ArraySegment<byte> seg);
    }

    class PlayerInfoReq : Packet    // Client -> Server
    {
        public long playerId;
        public string name;

        public PlayerInfoReq()      // 생성자
        {
            this.packetId = (ushort)PacketID.PlayerInfoReq;
        }
        public override void Read(ArraySegment<byte> segment)
        {
            // Deserialization
            ushort count = 0;

            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);

            // ushort size = BitConverter.ToUInt16(seg.Array, seg.Offset);
            count += sizeof(ushort);
            // ushort id = BitConverter.ToUInt16(seg.Array, seg.Offset + count);
            count += sizeof(ushort);

            this.playerId = BitConverter.ToInt64(span.Slice(count, span.Length - count));    // 필드 변수를 직접 채워줌
            count += sizeof(long);

            // string parsing
            // length[2byte] + byte[]
            ushort nameLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
            count += sizeof(ushort);

            // Decoding
            this.name = Encoding.Unicode.GetString(span.Slice(count, nameLen));

            // 범위를 초과하는 값을 Parsing을 하려고 하면 Exception 발생으로 Option이 있다.
            // BitConverter.ToInt64(new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count));

            // System.Console.WriteLine($"PlayerInfoReq: {playerId}");
        }

        public override ArraySegment<byte> Write()
        {
            // 연결되고 후처리
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);       // 공간 예약

            ushort count = 0;       // 자동화하기 편리하게
            bool success = true;

            Span<byte> span = new Span<byte>(segment.Array, segment.Offset, segment.Count);     // for Slice

            // 실패, 성공 여부가 갈림---version 1, 공간이 모자르면 실패
            count += sizeof(ushort);
            success &= BitConverter.TryWriteBytes(segment.Slice(count, span.Length - count), this.packetId);
            count += sizeof(ushort);     // 나중에 자동화
            success &= BitConverter.TryWriteBytes(segment.Slice(count, span.Length - count), this.playerId);
            count += sizeof(long);

            // string(C++은 \0로 판별)
            // string length [2byte] + byte[]

            // UTF-16(Unicode)
            // Length[2byte]
            ushort nameLen = (ushort)Encoding.Unicode.GetByteCount(this.name);
            success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), nameLen);
            count += sizeof(ushort);

            // Index는 count부터 nameLen크기만큼 seg.Array에 복사
            // byte[]
            Array.Copy(Encoding.Unicode.GetBytes(this.name), 0, segment.Array, count, nameLen);
            count += nameLen;

            success &= BitConverter.TryWriteBytes(span, count);         // 마지막 최종 카운트

            if (success == false)
            {
                return null;
            }

            // 사이즈는 마지막에 알 수 있다.
            return SendBufferHelper.Close(count);
        }
    }

    // class PlayerInfoOk : Packet     // Server -> Client
    // {
    //     public int hp;
    //     public int attack;
    // }

    public enum PacketID    // 자동화해야 할 내용
    {
        PlayerInfoReq = 1,
        PlayerInfoOk = 2,
    }

    // Session 클래스를 상속받아 사용(콘텐츠단)
    class ServerSession : Session
    {
        // unsafe를 표시하면 포인터 접근이 가능하다.(C++ 처럼) ---version 2, 비트연산노가다도 있다.
        // static unsafe void ToBytes(byte[] array, int offset, ulong value)
        // {
        //     fixed (byte* ptr = &array[offset])
        //         *(ulong*)ptr = value;
        // }

        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"Onconnected: {endPoint}");
            PlayerInfoReq packet = new PlayerInfoReq() { playerId = 1001, name = "ABCD", };

            // 보낸다(5번)
            // for (int i = 0; i < 5; i++)
            {
                // 옛날 방식
                // byte[] size = BitConverter.GetBytes(packet.size);   // int byte를 byte 배열로 만듦
                // byte[] packetId = BitConverter.GetBytes(packet.packetId);   // int byte를 byte 배열로 만듦
                // byte[] playerId = BitConverter.GetBytes(packet.playerId);

                // // buffer1 + buffer2 -> sendBuff : [ 100 ][ 10 ] : 8 byte
                // Array.Copy(size, 0, seg.Array, seg.Offset, 2);
                // count += 2;
                // Array.Copy(packetId, 0, seg.Array, seg.Offset + count, 2);
                // count += 2;
                // Array.Copy(playerId, 0, seg.Array, seg.Offset + count, 8);
                // count += 8;

                ArraySegment<byte> sendBuff = packet.Write();   // Serialize

                if (sendBuff != null)
                {
                    Send(sendBuff);  // SendBuff에 있는 것을 한 번에 보내줌(Blocking 함수)
                }
            }
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            System.Console.WriteLine($"OnDisconnected: {endPoint}");
        }

        public override int OnRecv(ArraySegment<byte> buffer)
        {
            // (버퍼, offset(어디부터 시작), 받은 바이트 수)
            string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"[From Server] {recvData}");
            return buffer.Count;
        }

        public override void OnSend(int numOfBytes)
        {
            // 몇 바이트를 보냈는지
            System.Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}