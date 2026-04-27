using System.Collections.Generic;

namespace Blockchain
{
    // miner in blockchain that collects pending transactions and performs proof of work to create new blocks
    public class Miner
    {
        private readonly List<string> _transactions;
        private string _minerName;
        private int _transactionsLimit;
        private int _nonceDifficulty;
        private readonly double _miningReward;

        public List<string> Transactions { get { return _transactions; } }
        public string MinerName { get { return _minerName; } set => _minerName = value; }
        public int TransactionLimit { get { return _transactionsLimit; } set => _transactionsLimit = value; }
        public int NonceDifficulty { get { return _nonceDifficulty; } set => _nonceDifficulty = value; }
        public double MiningReward { get { return _miningReward; } }

        public Miner()
        {
            _transactions = new List<string>();
            _minerName = string.Empty;
            _transactionsLimit = 0;
            _nonceDifficulty = 0;
            _miningReward = 0.0;
        }

        public Miner(string minerName, int transactionsLimit, int nonceDifficulty, double miningReward = 3.125)
        {
            _transactions = new List<string>();
            _minerName = minerName;
            _transactionsLimit = transactionsLimit;
            _nonceDifficulty = nonceDifficulty;
            _miningReward = miningReward;
        }
    }
}
