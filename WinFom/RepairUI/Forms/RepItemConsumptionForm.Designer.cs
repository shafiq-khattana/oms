namespace WinFom.RepairUI.Forms
{
    partial class RepItemConsumptionForm
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
            this.pHeader = new System.Windows.Forms.Panel();
            this.lblHeading = new System.Windows.Forms.Label();
            this.picBtnIcon = new System.Windows.Forms.PictureBox();
            this.picBtnClose = new System.Windows.Forms.PictureBox();
            this.pLeft = new System.Windows.Forms.Panel();
            this.pRight = new System.Windows.Forms.Panel();
            this.pBottom = new System.Windows.Forms.Panel();
            this.pTop = new System.Windows.Forms.Panel();
            this.pMain = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.bswChangeAccount = new Bunifu.Framework.UI.BunifuSwitch();
            this.btnSearchAccount = new System.Windows.Forms.Button();
            this.cbCreditAccounts = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbQtyConsumption = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbConsumptionPrice = new System.Windows.Forms.TextBox();
            this.tbSKUUnitPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSKU = new System.Windows.Forms.TextBox();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pMain.SuspendLayout();
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
            // pHeader
            // 
            this.pHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.pHeader.Controls.Add(this.lblHeading);
            this.pHeader.Controls.Add(this.picBtnIcon);
            this.pHeader.Controls.Add(this.picBtnClose);
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(600, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(518, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Item Consumption Form";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.picBtnClose.Location = new System.Drawing.Point(560, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 413);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(590, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 413);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 438);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(580, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(580, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.label9);
            this.pMain.Controls.Add(this.bswChangeAccount);
            this.pMain.Controls.Add(this.btnSearchAccount);
            this.pMain.Controls.Add(this.cbCreditAccounts);
            this.pMain.Controls.Add(this.label8);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.tbRemarks);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.dtp);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.tbQtyConsumption);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.tbConsumptionPrice);
            this.pMain.Controls.Add(this.tbSKUUnitPrice);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.tbSKU);
            this.pMain.Controls.Add(this.tbItem);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(580, 389);
            this.pMain.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 312);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 20);
            this.label9.TabIndex = 92;
            this.label9.Text = "Change Account:";
            // 
            // bswChangeAccount
            // 
            this.bswChangeAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswChangeAccount.BorderRadius = 5;
            this.bswChangeAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswChangeAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswChangeAccount.Location = new System.Drawing.Point(160, 313);
            this.bswChangeAccount.Margin = new System.Windows.Forms.Padding(12, 14, 12, 14);
            this.bswChangeAccount.Name = "bswChangeAccount";
            this.bswChangeAccount.Oncolor = System.Drawing.Color.Lime;
            this.bswChangeAccount.Onoffcolor = System.Drawing.Color.Crimson;
            this.bswChangeAccount.Size = new System.Drawing.Size(51, 19);
            this.bswChangeAccount.TabIndex = 91;
            this.bswChangeAccount.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswChangeAccount.Value = true;
            this.bswChangeAccount.Click += new System.EventHandler(this.bswChangeAccount_Click);
            // 
            // btnSearchAccount
            // 
            this.btnSearchAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSearchAccount.BackgroundImage = global::WinFom.Properties.Resources.search20white;
            this.btnSearchAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchAccount.Location = new System.Drawing.Point(543, 276);
            this.btnSearchAccount.Name = "btnSearchAccount";
            this.btnSearchAccount.Size = new System.Drawing.Size(31, 31);
            this.btnSearchAccount.TabIndex = 90;
            this.btnSearchAccount.UseVisualStyleBackColor = false;
            this.btnSearchAccount.Click += new System.EventHandler(this.btnSearchAccount_Click);
            // 
            // cbCreditAccounts
            // 
            this.cbCreditAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCreditAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCreditAccounts.FormattingEnabled = true;
            this.cbCreditAccounts.Location = new System.Drawing.Point(160, 277);
            this.cbCreditAccounts.Name = "cbCreditAccounts";
            this.cbCreditAccounts.Size = new System.Drawing.Size(377, 28);
            this.cbCreditAccounts.TabIndex = 89;
            this.cbCreditAccounts.SelectedIndexChanged += new System.EventHandler(this.cbCreditAccounts_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 88;
            this.label8.Text = "Debit Account:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(441, 335);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 35);
            this.btnClose.TabIndex = 87;
            this.btnClose.Text = "Close Me!";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(284, 335);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(151, 35);
            this.btnAdd.TabIndex = 86;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbRemarks
            // 
            this.tbRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemarks.Location = new System.Drawing.Point(160, 210);
            this.tbRemarks.Multiline = true;
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(377, 61);
            this.tbRemarks.TabIndex = 85;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(77, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 84;
            this.label6.Text = "Remarks:";
            // 
            // dtp
            // 
            this.dtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(160, 178);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(135, 26);
            this.dtp.TabIndex = 83;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(97, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 82;
            this.label5.Text = "Dated:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 20);
            this.label4.TabIndex = 81;
            this.label4.Text = "Qty Consumption:";
            // 
            // tbQtyConsumption
            // 
            this.tbQtyConsumption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbQtyConsumption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQtyConsumption.Location = new System.Drawing.Point(160, 114);
            this.tbQtyConsumption.Name = "tbQtyConsumption";
            this.tbQtyConsumption.Size = new System.Drawing.Size(95, 26);
            this.tbQtyConsumption.TabIndex = 80;
            this.tbQtyConsumption.TextChanged += new System.EventHandler(this.tbQtyConsumption_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Consumption Price:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "SKU Unit Price:";
            // 
            // tbConsumptionPrice
            // 
            this.tbConsumptionPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbConsumptionPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConsumptionPrice.Location = new System.Drawing.Point(160, 146);
            this.tbConsumptionPrice.Name = "tbConsumptionPrice";
            this.tbConsumptionPrice.ReadOnly = true;
            this.tbConsumptionPrice.Size = new System.Drawing.Size(377, 26);
            this.tbConsumptionPrice.TabIndex = 16;
            // 
            // tbSKUUnitPrice
            // 
            this.tbSKUUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSKUUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSKUUnitPrice.Location = new System.Drawing.Point(160, 82);
            this.tbSKUUnitPrice.Name = "tbSKUUnitPrice";
            this.tbSKUUnitPrice.ReadOnly = true;
            this.tbSKUUnitPrice.Size = new System.Drawing.Size(377, 26);
            this.tbSKUUnitPrice.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(108, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "SKU:";
            // 
            // tbSKU
            // 
            this.tbSKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSKU.Location = new System.Drawing.Point(160, 50);
            this.tbSKU.Name = "tbSKU";
            this.tbSKU.ReadOnly = true;
            this.tbSKU.Size = new System.Drawing.Size(377, 26);
            this.tbSKU.TabIndex = 14;
            // 
            // tbItem
            // 
            this.tbItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbItem.Location = new System.Drawing.Point(160, 18);
            this.tbItem.Name = "tbItem";
            this.tbItem.ReadOnly = true;
            this.tbItem.Size = new System.Drawing.Size(377, 26);
            this.tbItem.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Item/Name:";
            // 
            // RepItemConsumptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RepItemConsumptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSKUUnitPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSKU;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbQtyConsumption;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbConsumptionPrice;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbCreditAccounts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSearchAccount;
        private System.Windows.Forms.Label label9;
        private Bunifu.Framework.UI.BunifuSwitch bswChangeAccount;
    }
}