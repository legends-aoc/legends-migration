using Chaos.Core.Memory;
using Chaos.Core.Utilities;
using Chaos.Networking.Model.Client;

namespace Chaos.Networking.Deserializers;

public record MetafileRequestDeserializer : ClientPacketDeserializer<MetafileRequestArgs>
{
    public override ClientOpCode ClientOpCode => ClientOpCode.MetafileRequest;

    public override MetafileRequestArgs Deserialize(ref SpanReader reader)
    {
        var metafileRequestType = (MetafileRequestType)reader.ReadByte();
        var name = default(string?);

        switch (metafileRequestType)
        {
            case MetafileRequestType.DataByName:
                name = reader.ReadString8();

                break;
            case MetafileRequestType.AllCheckSums:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new MetafileRequestArgs(metafileRequestType, name);
    }
}