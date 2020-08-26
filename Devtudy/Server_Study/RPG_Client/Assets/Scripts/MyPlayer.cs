using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{
    NetworkManager _network;
    // Start is called before the first frame update
    void Start()
    {
        _network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();        // NetworkManager 오브젝트의 Script 연결
        StartCoroutine("CoSendPacket");
    }

    IEnumerator CoSendPacket()
    {
        while (true)
        {
            // 이동 패킷은 1초에 4번
            yield return new WaitForSeconds(0.25f);

            // 0.25초 후
            C_Move movePacket = new C_Move();
            movePacket.posX = UnityEngine.Random.Range(-50, 50);    // 랜덤값
            movePacket.posY = 0;    // 높이는 0
            movePacket.posZ = UnityEngine.Random.Range(-50, 50);    // 랜덤값

            _network.Send(movePacket.Write());      // ArraySegment를 보내줌
        }
    }
}
