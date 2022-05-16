using Chaos.Core.Memory;
using Chaos.Core.Utilities;
using Chaos.Networking.Model.Server;

namespace Chaos.Networking.Serializers;

public record HeartBeatResponseSerializer : ServerPacketSerializer<HeartBeatResponseArgs>
{
    public override ServerOpCode ServerOpCode => ServerOpCode.HeartBeatResponse;

    public override void Serialize(ref SpanWriter writer, HeartBeatResponseArgs responseArgs)
    {
        writer.WriteByte(responseArgs.First);
        writer.WriteByte(responseArgs.Second);
    }
}