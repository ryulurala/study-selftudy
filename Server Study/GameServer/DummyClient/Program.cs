using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // DNS: Domain Name System: DNS서버가 네트워크 망에 하나가 더 있어서 주소를 찾아준다.
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];  // 분산한 서버에 따라 해당 IP에 여러 개 있을 수도 있다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 최종 주소, 포트는 식당 정문, 후문 느낌

            while (true)
            {
                // 휴대폰 설정
                Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // 예외 처리
                try
                {
                    // 문지기한테 입장 문의
                    socket.Connect(endPoint);   // Blocking 함수: 계속 대기-게임에서는 치명적이다
                    // System.Console.WriteLine($"Connected To {socket.RemoteEndPoint.ToString()}");   // RemoteEndPoint: 연결한 반대쪽 대상 

                    // 보낸다
                    byte[] sendBuff = Encoding.UTF8.GetBytes("Hi!");
                    int sendBytes = socket.Send(sendBuff);  // SendBuff에 있는 것을 한 번에 보내줌(Blocking 함수)

                    // 받는다
                    byte[] recvBuff = new Byte[1024];
                    int recvBytes = socket.Receive(recvBuff);   // Blocking 함수
                    string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                    System.Console.WriteLine($"[From Server] {recvData}");

                    // 나간다
                    socket.Shutdown(SocketShutdown.Both);   // 예고
                    socket.Close();
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