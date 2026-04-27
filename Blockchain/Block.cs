using System;

namespace Blockchain
{
    // single block in blockchain
    public class Block
    {
        private readonly byte[] _blockHash;
        private readonly byte[] _prevBlockHash;
        private readonly int _nonce;
        private readonly string _data;

        public byte[] BlockHash { get { return _blockHash; } }
        public string BlockHashString { get { return BitConverter.ToString(_blockHash); } }

        public byte[] PrevBlockHash { get { return _prevBlockHash; } }
        public string PrevBlockHashString { get { return BitConverter.ToString(_prevBlockHash); } }

        public int Nonce { get { return _nonce; } }

        public string Data { get { return _data; } }

        public Block(byte[] blockHash, byte[] prevBlockHash, string data, int nonce = 0)
        {
            _blockHash = blockHash;
            _prevBlockHash = prevBlockHash;
            _nonce = nonce;
            _data = data;
        }
    }
}
