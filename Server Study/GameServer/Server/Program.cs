using System;
using System.Net;
using System.Text;
using System.Threading;
using ServerCore;

namespace Server
{
    // 기본적인 패킷 설계
    class Packet    // 패킷은 최대한 압축해서 보내야된다.
    {
        public ushort size;        // packet size를 모르므로   (ushort(2) vs uint(4))
        public ushort packetId;    // 무슨 패킷인지 구별       (ushort(2) vs uint(4))
    }

    // Session 클래스를 상속받아 사용(콘텐츠단)
    class GameSession : PacketSession
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
            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + 2);

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

    class Program
    {
        static Listener _listener = new Listener();

        static void Main(string[] args)
        {
            // DNS: Domain Name System: DNS서버가 네트워크 망에 하나가 더 있어서 주소를 찾아준다.
            string host = Dns.GetHostName();
            System.Console.WriteLine("Host = " + host);
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            System.Console.WriteLine("IPHost = " + ipHost.ToString());
            IPAddress ipAddr = ipHost.AddressList[0];  // 분산한 서버에 따라 해당 IP에 여러 개 있을 수도 있다.
            System.Console.WriteLine("IPAddress = " + ipAddr.ToString());
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 최종 주소, 포트는 식당 정문, 후문 느낌
            System.Console.WriteLine("EndPoint = " + endPoint.ToString());

            // Initialize
            // 혹시라도 누가 들어오면 OnAcceptHandler로 알려달라
            // 무엇을 만들지 지정만 해달라.(게임 매니저로 or 람다로)
            _listener.init(endPoint, () => { return new GameSession(); });      // GameSession을 만든다.
            System.Console.WriteLine("Listening...");

            // 24시간 영업: 무한루프 -> 프로그램이 종료되지 않게 함(아무 일도 하지 않지만)
            while (true) ;
        }
    }
}