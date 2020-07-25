using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

namespace DummyClient
{

    // 기본적인 패킷 헤더
    class Packet    // 패킷은 최대한 압축해서 보내야된다.
    {
        public ushort size;        // packet size를 모르므로   (ushort(2) vs uint(4))
        public ushort packetId;    // 무슨 패킷인지 구별       (ushort(2) vs uint(4))
    }

    class PlayerInfoReq : Packet
    {
        public long playerId;
    }

    class PlayerInfoOk : Packet
    {
        public int hp;
        public int attack;
    }

    public enum PacketID
    {
        PlayerInfoReq = 1,
        PlayerInfoOk = 2,
    }

    // Session 클래스를 상속받아 사용(콘텐츠단)
    class ServerSession : Session
    {
        // unsafe를 표시하면 포인터 접근이 가능하다.(C++ 처럼)
        // static unsafe void ToBytes(byte[] array, int offset, ulong value)
        // {
        //     fixed (byte* ptr = &array[offset])
        //         *(ulong*)ptr = value;
        // }

        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"Onconnected: {endPoint}");
            PlayerInfoReq packet = new PlayerInfoReq() { packetId = (ushort)PacketID.PlayerInfoReq, playerId = 1001 };

            // 보낸다(5번)
            // for (int i = 0; i < 5; i++)
            {
                // 연결되고 후처리
                // byte[] sendBuff = new byte[1024];
                ArraySegment<byte> seg = SendBufferHelper.Open(4096);       // 공간 예약

                ushort count = 0;
                bool success = true;

                // 실패, 성공 여부가 갈림
                count += 2;
                success &= BitConverter.TryWriteBytes(new Span<byte>(seg.Array, seg.Offset + count, seg.Count - count), packet.packetId);
                count += 2;
                success &= BitConverter.TryWriteBytes(new Span<byte>(seg.Array, seg.Offset + count, seg.Count - count), packet.playerId);
                count += 8;
                success &= BitConverter.TryWriteBytes(new Span<byte>(seg.Array, seg.Offset, seg.Count), count);

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

                // 사이즈는 마지막에 알 수 있다.
                ArraySegment<byte> sendBuff = SendBufferHelper.Close(count);
                if (success)
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