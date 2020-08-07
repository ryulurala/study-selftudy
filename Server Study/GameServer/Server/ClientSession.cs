using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using ServerCore;

namespace Server
{
    // Session 클래스를 상속받아 사용(콘텐츠단)
    class ClientSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"Onconnected: {endPoint}");

            // Packet packet = new Packet() { size = 100, packetId = 10 };

            // // 연결되고 후처리
            // // byte[] sendBuff = new byte[1024];
            // ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
            // byte[] buffer1 = BitConverter.GetBytes(packet.size);   // int byte를 byte 배열로 만듦
            // byte[] buffer2 = BitConverter.GetBytes(packet.packetId);   // int byte를 byte 배열로 만듦

            // // buffer1 + buffer2 -> sendBuff : [ 100 ][ 10 ] : 8 byte
            // Array.Copy(buffer1, 0, openSegment.Array, openSegment.Offset, buffer1.Length);
            // Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer1.Length, buffer2.Length);

            // ArraySegment<byte> sendBuff = SendBufferHelper.Close(buffer1.Length + buffer2.Length);

            // // byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");
            // Send(sendBuff);
            Thread.Sleep(5000);
            Disconnect();
        }
        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            // Deserialization
            ushort count = 0;
            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            count += 2;
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            count += 2;

            // Parsing
            switch ((PacketID)id)
            {
                case PacketID.PlayerInfoReq:
                    {
                        PlayerInfoReq playerInfoReq = new PlayerInfoReq();
                        playerInfoReq.Read(buffer);     // Deserialize
                        System.Console.WriteLine($"PlayerInfoReq: {playerInfoReq.playerId} {playerInfoReq.name}");

                        foreach (PlayerInfoReq.Skill skill in playerInfoReq.skills)
                        {
                            System.Console.WriteLine($"Skill: {skill.id} {skill.level} {skill.duration}");
                        }
                    }
                    break;
            }

            System.Console.WriteLine($"RecvPacketId: {id}, Size: {size}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            System.Console.WriteLine($"OnDisconnected: {endPoint}");
        }

        public override void OnSend(int numOfBytes)
        {
            // 몇 바이트를 보냈는지
            System.Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }

}