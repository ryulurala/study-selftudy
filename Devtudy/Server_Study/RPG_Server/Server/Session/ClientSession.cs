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
        public int SessionId { get; set; }
        public GameRoom Room { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }

        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"Onconnected: {endPoint}");

            Program.Room.Push(
                () => Program.Room.Enter(this)
            );
        }
        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);      // Singleton 호출
        }
        public override void OnDisconnected(EndPoint endPoint)
        {
            SessionManager.Instance.Remove(this);
            if (Room != null)
            {
                GameRoom room = Room;   // Room이 null로 되어 크래시 발생을 방지
                room.Push(
                    () => room.Leave(this)
                );
                Room = null;
            }
            System.Console.WriteLine($"OnDisconnected: {endPoint}");
        }
        public override void OnSend(int numOfBytes)
        {
            // 몇 바이트를 보냈는지
            // System.Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}