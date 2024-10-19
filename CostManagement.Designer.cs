namespace Project1_Laundry
{
    partial class CostManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CostManagement));
            this.dtCoG = new System.Windows.Forms.DateTimePicker();
            this.lblCid = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.txtCostName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdate = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnCancel = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblid = new System.Windows.Forms.Label();
            this.dtpCoG = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtCoG
            // 
            this.dtCoG.Location = new System.Drawing.Point(141, 281);
            this.dtCoG.Name = "dtCoG";
            this.dtCoG.Size = new System.Drawing.Size(544, 26);
            this.dtCoG.TabIndex = 36;
            // 
            // lblCid
            // 
            this.lblCid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCid.AutoSize = true;
            this.lblCid.Location = new System.Drawing.Point(-80, 196);
            this.lblCid.Name = "lblCid";
            this.lblCid.Size = new System.Drawing.Size(34, 20);
            this.lblCid.TabIndex = 44;
            this.lblCid.Text = "Cid";
            this.lblCid.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "Date :";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(673, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 33);
            this.btnClose.TabIndex = 40;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(140, 117);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(544, 26);
            this.txtCost.TabIndex = 35;
            this.txtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCost_KeyPress);
            // 
            // txtCostName
            // 
            this.txtCostName.Location = new System.Drawing.Point(140, 72);
            this.txtCostName.Name = "txtCostName";
            this.txtCostName.Size = new System.Drawing.Size(544, 26);
            this.txtCostName.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "Tên Chi Phí :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(0)))), ((int)(((byte)(55)))));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 23);
            this.label1.TabIndex = 42;
            this.label1.Text = "Quản Lý Chi Phí Hoạt Động";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(0)))), ((int)(((byte)(55)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 27);
            this.panel1.TabIndex = 41;
            // 
            // btnUpdate
            // 
            this.btnUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnUpdate.FillColor = System.Drawing.Color.White;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.ImageSize = new System.Drawing.Size(60, 60);
            this.btnUpdate.Location = new System.Drawing.Point(374, 200);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnUpdate.Size = new System.Drawing.Size(72, 68);
            this.btnUpdate.TabIndex = 47;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageSize = new System.Drawing.Size(60, 60);
            this.btnCancel.Location = new System.Drawing.Point(543, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnCancel.Size = new System.Drawing.Size(66, 68);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(203, 200);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(58, 68);
            this.btnSave.TabIndex = 48;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblid
            // 
            this.lblid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblid.AutoSize = true;
            this.lblid.Location = new System.Drawing.Point(12, 248);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(22, 20);
            this.lblid.TabIndex = 49;
            this.lblid.Text = "id";
            this.lblid.Visible = false;
            // 
            // dtpCoG
            // 
            this.dtpCoG.CustomFormat = "dd/MM/yyyy";
            this.dtpCoG.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCoG.Location = new System.Drawing.Point(140, 162);
            this.dtpCoG.Name = "dtpCoG";
            this.dtpCoG.Size = new System.Drawing.Size(544, 26);
            this.dtpCoG.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 53;
            this.label5.Text = "Ngày :";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(13, 123);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(72, 20);
            this.lblPass.TabIndex = 46;
            this.lblPass.Text = "Giá Tiền :";
            this.lblPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CostManagement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(708, 280);
            this.Controls.Add(this.dtpCoG);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblid);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dtCoG);
            this.Controls.Add(this.lblCid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.txtCostName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CostManagement";
            this.Text = "CostManagement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DateTimePicker dtCoG;
        public System.Windows.Forms.Label lblCid;
        public System.Windows.Forms.TextBox txtCost;
        public System.Windows.Forms.TextBox txtCostName;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel1;
        public Guna.UI2.WinForms.Guna2CircleButton btnUpdate;
        public Guna.UI2.WinForms.Guna2CircleButton btnCancel;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Label lblid;
        public System.Windows.Forms.DateTimePicker dtpCoG;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lblPass;
    }
}