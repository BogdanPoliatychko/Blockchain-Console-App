using System;
using System.Diagnostics;
using System.IO;

namespace Blockchain
{
    // main entry point for console blockchain app with command line interface (CLI)
    internal class Program
    {
        static readonly BlockchainService blockchainService = new BlockchainService();

        static void Main()
        {
            Console.WriteLine(@"
  ____  _            _        _           _       
 |  _ \| |          | |      | |         (_)      
 | |_) | | ___   ___| | _____| |__   __ _ _ _ __  
 |  _ <| |/ _ \ / __| |/ / __| '_ \ / _` | | '_ \ 
 | |_) | | (_) | (__|   < (__| | | | (_| | | | | |
 |____/|_|\___/ \___|_|\_\___|_| |_|\__,_|_|_| |_|
            ");

            // main CLI menu
            bool isRunning = true;
            while (isRunning) {
                Console.WriteLine("\n======== BLOCKCHAIN MENU ========\n" +
                    "1. Show Blockchain\n" +
                    "2. Add new Block\n" +
                    "3. Add new NFT Block\n" +
                    "4. Create Miner\n" +
                    "5. Add new Transaction for Miner\n" +
                    "6. Start Mining (Create Block)\n" +
                    "0. Exit");
                Console.Write("Select an option (0-6): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice) {
                    case "1":
                        PrintBlockchain();
                        PressAnyKeyToContinue();
                        break;
                    case "2":
                        CreateBlock();
                        PressAnyKeyToContinue();
                        break;
                    case "3":
                        CreateNFTBlock();
                        PressAnyKeyToContinue();
                        break;
                    case "4":
                        CreateMiner();
                        PressAnyKeyToContinue();
                        break;
                    case "5":
                        CreateMinerTransaction();
                        PressAnyKeyToContinue();
                        break;
                    case "6":
                        StartMiningTransactions();
                        PressAnyKeyToContinue();
                        break;
                    case "0":
                        Console.WriteLine("Exiting Blockchain...\n" +
                            "Have a nice day!");
                        PressAnyKeyToContinue();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Type a number from 0 to 5.");
                        PressAnyKeyToContinue();
                        break;
                }
            }
        }

        // pause console execution until user acknowledges output
        static void PressAnyKeyToContinue()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        // iterate through entire blockchain and print each block in a formatted UI box
        static void PrintBlockchain()
        {
            Console.WriteLine("------- Current Blockchain -------");

            int blockHeight = 0;
            foreach (var block in blockchainService.Blockchain) {
                int boxWidth = 114;
                string boxHorizontalBorder = new string('-', boxWidth);
                int boxInnerWidth = boxWidth - 4;

                Console.WriteLine("\n" + boxHorizontalBorder);
                Console.WriteLine($"| {$"Block Height: {blockHeight}".PadRight(boxInnerWidth)} |");
                Console.WriteLine($"| {$"Nonce: {block.Nonce}".PadRight(boxInnerWidth)} |");
                Console.WriteLine($"| {$"Block Hash: {block.BlockHashString}".PadRight(boxInnerWidth)} |");
                Console.WriteLine($"| {$"Previous Hash: {block.PrevBlockHashString}".PadRight(boxInnerWidth)} |");

                string transactionData = $"Transaction Data: {(block.Data.Contains("\n") ? Environment.NewLine : "")}{block.Data}";
                string[] dataLines = transactionData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (var line in dataLines) {
                    for (int i = 0; i < line.Length; i += boxInnerWidth) {
                        string chunk = line.Substring(i, Math.Min(boxInnerWidth, line.Length - i));
                            Console.WriteLine($"| {chunk.PadRight(boxInnerWidth)} |");
                    }
                }

                Console.WriteLine(boxHorizontalBorder);
                blockHeight++;
            }
        }

        // create and add standard transaction block by user input
        static void CreateBlock()
        {
            Console.WriteLine("---------- Create Block ----------\n");

            Console.Write("Enter Sender Name: ");
            string from = Console.ReadLine();

            Console.Write("Enter Action (pays, sells...): ");
            string action = Console.ReadLine();

            Console.Write("Enter Receiver Name: ");
            string to = Console.ReadLine();

            double amount;
            while (true) {
                Console.Write("Enter Amount of Currency (10.5, 23...): ");
                string amountString = Console.ReadLine();
                if (double.TryParse(amountString, out amount) && amount > 0)
                    break;
                Console.WriteLine("\nInvalid input! Please enter a positive number greater than 0.\n");
            }

            Console.Write("Enter Currency (BTC, ETH...): ");
            string currency = Console.ReadLine();

            Console.Write("Enter Purpose of Transaction (personal, coffee...): ");
            string purpose = Console.ReadLine();

            blockchainService.AddBlock(from, action, to, amount, currency, purpose);

            Console.WriteLine("\nSuccess! Block added to Blockchain.");
        }

        // create and add transaction NFT block with provided image to mint it by user input
        static void CreateNFTBlock()
        {
            Console.WriteLine("-------- Create NFT Block --------\n");

            Console.Write("Enter Sender Name: ");
            string from = Console.ReadLine();

            Console.Write("Enter Action (sells, buys...): ");
            string action = Console.ReadLine();

            Console.Write("Enter Receiver Name: ");
            string to = Console.ReadLine();
            
            double amount;
            while (true) {
                Console.Write("Enter Amount of Currency (10.5, 23...): ");
                string amountString = Console.ReadLine();
                if (double.TryParse(amountString, out amount) && amount > 0)
                    break;
                Console.WriteLine("\nInvalid input! Please enter a positive number greater than 0.\n");
            }
            Console.Write("Enter Currency (BTC, ETH...): ");
            string currency = Console.ReadLine();

            Console.Write("Enter Name of NFT: ");
            string nameNFT = Console.ReadLine();

            Console.Write("Enter Path to NFT Image or file name from Images (C:\\...\\image.bmp, image.bmp): ");
            string imagePathNFT = Console.ReadLine();

            if (imagePathNFT.StartsWith("\"") && imagePathNFT.EndsWith("\""))
                imagePathNFT = imagePathNFT.Trim('"');

            try {
                string baseDirectory = AppContext.BaseDirectory;
                string targetFolder = Path.Combine(baseDirectory, "Images");

                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);

                bool isJustFileName = string.IsNullOrEmpty(Path.GetDirectoryName(imagePathNFT));
                string destinationPath;

                if (isJustFileName) {
                    destinationPath = Path.Combine(targetFolder, imagePathNFT);

                    if (!File.Exists(destinationPath)) {
                        Console.WriteLine($"\nError. File '{imagePathNFT}' does not exist in the Images folder.");
                        return;
                    }
                    else Console.WriteLine($"\nSuccess! NFT Image '{imagePathNFT}' found in Images folder and is ready to use.");
                }
                else {
                    if (!File.Exists(imagePathNFT)) {
                        Console.WriteLine("\nError. File does not exist at provided path.");
                        return;
                    }

                    string fileName = Path.GetFileName(imagePathNFT);
                    destinationPath = Path.Combine(targetFolder, fileName);

                    if (File.Exists(destinationPath))
                        Console.WriteLine($"\nImage '{fileName}' already exists in Images folder. Skipping copy.");
                    else {
                        File.Copy(imagePathNFT, destinationPath, overwrite: true);
                        Console.WriteLine("\nSuccess! NFT Image copied to Blockchain Images folder.");
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"\nError occurred while handling Image file: {ex.Message}");
            }

            blockchainService.AddBlock(NFTService.MintNFT(from, action, to, amount, currency, nameNFT,
                $"./Images/{Path.GetFileName(imagePathNFT)}"));

            Console.WriteLine("\nSuccess! NFT Block added to Blockchain.");
        }

        // initialize miner with specific params by user input
        static void CreateMiner()
        {
            Console.WriteLine("---------- Create Miner ----------\n");

            Console.Write("Enter Miner Name: ");
            string minerName = Console.ReadLine();

            int transactionsLimit;
            while (true) {
                Console.Write("Enter Limit of Transactions (2, 5...): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out transactionsLimit) && transactionsLimit > 0)
                    break;
                Console.WriteLine("\nInvalid input! Please enter a positive whole number greater than 0.\n");
            }

            int nonceDifficulty;
            while (true) {
                Console.Write("Enter Nonce Difficulty (amount of 0 at start of block hash) (2, 3...): ");
                string nonceDifficultyString = Console.ReadLine();
                if (int.TryParse(nonceDifficultyString, out nonceDifficulty) && nonceDifficulty > 0)
                    break;
                Console.WriteLine("\nInvalid input! Please enter a positive whole number greater than 0.\n");
            }

            blockchainService.CreateMiner(minerName, transactionsLimit, nonceDifficulty);

            Console.WriteLine($"\nSuccess! Miner '{minerName}' added to Blockchain.");
        }

        // create and add single transaction to miner unconfirmed pool by user input
        static void CreateMinerTransaction()
        {
            Console.WriteLine("--- Create Miner Transaction ---\n");

            if (blockchainService.Miner.TransactionLimit == 0) {
                Console.WriteLine("Error. You need to Create Miner first.");
                return;
            }

            int transactionsLeft = blockchainService.Miner.TransactionLimit - blockchainService.Miner.Transactions.Count;
            Console.WriteLine($"Transactions left: {transactionsLeft}");
            if (transactionsLeft == 0)
                return;
            Console.WriteLine();

            Console.Write("Enter Sender Name: ");
            string from = Console.ReadLine();

            Console.Write("Enter Action (pays, sells...): ");
            string action = Console.ReadLine();

            Console.Write("Enter Receiver Name: ");
            string to = Console.ReadLine();

            double amount;
            while (true) {
                Console.Write("Enter Amount of Currency (10.5, 23...): ");
                string amountString = Console.ReadLine();
                if (double.TryParse(amountString, out amount) && amount > 0)
                    break;
                Console.WriteLine("\nInvalid input! Please enter a positive number greater than 0.\n");
            }

            Console.Write("Enter Currency (BTC, ETH...): ");
            string currency = Console.ReadLine();

            Console.Write("Enter Purpose of Transaction (personal, coffee...): ");
            string purpose = Console.ReadLine();

            blockchainService.AddMinerTransaction($"{from} {action} {to} {amount} {currency} for {purpose}");

            Console.WriteLine($"\nSuccess! Transaction added to Blockchain Miner '{blockchainService.Miner.MinerName}'.");
        }

        // initiate proof of work process by mining pending transactions into new block
        static void StartMiningTransactions()
        {
            Console.WriteLine("--- Starting Mining new Block ---\n");

            if (blockchainService.Miner.TransactionLimit == 0) {
                Console.WriteLine("Error. You need to Create Miner first.");
                return;
            }
            if (blockchainService.Miner.Transactions.Count == 0) {
                Console.WriteLine("Error. You need to Add Transactions for Miner first.");
                return;
            }

            Console.WriteLine($"Starting the mining process by '{blockchainService.Miner.MinerName}'...");

            Stopwatch stopwatch = Stopwatch.StartNew();
            blockchainService.StartMiningTransactions();
            stopwatch.Stop();

            Console.WriteLine($"\nSuccess! Block mined in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");

            Console.WriteLine($"\nReceived reward from Blockchain to '{blockchainService.Miner.MinerName}' Wallet: " +
                $"{blockchainService.Miner.MiningReward} BTC.");
        }
    }
}
