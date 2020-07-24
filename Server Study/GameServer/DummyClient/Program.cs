using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ServerCore;

namespace DummyClient
{
    // 기본적인 패킷 설계
    class Packet    // 패킷은 최대한 압축해서 보내야된다.
    {
        public ushort size;        // packet size를 모르므로   (ushort(2) vs uint(4))
        public ushort packetId;    // 무슨 패킷인지 구별       (ushort(2) vs uint(4))
    }
    // Session 클래스를 상속받아 사용(콘텐츠단)
    class GameSession : Session
    {
        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"Onconnected: {endPoint}");
            Packet packet = new Packet() { size = 4, packetId = 7 };

            // 보낸다(5번)
            for (int i = 0; i < 5; i++)
            {
                // 연결되고 후처리
                // byte[] sendBuff = new byte[1024];
                ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
                byte[] buffer1 = BitConverter.GetBytes(packet.size);   // int byte를 byte 배열로 만듦
                byte[] buffer2 = BitConverter.GetBytes(packet.packetId);   // int byte를 byte 배열로 만듦

                // buffer1 + buffer2 -> sendBuff : [ 100 ][ 10 ] : 8 byte
                Array.Copy(buffer1, 0, openSegment.Array, openSegment.Offset, buffer1.Length);
                Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer1.Length, buffer2.Length);

                ArraySegment<byte> sendBuff = SendBufferHelper.Close(packet.size);

                Send(sendBuff);  // SendBuff에 있는 것을 한 번에 보내줌(Blocking 함수)
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

    class Program
    {
        static void Main(string[] args)
        {
            // DNS: Domain Name System: DNS서버가 네트워크 망에 하나가 더 있어서 주소를 찾아준다.
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];  // 분산한 서버에 따라 해당 IP에 여러 개 있을 수도 있다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 최종 주소, 포트는 식당 정문, 후문 느낌

            Connector connector = new Connector();

            connector.Connect(endPoint, () => { return new GameSession(); });

            while (true)
            {
                try
                {
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                }
                Thread.Sleep(500);
            }
        }
    }
}