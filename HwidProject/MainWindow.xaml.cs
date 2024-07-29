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
        }

        private void FetchHwid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch information
                string motherboardInfo = GetMotherboardInfo();
                string processorInfo = GetProcessorInfo();
                string hardDriveInfo = GetHardDriveInfo();
                string graphicsCardInfo = GetGraphicsCardInfo();

                // Display information in UI
                txtMotherboard.Text = motherboardInfo;
                txtProcessor.Text = processorInfo;
                txtHardDrive.Text = hardDriveInfo;
                txtGraphicsCard.Text = graphicsCardInfo;

                // Define file path with actual directory
                string fileName = "Hwid_Backup.csv";
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                // Save information to CSV file
                SaveToCsv(filePath, motherboardInfo, processorInfo, hardDriveInfo, graphicsCardInfo);

                // Notify user with the actual file path
                MessageBox.Show($"HWID information has been saved to:\n{filePath}", "Save Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching HWID: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SaveToCsv(string filePath, string motherboardInfo, string processorInfo, string hardDriveInfo, string graphicsCardInfo)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    // Write the header
                    writer.WriteLine("Component,Information");

                    // Write motherboard information
                    writer.WriteLine($"Motherboard,\"{motherboardInfo.Replace("\"", "\"\"")}\"");

                    // Write processor information
                    writer.WriteLine($"Processor,\"{processorInfo.Replace("\"", "\"\"")}\"");

                    // Write hard drive information
                    writer.WriteLine($"Hard Drive,\"{hardDriveInfo.Replace("\"", "\"\"")}\"");

                    // Write graphics card information
                    writer.WriteLine($"Graphics Card,\"{graphicsCardInfo.Replace("\"", "\"\"")}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to CSV: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private static string GetMotherboardInfo()
        {
            string serialNumber = "N/A";
            string model = "N/A";

            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber, Product FROM Win32_BaseBoard"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        if (obj is ManagementObject mo)
                        {
                            serialNumber = mo["SerialNumber"]?.ToString() ?? "N/A";
                            model = mo["Product"]?.ToString() ?? "N/A";
                            break;
                        }
                    }
                }
            }
            catch
            {
                // Handle the exception silently or log it
            }

            return $"Model: {model}\nSerial Number: {serialNumber}";
        }

        private static string GetProcessorInfo()
        {
            string serialNumber = "N/A";
            string model = "N/A";

            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId, Name FROM Win32_Processor"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        if (obj is ManagementObject mo)
                        {
                            serialNumber = mo["ProcessorId"]?.ToString() ?? "N/A";
                            model = mo["Name"]?.ToString() ?? "N/A";
                            break;
                        }
                    }
                }
            }
            catch
            {
                // Handle the exception silently or log it
            }

            return $"Model: {model}\nProcessor ID: {serialNumber}";
        }

        private static string GetHardDriveInfo()
        {
            string serialNumber = "N/A";
            string model = "N/A";

            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber, Model FROM Win32_DiskDrive"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        if (obj is ManagementObject mo)
                        {
                            serialNumber = mo["SerialNumber"]?.ToString() ?? "N/A";
                            model = mo["Model"]?.ToString() ?? "N/A";
                            break;
                        }
                    }
                }
            }
            catch
            {
                // Handle the exception silently or log it
            }

            return $"Model: {model}\nSerial Number: {serialNumber}";
        }

        private static string GetGraphicsCardInfo()
        {
            string graphicsCardInfo = "Graphics Card Information:\n";

            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT PNPDeviceID, Name FROM Win32_VideoController"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        if (obj is ManagementObject mo)
                        {
                            graphicsCardInfo += $"Card: {mo["Name"]}\nUUID: {mo["PNPDeviceID"]}\n\n";
                        }
                    }
                }
            }
            catch
            {
                graphicsCardInfo += "Error fetching data\n";
            }

            return graphicsCardInfo;
        }

        private void LoadNetworkAdapters()
        {
            NetworkAdapterTabs.Items.Clear();

            var networkAdapters = GetNetworkAdapters();

            for (int i = 0; i < networkAdapters.Count; i++)
            {
                var adapter = networkAdapters[i];
                var tabItem = new TabItem
                {
                    Header = $"NIC {i + 1}"
                };

                var stackPanel = new StackPanel
                {
                    Margin = new Thickness(10)
                };

                var adapterInfo = new TextBlock
                {
                    Text = $"Adapter: {adapter.Name}\nMAC Address: {adapter.MacAddress}",
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = System.Windows.Media.Brushes.White
                };

                stackPanel.Children.Add(adapterInfo);
                tabItem.Content = stackPanel;
                NetworkAdapterTabs.Items.Add(tabItem);
            }
        }

        private List<NetworkAdapterInfo> GetNetworkAdapters()
        {
            var adapters = new List<NetworkAdapterInfo>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True");

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

            return adapters;
        }

        private class NetworkAdapterInfo
        {
            public string Name { get; set; }
            public string MacAddress { get; set; }
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedIndex == 1) // Spoof tab selected
            {
                // Example: Set default selection or load existing spoof settings if applicable
            }
        }

        private void DeviceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }

        private void ApplySpoof_Click(object sender, RoutedEventArgs e)
        {
            // Spoof logic here (currently non-functional)
        }

        private void Revert_Click(object sender, RoutedEventArgs e)
        {
            // Revert spoof logic here (currently non-functional)
        }
    }
}
