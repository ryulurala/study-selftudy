using System;
using System.Collections.Generic;
using System.Text;
using Server;
using ServerCore;

// 패킷이 조립되면 무엇을 호출할 건지
class PacketHandler
{
    public static void C_ChatHandler(PacketSession session, IPacket packet)
    {
        C_Chat chatPacket = packet as C_Chat;      // 캐스팅
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
            return;

        GameRoom room = clientSession.Room;     // Room이 null로 되어 크래시 발생을 방지
        room.Push(
            () => room.BroadCast(clientSession, chatPacket.chat)
        );
    }
}