using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSevices
{
    public class ADBService
    {
        public static string runCMD(string commandline, string deviceId, int timeout = 0)
        {
            Console.WriteLine(string.Format("/C adb -s {0} {1}", deviceId, commandline));
            return CMDProgress.ExecuteCommand(string.Format("/C adb -s {0} {1}", deviceId, commandline), timeout);
        }
        public bool UnlockBootloader(string deviceId)
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = $"fastboot -s {deviceId}",
                    Arguments = "flashing unlock",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (output.Contains("OK", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Unlock successful.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Unlock failed: {error}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during unlock process: {ex.Message}");
                return false;
            }
        }
        public async Task<(bool, string)> FlashBootImageAsync(string bootImagePath, string device)
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "fastboot",
                    Arguments = $"-s {device} flash boot {bootImagePath}", // Đảm bảo đường dẫn đúng
                    UseShellExecute = false, // Để sử dụng redirect IO
                    RedirectStandardOutput = true, // Redirect đầu ra
                    RedirectStandardError = true,  // Redirect lỗi
                    CreateNoWindow = true // Ẩn cửa sổ CMD
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    // Đọc đầu ra
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    // Đợi process hoàn thành
                    await process.WaitForExitAsync();
                    Thread.Sleep(5000);
                    // Kiểm tra đầu ra để xác định xem lệnh có thành công không
                    if (output.Contains("OKAY", StringComparison.OrdinalIgnoreCase) &&
                        output.Contains("Finished", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Flash successful.");
                        return (true, "successful"); // Lệnh thành công
                    }
                    else
                    {
                        if (error.Contains("OKAY", StringComparison.OrdinalIgnoreCase) &&
                        error.Contains("Finished", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Flash successful.");
                            return (true, "successful");  // Lệnh thành công
                        }
                        else
                        {
                            Console.WriteLine($"Flash failed: {error}");
                            return (true, "successful");  // Lệnh không thành công
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during flash process: {ex.Message}");
                return (false, ex.Message); // Lỗi trong quá trình thực hiện lệnh
            }
        }

        public async Task<(bool, string)> FlashBootImageXiaomiAsync(string bootImagePath, string device)
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "fastboot",
                    Arguments = $"-s {device} boot {bootImagePath}", // Đảm bảo đường dẫn đúng
                    UseShellExecute = false, // Để sử dụng redirect IO
                    RedirectStandardOutput = true, // Redirect đầu ra
                    RedirectStandardError = true,  // Redirect lỗi
                    CreateNoWindow = true // Ẩn cửa sổ CMD
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    // Đọc đầu ra
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    // Đợi process hoàn thành
                    await process.WaitForExitAsync();
                    Thread.Sleep(5000);
                    // Kiểm tra đầu ra để xác định xem lệnh có thành công không
                    if (output.Contains("OKAY", StringComparison.OrdinalIgnoreCase) &&
                        output.Contains("Finished", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Flash successful.");
                        return (true, "successful"); // Lệnh thành công
                    }
                    else
                    {
                        if (error.Contains("OKAY", StringComparison.OrdinalIgnoreCase) &&
                        error.Contains("Finished", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Flash successful.");
                            return (true, "successful");  // Lệnh thành công
                        }
                        else
                        {
                            Console.WriteLine($"Flash failed: {error}");
                            return (true, "successful");  // Lệnh không thành công
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during flash process: {ex.Message}");
                return (false, ex.Message); // Lỗi trong quá trình thực hiện lệnh
            }
        }
        public static string GetDeviceManufacturer(string deviceId)
        {
            try
            {
                // Thiết lập Process để chạy lệnh adb
                Process process = new Process();
                process.StartInfo.FileName = "adb"; // Đảm bảo adb có trong PATH
                process.StartInfo.Arguments = $"-s {deviceId} shell getprop ro.product.manufacturer";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = process.StandardOutput.ReadToEnd().Trim();
                string error = process.StandardError.ReadToEnd().Trim();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("ADB Error: " + error);
                    return null;
                }

                return output; // Trả về nhà sản xuất
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
                return null;
            }
        }

        public static void StartBootloader(string deviceIP)
        {
            runCMD(String.Format(" reboot bootloader"), deviceIP);
        }
        public static void StartRecovey(string deviceIP)
        {
            runCMD(String.Format(" reboot recovery"), deviceIP);
        }
        public static bool IsAdbDeviceConnected(string deviceId)
        {
            try
            {
                // Thiết lập process để chạy adb
                Process process = new Process();
                process.StartInfo.FileName = "adb"; // Đảm bảo adb có trong PATH hoặc cung cấp đường dẫn đầy đủ
                process.StartInfo.Arguments = "devices";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                // Đọc kết quả từ adb
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("ADB Error: " + error);
                    return false; // Nếu có lỗi, coi như thiết bị không kết nối
                }

                // Phân tích kết quả đầu ra
                string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    if (line.Contains(deviceId) && line.EndsWith("device"))
                    {
                        return true; // Thiết bị đã kết nối và trạng thái là "device"
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
            }

            return false; // Nếu không tìm thấy thiết bị hoặc có lỗi
        }
        public static void FastBootReboot(string deviceIP)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "fastboot", // Lệnh fastboot
                Arguments = $"-s {deviceIP} reboot fastboot", // Tham số của fastboot
                RedirectStandardOutput = true, // Redirect đầu ra nếu cần
                RedirectStandardError = true, // Redirect lỗi nếu có
                UseShellExecute = false, // Để sử dụng redirect
                CreateNoWindow = true // Không hiển thị cửa sổ console
            };

            try
            {
                // Bắt đầu tiến trình
                process.Start();

                // Đọc đầu ra và lỗi (nếu có)
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Hiển thị kết quả
                if (!string.IsNullOrWhiteSpace(output))
                    Console.WriteLine("Output: " + output);

                if (!string.IsNullOrWhiteSpace(error))
                    Console.WriteLine("Error: " + error);

                Console.WriteLine("Command executed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static bool CheckDeviceBootCompleted(string deviceId)
        {
            try
            {
                // Tạo tiến trình để gọi lệnh ADB
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "adb", // Lệnh thực thi ADB
                    Arguments = $"-s {deviceId} shell getprop sys.boot_completed", // Lệnh kiểm tra trạng thái boot
                    RedirectStandardOutput = true, // Redirect đầu ra
                    RedirectStandardError = true, // Redirect lỗi
                    UseShellExecute = false, // Không sử dụng shell mặc định
                    CreateNoWindow = true // Không tạo cửa sổ mới
                };

                // Bắt đầu tiến trình
                process.Start();

                // Đọc kết quả đầu ra
                string output = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();

                // Kiểm tra kết quả đầu ra
                return output == "1";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("An error occurred: " + ex.Message);
                return false; // Trả về false nếu có lỗi
            }
        }

        public static bool RebootToRecovery(string deviceID)
        {
            try
            {
                // Tạo tiến trình để gọi lệnh ADB
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "adb", // Lệnh thực thi ADB
                    Arguments = $"-s {deviceID} reboot recovery", // Lệnh vào Recovery Mode
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Bắt đầu tiến trình
                process.Start();

                // Đọc kết quả
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Kiểm tra nếu không có lỗi
                if (!string.IsNullOrWhiteSpace(output))
                    Console.WriteLine("Output: " + output);

                if (!string.IsNullOrWhiteSpace(error))
                    Console.WriteLine("Error: " + error);

                return string.IsNullOrWhiteSpace(error); // Trả về true nếu không có lỗi
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        public static bool CheckDevice(string deviceId)
        {
            try
            {
                // Tạo tiến trình để gọi lệnh adb devices
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "adb", // Lệnh ADB
                    Arguments = "devices", // Tham số kiểm tra danh sách thiết bị
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Bắt đầu tiến trình
                process.Start();

                // Đọc đầu ra
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Hiển thị thông báo lỗi nếu có
                if (!string.IsNullOrWhiteSpace(error))
                {
                    Console.WriteLine("Error: " + error);
                    return false;
                }

                // Kiểm tra xem danh sách thiết bị có chứa ID cần tìm không
                return output.Contains(deviceId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false; // Trả về false nếu có lỗi
            }
        }
        public static bool RunSideload(string deviceId, string filePath)
        {
            try
            {
                // Tạo tiến trình thực hiện lệnh adb sideload
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = $"-s {deviceId} sideload \"{filePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Bắt đầu tiến trình
                process.Start();

                // Đọc kết quả đầu ra
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Hiển thị kết quả đầu ra (tuỳ chọn)
                if (!string.IsNullOrWhiteSpace(output))
                    Console.WriteLine("Output: " + output);

                if (!string.IsNullOrWhiteSpace(error))
                    Console.WriteLine("Error: " + error);

                // Kiểm tra nếu có chữ "serving" trong đầu ra
                return output.Contains("serving", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
    }
}
