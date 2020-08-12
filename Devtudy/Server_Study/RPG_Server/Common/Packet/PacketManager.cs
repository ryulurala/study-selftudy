using System;
using System.Collections.Generic;
using ServerCore;

class PacketManager
{
    // 고정된 메모리 영역을 최초로 한 번만 할당한다.
    #region Singleton       
    static PacketManager _instance;
    public static PacketManager Instance
    {
        get
        {
            if (_instance == null)      // 처음만 만든다.
                _instance = new PacketManager();
            return _instance;
        }
    }
    #endregion

    Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
    Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();

    public void Register()
    {
        _onRecv.Add((ushort)PacketID.PlayerInfoReq, MakePacket<PlayerInfoReq>);
        _handler.Add((ushort)PacketID.PlayerInfoReq, PacketHandler.PlayerInfoReqHandler);

        _onRecv.Add((ushort)PacketID.Test, MakePacket<Test>);
        _handler.Add((ushort)PacketID.Test, PacketHandler.TestHandler);

    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
    {
        // Deserialization(size, id)
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        // Parsing
        Action<PacketSession, ArraySegment<byte>> action = null;
        if (_onRecv.TryGetValue(id, out action))        // dictionary에서 찾아서 handler를 등록했으면 invoke
            action.Invoke(session, buffer);
    }

    void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()  // Generic으로 넘겨준다.
    {
        T packet = new T();
        packet.Read(buffer);     // Deserialize

        Action<PacketSession, IPacket> action = null;
        if (_handler.TryGetValue(packet.Protocol, out action))
            action.Invoke(session, packet);
    }
}