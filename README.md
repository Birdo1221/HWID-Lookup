# HWID Checker

![image](https://github.com/user-attachments/assets/61ac0375-1075-4e40-8a5b-e1452c211086)

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

![image](https://github.com/user-attachments/assets/29f1e35c-4d34-48cc-b5ce-8a4f692a01e9)

- **User-Friendly Interface:** A dark-gray themed interface for better ease-of-use and aesthetics.

## Prerequisites

To build and run the HWID Checker application, you will need:

- .NET Framework 4.7.2 or later
- Visual Studio 2019 or later (for building the project)

![image](https://github.com/user-attachments/assets/7308fdcd-e296-4281-9ae5-8cf2bde757bf)


## Setup

**Just Clone the Repository and Open the folder via Visual studio 2019-2024, Using the .Net 6 Framework**

   ```bash
   git clone https://github.com/Birdo1221/HWID-Lookup.git
   ```

  ```bash
   The best way to compile this is to make it self-contained with the frameworks for .NET 6 instead of it redirecting download it.  
   ```
![image](https://github.com/user-attachments/assets/8e3ededc-4f39-4244-9814-250ee8b3c17b)

   ```bash
   This is the Publish config i used to make the stand-alone .exe file  
   ```

![image](https://github.com/user-attachments/assets/7a563d2f-744a-459c-94f2-24eab6d3f321)




