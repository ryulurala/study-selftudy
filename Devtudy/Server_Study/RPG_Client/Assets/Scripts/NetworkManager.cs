using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using ServerCore;
using DummyClient;
using System;

public class NetworkManager : MonoBehaviour
{
    ServerSession _session = new ServerSession();

    public void Send(ArraySegment<byte> sendBuff)
    {
        _session.Send(sendBuff);
    }
    // Start is called before the first frame update
    void Start()
    {
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Connector connector = new Connector();

        connector.Connect(endPoint,
            () => { return _session; },
            1
        );
    }

    void Update()
    {
        // 1Frame에 모든 패킷 처리
        List<IPacket> packetList = PacketQueue.Instance.PopAll();
        foreach (IPacket packet in packetList)
        {
            PacketManager.Instance.HandlePacket(_session, packet);
        }
    }
}
