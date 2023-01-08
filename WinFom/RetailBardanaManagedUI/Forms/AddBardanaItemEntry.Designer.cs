﻿namespace WinFom.RetailBardanaManagedUI.Forms
{ 
    partial class AddBardanaItemEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBardanaItemEntry));
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTotalPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUnitPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.cbSuppliers = new System.Windows.Forms.ComboBox();
            this.abdwa = new System.Windows.Forms.Label();
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
            this.pHeader.Size = new System.Drawing.Size(354, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(272, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Bardana item entry";
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
            this.picBtnClose.Location = new System.Drawing.Point(314, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 563);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(344, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 563);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 588);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(334, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(334, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.btnAddSupplier);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Controls.Add(this.dtp);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.tbRemarks);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.tbTotalPrice);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.tbUnitPrice);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.tbQty);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.tbItem);
            this.pMain.Controls.Add(this.cbSuppliers);
            this.pMain.Controls.Add(this.abdwa);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(334, 539);
            this.pMain.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(199, 488);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 35);
            this.btnClose.TabIndex = 36;
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
            this.btnAdd.Location = new System.Drawing.Point(36, 488);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(157, 35);
            this.btnAdd.TabIndex = 35;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddSupplier.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddSupplier.BackgroundImage")));
            this.btnAddSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddSupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddSupplier.Location = new System.Drawing.Point(264, 104);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(31, 31);
            this.btnAddSupplier.TabIndex = 34;
            this.btnAddSupplier.UseVisualStyleBackColor = false;
            this.btnAddSupplier.Click += new System.EventHandler(this.btnAddSupplier_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 413);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Dated";
            // 
            // dtp
            // 
            this.dtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(36, 436);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(133, 26);
            this.dtp.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(32, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Remarks";
            // 
            // tbRemarks
            // 
            this.tbRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemarks.Location = new System.Drawing.Point(36, 344);
            this.tbRemarks.Multiline = true;
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(259, 55);
            this.tbRemarks.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Total Price:";
            // 
            // tbTotalPrice
            // 
            this.tbTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalPrice.Location = new System.Drawing.Point(36, 288);
            this.tbTotalPrice.Name = "tbTotalPrice";
            this.tbTotalPrice.Size = new System.Drawing.Size(259, 26);
            this.tbTotalPrice.TabIndex = 18;
            this.tbTotalPrice.Leave += new System.EventHandler(this.tb_leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(32, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Unit Price";
            // 
            // tbUnitPrice
            // 
            this.tbUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUnitPrice.Location = new System.Drawing.Point(36, 228);
            this.tbUnitPrice.Name = "tbUnitPrice";
            this.tbUnitPrice.Size = new System.Drawing.Size(259, 26);
            this.tbUnitPrice.TabIndex = 16;
            this.tbUnitPrice.Leave += new System.EventHandler(this.tb_leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Qty";
            // 
            // tbQty
            // 
            this.tbQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQty.Location = new System.Drawing.Point(36, 170);
            this.tbQty.Name = "tbQty";
            this.tbQty.Size = new System.Drawing.Size(259, 26);
            this.tbQty.TabIndex = 14;
            this.tbQty.Leave += new System.EventHandler(this.tb_leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Supplier";
            // 
            // tbItem
            // 
            this.tbItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbItem.Location = new System.Drawing.Point(36, 42);
            this.tbItem.Name = "tbItem";
            this.tbItem.ReadOnly = true;
            this.tbItem.Size = new System.Drawing.Size(259, 26);
            this.tbItem.TabIndex = 12;
            // 
            // cbSuppliers
            // 
            this.cbSuppliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSuppliers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSuppliers.FormattingEnabled = true;
            this.cbSuppliers.Location = new System.Drawing.Point(36, 104);
            this.cbSuppliers.Name = "cbSuppliers";
            this.cbSuppliers.Size = new System.Drawing.Size(222, 28);
            this.cbSuppliers.TabIndex = 8;
            this.cbSuppliers.SelectedIndexChanged += new System.EventHandler(this.cbSuppliers_SelectedIndexChanged);
            // 
            // abdwa
            // 
            this.abdwa.AutoSize = true;
            this.abdwa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.abdwa.Location = new System.Drawing.Point(32, 19);
            this.abdwa.Name = "abdwa";
            this.abdwa.Size = new System.Drawing.Size(151, 20);
            this.abdwa.TabIndex = 7;
            this.abdwa.Text = "Retail Bardana Item";
            // 
            // AddBardanaItemEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 600);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddBardanaItemEntry";
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
        private System.Windows.Forms.ComboBox cbSuppliers;
        private System.Windows.Forms.Label abdwa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbUnitPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTotalPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Button btnAddSupplier;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
    }
}