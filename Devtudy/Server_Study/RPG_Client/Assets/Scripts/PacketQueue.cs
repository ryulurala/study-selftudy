using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketQueue : MonoBehaviour
{
    // Singleton
    public static PacketQueue Instance { get; } = new PacketQueue();
    Queue<IPacket> _packetQueue = new Queue<IPacket>();
    object _lock = new object();

    public void Push(IPacket packet)
    {
        lock (_lock)
        {
            _packetQueue.Enqueue(packet);
        }
    }

    public IPacket Pop()   // 메인 쓰레드가 쓰도록
    {
        lock (_lock)
        {
            if (_packetQueue.Count == 0)
                return null;

            return _packetQueue.Dequeue();
        }
    }
}