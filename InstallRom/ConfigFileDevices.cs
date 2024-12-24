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

namespace InstallRom
{
    public partial class ConfigFileDevices : Form
    {
        LogicScript LogicScript;
        public ConfigFileDevices(string serial, string name)
        {
            InitializeComponent();
            LogicScript = new LogicScript();
            this.StartPosition = FormStartPosition.CenterScreen;
            updateUi(serial);
            init();
        }
        private void init()
        {
            txtSerial.ReadOnly = true;
        }
        private void updateUi(string serial)
        {
            // get data json 
            var result = LogicScript.CallGetDeviceById(serial);
            if (result != null)
            {
                txtName.Text = result.deviceName;
                txtSerial.Text = serial;
                lbBoot.Text = result.boot;
                lbRom.Text = result.rom;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var serial = txtSerial.Text;
            var name = txtName.Text;
            var boot = lbBoot.Text;
            var rom = lbRom.Text;

            LogicScript.CallUpdateDevice(serial, name, boot, rom);

            MessageBox.Show("Update Success!");

        }

        private void btnBoot_Click(object sender, EventArgs e)
        {
            var result = LogicScript.CallLogic(1);
            lbBoot.Text = result.Item2;
        }

        private void btnRom_Click(object sender, EventArgs e)
        {
            var result = LogicScript.CallLogic(2);
            lbRom.Text = result.Item2;
        }
    }
}
