namespace Chaos.MetaData.Abstractions;

/// <summary>
///     Represents a collection of nodes
/// </summary>
/// <typeparam name="TNode">The type of node stored in the collection</typeparam>
public abstract class MetaNodeCollection<TNode> where TNode: IMetaNode
{
    /// <summary>
    ///     The maximum size of a node collection
    /// </summary>
    protected const ushort MAX_SIZE = (ushort)(ushort.MaxValue * 0.95);
    /// <summary>
    ///     The nodes contained in the collection
    /// </summary>
    public List<TNode> Nodes { get; }

    /// <summary>
    ///     Creates a new instance of <see cref="MetaNodeCollection{TNode}" />
    /// </summary>
    protected MetaNodeCollection() => Nodes = new List<TNode>();

    /// <summary>
    ///     Adds a node to the collection
    /// </summary>
    /// <param name="node">The node to add to the collection</param>
    public void AddNode(TNode node) => Nodes.Add(node);
}