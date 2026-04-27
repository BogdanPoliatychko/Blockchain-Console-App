using System.IO;
using System.Security.Cryptography;

namespace Blockchain
{
    // utility methods for generating and processing Non-Fungible Tokens (NFTs)
    public static class NFTService
    {
        // "mints" new NFT by taking local image file and generating valid transaction string
        public static string MintNFT(string from, string action, string to, double price, string currency,
            string nameNFT, string imagePathNFT)
        {
            byte[] byteArray = File.ReadAllBytes(imagePathNFT);
            byte[] NFTHash;
            using (SHA256 sha256 = SHA256.Create()) {
                NFTHash = sha256.ComputeHash(byteArray);
            }

            NFT newNFT = new NFT(from, action, to, price, currency, nameNFT, NFTHash);

            return newNFT.TransactionString;
        }
    }
}
