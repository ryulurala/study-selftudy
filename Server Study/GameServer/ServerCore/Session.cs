using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerCore
{
    abstract class Session
    {
        Socket _socket;
        int _disconnected = 0;
        object _lock = new Object();    // 락을 쓰기 위해서
        Queue<byte[]> _sendQueue = new Queue<byte[]>(); // sendBuff를 넣어 한 번에 보내기용
        // 다른 쓰레드가 예약했는지 구분
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();     // ArraySegment 사용하기 위해서
        SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();    // 재사용 하기 위해서
        public void Start(Socket socket)
        {
            _socket = socket;
            SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);  // 자동 콜백
            _sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);  // 자동 콜백

            // new SocketAsyncEventArgs().UserToken : 식별자 or 연동하고 싶은 데이터를 받고 싶을 때 사용

            // SetBuffer(메모리, offset, count)
            // Session끼리 버퍼를 나눠서 쓰기도 하기 때문에 offset과 count가 있다.
            recvArgs.SetBuffer(new byte[1024], 0, 1024);

            RegisterRecv(recvArgs);     // init말고 start 함수로 관리한다.
        }

        // 추상 함수(상속해서 사용)
        public abstract void OnConnected(EndPoint endPoint);
        public abstract void OnRecv(ArraySegment<byte> buffer);
        public abstract void OnSend(int numOfBytes);
        public abstract void OnDisconnected(EndPoint endPoint);

        public void Disconnect()
        {
            // Disconnect를 두 번하면 에러가 난다.
            // _disconnected = 1, original value가 1이면 또 close() 못하도록 함
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
            {
                return;
            }
            OnDisconnected(_socket.RemoteEndPoint);

            // 쫓아낸다
            _socket.Shutdown(SocketShutdown.Both); // 예고(옵션), 듣기도 싫고 말하기도 싫다.
            _socket.Close();
        }

        public void Send(byte[] sendBuff)       // 언제 할 지 예측 불가
        {
            lock (_lock)    // 한 번에 한 쓰레드만 들어올 수 있게 한다.
            {
                _sendQueue.Enqueue(sendBuff);       // Queue에만 넣고 스킵할 수도 있다.
                if (_pendingList.Count == 0)  // 쓰레드 1빠로 send() 호출(전송까지 할 수 있다)
                {
                    RegisterSend();
                }
            }
            // _socket.Send(sendBuff);
            // SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();     // 매번 만들어 비효율
            // _sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);  // 자동 콜백
            // _sendArgs.SetBuffer(sendBuff, 0, sendBuff.Length);
            // RegisterSend();
        }

        #region 네트워크 통신
        void RegisterSend()
        {
            // 이미 앞에 send()함수에서 락을 걸어 호출하기 때문에 락을 걸 필요가 없다.

            // ary[][][][][][][][] 일 때, C++은 Pointer로 시작 주소를 넘길 수 있지만 C#은 시작주소에서 offset을 해야된다.
            // 기본적으로 버퍼의 범위를 표현할 때는 (버퍼, 시작인덱스, 크기)를 넘겨준다.
            while (_sendQueue.Count > 0)
            {
                byte[] buff = _sendQueue.Dequeue();
                _pendingList.Add(new ArraySegment<byte>(buff, 0, buff.Length));     // 더 효율적이다(힙 X, 스택 O)
                // _sendArgs.SetBuffer(buff, 0, buff.Length);      // 실제 있는 Data의 버퍼 길이를 넣어준다
            }
            _sendArgs.BufferList = _pendingList;        // 예약된 목록들이 다 들어있다.(BufferList)

            // SendAsync를 여러 번 호출하면 부하가 심하다.
            bool pending = _socket.SendAsync(_sendArgs);    // 예약되어 있는지 확인하면서 send()
            if (pending == false)
            {
                // 보내는 것이 성공
                OnSendCompleted(null, _sendArgs);
            }
        }
        void OnSendCompleted(object sender, SocketAsyncEventArgs args)
        {
            lock (_lock)    // Start에서 들어올 수도 있으므로 락을 걸어준다.
            {
                // 상대방이 연결을 끊으면 가끔 0 byte로 오기 때문에
                if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
                {
                    try
                    {
                        // 다음 단계를 보낼 준비
                        _sendArgs.BufferList = null;        // _pendList를 가지고 있을 필요X
                        _pendingList.Clear();       // bool pending 역할

                        OnSend(_sendArgs.BytesTransferred);

                        // 다른 쓰레드가 Enqueue 했던 것을 처리해준다.
                        // 내가 보내는 동안에 pending=false 하기 전에 다른 쓰레드가 예약을 했을 경우
                        if (_sendQueue.Count > 0)
                        {
                            RegisterSend();     // 내가 그 쓰레드 것을 처리해줌
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine($"OnSendCompleted Failed{e}");
                    }
                }
                else
                {
                    // TODO Disconnect
                    Disconnect();
                }
            }
        }
        void RegisterRecv(SocketAsyncEventArgs args)
        {
            // Non-blocking 버전 receive
            bool pending = _socket.ReceiveAsync(args);
            if (pending == false)
            {
                // 성공
                OnRecvCompleted(null, args);
            }
        }
        void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
        {
            // 상대방이 연결을 끊으면 가끔 0byte로 오기 때문에
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {   // TODO(Connect: 데이터 받음)
                try     // 예외 방지
                {
                    OnRecv(new ArraySegment<byte>(args.Buffer, args.Offset, args.BytesTransferred));
                    // 메시지를 받았다는 것을 연동

                    // Receive를 받을 준비를 다시 함
                    RegisterRecv(args);
                }
                catch (Exception e)
                {
                    Disconnect();
                    System.Console.WriteLine($"OnRecvCompleted Failed{e}");
                }
            }
            else
            {
                // TODO Disconnect
                System.Console.WriteLine($"Disconnect");
                Disconnect();
            }
        }
        #endregion
    }
}