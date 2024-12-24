using ADBSevices;
using InstallRom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstallRom.Logic
{
    public class LogicScript
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        ADBSever adb = new ADBSever();
        DevicesCustomData device = new DevicesCustomData();
       
        private string JsonPath = "../Config/devices.json";
        public LogicScript() { }
        private (string, string) logic(int index)
        {
            string textStatus = "";
            string textUrl = "";
            if (index == 1)
            {
                // boot
                textUrl = logicImportFile("(*.img) | *.img");
                textStatus = "Import file boot success!";
            }
            else if (index == 2)
            {
                // rom
                textUrl = logicImportFile("(*.zip) | *.zip");
               
                textStatus = "Import file rom success!";
            }
            return (textStatus, textUrl);
        }
        private string logicImportFile(string RegexFile)
        {
            string filePath = "";
            openFileDialog.Title = "Chọn Tệp";
            openFileDialog.Filter = $"File Boot {RegexFile}"; 
            openFileDialog.FilterIndex = 1;  
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            return filePath;
        }
        private List<string> loadDataDeviceCombobox()
        {
            var deviceIds = adb.GetConnectedDeviceIds();
           
            return deviceIds;
        }
        private List<string> checkDataDeviceJson(string filePath) 
        {
            if (!File.Exists(filePath))
            {
                return new List<string>();
            }
            var json = File.ReadAllText(filePath);
            var devices = JsonConvert.DeserializeObject<List<DevicesCustomData>>(json);

            return devices?.Select(d => d.Serial).ToList() ?? new List<string>();
        }
        private string addDataDeviceJson(DevicesCustomData device)
        {
            List<DevicesCustomData> devicesList;

            if (File.Exists(JsonPath))
            {
                var json = File.ReadAllText(JsonPath);
                devicesList = JsonConvert.DeserializeObject<List<DevicesCustomData>>(json) ?? new List<DevicesCustomData>();
            }
            else
            {
                devicesList = new List<DevicesCustomData>(); 
            }
            devicesList.Add(device);

            var newJson = JsonConvert.SerializeObject(devicesList, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(JsonPath, newJson);
            
            return "Save Success !";
        }
        private List<DevicesCustomData> LoadDevicesFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<DevicesCustomData>(); 
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<DevicesCustomData>>(json) ?? new List<DevicesCustomData>();
        }
        private void DeleteDeviceById(string deviceId)
        {
            var devices = LoadDevices();

            var deviceToRemove = devices.Find(d => d.Serial == deviceId);
            if (deviceToRemove != null)
            {
                devices.Remove(deviceToRemove);
                SaveDevices(devices); 
            }
        }
        private List<DevicesCustomData> LoadDevices()
        {
            if (!File.Exists(JsonPath))
                return new List<DevicesCustomData>();

            var json = File.ReadAllText(JsonPath);
            return JsonConvert.DeserializeObject<List<DevicesCustomData>>(json);
        }
        private void UpdateDevice(string deviceId, string name, string rom, string boot)
        {
            var devices = LoadDevices();

            var device = devices.Find(d => d.Serial == deviceId);
            if (device != null)
            {
                device.deviceName = name;
                device.rom = rom;
                device.boot = boot;

                SaveDevices(devices);
                Console.WriteLine("Thiết bị đã được cập nhật.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy thiết bị với deviceID đã cho.");
            }
        }
        private void SaveDevices(List<DevicesCustomData> devices)
        {
            var json = JsonConvert.SerializeObject(devices, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(JsonPath, json);
        }

        private DevicesCustomData GetDeviceById(string deviceId)
        {
            var devices = LoadDevicesByID();

            var device = devices.Find(d => d.Serial == deviceId);
            if (device != null)
            {
                return device; 
            }
            else
            {
                Console.WriteLine("Thiết bị không tồn tại.");
                return null;
            }
        }
        private List<DevicesCustomData> LoadDevicesByID()
        {
            if (!File.Exists(JsonPath))
                return new List<DevicesCustomData>();

            var json = File.ReadAllText(JsonPath);
            return JsonConvert.DeserializeObject<List<DevicesCustomData>>(json);
        }

        

        /// <summary>
        /// function call 
        /// </summary>
        /// <param name="Call Logic"></param>
        /// <returns></returns>
        public (string, string) CallLogic(int index)
        {return logic(index);}
        public List<string> CallLoadDataDeviceCombobox() 
        {return loadDataDeviceCombobox();}
        public List<string> CallCheckDataDeviceJson(string path)
        { return checkDataDeviceJson(path); }
        public string CallAddData(DevicesCustomData device)
        {return addDataDeviceJson(device);}
        public List<DevicesCustomData> CallLoadDevicesFromJson(string filePath)
        { return LoadDevicesFromJson(filePath); }
        public void CallDeleteDeviceById(string serial) 
        { DeleteDeviceById(serial); }
        public DevicesCustomData CallGetDeviceById(string devices) 
        {  return GetDeviceById(devices); }
        public void CallUpdateDevice(string serial, string name, string boot, string rom)
        { UpdateDevice(serial, name, rom, boot); }
    }
}
