using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;
using DummyClient;
using UnityEngine;


// 패킷이 조립되면 무엇을 호출할 건지
class PacketHandler
{
    public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
    {
        // 누군가가 들어왔다.
        S_BroadcastEnterGame pkt = packet as S_BroadcastEnterGame;
        ServerSession srverSession = session as ServerSession;

        PlayerManager.Instance.EnterGame(pkt);
    }
    public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
    {
        // 누군가가 나갔다.
        S_BroadcastLeaveGame pkt = packet as S_BroadcastLeaveGame;
        ServerSession srverSession = session as ServerSession;

        PlayerManager.Instance.LeaveGame(pkt);
    }
    public static void S_PlayerListHandler(PacketSession session, IPacket packet)
    {
        // 내가 들어왔다.
        S_PlayerList pkt = packet as S_PlayerList;
        ServerSession srverSession = session as ServerSession;

        PlayerManager.Instance.Add(pkt);
    }
    public static void S_BroadcastMoveHandler(PacketSession session, IPacket packet)
    {
        // 누군가가 이동했다.
        S_BroadcastMove pkt = packet as S_BroadcastMove;
        ServerSession srverSession = session as ServerSession;

        PlayerManager.Instance.Move(pkt);
    }
}