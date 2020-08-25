using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;
using DummyClient;
using UnityEngine;


// 패킷이 조립되면 무엇을 호출할 건지
class PacketHandler
{
    public static void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;

        // if (chatPacket.playerId == 1)
        {
            Debug.Log(chatPacket.chat);

            GameObject go = GameObject.Find("Player");
            if (go == null)
                Debug.Log("Player not found");
            else
                Debug.Log("Player found");
        }
    }
}