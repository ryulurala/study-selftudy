using System;
using System.Net;
using System.Net.Sockets;

namespace ServerCore
{
    public class Listener
    {
        Socket _listenSocket;
        Func<Session> _sessionFactory;  // Session을 누구로 만들지 정의
        public void init(IPEndPoint endPoint, Func<Session> sessionFactory, int register = 10, int backlog = 100)
        {
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _sessionFactory += sessionFactory;
            // 문지기 교육
            _listenSocket.Bind(endPoint);

            // 영업 시작
            // backlog: 최대 대기 수 Listen(int backlog) --- 동시 접근시
            _listenSocket.Listen(backlog);

            for (int i = 0; i < register; i++)
            {
                // 이벤트 방식으로 콜백으로 전달해줌
                // EventHandler<TEventArgs>(Object sender, TEventArgs e)
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                RegisterAccept(args);   // 초기화 단게에서 등록
            }
        }

        void RegisterAccept(SocketAsyncEventArgs args)
        {
            // 기존에 있던 것을 없앰.
            args.AcceptSocket = null;

            // Async: 비동기-안되면 return 먼저 함: 예약
            // return: pending 여부
            bool pending = _listenSocket.AcceptAsync(args);
            if (pending == false)
            {
                // 바로 완료(클라이언트 접속함)
                OnAcceptCompleted(null, args);
            }
        }

        void OnAcceptCompleted(Object sender, SocketAsyncEventArgs args)
        {
            // 소켓 에러 체크: 진짜 에러있는 경우 or 성공했다 
            if (args.SocketError == SocketError.Success)
            {
                // GameSession만 만들 수 있는 단점이 있다.
                // 보내는 부분 + 받는 부분 + Disconnect => Session

                // 어떤 Session인지 모르므로 루트 부모로 받아서 Invoke한다.
                Session session = _sessionFactory.Invoke();    // polling 방식도 가능
                session.Start(args.AcceptSocket);       // 외부에서 사용 X
                session.OnConnected(args.AcceptSocket.RemoteEndPoint);      // 이쪽에서 클라이언트가 연결을 끊으면 에러

                // Accept 성공
                // User가 오면 서버가 해야할 일
                // _onAcceptHandler.Invoke(args.AcceptSocket);
            }
            else
            {
                System.Console.WriteLine(args.SocketError.ToString());  // 에러 이유 출력
            }
            RegisterAccept(args);   // 다음 턴을 위해서
        }
    }
}