using System;

public class NodeConfig
{
    public bool AcceptStaleRequests { get; set; }
    public string NodeConfigFile { get; set; }
    public string NodeType { get; set; }
    public UInt64 BlockchainHeight { get; set; }
}