using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

public enum PacketID
{
    C_PlayerInfoReq = 1,
    S_Test = 2,

}

interface IPacket
{
    ushort Protocol { get; }
    void Read(ArraySegment<byte> segment);
    ArraySegment<byte> Write();
}

class C_PlayerInfoReq : IPacket
{
    public byte testByte;
    public long playerId;
    public string name;
    public struct Skill
    {
        public int id;
        public short level;
        public float duration;

        public void Read(ReadOnlySpan<byte> span, ref ushort count)
        {
            this.id = BitConverter.ToInt32(span.Slice(count, span.Length - count));
            count += sizeof(int);
            this.level = BitConverter.ToInt16(span.Slice(count, span.Length - count));
            count += sizeof(short);
            this.duration = BitConverter.ToSingle(span.Slice(count, span.Length - count));
            count += sizeof(float);

        }

        public bool Write(Span<byte> span, ref ushort count)
        {
            bool success = true;
            success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.id);
            count += sizeof(int);
            success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.level);
            count += sizeof(short);
            success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.duration);
            count += sizeof(float);

            return success;
        }
    }

    public List<Skill> skills = new List<Skill>();

    public ushort Protocol { get { return (ushort)PacketID.C_PlayerInfoReq; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        this.testByte = (byte)segment.Array[segment.Offset + count];
        count += sizeof(byte);
        this.playerId = BitConverter.ToInt64(span.Slice(count, span.Length - count));
        count += sizeof(long);
        ushort nameLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
        count += sizeof(ushort);
        this.name = Encoding.Unicode.GetString(span.Slice(count, nameLen));
        count += nameLen;
        skills.Clear();
        ushort skillLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
        count += sizeof(ushort);
        for (int i = 0; i < skillLen; i++)
        {
            Skill skill = new Skill();
            skill.Read(span, ref count);
            skills.Add(skill);
        }

    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);       // 공간 예약

        ushort count = 0;       // 자동화하기 편리하게
        bool success = true;

        Span<byte> span = new Span<byte>(segment.Array, segment.Offset, segment.Count);     // for Slice

        count += sizeof(ushort);        // 처음 패킷Id
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), (ushort)PacketID.C_PlayerInfoReq);
        count += sizeof(ushort);     // 나중에 자동화
        segment.Array[segment.Offset + count] = (byte)this.testByte;
        count += sizeof(byte);
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.playerId);
        count += sizeof(long);
        ushort nameLen = (ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count + sizeof(ushort));
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), nameLen);
        count += sizeof(ushort);
        count += nameLen;
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), (ushort)skills.Count);
        count += sizeof(ushort);
        foreach (Skill skill in this.skills)
            success &= skill.Write(span, ref count);

        success &= BitConverter.TryWriteBytes(span, count);         // 마지막 최종 카운트
        if (success == false)
            return null;

        return SendBufferHelper.Close(count);
    }
}
class S_Test : IPacket
{
    public int testInt;

    public ushort Protocol { get { return (ushort)PacketID.S_Test; } }

    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        this.testInt = BitConverter.ToInt32(span.Slice(count, span.Length - count));
        count += sizeof(int);

    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);       // 공간 예약

        ushort count = 0;       // 자동화하기 편리하게
        bool success = true;

        Span<byte> span = new Span<byte>(segment.Array, segment.Offset, segment.Count);     // for Slice

        count += sizeof(ushort);        // 처음 패킷Id
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), (ushort)PacketID.S_Test);
        count += sizeof(ushort);     // 나중에 자동화
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.testInt);
        count += sizeof(int);

        success &= BitConverter.TryWriteBytes(span, count);         // 마지막 최종 카운트
        if (success == false)
            return null;

        return SendBufferHelper.Close(count);
    }
}

