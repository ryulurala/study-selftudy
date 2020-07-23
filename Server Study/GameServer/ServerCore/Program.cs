using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    // Session 클래스를 상속받아 사용(콘텐츠단)
    class GameSession : Session
    {
        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"Onconnected: {endPoint}");

            // 연결되고 후처리
            byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");
            Send(sendBuff);
            Thread.Sleep(1000);
            Disconnect();
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            System.Console.WriteLine($"OnDisconnected: {endPoint}");
        }

        public override void OnRecv(ArraySegment<byte> buffer)
        {
            // (버퍼, offset(어디부터 시작), 받은 바이트 수)
            string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"[From Client] {recvData}");
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