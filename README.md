# HWID Checker

## Overview

HWID Checker is a desktop application built using WPF (Windows Presentation Foundation) in C#. The application allows users to fetch and display detailed system hardware information, including:

- Motherboard
- Processor
- Storage Devices (Hard Drives)
- Network Adapters
- Graphics Cards

The application also saves this information to a CSV file for future reference.

**Please Note:** Currently, the application is limited to hardware checking functionality only. It does **not** include any HWID spoofing features, and the spoofing logic is yet to be implemented.

## Features

- **Fetch and Display Hardware Information:**
  - Motherboard
  - Processor
  - Hard Drive
  - Network Adapters (Displayed in separate tabs)
  - Graphics Card
- **Save Information to CSV File:** Automatically saves the fetched information to a CSV file named `Hwid_Backup.csv` in the application's base directory.
- **User-Friendly Interface:** A dark-themed interface for better usability and aesthetics.

## Prerequisites

To build and run the HWID Checker application, you will need:

- .NET Framework 4.7.2 or later
- Visual Studio 2019 or later (for building the project)

## Setup

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/hwid-checker.git
