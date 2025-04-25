using ADBSevices;
using InstallRom.Data;
using InstallRom.Logic;
using InstallRom.Start;
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
    public partial class Home : Form
    {
        RomScript rom;
        private ADBSever adbServer;
        private LogicScript logicScript;
        private AddDevices device;
        private ConfigFileDevices deviceDetail;
        private FileSystemWatcher _fileSystemWatcher;
        private ContextMenuStrip contextMenu;
        private RomScript romScript;

        public Home()
        {
            InitializeComponent();
            adbServer = new ADBSever();
            logicScript = new LogicScript();
            romScript = new RomScript(UpdateUI);
            dgvDevices.MouseDown += DgvDevices_MouseDown;
            dgvDevices.CellClick += dgvDevices_CellClick;
            init();
        }
        private void init()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            // set up function
            setCustomUI();
            SetupFileSystemWatcher();
            LoadData();
            SetupContextMenu();
        }
        private void setCustomUI()
        {
            // dgvDevice
            dgvDevices.Dock = DockStyle.Fill;
            dgvDevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDevices.MultiSelect = false;
            dgvDevices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDevices.Columns.Clear();
            dgvDevices.AllowUserToAddRows = false;
            /*
             * set up columns
             */
            dgvDevices.Columns.Add("Index", "Stt");
            dgvDevices.Columns.Add("Serial", "Serial");
            dgvDevices.Columns.Add("Name", "Name device");
            dgvDevices.Columns.Add("Status", "Status");
            dgvDevices.Columns.Add("Progress", "Progress");

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "Start";
            checkBoxColumn.Name = "Start";
            dgvDevices.Columns.Add(checkBoxColumn);

            /*
             * set up size height weight columns
             */
            dgvDevices.Columns[0].Width = 50;
            dgvDevices.Columns[1].Width = 200;
            dgvDevices.Columns[2].Width = 300;
            dgvDevices.Columns[3].Width = 200;
            dgvDevices.Columns[4].Width = 200;
            dgvDevices.Columns[5].Width = 200;

            dgvDevices.Columns[0].MinimumWidth = 50;
            dgvDevices.Columns[1].MinimumWidth = 100;
            dgvDevices.Columns[2].MinimumWidth = 150;
            dgvDevices.Columns[3].MinimumWidth = 100;
            dgvDevices.Columns[4].MinimumWidth = 100;
            dgvDevices.Columns[5].MinimumWidth = 100;

            dgvDevices.Columns[0].ReadOnly = true;
            dgvDevices.Columns[1].ReadOnly = true;
            dgvDevices.Columns[2].ReadOnly = true;
            dgvDevices.Columns[3].ReadOnly = true;
            dgvDevices.Columns[4].ReadOnly = true;
            dgvDevices.Columns[5].ReadOnly = true;

            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // button
            btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnNewDevice.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            // lable
            lbStatus.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            // combobox
            cbDevice.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            cbDevice.Items.Clear();
            cbDevice.Items.Add("Pixcel");
            cbDevice.Items.Add("Xiaomi");
            cbDevice.SelectedIndex = 0;
        }
        private void dgvDevices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDevices.Rows[e.RowIndex];

                string deviceID = row.Cells["Serial"].Value.ToString();

                if (checkDeviceRomBoot(deviceID, true))
                {
                    dgvDevices.Columns[5].ReadOnly = false;
                }
                else
                {
                    dgvDevices.Columns[5].ReadOnly = true;
                }
            }
        }

        private bool checkDeviceRomBoot(string device, bool check)
        {
            DevicesCustomData result = logicScript.CallGetDeviceById(device);

            if (result.rom != "" && result.boot != "" && check)
            {
                lbStatus.Text = "File Boot and Rom Exits";
                return true;
            }
            else
            {
                lbStatus.Text = "No File setup !";
                return false;
            }
        }
        private void LoadData()
        {
            string filePath = "../Config/devices.json";
            var devices = logicScript.CallLoadDevicesFromJson(filePath);

            dgvDevices.Rows.Clear();

            for (int i = 0; i < devices.Count; i++)
            {
                var device = devices[i];

                dgvDevices.Rows.Add(
                    i + 1,
                    device.Serial,
                    device.deviceName,
                    GetDeviceStatus(device.Serial)
                );
            }
        }
        private string GetDeviceStatus(string serial)
        {
            bool isOnline = adbServer.IsDeviceOnline(serial);

            if (isOnline)
            {
                Console.WriteLine($"Device {serial} is online.");
                return "online";
            }
            else
            {
                Console.WriteLine($"Device {serial} is offline.");
                return "offline";
            }
        }

        private void btnFileBoot_Click(object sender, EventArgs e)
        {
            var result = logicScript.CallLogic(1);
            lbStatus.Text = result.Item1;

        }

        private void btnFileRom_Click(object sender, EventArgs e)
        {
            var result = logicScript.CallLogic(2);
            lbStatus.Text = result.Item1;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvDevices.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["Start"].Value);

                if (isChecked)
                {
                   // string selectedItem = cbDevice.SelectedItem?.ToString() ?? "None";
                    DevicesCustomData device = new DevicesCustomData
                    {
                        Serial = row.Cells["Serial"].Value.ToString(),
                        checkEdit = false
                    };
                    checkDeviceRomBoot(device.Serial, device.checkEdit);
                    StartDeviceThread(device, cbDevice.SelectedIndex);
                }
            }
        }
        private void StartDeviceThread(DevicesCustomData deviceID, int deviceName)
        {
            // lbStatus.Text = $"Start device {deviceID.Serial}";
            Thread deviceThread = new Thread(() => romScript.StartRomScript(deviceID, deviceName));
            deviceThread.Start();
        }
        private void btnNewDevice_Click(object sender, EventArgs e)
        {
            if (device != null && !device.IsDisposed)
            {
                device.Close();
            }

            device = new AddDevices();
            device.Show();
        }
        private void SetupFileSystemWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.Path = Path.GetDirectoryName("../Config/devices.json");
            _fileSystemWatcher.Filter = "devices.json";
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (dgvDevices.InvokeRequired)
                {
                    dgvDevices.Invoke(new MethodInvoker(delegate
                    {
                        LoadData();
                    }));
                }
                else
                {
                    LoadData();
                }
            }
        }
        private void DgvDevices_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvDevices.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex >= 0)
                {
                    dgvDevices.ClearSelection();
                    dgvDevices.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Hiển thị menu ngữ cảnh
                    contextMenu.Show(dgvDevices, e.Location);
                }
            }
        }

        private void SetupContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Information Device", null, ShowDeviceInfo);
            contextMenu.Items.Add("Remove Device", null, RemoveDevice);
        }

        private void ShowDeviceInfo(object sender, EventArgs e)
        {
            if (dgvDevices.SelectedRows.Count > 0)
            {
                var serial = dgvDevices.SelectedRows[0].Cells["Serial"].Value.ToString();
                var name = dgvDevices.SelectedRows[0].Cells["Name"].Value.ToString();

                if (deviceDetail != null && !deviceDetail.IsDisposed)
                {
                    deviceDetail.Close();
                }

                deviceDetail = new ConfigFileDevices(serial, name);
                deviceDetail.Show();
            }
        }

        private void RemoveDevice(object sender, EventArgs e)
        {
            if (dgvDevices.SelectedRows.Count > 0)
            {
                var serial = dgvDevices.SelectedRows[0].Cells["Serial"].Value.ToString();

                logicScript.CallDeleteDeviceById(serial);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void UpdateUI(DevicesCustomData device, string column, string value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUI(device, column, value)));
            }
            else
            {
                foreach (DataGridViewRow row in dgvDevices.Rows)
                {
                    if (row.Cells["Serial"].Value.ToString() == device.Serial)
                    {
                        row.Cells[$"{column}"].Value = value;
                        break;
                    }
                }
            }
        }

        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            
          
        }
    }
}
