using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSevices
{
    public class ADBSever
    {
        public List<string> GetConnectedDeviceIds()
        {
            List<string> deviceIds = new List<string>();

            // Tạo ProcessStartInfo để gọi adb
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "adb", 
                Arguments = "devices",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Khởi động process
            using (var process = Process.Start(processStartInfo))
            {
                using (var reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    var lines = result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    // Bỏ qua dòng tiêu đề và lấy ID thiết bị từ các dòng sau
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var parts = lines[i].Split('\t');
                        if (parts.Length >= 1)
                        {
                            string deviceId = parts[0]; // Lấy ID thiết bị
                            if (!string.IsNullOrWhiteSpace(deviceId) && parts.Length > 1 && parts[1] == "device")
                            {
                                deviceIds.Add(deviceId); // Thêm ID thiết bị vào danh sách
                            }
                        }
                    }
                }
            }

            return deviceIds;
        }

        public bool IsDeviceOnline(string deviceId)
        {
            // Gọi lệnh ADB để lấy danh sách thiết bị kết nối
            var processInfo = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = "devices",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = processInfo };
            process.Start();

            // Đọc kết quả từ lệnh ADB
            using (var reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                process.WaitForExit();

                // Phân tích kết quả để kiểm tra sự hiện diện của deviceId
                string[] lines = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Contains(deviceId) && line.Contains("device")) // Kiểm tra nếu ID thiết bị và trạng thái là "device"
                    {
                        return true; // Thiết bị đang online
                    }
                }
            }

            return false; // Thiết bị không online
        }

        //
        public bool FlasbootIsDeviceOnline(string deviceId)
        {
            // Gọi lệnh ADB để lấy danh sách thiết bị kết nối
            var processInfo = new ProcessStartInfo
            {
                FileName = "fastboot",
                Arguments = "devices",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = processInfo };
            process.Start();

            // Đọc kết quả từ lệnh ADB
            using (var reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                process.WaitForExit();

                // Phân tích kết quả để kiểm tra sự hiện diện của deviceId
                string[] lines = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Contains(deviceId) && line.Contains("fastboot")) // Kiểm tra nếu ID thiết bị và trạng thái là "device"
                    {
                        return true; // Thiết bị đang online
                    }
                }
            }

            return false; // Thiết bị không online
        }
    }
}
