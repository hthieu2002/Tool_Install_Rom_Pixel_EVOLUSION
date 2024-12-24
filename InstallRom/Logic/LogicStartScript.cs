using InstallRom.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallRom.Logic
{
    public class LogicStartScript
    {
        private string JsonPath = "../Config/devices.json";
        public LogicStartScript() { }

        private DevicesCustomData getDataDeviceById(string deviceId)
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
        /*
         * function call
         */
        public DevicesCustomData callGetDataDeviceById(string device)
        {
            return getDataDeviceById(device);
        }
    }
}
