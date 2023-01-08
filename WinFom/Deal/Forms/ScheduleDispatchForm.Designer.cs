namespace WinFom.Deal.Forms
{
    partial class ScheduleDispatchForm
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
            DevExpress.Sparkline.LineSparklineView lineSparklineView1 = new DevExpress.Sparkline.LineSparklineView();
            DevExpress.Sparkline.LineSparklineView lineSparklineView2 = new DevExpress.Sparkline.LineSparklineView();
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
            this.tbPerTradeUnitPrice = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbFareAmount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAddVehicle = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cbVehicles = new System.Windows.Forms.ComboBox();
            this.btnAddDriver = new System.Windows.Forms.Button();
            this.cbDrivers = new System.Windows.Forms.ComboBox();
            this.btnAddGoodsCompany = new System.Windows.Forms.Button();
            this.cbGoodsCompanies = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbDealItem = new System.Windows.Forms.TextBox();
            this.sparklineEdit2 = new DevExpress.XtraEditors.SparklineEdit();
            this.sparklineEdit1 = new DevExpress.XtraEditors.SparklineEdit();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.lblPackings = new System.Windows.Forms.Label();
            this.lblTradeUnit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSchedulePrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbScheduleTotalQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSchedulePackings = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbScheduledTradeUnits = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDealDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBroker = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDealNo = new System.Windows.Forms.TextBox();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sparklineEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sparklineEdit1.Properties)).BeginInit();
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
            this.pHeader.Size = new System.Drawing.Size(571, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(489, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Dispatch Schedule Form";
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
            this.picBtnClose.Location = new System.Drawing.Point(531, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 658);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(561, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 658);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 683);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(551, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(551, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.tbPerTradeUnitPrice);
            this.pMain.Controls.Add(this.label12);
            this.pMain.Controls.Add(this.tbFareAmount);
            this.pMain.Controls.Add(this.label11);
            this.pMain.Controls.Add(this.tbRemarks);
            this.pMain.Controls.Add(this.label10);
            this.pMain.Controls.Add(this.dtp);
            this.pMain.Controls.Add(this.label9);
            this.pMain.Controls.Add(this.btnAddVehicle);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.label19);
            this.pMain.Controls.Add(this.label18);
            this.pMain.Controls.Add(this.cbVehicles);
            this.pMain.Controls.Add(this.btnAddDriver);
            this.pMain.Controls.Add(this.cbDrivers);
            this.pMain.Controls.Add(this.btnAddGoodsCompany);
            this.pMain.Controls.Add(this.cbGoodsCompanies);
            this.pMain.Controls.Add(this.label14);
            this.pMain.Controls.Add(this.label13);
            this.pMain.Controls.Add(this.tbDealItem);
            this.pMain.Controls.Add(this.sparklineEdit2);
            this.pMain.Controls.Add(this.sparklineEdit1);
            this.pMain.Controls.Add(this.lblTotalQty);
            this.pMain.Controls.Add(this.lblPackings);
            this.pMain.Controls.Add(this.lblTradeUnit);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.tbSchedulePrice);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.tbScheduleTotalQty);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.tbSchedulePackings);
            this.pMain.Controls.Add(this.label8);
            this.pMain.Controls.Add(this.tbScheduledTradeUnits);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.tbDealDate);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.tbBroker);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.tbCompany);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Controls.Add(this.tbDealNo);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(551, 634);
            this.pMain.TabIndex = 5;
            // 
            // tbPerTradeUnitPrice
            // 
            this.tbPerTradeUnitPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbPerTradeUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitPrice.Location = new System.Drawing.Point(215, 313);
            this.tbPerTradeUnitPrice.Name = "tbPerTradeUnitPrice";
            this.tbPerTradeUnitPrice.ReadOnly = true;
            this.tbPerTradeUnitPrice.Size = new System.Drawing.Size(195, 26);
            this.tbPerTradeUnitPrice.TabIndex = 51;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(53, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 20);
            this.label12.TabIndex = 50;
            this.label12.Text = "Per trade unit price:";
            // 
            // tbFareAmount
            // 
            this.tbFareAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFareAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFareAmount.Location = new System.Drawing.Point(215, 496);
            this.tbFareAmount.Name = "tbFareAmount";
            this.tbFareAmount.Size = new System.Drawing.Size(269, 26);
            this.tbFareAmount.TabIndex = 49;
            this.tbFareAmount.TextChanged += new System.EventHandler(this.tbFareAmount_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(93, 498);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 20);
            this.label11.TabIndex = 48;
            this.label11.Text = "Fare Amount:";
            // 
            // tbRemarks
            // 
            this.tbRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemarks.Location = new System.Drawing.Point(215, 528);
            this.tbRemarks.Multiline = true;
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(306, 59);
            this.tbRemarks.TabIndex = 47;
            this.tbRemarks.Text = "N/A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(55, 518);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 20);
            this.label10.TabIndex = 46;
            this.label10.Text = "Dispatch Remarks:";
            // 
            // dtp
            // 
            this.dtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(215, 465);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(177, 26);
            this.dtp.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(84, 467);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 20);
            this.label9.TabIndex = 44;
            this.label9.Text = "Dispatch Date:";
            // 
            // btnAddVehicle
            // 
            this.btnAddVehicle.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddVehicle.BackgroundImage = global::WinFom.Properties.Resources.Plus_25px;
            this.btnAddVehicle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddVehicle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddVehicle.Location = new System.Drawing.Point(490, 430);
            this.btnAddVehicle.Name = "btnAddVehicle";
            this.btnAddVehicle.Size = new System.Drawing.Size(31, 31);
            this.btnAddVehicle.TabIndex = 43;
            this.btnAddVehicle.UseVisualStyleBackColor = false;
            this.btnAddVehicle.Click += new System.EventHandler(this.btnAddVehicle_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(398, 594);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(123, 35);
            this.btnClose.TabIndex = 7;
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
            this.btnAdd.Location = new System.Drawing.Point(215, 593);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(177, 35);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Update";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(134, 434);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 20);
            this.label19.TabIndex = 9;
            this.label19.Text = "Vehicle:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(145, 400);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 20);
            this.label18.TabIndex = 10;
            this.label18.Text = "Driver:";
            // 
            // cbVehicles
            // 
            this.cbVehicles.BackColor = System.Drawing.Color.Gainsboro;
            this.cbVehicles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVehicles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVehicles.FormattingEnabled = true;
            this.cbVehicles.Location = new System.Drawing.Point(215, 431);
            this.cbVehicles.Name = "cbVehicles";
            this.cbVehicles.Size = new System.Drawing.Size(269, 28);
            this.cbVehicles.TabIndex = 4;
            this.cbVehicles.SelectedIndexChanged += new System.EventHandler(this.cbVehicles_SelectedIndexChanged);
            // 
            // btnAddDriver
            // 
            this.btnAddDriver.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddDriver.BackgroundImage = global::WinFom.Properties.Resources.Plus_25px;
            this.btnAddDriver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddDriver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDriver.Location = new System.Drawing.Point(490, 397);
            this.btnAddDriver.Name = "btnAddDriver";
            this.btnAddDriver.Size = new System.Drawing.Size(31, 31);
            this.btnAddDriver.TabIndex = 3;
            this.btnAddDriver.UseVisualStyleBackColor = false;
            this.btnAddDriver.Click += new System.EventHandler(this.btnAddDriver_Click);
            // 
            // cbDrivers
            // 
            this.cbDrivers.BackColor = System.Drawing.Color.Gainsboro;
            this.cbDrivers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDrivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDrivers.FormattingEnabled = true;
            this.cbDrivers.Location = new System.Drawing.Point(215, 397);
            this.cbDrivers.Name = "cbDrivers";
            this.cbDrivers.Size = new System.Drawing.Size(269, 28);
            this.cbDrivers.TabIndex = 2;
            this.cbDrivers.SelectedIndexChanged += new System.EventHandler(this.cbDrivers_SelectedIndexChanged);
            // 
            // btnAddGoodsCompany
            // 
            this.btnAddGoodsCompany.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddGoodsCompany.BackgroundImage = global::WinFom.Properties.Resources.Plus_25px;
            this.btnAddGoodsCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddGoodsCompany.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddGoodsCompany.Location = new System.Drawing.Point(490, 363);
            this.btnAddGoodsCompany.Name = "btnAddGoodsCompany";
            this.btnAddGoodsCompany.Size = new System.Drawing.Size(31, 31);
            this.btnAddGoodsCompany.TabIndex = 1;
            this.btnAddGoodsCompany.UseVisualStyleBackColor = false;
            this.btnAddGoodsCompany.Click += new System.EventHandler(this.btnAddGoodsCompany_Click);
            // 
            // cbGoodsCompanies
            // 
            this.cbGoodsCompanies.BackColor = System.Drawing.Color.Gainsboro;
            this.cbGoodsCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGoodsCompanies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGoodsCompanies.FormattingEnabled = true;
            this.cbGoodsCompanies.Location = new System.Drawing.Point(215, 363);
            this.cbGoodsCompanies.Name = "cbGoodsCompanies";
            this.cbGoodsCompanies.Size = new System.Drawing.Size(269, 28);
            this.cbGoodsCompanies.TabIndex = 0;
            this.cbGoodsCompanies.SelectedIndexChanged += new System.EventHandler(this.cbGoodsCompanies_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(67, 367);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 20);
            this.label14.TabIndex = 11;
            this.label14.Text = "Goods Company:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(117, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 20);
            this.label13.TabIndex = 42;
            this.label13.Text = "Deal Item:";
            // 
            // tbDealItem
            // 
            this.tbDealItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealItem.Location = new System.Drawing.Point(215, 41);
            this.tbDealItem.Name = "tbDealItem";
            this.tbDealItem.ReadOnly = true;
            this.tbDealItem.Size = new System.Drawing.Size(306, 26);
            this.tbDealItem.TabIndex = 41;
            // 
            // sparklineEdit2
            // 
            this.sparklineEdit2.Location = new System.Drawing.Point(0, 347);
            this.sparklineEdit2.Name = "sparklineEdit2";
            this.sparklineEdit2.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sparklineEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.sparklineEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.sparklineEdit2.Properties.View = lineSparklineView1;
            this.sparklineEdit2.Size = new System.Drawing.Size(558, 6);
            this.sparklineEdit2.TabIndex = 12;
            // 
            // sparklineEdit1
            // 
            this.sparklineEdit1.Location = new System.Drawing.Point(0, 173);
            this.sparklineEdit1.Name = "sparklineEdit1";
            this.sparklineEdit1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sparklineEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.sparklineEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.sparklineEdit1.Properties.View = lineSparklineView2;
            this.sparklineEdit1.Size = new System.Drawing.Size(551, 6);
            this.sparklineEdit1.TabIndex = 39;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.Location = new System.Drawing.Point(416, 251);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(105, 20);
            this.lblTotalQty.TabIndex = 38;
            this.lblTotalQty.Text = "Deal No:";
            this.lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPackings
            // 
            this.lblPackings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackings.Location = new System.Drawing.Point(416, 217);
            this.lblPackings.Name = "lblPackings";
            this.lblPackings.Size = new System.Drawing.Size(105, 20);
            this.lblPackings.TabIndex = 37;
            this.lblPackings.Text = "Deal No:";
            this.lblPackings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTradeUnit
            // 
            this.lblTradeUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTradeUnit.Location = new System.Drawing.Point(416, 187);
            this.lblTradeUnit.Name = "lblTradeUnit";
            this.lblTradeUnit.Size = new System.Drawing.Size(105, 20);
            this.lblTradeUnit.TabIndex = 36;
            this.lblTradeUnit.Text = "Deal No:";
            this.lblTradeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(80, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 20);
            this.label5.TabIndex = 27;
            this.label5.Text = "Schedule Price:";
            // 
            // tbSchedulePrice
            // 
            this.tbSchedulePrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSchedulePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSchedulePrice.Location = new System.Drawing.Point(215, 281);
            this.tbSchedulePrice.Name = "tbSchedulePrice";
            this.tbSchedulePrice.ReadOnly = true;
            this.tbSchedulePrice.Size = new System.Drawing.Size(306, 26);
            this.tbSchedulePrice.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(52, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Schedule Total Qty:";
            // 
            // tbScheduleTotalQty
            // 
            this.tbScheduleTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbScheduleTotalQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScheduleTotalQty.Location = new System.Drawing.Point(215, 249);
            this.tbScheduleTotalQty.Name = "tbScheduleTotalQty";
            this.tbScheduleTotalQty.ReadOnly = true;
            this.tbScheduleTotalQty.Size = new System.Drawing.Size(195, 26);
            this.tbScheduleTotalQty.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(51, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "Schedule Packings:";
            // 
            // tbSchedulePackings
            // 
            this.tbSchedulePackings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSchedulePackings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSchedulePackings.Location = new System.Drawing.Point(215, 217);
            this.tbSchedulePackings.Name = "tbSchedulePackings";
            this.tbSchedulePackings.ReadOnly = true;
            this.tbSchedulePackings.Size = new System.Drawing.Size(195, 26);
            this.tbSchedulePackings.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(33, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Schedule Trade Units:";
            // 
            // tbScheduledTradeUnits
            // 
            this.tbScheduledTradeUnits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbScheduledTradeUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScheduledTradeUnits.Location = new System.Drawing.Point(215, 185);
            this.tbScheduledTradeUnits.Name = "tbScheduledTradeUnits";
            this.tbScheduledTradeUnits.ReadOnly = true;
            this.tbScheduledTradeUnits.Size = new System.Drawing.Size(195, 26);
            this.tbScheduledTradeUnits.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(114, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Deal Date:";
            // 
            // tbDealDate
            // 
            this.tbDealDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealDate.Location = new System.Drawing.Point(215, 105);
            this.tbDealDate.Name = "tbDealDate";
            this.tbDealDate.ReadOnly = true;
            this.tbDealDate.Size = new System.Drawing.Size(306, 26);
            this.tbDealDate.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(139, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Broker:";
            // 
            // tbBroker
            // 
            this.tbBroker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBroker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBroker.Location = new System.Drawing.Point(215, 73);
            this.tbBroker.Name = "tbBroker";
            this.tbBroker.ReadOnly = true;
            this.tbBroker.Size = new System.Drawing.Size(306, 26);
            this.tbBroker.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Company:";
            // 
            // tbCompany
            // 
            this.tbCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCompany.Location = new System.Drawing.Point(215, 137);
            this.tbCompany.Name = "tbCompany";
            this.tbCompany.ReadOnly = true;
            this.tbCompany.Size = new System.Drawing.Size(306, 26);
            this.tbCompany.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Deal No:";
            // 
            // tbDealNo
            // 
            this.tbDealNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealNo.Location = new System.Drawing.Point(215, 9);
            this.tbDealNo.Name = "tbDealNo";
            this.tbDealNo.ReadOnly = true;
            this.tbDealNo.Size = new System.Drawing.Size(306, 26);
            this.tbDealNo.TabIndex = 12;
            // 
            // ScheduleDispatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 695);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScheduleDispatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DealSample";
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sparklineEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sparklineEdit1.Properties)).EndInit();
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
        private System.Windows.Forms.TextBox tbDealNo;
        private DevExpress.XtraEditors.SparklineEdit sparklineEdit2;
        private DevExpress.XtraEditors.SparklineEdit sparklineEdit1;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label lblPackings;
        private System.Windows.Forms.Label lblTradeUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSchedulePrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbScheduleTotalQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSchedulePackings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbScheduledTradeUnits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDealDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBroker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbDealItem;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbVehicles;
        private System.Windows.Forms.Button btnAddDriver;
        private System.Windows.Forms.ComboBox cbDrivers;
        private System.Windows.Forms.Button btnAddGoodsCompany;
        private System.Windows.Forms.ComboBox cbGoodsCompanies;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddVehicle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbFareAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbPerTradeUnitPrice;
        private System.Windows.Forms.Label label12;
    }
}