namespace ResourceReader.Enum
{
    public enum ENodeType : byte
    {
        DocumentStart,
        DocumentEnd,

        NewNode,
        NewChildNode,
        EndChildNode,
        EndNode,

        Property,
        PropertyName,
        Value,

        Array
    }
}
