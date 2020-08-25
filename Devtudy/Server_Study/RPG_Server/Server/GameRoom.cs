using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;

namespace Server
{
    class GameRoom : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();
        JobQueue _jobQueue = new JobQueue();
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }
        public void BroadCast(ArraySegment<byte> segment)
        {
            _pendingList.Add(segment);     // 바로 뿌리는 것이 아니라 pending 시킨다. Flush할 때 처리
        }
        public void Flush()
        {
            foreach (ClientSession s in _sessions)
                s.Send(_pendingList);

            // Console.WriteLine($"Flushed {_pendingList.Count} items");
            _pendingList.Clear();
        }
        public void Enter(ClientSession session)
        {
            // Player 추가
            _sessions.Add(session);
            session.Room = this;    // 자신을 Room에 연결

            // 신입 Player 모든 Player에게 목록 전송
            S_PlayerList players = new S_PlayerList();

            foreach (ClientSession s in _sessions)
            {
                players.players.Add(new S_PlayerList.Player()
                {
                    isSelf = s == session,
                    playerId = s.SessionId,
                    posX = s.PosX,
                    posY = s.PosY,
                    posZ = s.PosZ,
                });
            }
            session.Send(players.Write());

            // 신입 Player 입장은 Broadcasting(기존 애들한테)
            S_BroadcastEnterGame enter = new S_BroadcastEnterGame();
            enter.playerId = session.SessionId;
            enter.posX = 0;     // 처음 시작
            enter.posY = 0;     // 처음 시작
            enter.posZ = 0;     // 처음 시작
            BroadCast(enter.Write());
        }
        public void Leave(ClientSession session)
        {
            // 플레이어 제거
            _sessions.Remove(session);

            // BroadCasting
            S_BroadcastLeaveGame leave = new S_BroadcastLeaveGame();
            leave.playerId = session.SessionId;
            BroadCast(leave.Write());
        }
        public void Move(ClientSession session, C_Move packet)
        {
            // 좌표 바꾸기
            session.PosX = packet.posX;
            session.PosY = packet.posY;
            session.PosZ = packet.posZ;

            // BroadCast
            S_BroadcastMove move = new S_BroadcastMove();
            move.playerId = session.SessionId;
            move.posX = session.PosX;
            move.posY = session.PosY;
            move.posZ = session.PosZ;
            BroadCast(move.Write());
        }
    }
}
