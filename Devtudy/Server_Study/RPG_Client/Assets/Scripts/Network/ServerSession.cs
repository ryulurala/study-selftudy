using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

namespace DummyClient
{
    // Session 클래스를 상속받아 사용(콘텐츠단)
    class ServerSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"OnConnected: {endPoint}");
        }
        public override void OnDisconnected(EndPoint endPoint)
        {
            System.Console.WriteLine($"OnDisconnected: {endPoint}");
        }
        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            // 몇 바이트를 보냈는지
            // System.Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}