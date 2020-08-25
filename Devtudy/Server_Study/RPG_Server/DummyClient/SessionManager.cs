using System;
using System.Collections.Generic;
using System.Text;

namespace DummyClient
{
    class SessionManager
    {
        static SessionManager _session = new SessionManager();
        public static SessionManager Instance { get { return _session; } }

        List<ServerSession> _sessions = new List<ServerSession>();
        object _lock = new object();
        Random _rend = new Random();

        public ServerSession Generate()
        {
            lock (_lock)
            {
                ServerSession session = new ServerSession();
                _sessions.Add(session);
                return session;
            }
        }

        public void SendForEach()
        {
            lock (_lock)
            {
                foreach (ServerSession session in _sessions)
                {
                    C_Move movePacket = new C_Move();
                    movePacket.posX = _rend.Next(-50, 50);
                    movePacket.posY = 0;
                    movePacket.posZ = _rend.Next(-50, 50);

                    session.Send(movePacket.Write());
                }
            }
        }
    }
}