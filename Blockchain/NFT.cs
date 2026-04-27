using System;

namespace Blockchain
{
    // Non-Fungible Token (NFT) transaction in blockchain
    public class NFT
    {
        private readonly string _transactionString;
        public string TransactionString { get { return _transactionString; } }

        public NFT(string from, string action, string to, double price, string currency, 
            string nameNFT, byte[] imageNFT)
        {
            _transactionString = $"{from} {action} {to} NFT '{nameNFT}' for {price} {currency} " +
                $"[NFT Hash: {BitConverter.ToString(imageNFT)}]";
        }
    }
}
