using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using ServerCore;

namespace Server
{
    // 패킷은 공통적으로 코드를 작성한다.
    // 기본적인 패킷 헤더
    public abstract class Packet    // 패킷은 최대한 압축해서 보내야된다.
    {   // 경우에 따라서 제거(size packetId)
        public ushort size;        // packet size를 모르므로   (ushort(2) vs uint(4))
        public ushort packetId;    // 무슨 패킷인지 구별       (ushort(2) vs uint(4))

        public abstract ArraySegment<byte> Write();
        public abstract void Read(ArraySegment<byte> seg);
    }

    class PlayerInfoReq : Packet    // Client -> Server
    {
        public long playerId;
        public string name;

        public struct SkillInfo
        {
            public int id;
            public short level;
            public float duration;

            public bool Write(Span<byte> span, ref ushort count)
            {
                bool success = true;
                success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), id);
                count += sizeof(int);
                success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), level);
                count += sizeof(short);
                success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), duration);
                count += sizeof(float);
                return success;
            }

            public void Read(ReadOnlySpan<byte> span, ref ushort count)
            {
                id = BitConverter.ToInt32(span.Slice(count, span.Length - count));
                count += sizeof(int);
                level = BitConverter.ToInt16(span.Slice(count, span.Length - count));
                count += sizeof(short);
                duration = BitConverter.ToSingle(span.Slice(count, span.Length - count));     // Single : float value, Double : double value
                count += sizeof(float);
            }
        }

        public List<SkillInfo> skills = new List<SkillInfo>();

        public PlayerInfoReq()      // 생성자
        {
            this.packetId = (ushort)PacketID.PlayerInfoReq;
        }
        public override void Read(ArraySegment<byte> segment)
        {
            // Deserialization
            ushort count = 0;

            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);

            // ushort size = BitConverter.ToUInt16(seg.Array, seg.Offset);
            count += sizeof(ushort);
            // ushort id = BitConverter.ToUInt16(seg.Array, seg.Offset + count);
            count += sizeof(ushort);

            this.playerId = BitConverter.ToInt64(span.Slice(count, span.Length - count));    // 필드 변수를 직접 채워줌
            count += sizeof(long);

            // string parsing
            // length[2byte] + byte[]
            ushort nameLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
            count += sizeof(ushort);

            // Decoding
            this.name = Encoding.Unicode.GetString(span.Slice(count, nameLen));
            count += nameLen;

            // skill list
            // 스킬 개수 추출
            ushort skillLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
            count += sizeof(ushort);

            skills.Clear();     // 혹시나 다른 정보를 들고 있을까봐
            for (int i = 0; i < skillLen; i++)
            {
                SkillInfo skill = new SkillInfo();
                skill.Read(span, ref count);

                // 추가
                skills.Add(skill);
            }

            // 범위를 초과하는 값을 Parsing을 하려고 하면 Exception 발생으로 Option이 있다.
            // BitConverter.ToInt64(new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count));

            // System.Console.WriteLine($"PlayerInfoReq: {playerId}");
        }

        public override ArraySegment<byte> Write()
        {
            // 연결되고 후처리
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);       // 공간 예약

            ushort count = 0;       // 자동화하기 편리하게
            bool success = true;

            Span<byte> span = new Span<byte>(segment.Array, segment.Offset, segment.Count);     // for Slice

            // 실패, 성공 여부가 갈림---version 1, 공간이 모자르면 실패
            count += sizeof(ushort);
            success &= BitConverter.TryWriteBytes(segment.Slice(count, span.Length - count), this.packetId);
            count += sizeof(ushort);     // 나중에 자동화
            success &= BitConverter.TryWriteBytes(segment.Slice(count, span.Length - count), this.playerId);
            count += sizeof(long);

            // string(C++은 \0로 판별)
            // string length [2byte] + byte[]

            // UTF-16(Unicode)
            // Length[2byte]
            ushort nameLen = (ushort)Encoding.Unicode.GetByteCount(this.name);
            success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), nameLen);
            count += sizeof(ushort);

            // Index는 count부터 nameLen크기만큼 seg.Array에 복사
            // byte[]
            Array.Copy(Encoding.Unicode.GetBytes(this.name), 0, segment.Array, count, nameLen);
            count += nameLen;

            // skill list
            success &= BitConverter.TryWriteBytes(segment.Slice(count, span.Length - count), (ushort)skills.Count);
            count += sizeof(ushort);

            foreach (SkillInfo skill in skills)
            {
                // TODO
                success &= skill.Write(span, ref count);    // 들고 있는 스킬 모두 Serialize
            }

            success &= BitConverter.TryWriteBytes(span, count);         // 마지막 최종 카운트

            if (success == false)
            {
                return null;
            }

            // 사이즈는 마지막에 알 수 있다.
            return SendBufferHelper.Close(count);
        }
    }

    // class PlayerInfoOk : Packet     // Server -> Client
    // {
    //     public int hp;
    //     public int attack;
    // }

    public enum PacketID    // 자동화해야 할 내용
    {
        PlayerInfoReq = 1,
        PlayerInfoOk = 2,
    }

    // Session 클래스를 상속받아 사용(콘텐츠단)
    class ClientSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            // 연결됨
            System.Console.WriteLine($"Onconnected: {endPoint}");

            // Packet packet = new Packet() { size = 100, packetId = 10 };

            // // 연결되고 후처리
            // // byte[] sendBuff = new byte[1024];
            // ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
            // byte[] buffer1 = BitConverter.GetBytes(packet.size);   // int byte를 byte 배열로 만듦
            // byte[] buffer2 = BitConverter.GetBytes(packet.packetId);   // int byte를 byte 배열로 만듦

            // // buffer1 + buffer2 -> sendBuff : [ 100 ][ 10 ] : 8 byte
            // Array.Copy(buffer1, 0, openSegment.Array, openSegment.Offset, buffer1.Length);
            // Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer1.Length, buffer2.Length);

            // ArraySegment<byte> sendBuff = SendBufferHelper.Close(buffer1.Length + buffer2.Length);

            // // byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");
            // Send(sendBuff);
            Thread.Sleep(5000);
            Disconnect();
        }
        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            // Deserialization
            ushort count = 0;
            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            count += 2;
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            count += 2;

            // Parsing
            switch ((PacketID)id)
            {
                case PacketID.PlayerInfoReq:
                    {
                        PlayerInfoReq playerInfoReq = new PlayerInfoReq();
                        playerInfoReq.Read(buffer);     // Deserialize
                        System.Console.WriteLine($"PlayerInfoReq: {playerInfoReq.playerId} {playerInfoReq.name}");

                        foreach (PlayerInfoReq.SkillInfo skill in playerInfoReq.skills)
                        {
                            System.Console.WriteLine($"Skill: {skill.id} {skill.level} {skill.duration}");
                        }
                    }
                    break;
            }

            System.Console.WriteLine($"RecvPacketId: {id}, Size: {size}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            System.Console.WriteLine($"OnDisconnected: {endPoint}");
        }

        public override void OnSend(int numOfBytes)
        {
            // 몇 바이트를 보냈는지
            System.Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }

}