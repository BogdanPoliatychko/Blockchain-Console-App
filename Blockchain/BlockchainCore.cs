using System;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    // core functionality methods for building and managing blockchain
    public class BlockchainCore
    {
        private const string _genesisBlockData = "Genesis Block";

        // create foundational first genesis block of blockchain
        public static Block CreateGenesisBlock()
        {
            byte[] previousBlockHash = BitConverter.GetBytes(0);
            byte[] blockHash = HashBlock(previousBlockHash, _genesisBlockData);
            Block genesisBlock = new Block(blockHash, previousBlockHash, _genesisBlockData);

            return genesisBlock;
        }

        // cenerate secure SHA-256 cryptographic hash for block based on its core components
        public static byte[] HashBlock(byte[] previousBlockHash, string blockData, int nonce = 0)
        {
            string previousHashString = BitConverter.ToString(previousBlockHash);
            string nonceString = nonce.ToString();
            string completeBlock = previousHashString + nonceString + blockData;

            byte[] newBlockHash;
            using (SHA256 sha256 = SHA256.Create()) {
                byte[] inputBytes = Encoding.UTF8.GetBytes(completeBlock);
                newBlockHash = sha256.ComputeHash(inputBytes);
            }

            return newBlockHash;
        }
    }
}
