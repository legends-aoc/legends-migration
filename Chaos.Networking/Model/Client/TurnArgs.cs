using Chaos.Geometry.Definitions;
using Chaos.Packets.Interfaces;

namespace Chaos.Networking.Model.Client;

public record TurnArgs(Direction Direction) : IReceiveArgs;