namespace Bitprim
{
    /// <summary>
    /// Error codes returned by the C API
    /// </summary>
    public enum ErrorCode
    {
        // general codes
        /// <summary>
        /// The operation finished without errors
        /// </summary>
        Success = 0,
        /// <summary>
        /// The operation is deprecated
        /// </summary>
        Deprecated = 6,
        /// <summary>
        /// Unknown error
        /// </summary>
        Unknown = 43,
        /// <summary>
        /// The resource not exists
        /// </summary>
        NotFound = 3,
        /// <summary>
        /// File system error
        /// </summary>
        FileSystem = 42,
        /// <summary>
        /// Transaction not standard
        /// </summary>
        NonStandard = 17,
        /// <summary>
        /// The operation isn't implemented
        /// </summary>
        NotImplemented = 4,
        /// <summary>
        /// Service oversubscribed
        /// </summary>
        Oversubscribed = 71,

        // network
        /// <summary>
        /// Service is stopped
        /// </summary>
        ServiceStopped = 1,
        /// <summary>
        /// The operation failed 
        /// </summary>
        OperationFailed = 2,
        /// <summary>
        /// Resolving hostname failed
        /// </summary>
        ResolveFailed = 7,
        /// <summary>
        /// Unable to reach remote host
        /// </summary>
        NetworkUnreachable = 8,
        /// <summary>
        /// Address already in use
        /// </summary>
        AddressInUse = 9,
        /// <summary>
        /// Incoming connection failed
        /// </summary>
        ListenFailed = 10,
        /// <summary>
        /// Connection acceptance failed
        /// </summary>
        AcceptFailed = 11,
        /// <summary>
        /// Bad data stream
        /// </summary>
        BadStream = 12,
        /// <summary>
        /// Connection timed out
        /// </summary>
        ChannelTimeout = 13,
        /// <summary>
        /// Address blocked by policy
        /// </summary>
        AddressBlocked = 44,
        /// <summary>
        /// Channel stopped
        /// </summary>
        ChannelStopped = 45,
        /// <summary>
        /// Unresponsive peer may be throttling
        /// </summary>
        PeerThrottling = 73,

        // database
        /// <summary>
        /// Block duplicate
        /// </summary>
        StoreBlockDuplicate = 66,
        /// <summary>
        /// Block out of order
        /// </summary>
        StoreBlockInvalidHeight = 67,
        /// <summary>
        /// Block missing parent
        /// </summary>
        StoreBlockMissingParent = 68,

        // blockchain
        /// <summary>
        /// Duplicate block
        /// </summary>
        DuplicateBlock = 51,
        /// <summary>
        /// Missing block parent
        /// </summary>
        OrphanBlock = 5,
        /// <summary>
        /// Previous block failed to validate
        /// </summary>
        InvalidPreviousBlock = 24,
        /// <summary>
        /// Insufficient work to reorganize
        /// </summary>
        InsufficientWork = 48,
        /// <summary>
        /// Missing transaction parent
        /// </summary>
        OrphanTransaction = 14,
        /// <summary>
        /// Insufficient transaction fee
        /// </summary>
        InsufficientFee = 70,
        /// <summary>
        /// Output value too low
        /// </summary>
        DustyTransaction = 76,
        /// <summary>
        /// Blockchain too far behind
        /// </summary>
        StaleChain = 75,

        // check header
        /// <summary>
        /// Proof of work invalid
        /// </summary>
        InvalidProofOfWork = 26,
        /// <summary>
        /// Timestamp too far in the future
        /// </summary>
        FuturisticTimestamp = 27,

        // accept header
        /// <summary>
        /// Block hash rejected by checkpoint
        /// </summary>
        CheckpointsFailed = 35,
        /// <summary>
        /// Block version rejected at current height
        /// </summary>
        OldVersionBlock = 36,
        /// <summary>
        /// Proof of work does not match bits field
        /// </summary>
        IncorrectProofOfWork = 32,
        /// <summary>
        /// Block timestamp is too early
        /// </summary>
        TimestampTooEarly = 33,

        // check block
        /// <summary>
        /// Block size limit exceeded
        /// </summary>
        BlockSizeLimit = 50,
        /// <summary>
        /// Block has no transactions
        /// </summary>
        EmptyBlock = 47,
        /// <summary>
        /// First transaction not a coinbase
        /// </summary>
        FirstNotCoinbase = 28,
        /// <summary>
        /// More than one coinbase
        /// </summary>
        ExtraCoinbases = 29,
        /// <summary>
        /// Matching transaction hashes in block
        /// </summary>
        InternalDuplicate = 49,
        /// <summary>
        /// Double spend internal to block
        /// </summary>
        BlockInternalDoubleSpend = 15,
        /// <summary>
        /// Merkle root mismatch
        /// </summary>
        MerkleMismatch = 31,
        /// <summary>
        /// Too many block legacy signature operations
        /// </summary>
        BlockLegacySigopLimit = 30,
        /// <summary>
        /// Transactions out of order
        /// </summary>
        ForwardReference = 79,

        // accept block
        /// <summary>
        /// Block contains a non-final transaction
        /// </summary>
        BlockNonFinal = 34,
        /// <summary>
        /// Block height mismatch in coinbase
        /// </summary>
        CoinbaseHeightMismatch = 37,
        /// <summary>
        /// Coinbase value too high
        /// </summary>
        CoinbaseValueLimit = 41,
        /// <summary>
        /// too many block embedded signature operations
        /// </summary>
        BlockEmbeddedSigopLimit = 52,
        /// <summary>
        /// Invalid witness commitment
        /// </summary>
        InvalidWitnessCommitment = 25,
        /// <summary>
        /// Block weight limit exceeded
        /// </summary>
        BlockWeightLimit = 82,

        // check transaction
        /// <summary>
        /// Transaction inputs or outputs empty
        /// </summary>
        EmptyTransaction = 20,
        /// <summary>
        /// Non-coinbase transaction has input with null previous output
        /// </summary>
        PreviousOutputNull = 23,
        /// <summary>
        /// Spend outside valid range
        /// </summary>
        SpendOverflow = 21,
        /// <summary>
        /// Coinbase script too small or large
        /// </summary>
        InvalidCoinbaseScriptSize = 22,
        /// <summary>
        /// Coinbase transaction disallowed in memory pool
        /// </summary>
        CoinbaseTransaction = 16,
        /// <summary>
        /// Double spend internal to transaction
        /// </summary>
        TransactionInternalDoubleSpend = 72,
        /// <summary>
        /// Transaction size limit exceeded
        /// </summary>
        TransactionSizeLimit = 53,
        /// <summary>
        /// Too many transaction legacy signature operations
        /// </summary>
        TransactionLegacySigopLimit = 54,

        // accept transaction
        /// <summary>
        /// Transaction currently non-final for next block
        /// </summary>
        TransactionNonFinal = 74,
        /// <summary>
        /// Transaction validation under checkpoint
        /// </summary>
        PrematureValidation = 69,
        /// <summary>
        /// Matching transaction with unspent outputs
        /// </summary>
        UnspentDuplicate = 38,
        /// <summary>
        /// Previous output not found
        /// </summary>
        MissingPreviousOutput = 19,
        /// <summary>
        /// Previous output not found
        /// </summary>
        DoubleSpend = 18,
        /// <summary>
        /// Immature coinbase spent
        /// </summary>
        CoinbaseMaturity = 46,
        /// <summary>
        /// Spend exceeds value of inputs
        /// </summary>
        SpendExceedsValue = 40,
        /// <summary>
        /// Too many transaction embedded signature operations
        /// </summary>
        TransactionEmbeddedSigopLimit = 55,
        /// <summary>
        /// Transaction currently locked
        /// </summary>
        SequenceLocked = 78,
        /// <summary>
        /// Transaction weight limit exceeded
        /// </summary>
        TransactionWeightLimit = 83,

        // connect input
        /// <summary>
        /// Invalid script
        /// </summary>
        InvalidScript = 39,
        /// <summary>
        /// Invalid script size
        /// </summary>
        InvalidScriptSize = 56,
        /// <summary>
        /// Invalid push data size
        /// </summary>
        InvalidPushDataSize = 57,
        /// <summary>
        /// Invalid operation count
        /// </summary>
        InvalidOperationCount = 58,
        /// <summary>
        /// Invalid stack size
        /// </summary>
        InvalidStackSize = 59,
        /// <summary>
        /// Invalid stack scope
        /// </summary>
        InvalidStackScope = 60,
        /// <summary>
        /// Invalid script embed
        /// </summary>
        InvalidScriptEmbed = 61,
        /// <summary>
        /// Invalid signature encoding
        /// </summary>
        InvalidSignatureEncoding = 62,
        /// <summary>
        /// Invalid signature lax encoding
        /// </summary>
        InvalidSignatureLaxEncoding = 63,
        /// <summary>
        /// Incorrect signature
        /// </summary>
        IncorrectSignature = 64,
        /// <summary>
        /// stack false
        /// </summary>
        StackFalse = 65,
        /// <summary>
        /// Unexpected witness
        /// </summary>
        UnexpectedWitness = 77,
        /// <summary>
        /// Invalid witness
        /// </summary>
        InvalidWitness = 80,
        /// <summary>
        /// Dirty witness
        /// </summary>
        DirtyWitness = 81,

        // op eval
        OpDisabled = 100,
        OpReserved,
        OpPushSize,
        OpPushData,
        OpIf,
        OpNotIf,
        OpElse,
        OpEndIf,
        OpVerify1,
        OpVerify2,
        OpReturn,
        OpToAltStack,
        OpFromAltStack,
        OpDrop2,
        OpDup2,
        OpDup3,
        OpOver2,
        OpRot2,
        OpSwap2,
        OpIfDup,
        OpDrop,
        OpDup,
        OpNip,
        OpOver,
        OpPick,
        OpRoll,
        OpRot,
        OpSwap,
        OpTuck,
        OpSize,
        OpEqual,
        OpEqualVerify1,
        OpEqualVerify2,
        OpAdd1,
        OpSub1,
        OpNegate,
        OpAbs,
        OpNot,
        OpNonZero,
        OpAdd,
        OpSub,
        OpBoolAnd,
        OpBoolOr,
        OpNumEqual,
        OpNumEqualVerify1,
        OpNumEqualVerify2,
        OpNumNotEqual,
        OpLessThan,
        OpGreaterThan,
        OpLessThanOrEqual,
        OpGreaterThanOrEqual,
        OpMin,
        OpMax,
        OpWithin,
        OpRipemd160,
        OpSha1,
        OpSha256,
        OpHash160,
        OpHash256,
        OpCodeSeperator,
        OpCheckSigVerify1,
        OpCheckSig,
        OpCheckMultisigVerify1,
        OpCheckMultisigVerify2,
        OpCheckMultisigVerify3,
        OpCheckMultisigVerify4,
        OpCheckMultisigVerify5,
        OpCheckMultisigVerify6,
        OpCheckMultisigVerify7,
        OpCheckMultisig,
        OpCheckLocktimeVerify1,
        OpCheckLocktimeVerify2,
        OpCheckLocktimeVerify3,
        OpCheckLocktimeVerify4,
        OpCheckLocktimeVerify5,
        OpCheckLocktimeVerify6,
        OpCheckSequenceVerify1,
        OpCheckSequenceVerify2,
        OpCheckSequenceVerify3,
        OpCheckSequenceVerify4,
        OpCheckSequenceVerify5,
        OpCheckSequenceVerify6,
        OpCheckSequenceVerify7
    };

}