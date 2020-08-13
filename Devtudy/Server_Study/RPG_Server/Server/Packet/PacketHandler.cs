using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;

// 패킷이 조립되면 무엇을 호출할 건지
class PacketHandler
{
    public static void C_PlayerInfoReqHandler(PacketSession session, IPacket packet)
    {
        C_PlayerInfoReq p = packet as C_PlayerInfoReq;      // 캐스팅

        System.Console.WriteLine($"PlayerInfoReq: {p.playerId} {p.name}");

        foreach (C_PlayerInfoReq.Skill skill in p.skills)
        {
            System.Console.WriteLine($"Skill: {skill.id} {skill.level} {skill.duration}");
        }
    }
}