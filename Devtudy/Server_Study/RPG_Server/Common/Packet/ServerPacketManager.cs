using System;
using System.Collections.Generic;
using ServerCore;

public class PacketManager
{
    #region Singleton       
    static PacketManager _instance = new PacketManager();
    public static PacketManager Instance { get { return _instance; } }
    #endregion

    PacketManager()
    {
        Register();
    }

    Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _makeFunc = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();
    Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();

    public void Register()
    {

        _makeFunc.Add((ushort)PacketID.C_LeaveGame, MakePacket<C_LeaveGame>);
        _handler.Add((ushort)PacketID.C_LeaveGame, PacketHandler.C_LeaveGameHandler);

        _makeFunc.Add((ushort)PacketID.C_Move, MakePacket<C_Move>);
        _handler.Add((ushort)PacketID.C_Move, PacketHandler.C_MoveHandler);

    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer, Action<PacketSession, IPacket> onRecvCallback = null)
    {
        // Deserialization(size, id)
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        // Parsing
        Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
        if (_makeFunc.TryGetValue(id, out func))
        {        // dictionary에서 찾아서 handler를 등록했으면 invoke
            IPacket packet = func.Invoke(session, buffer);
            if (onRecvCallback != null)
                onRecvCallback.Invoke(session, packet);     // Packet Queue(다른 옵션이 있으면)
            else
                HandlePacket(session, packet);      // Default
        }
    }

    T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()  // Generic으로 넘겨준다.
    {
        T packet = new T();
        packet.Read(buffer);     // Deserialize

        return packet;
    }

    public void HandlePacket(PacketSession session, IPacket packet)
    {
        Action<PacketSession, IPacket> action = null;
        if (_handler.TryGetValue(packet.Protocol, out action))
            action.Invoke(session, packet);
    }
}