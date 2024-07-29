# HWID Checker

![image](https://github.com/user-attachments/assets/7308fdcd-e296-4281-9ae5-8cf2bde757bf)

## Overview

HWID Checker is a desktop application built using WPF (Windows Presentation Foundation) in C#. The application allows users to fetch and display detailed system hardware information, including:

- Motherboard Serial Number
- Processor ID
- Storage Device Serial Numbers *`Excludes external devices and focuses on internal storage like HDDs, SATA SSDs, and NVMe drives.`*
- Network Adapter MAC Address
- Graphics Card UUID

The application also saves this information to a CSV file for future reference.

**Please Note:** Currently, the application is limited to hardware checking functionality only. It does **not** include any HWID spoofing features, and the spoofing logic is yet to be implemented.

## Features

- **Fetch and Display Hardware Information:**
  - Network Adapters (Displayed in separate tabs)
- **Save Information to CSV File:** Automatically saves the fetched information to a CSV file named `Hwid_Backup.csv` in the application's base directory.

  ![image](https://github.com/user-attachments/assets/99e09c3d-079f-4b7e-b597-19b066a773a4)

- **User-Friendly Interface:** A dark-gray themed interface for better ease-of-use and aesthetics.

## Prerequisites

To build and run the HWID Checker application, you will need:

- .NET Framework 4.7.2 or later
- Visual Studio 2019 or later (for building the project)

## Setup

**Just Clone the Repository and Open the folder via Visual studio 2019-2024, Using the .Net 6 Framework**

   ```bash
   git clone https://github.com/yourusername/hwid-checker.git
   ```

https://github.com/user-attachments/assets/fda769c1-bcfe-42a1-8a0b-291b867bafcc


