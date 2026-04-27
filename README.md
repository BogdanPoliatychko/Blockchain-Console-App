# Blockchain Console App

A Console application written in C# (.NET Framework 4.8.1) that simulates a functioning blockchain environment. This project demonstrates core distributed ledger concepts, including block generation, cryptographic hashing, Proof-of-Work (PoW) mining, and Non-Fungible Token (NFT) minting.

<p align="center">
  <img width="396" alt="Blockchain Console Menu Output Example" src="https://github.com/user-attachments/assets/478fc155-2517-4192-b872-3f3049e1d769"/>
</p>

<p align="center">
  <img width="490" alt="Blockchain Console Mining Output Example" src="https://github.com/user-attachments/assets/9f167c45-3660-43c8-b267-891285a5cfdc"/>
</p>

<p align="center">
  <img width="888" alt="Blockchain Console Blockchain Output Example" src="https://github.com/user-attachments/assets/72469535-715e-413d-8def-e4b352c3b31e"/>
</p>

## Overview and Purpose of the System
The purpose of this system is to provide an educational and interactive simulation of how a blockchain operates under the hood. Users can manually interact with the ledger through a Command Line Interface (CLI) to understand how transactions are bundled, how Proof-of-Work secures the network, and how physical or digital assets (like images) can be represented cryptographically as NFTs.

## System Content (System Boundary)
The system is entirely self-contained within the console application. Its boundaries include:
* **Core Ledger:** An in-memory linked list structure storing sequential blocks.
* **Cryptographic Engine:** SHA-256 implementation for hashing block data and local files.
* **Miner Module:** A simulated mining entity that pools unconfirmed transactions and performs Proof-of-Work calculations based on adjustable difficulty parameters.
* **File System I/O:** Local directory access (specifically an `Images` folder) to read and copy files for the purpose of generating NFT hashes.
* **Command Line Interface:** A text-based menu system for user orchestration.

## Product Interactions (Potential)
Currently, the application runs entirely locally. However, its modular architecture allows for potential future interactions with:
* **Local File System:** Actively interacts with local storage to read image files (`.bmp`, `.png`, `.jpg`, etc.) to mint them as NFTs.
* **External Nodes (Potential):** Could be expanded to interact with network sockets (TCP/IP) for Peer-to-Peer (P2P) ledger synchronization.
* **Persistent Databases (Potential):** Could interact with SQL or NoSQL databases to save the ledger state between sessions.

## Product Functions
* **View Blockchain:** Displays the current state of the ledger, including block height, nonce, current/previous hashes, and transaction data in a formatted UI box.
* **Standard Transactions:** Allows users to create blocks representing direct financial transfers (e.g., Sender pays Receiver amount in cryptocurrency).
* **NFT Minting:** Creates transaction blocks that represent digital ownership by generating a secure SHA-256 hash of a local image file. 
* **Configure Miner:** Sets up a mining entity with custom constraints, including transaction pool limits and nonce difficulty (leading zeros required in the hash).
* **Transaction Pooling:** Queues pending transactions into the miner's unconfirmed pool.
* **Proof-of-Work Mining:** Executes the intensive computational process of finding a valid hash (nonce) to officially record queued transactions into a new block and distribute a block reward.

## Security Requirements
* **Cryptographic Integrity:** The system relies strictly on the `System.Security.Cryptography.SHA256` library to ensure block immutability. Altering any past block requires recalculating the hash of all subsequent blocks.
* **Local Permissions:** The application requires read/write permissions within its execution directory to safely create and manage the `Images` folder for NFT processing.

## User Characteristics
The end user of this system is primarily developers, computer science students, or cryptography enthusiasts seeking a hands-on, foundational understanding of blockchain architecture, hashing algorithms, and Proof-of-Work mechanics without the overhead of deploying to a live network.

## Constraints
* **Platform Dependency:** Built on .NET Framework 4.8.1, making it primarily a Windows-centric application (unlike modern cross-platform .NET versions).
* **Volatile Memory:** The blockchain is stored entirely in RAM (`LinkedList<Block>`). All data is lost when the console application is closed.
* **Single-Threaded Mining:** The Proof-of-Work algorithm currently executes sequentially on a single thread, which simulates difficulty but does not utilize modern multi-core GPU/CPU mining architectures.

---

## Getting Started

### Prerequisites
* [.NET Framework Runtime](https://dotnet.microsoft.com/download/dotnet-framework/net481) (.NET Framework 4.8.1)
* Visual Studio 2022 or 2026 (to build from source)

### How to Run
1.  Download the latest release from the [Releases](../../releases) tab.
2.  Extract the ZIP file.
3.  Run `Blockchain.exe`. 
4.  Existing NFT mage files: Azuki8559.bmp; BoredApe8162.bmp; CryptoPunk3084.bmp; Gimdoz3157.bmp; Hypurr3.bmp.

### How to Build
1.  Clone the repository.
2.  Open `Blockchain.sln` (or `Blockchain.csproj`) in Visual Studio.
3.  Ensure your target framework is set to `.NET Framework 4.8.1`.
4.  Build and run the project (F5).

## Project
*Created: 27.04.2026*
