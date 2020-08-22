using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using ServerCore;
using DummyClinet;

public class NetworkManager : MonoBehaviour
{
    ServerSession _session = new ServerSession();

    // Start is called before the first frame update
    void Start()
    {
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IpEndPoint endPoint = new IpEndPoint(ipAddr, 7777);

        Connector connector = new Connector();

        connector.Connect(endPoint,
            () => { return _session; },
            1
        );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
