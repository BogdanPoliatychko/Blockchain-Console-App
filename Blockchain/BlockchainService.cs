using System;
using System.Collections.Generic;

namespace Blockchain
{
    // main orchestrator for blockchain
    public class BlockchainService
    {
        private readonly LinkedList<Block> _blockchain;
        public LinkedList<Block> Blockchain { get { return _blockchain; } }

        private Miner _miner;
        public Miner Miner { get { return _miner; } }

        public BlockchainService()
        {
            _blockchain = new LinkedList<Block>();
            _blockchain.AddLast(BlockchainCore.CreateGenesisBlock());

            _miner = new Miner();
        }

        // configure new miner with specific parameters
        public void CreateMiner(string minerName, int transactionsLimit, int nonceDifficulty)
        {
            _miner = new Miner(minerName, transactionsLimit, nonceDifficulty);
        }

        // adds new block to blockchain with raw string data
        public void AddBlock(string data)
        {
            byte[] newBlockHash = BlockchainCore.HashBlock(_blockchain.Last.Value.BlockHash, data);
            Block newBlock = new Block(newBlockHash, _blockchain.Last.Value.BlockHash, data);
            _blockchain.AddLast(newBlock);
        }

        // adds new block to blockchain with formatted string data
        public void AddBlock(string from, string action, string to, double price, string currency, string purpose)
        {
            string data = $"{from} {action} {to} {price} {currency} for {purpose}";
            byte[] newBlockHash = BlockchainCore.HashBlock(_blockchain.Last.Value.BlockHash, data);
            Block newBlock = new Block(newBlockHash, _blockchain.Last.Value.BlockHash, data);
            _blockchain.AddLast(newBlock);
        }

        // add pending transaction to miner unconfirmed pool
        public void AddMinerTransaction(string minerTransaction)
        {
            if (_miner.Transactions.Count <= _miner.TransactionLimit)
                _miner.Transactions.Add(minerTransaction);
        }

        // mining process to bundle pending transactions into block
        public void StartMiningTransactions()
        {
            if (_miner.Transactions.Count <= 0)
                return;

            Console.WriteLine("\nMining block...");

            _miner.Transactions.Add("Blockchain rewards " + _miner.MinerName + " with " +
                _miner.MiningReward.ToString() + " BTC");

            string preHashedTransactions = string.Join(Environment.NewLine, _miner.Transactions);
            Block prevBlock = _blockchain.Last.Value;

            _blockchain.AddLast(FindHashAndReturnBlock(preHashedTransactions, prevBlock));
            
            _miner.Transactions.Clear();
            _miner.TransactionLimit = 0;
        }

        // hash block contents while finding nonce until target difficulty (leading zeros) (proof of work)
        private Block FindHashAndReturnBlock(string transactions, Block prevBlock)
        {
            int nonce = 0;
            bool hashFound = false;
            byte[] currentBlockHash;

            while ((hashFound != true) && (nonce != int.MaxValue)) {
                currentBlockHash = BlockchainCore.HashBlock(prevBlock.BlockHash, transactions, nonce);

                if (TestForZeros(currentBlockHash) == true) {
                    Console.WriteLine("\nBlock Mined!");

                    Block minedBlock = new Block(currentBlockHash, prevBlock.BlockHash, transactions, nonce);
                    return minedBlock;
                }
                nonce++;
            }

            return null;
        }

        // check if hash meets mining difficulty by checking for leading zero bytes
        private bool TestForZeros(byte[] Hash)
        {
            byte Zero = 0;
            byte[] ZeroByteArray = new byte[_miner.NonceDifficulty];
            Buffer.BlockCopy(Hash, 0, ZeroByteArray, 0, _miner.NonceDifficulty);

            foreach (var item in ZeroByteArray)
                if (item != Zero)
                    return false;

            return true;
        }
    }
}
