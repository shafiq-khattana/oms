namespace WinFom.Deal.Forms
{
    partial class DealReportsForm
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
            this.components = new System.ComponentModel.Container();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.lblHeading = new System.Windows.Forms.Label();
            this.pHeader = new System.Windows.Forms.Panel();
            this.picBtnIcon = new System.Windows.Forms.PictureBox();
            this.picBtnClose = new System.Windows.Forms.PictureBox();
            this.pLeft = new System.Windows.Forms.Panel();
            this.pRight = new System.Windows.Forms.Panel();
            this.pBottom = new System.Windows.Forms.Panel();
            this.pTop = new System.Windows.Forms.Panel();
            this.pMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnTrialBalance = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReceivedSchedules = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.lblHeading;
            this.bunifuDragControl1.Vertical = true;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(381, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Reports";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pHeader
            // 
            this.pHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.pHeader.Controls.Add(this.lblHeading);
            this.pHeader.Controls.Add(this.picBtnIcon);
            this.pHeader.Controls.Add(this.picBtnClose);
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(463, 37);
            this.pHeader.TabIndex = 0;
            // 
            // picBtnIcon
            // 
            this.picBtnIcon.BackColor = System.Drawing.Color.DodgerBlue;
            this.picBtnIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.picBtnIcon.Image = global::WinFom.Properties.Resources.icons8_robot_48;
            this.picBtnIcon.Location = new System.Drawing.Point(0, 0);
            this.picBtnIcon.Name = "picBtnIcon";
            this.picBtnIcon.Size = new System.Drawing.Size(42, 37);
            this.picBtnIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnIcon.TabIndex = 1;
            this.picBtnIcon.TabStop = false;
            // 
            // picBtnClose
            // 
            this.picBtnClose.BackColor = System.Drawing.Color.Tomato;
            this.picBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.picBtnClose.Image = global::WinFom.Properties.Resources.Delete_52px;
            this.picBtnClose.Location = new System.Drawing.Point(423, 0);
            this.picBtnClose.Name = "picBtnClose";
            this.picBtnClose.Size = new System.Drawing.Size(40, 37);
            this.picBtnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnClose.TabIndex = 1;
            this.picBtnClose.TabStop = false;
            this.picBtnClose.Click += new System.EventHandler(this.picBtnClose_Click);
            // 
            // pLeft
            // 
            this.pLeft.BackColor = System.Drawing.Color.Gray;
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLeft.Location = new System.Drawing.Point(0, 37);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(10, 301);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(453, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 301);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 326);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(443, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(443, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.panel1);
            this.pMain.Controls.Add(this.panel3);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(443, 277);
            this.pMain.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Controls.Add(this.btnTrialBalance);
            this.panel3.Location = new System.Drawing.Point(7, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(430, 76);
            this.panel3.TabIndex = 33;
            // 
            // btnTrialBalance
            // 
            this.btnTrialBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTrialBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrialBalance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTrialBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrialBalance.ForeColor = System.Drawing.Color.White;
            this.btnTrialBalance.Location = new System.Drawing.Point(61, 22);
            this.btnTrialBalance.Name = "btnTrialBalance";
            this.btnTrialBalance.Size = new System.Drawing.Size(315, 35);
            this.btnTrialBalance.TabIndex = 30;
            this.btnTrialBalance.Text = "Cotton Seed Companies Trial Balance";
            this.btnTrialBalance.UseVisualStyleBackColor = false;
            this.btnTrialBalance.Click += new System.EventHandler(this.btnTrialBalance_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnReceivedSchedules);
            this.panel1.Location = new System.Drawing.Point(6, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 94);
            this.panel1.TabIndex = 34;
            // 
            // btnReceivedSchedules
            // 
            this.btnReceivedSchedules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnReceivedSchedules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReceivedSchedules.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReceivedSchedules.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceivedSchedules.ForeColor = System.Drawing.Color.White;
            this.btnReceivedSchedules.Location = new System.Drawing.Point(7, 47);
            this.btnReceivedSchedules.Name = "btnReceivedSchedules";
            this.btnReceivedSchedules.Size = new System.Drawing.Size(410, 35);
            this.btnReceivedSchedules.TabIndex = 30;
            this.btnReceivedSchedules.Text = "Received Schedules";
            this.btnReceivedSchedules.UseVisualStyleBackColor = false;
            this.btnReceivedSchedules.Click += new System.EventHandler(this.btnReceivedSchedules_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 79;
            this.label4.Text = "From Date:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(98, 4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(123, 26);
            this.dtpFrom.TabIndex = 80;
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(304, 3);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(123, 26);
            this.dtpTo.TabIndex = 82;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 81;
            this.label1.Text = "To Date:";
            // 
            // DealReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 338);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DealReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pHeader;
        private System.Windows.Forms.PictureBox picBtnIcon;
        private System.Windows.Forms.PictureBox picBtnClose;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnTrialBalance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReceivedSchedules;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
    }
}