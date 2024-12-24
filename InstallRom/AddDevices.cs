using InstallRom.Data;
using InstallRom.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InstallRom
{
    public partial class AddDevices : Form
    {
        private LogicScript logic;
        private DevicesCustomData devices;
        public AddDevices()
        {
            InitializeComponent();
            logic = new LogicScript();
            init();
        }

        private void init()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            // setup
            setUpCustomUI();
            loadData();
        }
        private void setUpCustomUI()
        {

        }
        private void loadData()
        {
            var deviceIds = logic.CallLoadDataDeviceCombobox();
            var existingSerials = logic.CallCheckDataDeviceJson("../Config/devices.json");
            foreach (var deviceId in deviceIds)
            {
                if (!existingSerials.Contains(deviceId))
                {
                    cbDevice.Items.Add(deviceId); 
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var selectedDeviceId = cbDevice.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedDeviceId))
            {
                lbStatus.Text = $"Device : {selectedDeviceId}";

                var txtNameDevice = txtName.Text;
                devices = new DevicesCustomData();
                devices.Serial = selectedDeviceId;
                devices.deviceName = txtNameDevice;

                var status = logic.CallAddData(devices);

                MessageBox.Show(status);
            }
            else
            {
                lbStatus.Text = "No Device Error !";
            }
        }
    }
}
