using System;
using System.Collections.Generic;
using System.Text;
using Server;
using ServerCore;

// 패킷이 조립되면 무엇을 호출할 건지
class PacketHandler
{
    public static void C_LeaveGameHandler(PacketSession session, IPacket packet)
    {
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
            return;

        GameRoom room = clientSession.Room;     // Room이 null로 되어 크래시 발생을 방지
        room.Push(
            () => room.Leave(clientSession)
        );
    }
    public static void C_MoveHandler(PacketSession session, IPacket packet)
    {
        C_Move movePacket = packet as C_Move;
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
            return;

        Console.WriteLine($"({movePacket.posX}, {movePacket.posY}, {movePacket.posZ})");

        GameRoom room = clientSession.Room;     // Room이 null로 되어 크래시 발생을 방지
        room.Push(
            () => room.Move(clientSession, movePacket)
        );
    }
}