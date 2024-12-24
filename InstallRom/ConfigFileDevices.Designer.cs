namespace InstallRom
{
    partial class ConfigFileDevices
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
            label1 = new Label();
            label2 = new Label();
            txtSerial = new TextBox();
            txtName = new TextBox();
            btnBoot = new Button();
            btnRom = new Button();
            btnSave = new Button();
            lbBoot = new Label();
            lbRom = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 24);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 0;
            label1.Text = "Serial";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 68);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 1;
            label2.Text = "Name";
            // 
            // txtSerial
            // 
            txtSerial.Location = new Point(134, 21);
            txtSerial.Name = "txtSerial";
            txtSerial.Size = new Size(215, 27);
            txtSerial.TabIndex = 2;
            // 
            // txtName
            // 
            txtName.Location = new Point(134, 68);
            txtName.Name = "txtName";
            txtName.Size = new Size(215, 27);
            txtName.TabIndex = 3;
            // 
            // btnBoot
            // 
            btnBoot.Location = new Point(31, 120);
            btnBoot.Name = "btnBoot";
            btnBoot.Size = new Size(94, 29);
            btnBoot.TabIndex = 4;
            btnBoot.Text = "Boot";
            btnBoot.UseVisualStyleBackColor = true;
            btnBoot.Click += btnBoot_Click;
            // 
            // btnRom
            // 
            btnRom.Location = new Point(31, 166);
            btnRom.Name = "btnRom";
            btnRom.Size = new Size(94, 29);
            btnRom.TabIndex = 5;
            btnRom.Text = "Rom";
            btnRom.UseVisualStyleBackColor = true;
            btnRom.Click += btnRom_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(159, 229);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lbBoot
            // 
            lbBoot.AutoSize = true;
            lbBoot.Location = new Point(143, 129);
            lbBoot.Name = "lbBoot";
            lbBoot.Size = new Size(18, 20);
            lbBoot.TabIndex = 7;
            lbBoot.Text = "...";
            // 
            // lbRom
            // 
            lbRom.AutoSize = true;
            lbRom.Location = new Point(143, 175);
            lbRom.Name = "lbRom";
            lbRom.Size = new Size(18, 20);
            lbRom.TabIndex = 8;
            lbRom.Text = "...";
            // 
            // ConfigFileDevices
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 294);
            Controls.Add(lbRom);
            Controls.Add(lbBoot);
            Controls.Add(btnSave);
            Controls.Add(btnRom);
            Controls.Add(btnBoot);
            Controls.Add(txtName);
            Controls.Add(txtSerial);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ConfigFileDevices";
            Text = "ConfigFileDevices";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtSerial;
        private TextBox txtName;
        private Button btnBoot;
        private Button btnRom;
        private Button btnSave;
        private Label lbBoot;
        private Label lbRom;
    }
}