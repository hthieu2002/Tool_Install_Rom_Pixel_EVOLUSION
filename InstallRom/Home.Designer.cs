namespace InstallRom
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvDevices = new DataGridView();
            btnStart = new Button();
            lbStatus = new Label();
            cbDevice = new ComboBox();
            btnNewDevice = new Button();
            btnLoad = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            SuspendLayout();
            // 
            // dgvDevices
            // 
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevices.Location = new Point(3, 169);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.RowHeadersWidth = 51;
            dgvDevices.RowTemplate.Height = 29;
            dgvDevices.Size = new Size(986, 279);
            dgvDevices.TabIndex = 0;
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.Location = new Point(23, 24);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(128, 35);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start ";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(175, 35);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(18, 20);
            lbStatus.TabIndex = 4;
            lbStatus.Text = "...";
            // 
            // cbDevice
            // 
            cbDevice.FormattingEnabled = true;
            cbDevice.Location = new Point(811, 32);
            cbDevice.Name = "cbDevice";
            cbDevice.Size = new Size(151, 28);
            cbDevice.TabIndex = 7;
            cbDevice.SelectedIndexChanged += cbDevice_SelectedIndexChanged;
            // 
            // btnNewDevice
            // 
            btnNewDevice.Location = new Point(23, 114);
            btnNewDevice.Name = "btnNewDevice";
            btnNewDevice.Size = new Size(128, 43);
            btnNewDevice.TabIndex = 8;
            btnNewDevice.Text = "New Devices";
            btnNewDevice.UseVisualStyleBackColor = true;
            btnNewDevice.Click += btnNewDevice_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(23, 65);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(128, 43);
            btnLoad.TabIndex = 9;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(991, 450);
            Controls.Add(btnLoad);
            Controls.Add(btnNewDevice);
            Controls.Add(cbDevice);
            Controls.Add(lbStatus);
            Controls.Add(btnStart);
            Controls.Add(dgvDevices);
            Name = "Home";
            Text = "Home";
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDevices;
        private Button btnStart;
        private Label lbStatus;
        private ComboBox cbDevice;
        private Button btnNewDevice;
        private Button btnLoad;
    }
}