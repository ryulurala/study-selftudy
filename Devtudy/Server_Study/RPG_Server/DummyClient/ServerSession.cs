using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

namespace DummyClient
{
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
            var skill = new PlayerInfoReq.Skill() { id = 101, level = 1, duration = 3.0f };
            packet.skills.Add(skill);

            packet.skills.Add(new PlayerInfoReq.Skill() { id = 101, level = 1, duration = 3.0f });
            packet.skills.Add(new PlayerInfoReq.Skill() { id = 201, level = 2, duration = 4.0f });
            packet.skills.Add(new PlayerInfoReq.Skill() { id = 301, level = 3, duration = 5.0f });
            packet.skills.Add(new PlayerInfoReq.Skill() { id = 401, level = 4, duration = 6.0f });

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