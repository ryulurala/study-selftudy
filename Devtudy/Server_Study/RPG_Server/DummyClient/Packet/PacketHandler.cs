using System;
using System.Collections.Generic;
using System.Text;
using DummyClient;
using ServerCore;

// 패킷이 조립되면 무엇을 호출할 건지
class PacketHandler
{
    public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
    {
        // S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;
    }
    public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
    {
        // S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;
    }
    public static void S_PlayerListHandler(PacketSession session, IPacket packet)
    {
        // S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;

    }
    public static void S_BroadcastMoveHandler(PacketSession session, IPacket packet)
    {
        // S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;
    }
}