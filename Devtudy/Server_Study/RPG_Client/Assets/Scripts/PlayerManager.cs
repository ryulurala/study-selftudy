using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    MyPlayer _myPlayer;
    Dictionary<int, Player> _players = new Dictionary<int, Player>();

    public static PlayerManager Instance { get; } = new PlayerManager();   // 어디서든 접근할 수 있도록

    public void Add(S_PlayerList packet)
    {
        Object obj = Resources.Load("Player");

        foreach (S_PlayerList.Player p in packet.players)
        {
            GameObject go = Object.Instantiate(obj) as GameObject;
            if (p.isSelf)    // 내가 관리
            {
                MyPlayer myPlayer = go.AddComponent<MyPlayer>();        // MyPlayer Script 첨가
                myPlayer.PlayerId = p.playerId;
                myPlayer.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                _myPlayer = myPlayer;
            }
            else    // 남들이 관리
            {
                Player player = go.AddComponent<Player>();      // Player Script 첨가
                player.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                player.PlayerId = p.playerId;
                _players.Add(p.playerId, player);
            }
        }
    }
    public void Move(S_BroadcastMove packet)
    {
        // 서버 응답이 와서 움직일 수 있다.
        if (_myPlayer.PlayerId == packet.playerId)
        {
            _myPlayer.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
        }
        else
        {
            Player player = null;
            if (_players.TryGetValue(packet.playerId, out player))
            {
                // PlayerId를 찾았다.
                player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
            }
        }
    }
    public void EnterGame(S_BroadcastEnterGame packet)
    {
        if (packet.playerId == _myPlayer.PlayerId)
            return;

        Object obj = Resources.Load("Player");
        GameObject go = Object.Instantiate(obj) as GameObject;

        Player player = go.AddComponent<Player>();      // Player Script 첨가
        player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
        _players.Add(packet.playerId, player);
    }
    public void LeaveGame(S_BroadcastLeaveGame packet)
    {
        if (_myPlayer.PlayerId == packet.playerId)
        {
            GameObject.Destroy(_myPlayer.gameObject);
            _myPlayer = null;
        }
        else
        {
            Player player = null;
            if (_players.TryGetValue(packet.playerId, out player))
            {
                // PlayerId를 찾았다.
                GameObject.Destroy(player.gameObject);
                _players.Remove(packet.playerId);
            }
        }
    }
}
