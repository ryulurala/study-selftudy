using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;

// 패킷이 조립되면 무엇을 호출할 건지
class PacketHandler
{
    public static void PlayerInfoReqHandler(PacketSession session, IPacket packet)
    {
        PlayerInfoReq p = packet as PlayerInfoReq;      // 캐스팅

        System.Console.WriteLine($"PlayerInfoReq: {p.playerId} {p.name}");

        foreach (PlayerInfoReq.Skill skill in p.skills)
        {
            System.Console.WriteLine($"Skill: {skill.id} {skill.level} {skill.duration}");
        }
    }
    public static void TestHandler(PacketSession session, IPacket packet)
    {

    }
}