using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;

namespace HwidProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadNetworkAdapters();
            PopulateNetworkAdapters();

        }

        private void FetchHwid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch information
                var hwidInfo = new Dictionary<string, string>
        {
            { "Motherboard", GetMotherboardInfo() },
            { "Processor", GetProcessorInfo() },
            { "Hard Drive", GetHardDriveInfo() },
            { "Graphics Card", GetGraphicsCardInfo() },
            { "ARP Table", GetArpTableInfo() },
            { "SMBIOS", GetSmbiosInfo() },
            { "Boot Identifiers", GetBootIdentifiersInfo() },
            { "USB Devices", GetUsbDevices() },
            { "PCI Device", GetStorageDeviceDetails() },
            { "Battery", GetBatteryInfo() },
            { "RAM", GetRamModules() }
        };
                // Display information in UI
                txtMotherboard.Text = hwidInfo["Motherboard"];
                txtProcessor.Text = hwidInfo["Processor"];
                txtHardDrive.Text = hwidInfo["Hard Drive"];
                txtGraphicsCard.Text = hwidInfo["Graphics Card"];
                txtArpTable.Text = hwidInfo["ARP Table"];
                txtSmbios.Text = hwidInfo["SMBIOS"];
                txtBootIdentifiers.Text = hwidInfo["Boot Identifiers"];
                txtUsbDevicesInfo.Text = hwidInfo["USB Devices"];
                txtPCIDevice.Text = hwidInfo["PCI Device"];
                txtLaptopBattery.Text = hwidInfo["Battery"];
                txtRamInfo.Text = hwidInfo["RAM"];


                string fileName = "Hwid_Backup.csv";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                // Save information to CSV file
                SaveToCsv(filePath, hwidInfo);

                // Notify user with the actual file path
                MessageBox.Show($"HWID information has been saved to:\n{filePath}", "Save Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching HWID: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveToCsv(string filePath, Dictionary<string, string> info)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    // Write the header
                    writer.WriteLine("Component,Information");

                    // Write each component information
                    foreach (var entry in info)
                    {
                        writer.WriteLine($"{entry.Key},\"{entry.Value.Replace("\"", "\"\"")}\"");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to CSV: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateNetworkAdapters()
        {
            try
            {
                var networkAdapters = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    .Select(nic => new
                    {
                        Name = nic.Description,
                        MacAddress = nic.GetPhysicalAddress().ToString()
                    })
                    .ToList();

                if (networkAdapters.Any())
                {
                    var firstAdapter = networkAdapters.First();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving network adapters: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string GetMotherboardInfo()
        {
            return GetSystemInfo("SELECT SerialNumber, Product FROM Win32_BaseBoard",
                                 "SerialNumber", "Product");
        }

        private static string GetProcessorInfo()
        {
            return GetSystemInfo("SELECT ProcessorId, Name FROM Win32_Processor",
                                 "ProcessorId", "Name");
        }

        private static string GetHardDriveInfo()
        {
            return GetSystemInfo("SELECT SerialNumber, Model FROM Win32_DiskDrive",
                                 "SerialNumber", "Model");
        }

        private static string GetGraphicsCardInfo()
        {
            string graphicsCardInfo = "Graphics Card Information:\n";
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT PNPDeviceID, Name FROM Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        graphicsCardInfo += $"Card: {obj["Name"]}\nUUID: {obj["PNPDeviceID"]}\n\n";
                    }
                }
            }
            catch
            {
                graphicsCardInfo += "Error fetching data\n";
            }
            return graphicsCardInfo;
        }

        private static string GetSystemInfo(string query, params string[] properties)
        {
            string result = string.Empty;
            try
            {
                using (var searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        foreach (var property in properties)
                        {
                            result += $"{property}: {obj[property]}\n";
                        }
                        break; // Only fetch the first item
                    }
                }
            }
            catch
            {
                result = "Error fetching data\n";
            }
            return result;
        }

        private string GetArpTableInfo()
        {
            string arpTableInfo = string.Empty;
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    foreach (var address in ni.GetIPProperties().UnicastAddresses)
                    {
                        arpTableInfo += $"Interface: {ni.Description}, IP: {address.Address}\n";
                    }
                }
            }
            catch (Exception ex)
            {
                arpTableInfo = $"Error: {ex.Message}";
            }
            return arpTableInfo;
        }

        private string GetSmbiosInfo()
        {
            return GetSystemInfo("SELECT * FROM Win32_BIOS",
                                 "Manufacturer", "SMBIOSBIOSVersion", "ReleaseDate");
        }

        private string GetBootIdentifiersInfo()
        {
            return GetSystemInfo("SELECT * FROM Win32_OperatingSystem",
                                 "BootDevice", "SystemDirectory");
        }

        private string GetUsbDevices()
        {
            string usbDevices = "USB Devices:\n";
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_USBHub"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        usbDevices += $"Device: {obj["DeviceID"]}\n";
                    }
                }
            }
            catch (Exception ex)
            {
                usbDevices = $"Error: {ex.Message}";
            }
            return usbDevices;
        }

        private string GetStorageDeviceDetails()
        {
            string storageDevices = string.Empty;
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        storageDevices += $"Device ID: {obj["DeviceID"]}\n";
                        storageDevices += $"Model: {obj["Model"]}\n";
                        storageDevices += $"Serial Number: {obj["SerialNumber"]}\n";
                        storageDevices += $"Interface Type: {obj["InterfaceType"]}\n";
                        storageDevices += $"Firmware Version: {obj["FirmwareRevision"]}\n";
                        storageDevices += $"Capacity: {Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024)} GB\n\n";
                    }
                }
            }
            catch (Exception ex)
            {
                storageDevices = $"Error: {ex.Message}";
            }
            return storageDevices;
        }

        private string GetBatteryInfo()
        {
            return GetSystemInfo("SELECT * FROM Win32_Battery",
                                 "Name", "BatteryStatus", "EstimatedChargeRemaining", "DesignCapacity", "FullChargeCapacity");
        }

        private string GetRamModules()
        {
            string ramModules = string.Empty;
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        ramModules += $"Manufacturer: {obj["Manufacturer"]}\n";
                        ramModules += $"Capacity: {Convert.ToInt64(obj["Capacity"]) / (1024 * 1024 * 1024)} GB\n";
                        ramModules += $"Speed: {obj["Speed"]} MHz\n";
                        ramModules += $"Part Number: {obj["PartNumber"]}\n\n";
                    }
                }
            }
            catch (Exception ex)
            {
                ramModules = $"Error: {ex.Message}";
            }
            return ramModules;
        }

        private void LoadNetworkAdapters()
        {
            NetworkAdapterTabs.Items.Clear();
            var networkAdapters = GetNetworkAdapters();

            foreach (var (adapter, index) in networkAdapters.Select((value, i) => (value, i)))
            {
                var tabItem = new TabItem
                {
                    Header = $"NIC {index + 1}",
                    Content = new StackPanel
                    {
                        Margin = new Thickness(10),
                        Children =
                        {
                            new TextBlock
                            {
                                Text = $"Adapter: {adapter.Name}\nMAC Address: {adapter.MacAddress}",
                                TextWrapping = TextWrapping.Wrap,
                                Foreground = System.Windows.Media.Brushes.White
                            }
                        }
                    }
                };

                NetworkAdapterTabs.Items.Add(tabItem);
            }
        }

        private List<NetworkAdapterInfo> GetNetworkAdapters()
        {
            var adapters = new List<NetworkAdapterInfo>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    var name = obj["Description"]?.ToString() ?? "Unknown";
                    var macAddress = obj["MACAddress"]?.ToString() ?? string.Empty;

                    adapters.Add(new NetworkAdapterInfo
                    {
                        Name = name,
                        MacAddress = macAddress
                    });
                }
            }
            return adapters;
        }

        private class NetworkAdapterInfo
        {
            public string Name { get; set; }
            public string MacAddress { get; set; }
        }
    }
}
