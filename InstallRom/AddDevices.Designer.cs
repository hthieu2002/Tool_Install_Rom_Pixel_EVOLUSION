namespace InstallRom
{
    partial class AddDevices
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
            cbDevice = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            txtName = new TextBox();
            btnSave = new Button();
            lbStatus = new Label();
            SuspendLayout();
            // 
            // cbDevice
            // 
            cbDevice.FormattingEnabled = true;
            cbDevice.Location = new Point(124, 45);
            cbDevice.Name = "cbDevice";
            cbDevice.Size = new Size(332, 28);
            cbDevice.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 48);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 1;
            label1.Text = "Device";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 133);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 2;
            label2.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(124, 133);
            txtName.Name = "txtName";
            txtName.Size = new Size(332, 27);
            txtName.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(206, 184);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(124, 86);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(18, 20);
            lbStatus.TabIndex = 5;
            lbStatus.Text = "...";
            // 
            // AddDevices
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 246);
            Controls.Add(lbStatus);
            Controls.Add(btnSave);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbDevice);
            Name = "AddDevices";
            Text = "AddDevices";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbDevice;
        private Label label1;
        private Label label2;
        private TextBox txtName;
        private Button btnSave;
        private Label lbStatus;
    }
}