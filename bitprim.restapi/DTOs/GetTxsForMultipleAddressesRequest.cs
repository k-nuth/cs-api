namespace api.DTOs
{
    public class GetTxsForMultipleAddressesRequest
    {
        public string addrs { get; set; }
        public int? from { get; set; } = 0;
        public int? to { get; set; } = 20;
        public bool? noAsm { get; set; } = true;
        public bool? noScriptSig { get; set; } = true;
        public bool? noSpend { get; set; } = true;
    }
}