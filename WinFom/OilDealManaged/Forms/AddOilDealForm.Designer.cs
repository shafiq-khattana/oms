namespace WinFom.OilDealManaged.Forms
{
    partial class AddOilDealForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOilDealForm));
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
            this.label7 = new System.Windows.Forms.Label();
            this.tbTotalPriceApprox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbTotalTradeUnits = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbPerTradeUnitPrice = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbPerTradeUnitQty = new System.Windows.Forms.TextBox();
            this.btnAddBharthiType = new System.Windows.Forms.Button();
            this.cbTradeUnits = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbReadyAfterDays = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpReadyDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDealDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddDealItem = new System.Windows.Forms.Button();
            this.btnAddBroker = new System.Windows.Forms.Button();
            this.cbDealItems = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBrokers = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDealQty = new System.Windows.Forms.TextBox();
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
            this.pHeader.Size = new System.Drawing.Size(593, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(511, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Add Oil Deal";
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
            this.picBtnClose.Location = new System.Drawing.Point(553, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 507);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(583, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 507);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 532);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(573, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(573, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.tbTotalPriceApprox);
            this.pMain.Controls.Add(this.label22);
            this.pMain.Controls.Add(this.tbTotalTradeUnits);
            this.pMain.Controls.Add(this.label23);
            this.pMain.Controls.Add(this.tbPerTradeUnitPrice);
            this.pMain.Controls.Add(this.label24);
            this.pMain.Controls.Add(this.tbPerTradeUnitQty);
            this.pMain.Controls.Add(this.btnAddBharthiType);
            this.pMain.Controls.Add(this.cbTradeUnits);
            this.pMain.Controls.Add(this.label14);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.tbReadyAfterDays);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.dtpReadyDate);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Controls.Add(this.dtpDealDate);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.btnAddDealItem);
            this.pMain.Controls.Add(this.btnAddBroker);
            this.pMain.Controls.Add(this.cbDealItems);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.cbBrokers);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.tbDealQty);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(573, 483);
            this.pMain.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(76, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Total Price (Approx):";
            // 
            // tbTotalPriceApprox
            // 
            this.tbTotalPriceApprox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalPriceApprox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalPriceApprox.Location = new System.Drawing.Point(233, 247);
            this.tbTotalPriceApprox.Name = "tbTotalPriceApprox";
            this.tbTotalPriceApprox.ReadOnly = true;
            this.tbTotalPriceApprox.Size = new System.Drawing.Size(259, 26);
            this.tbTotalPriceApprox.TabIndex = 17;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(29, 185);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(198, 20);
            this.label22.TabIndex = 19;
            this.label22.Text = "Total Trade Units (Approx):";
            // 
            // tbTotalTradeUnits
            // 
            this.tbTotalTradeUnits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalTradeUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalTradeUnits.Location = new System.Drawing.Point(233, 183);
            this.tbTotalTradeUnits.Name = "tbTotalTradeUnits";
            this.tbTotalTradeUnits.ReadOnly = true;
            this.tbTotalTradeUnits.Size = new System.Drawing.Size(259, 26);
            this.tbTotalTradeUnits.TabIndex = 20;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(73, 216);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(154, 20);
            this.label23.TabIndex = 18;
            this.label23.Text = "Per Trade Unit Price:";
            // 
            // tbPerTradeUnitPrice
            // 
            this.tbPerTradeUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitPrice.Location = new System.Drawing.Point(233, 215);
            this.tbPerTradeUnitPrice.Name = "tbPerTradeUnitPrice";
            this.tbPerTradeUnitPrice.Size = new System.Drawing.Size(259, 26);
            this.tbPerTradeUnitPrice.TabIndex = 7;
            this.tbPerTradeUnitPrice.TextChanged += new System.EventHandler(this.tbPerTradeUnitPrice_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(84, 152);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(143, 20);
            this.label24.TabIndex = 22;
            this.label24.Text = "Per Trade Unit Qty:";
            // 
            // tbPerTradeUnitQty
            // 
            this.tbPerTradeUnitQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitQty.Location = new System.Drawing.Point(233, 151);
            this.tbPerTradeUnitQty.Name = "tbPerTradeUnitQty";
            this.tbPerTradeUnitQty.ReadOnly = true;
            this.tbPerTradeUnitQty.Size = new System.Drawing.Size(259, 26);
            this.tbPerTradeUnitQty.TabIndex = 21;
            // 
            // btnAddBharthiType
            // 
            this.btnAddBharthiType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddBharthiType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddBharthiType.BackgroundImage")));
            this.btnAddBharthiType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddBharthiType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBharthiType.Location = new System.Drawing.Point(498, 116);
            this.btnAddBharthiType.Name = "btnAddBharthiType";
            this.btnAddBharthiType.Size = new System.Drawing.Size(31, 31);
            this.btnAddBharthiType.TabIndex = 6;
            this.btnAddBharthiType.UseVisualStyleBackColor = false;
            this.btnAddBharthiType.Click += new System.EventHandler(this.btnAddBharthiType_Click);
            // 
            // cbTradeUnits
            // 
            this.cbTradeUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTradeUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTradeUnits.FormattingEnabled = true;
            this.cbTradeUnits.Location = new System.Drawing.Point(233, 117);
            this.cbTradeUnits.Name = "cbTradeUnits";
            this.cbTradeUnits.Size = new System.Drawing.Size(259, 28);
            this.cbTradeUnits.TabIndex = 5;
            this.cbTradeUnits.SelectedIndexChanged += new System.EventHandler(this.cbTradeUnits_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(140, 119);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 20);
            this.label14.TabIndex = 23;
            this.label14.Text = "Trade Unit:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(396, 405);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 35);
            this.btnClose.TabIndex = 12;
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
            this.btnAdd.Location = new System.Drawing.Point(233, 405);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(157, 35);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(89, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Ready After Days:";
            // 
            // tbReadyAfterDays
            // 
            this.tbReadyAfterDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReadyAfterDays.Location = new System.Drawing.Point(233, 311);
            this.tbReadyAfterDays.Name = "tbReadyAfterDays";
            this.tbReadyAfterDays.Size = new System.Drawing.Size(259, 26);
            this.tbReadyAfterDays.TabIndex = 9;
            this.tbReadyAfterDays.TextChanged += new System.EventHandler(this.tbReadyAfterDays_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(129, 345);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Ready Date:";
            // 
            // dtpReadyDate
            // 
            this.dtpReadyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReadyDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReadyDate.Location = new System.Drawing.Point(233, 343);
            this.dtpReadyDate.Name = "dtpReadyDate";
            this.dtpReadyDate.Size = new System.Drawing.Size(259, 26);
            this.dtpReadyDate.TabIndex = 10;
            this.dtpReadyDate.ValueChanged += new System.EventHandler(this.dtpReadyDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(142, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Deal Date:";
            // 
            // dtpDealDate
            // 
            this.dtpDealDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDealDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDealDate.Location = new System.Drawing.Point(233, 279);
            this.dtpDealDate.Name = "dtpDealDate";
            this.dtpDealDate.Size = new System.Drawing.Size(259, 26);
            this.dtpDealDate.TabIndex = 8;
            this.dtpDealDate.ValueChanged += new System.EventHandler(this.dtpDealDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(153, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "Deal Qty:";
            // 
            // btnAddDealItem
            // 
            this.btnAddDealItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddDealItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDealItem.BackgroundImage")));
            this.btnAddDealItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddDealItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDealItem.Location = new System.Drawing.Point(498, 51);
            this.btnAddDealItem.Name = "btnAddDealItem";
            this.btnAddDealItem.Size = new System.Drawing.Size(31, 31);
            this.btnAddDealItem.TabIndex = 3;
            this.btnAddDealItem.UseVisualStyleBackColor = false;
            this.btnAddDealItem.Click += new System.EventHandler(this.btnAddDealItem_Click);
            // 
            // btnAddBroker
            // 
            this.btnAddBroker.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddBroker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddBroker.BackgroundImage")));
            this.btnAddBroker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddBroker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBroker.Location = new System.Drawing.Point(498, 16);
            this.btnAddBroker.Name = "btnAddBroker";
            this.btnAddBroker.Size = new System.Drawing.Size(31, 31);
            this.btnAddBroker.TabIndex = 1;
            this.btnAddBroker.UseVisualStyleBackColor = false;
            this.btnAddBroker.Click += new System.EventHandler(this.btnAddBroker_Click);
            // 
            // cbDealItems
            // 
            this.cbDealItems.BackColor = System.Drawing.Color.Gainsboro;
            this.cbDealItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDealItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDealItems.FormattingEnabled = true;
            this.cbDealItems.Location = new System.Drawing.Point(233, 51);
            this.cbDealItems.Name = "cbDealItems";
            this.cbDealItems.Size = new System.Drawing.Size(259, 28);
            this.cbDealItems.TabIndex = 2;
            this.cbDealItems.SelectedIndexChanged += new System.EventHandler(this.cbDealItems_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(145, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 25;
            this.label5.Text = "Deal Item:";
            // 
            // cbBrokers
            // 
            this.cbBrokers.BackColor = System.Drawing.Color.Gainsboro;
            this.cbBrokers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrokers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBrokers.FormattingEnabled = true;
            this.cbBrokers.Location = new System.Drawing.Point(233, 17);
            this.cbBrokers.Name = "cbBrokers";
            this.cbBrokers.Size = new System.Drawing.Size(259, 28);
            this.cbBrokers.TabIndex = 0;
            this.cbBrokers.SelectedIndexChanged += new System.EventHandler(this.cbBrokers_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(167, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "Broker:";
            // 
            // tbDealQty
            // 
            this.tbDealQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealQty.Location = new System.Drawing.Point(233, 85);
            this.tbDealQty.Name = "tbDealQty";
            this.tbDealQty.Size = new System.Drawing.Size(259, 26);
            this.tbDealQty.TabIndex = 4;
            // 
            // AddOilDealForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 544);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddOilDealForm";
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDealDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddDealItem;
        private System.Windows.Forms.Button btnAddBroker;
        private System.Windows.Forms.ComboBox cbDealItems;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbBrokers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDealQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbReadyAfterDays;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpReadyDate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbPerTradeUnitQty;
        private System.Windows.Forms.Button btnAddBharthiType;
        private System.Windows.Forms.ComboBox cbTradeUnits;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbPerTradeUnitPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbTotalPriceApprox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbTotalTradeUnits;
    }
}