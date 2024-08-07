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

**Please Note:** Currently, the application is limited to hardware checking functionality only. It does **not** include any HWID spoofing features.

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

## Setup

**Just Clone the Repository and Open the folder via Visual studio 2019-2024, Using the .Net 6 Framework**

  ```bash
   git clone https://github.com/Birdo1221/HWID-Lookup.git

   **The best way to compile this is to make it self-contained with the framework for the application.**

   **This avoids the need to download the framework separately.** 
   ```

   ```bash
   This is the Publish configuration i used to make the stand-alone .exe files work without having to install the framework  
   ```
![image](https://github.com/user-attachments/assets/37866df8-5022-4634-b3cd-bc3d49a63242)




