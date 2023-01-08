namespace WinFom.OilDealManaged.Forms
{
    partial class OilDealCompleteForm
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
            this.label25 = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TextBox();
            this.tbTradeUnit = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbPerTradeUnitQty = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbPerTradeUnitPrice = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbTotalTradeUnits = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbVehicleWeightLoaded = new System.Windows.Forms.TextBox();
            this.tbNetPrice = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbBrokerShare = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tbBrokerSharePercentage = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbTotalPrice = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbWeightDifference = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbWeighBridgeWeight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAddWeighBridge = new System.Windows.Forms.Button();
            this.cbWeighBridges = new System.Windows.Forms.ComboBox();
            this.tbDriver = new System.Windows.Forms.TextBox();
            this.tbSelector = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLoadedWeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbVehicleWeightEmpty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbVehicleNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbReadyDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDealDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDealQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDealItem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBroker = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDealNo = new System.Windows.Forms.TextBox();
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
            this.pHeader.Size = new System.Drawing.Size(1062, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(980, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Deal Process Form";
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
            this.picBtnClose.Location = new System.Drawing.Point(1022, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 493);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(1052, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 493);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 518);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(1042, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1042, 12);
            this.pTop.TabIndex = 4;
            this.pTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pTop_Paint);
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.label25);
            this.pMain.Controls.Add(this.tbRate);
            this.pMain.Controls.Add(this.tbTradeUnit);
            this.pMain.Controls.Add(this.label24);
            this.pMain.Controls.Add(this.tbPerTradeUnitQty);
            this.pMain.Controls.Add(this.label23);
            this.pMain.Controls.Add(this.tbPerTradeUnitPrice);
            this.pMain.Controls.Add(this.label22);
            this.pMain.Controls.Add(this.tbTotalTradeUnits);
            this.pMain.Controls.Add(this.label21);
            this.pMain.Controls.Add(this.tbVehicleWeightLoaded);
            this.pMain.Controls.Add(this.tbNetPrice);
            this.pMain.Controls.Add(this.label17);
            this.pMain.Controls.Add(this.tbBrokerShare);
            this.pMain.Controls.Add(this.label16);
            this.pMain.Controls.Add(this.label19);
            this.pMain.Controls.Add(this.tbBrokerSharePercentage);
            this.pMain.Controls.Add(this.label20);
            this.pMain.Controls.Add(this.tbTotalPrice);
            this.pMain.Controls.Add(this.label18);
            this.pMain.Controls.Add(this.tbWeightDifference);
            this.pMain.Controls.Add(this.label14);
            this.pMain.Controls.Add(this.label15);
            this.pMain.Controls.Add(this.tbWeighBridgeWeight);
            this.pMain.Controls.Add(this.label13);
            this.pMain.Controls.Add(this.btnAddWeighBridge);
            this.pMain.Controls.Add(this.cbWeighBridges);
            this.pMain.Controls.Add(this.tbDriver);
            this.pMain.Controls.Add(this.tbSelector);
            this.pMain.Controls.Add(this.label12);
            this.pMain.Controls.Add(this.dtp);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.label11);
            this.pMain.Controls.Add(this.tbLoadedWeight);
            this.pMain.Controls.Add(this.label10);
            this.pMain.Controls.Add(this.tbVehicleWeightEmpty);
            this.pMain.Controls.Add(this.label9);
            this.pMain.Controls.Add(this.tbVehicleNo);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.label8);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.tbReadyDate);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.tbDealDate);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.tbDealQty);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.tbDealItem);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.tbBroker);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Controls.Add(this.tbDealNo);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(1042, 469);
            this.pMain.TabIndex = 2;
            this.pMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pMain_Paint);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(149, 379);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(48, 20);
            this.label25.TabIndex = 163;
            this.label25.Text = "Rate:";
            // 
            // tbRate
            // 
            this.tbRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRate.Location = new System.Drawing.Point(212, 377);
            this.tbRate.Name = "tbRate";
            this.tbRate.ReadOnly = true;
            this.tbRate.Size = new System.Drawing.Size(288, 26);
            this.tbRate.TabIndex = 162;
            // 
            // tbTradeUnit
            // 
            this.tbTradeUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTradeUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTradeUnit.Location = new System.Drawing.Point(709, 153);
            this.tbTradeUnit.Name = "tbTradeUnit";
            this.tbTradeUnit.ReadOnly = true;
            this.tbTradeUnit.Size = new System.Drawing.Size(269, 26);
            this.tbTradeUnit.TabIndex = 161;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(557, 189);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(143, 20);
            this.label24.TabIndex = 160;
            this.label24.Text = "Per Trade Unit Qty:";
            // 
            // tbPerTradeUnitQty
            // 
            this.tbPerTradeUnitQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitQty.Location = new System.Drawing.Point(709, 187);
            this.tbPerTradeUnitQty.Name = "tbPerTradeUnitQty";
            this.tbPerTradeUnitQty.ReadOnly = true;
            this.tbPerTradeUnitQty.Size = new System.Drawing.Size(269, 26);
            this.tbPerTradeUnitQty.TabIndex = 159;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(546, 254);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(154, 20);
            this.label23.TabIndex = 158;
            this.label23.Text = "Per Trade Unit Price:";
            // 
            // tbPerTradeUnitPrice
            // 
            this.tbPerTradeUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitPrice.Enabled = false;
            this.tbPerTradeUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitPrice.Location = new System.Drawing.Point(709, 252);
            this.tbPerTradeUnitPrice.Name = "tbPerTradeUnitPrice";
            this.tbPerTradeUnitPrice.ReadOnly = true;
            this.tbPerTradeUnitPrice.Size = new System.Drawing.Size(269, 26);
            this.tbPerTradeUnitPrice.TabIndex = 157;
            this.tbPerTradeUnitPrice.TextChanged += new System.EventHandler(this.tbPerTradeUnitPrice_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(569, 221);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(134, 20);
            this.label22.TabIndex = 156;
            this.label22.Text = "Total Trade Units:";
            // 
            // tbTotalTradeUnits
            // 
            this.tbTotalTradeUnits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalTradeUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalTradeUnits.Location = new System.Drawing.Point(709, 219);
            this.tbTotalTradeUnits.Name = "tbTotalTradeUnits";
            this.tbTotalTradeUnits.ReadOnly = true;
            this.tbTotalTradeUnits.Size = new System.Drawing.Size(269, 26);
            this.tbTotalTradeUnits.TabIndex = 155;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(513, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(187, 20);
            this.label21.TabIndex = 154;
            this.label21.Text = "Vehicle Weight (Loaded):";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // tbVehicleWeightLoaded
            // 
            this.tbVehicleWeightLoaded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVehicleWeightLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVehicleWeightLoaded.Location = new System.Drawing.Point(709, 57);
            this.tbVehicleWeightLoaded.Name = "tbVehicleWeightLoaded";
            this.tbVehicleWeightLoaded.Size = new System.Drawing.Size(269, 26);
            this.tbVehicleWeightLoaded.TabIndex = 153;
            this.tbVehicleWeightLoaded.TextChanged += new System.EventHandler(this.tbVehicleWeightLoaded_TextChanged);
            // 
            // tbNetPrice
            // 
            this.tbNetPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNetPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNetPrice.Location = new System.Drawing.Point(709, 379);
            this.tbNetPrice.Name = "tbNetPrice";
            this.tbNetPrice.ReadOnly = true;
            this.tbNetPrice.Size = new System.Drawing.Size(269, 26);
            this.tbNetPrice.TabIndex = 152;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(627, 382);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 20);
            this.label17.TabIndex = 151;
            this.label17.Text = "Net Price:";
            // 
            // tbBrokerShare
            // 
            this.tbBrokerShare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBrokerShare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBrokerShare.Location = new System.Drawing.Point(709, 347);
            this.tbBrokerShare.Name = "tbBrokerShare";
            this.tbBrokerShare.ReadOnly = true;
            this.tbBrokerShare.Size = new System.Drawing.Size(269, 26);
            this.tbBrokerShare.TabIndex = 150;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(593, 351);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(107, 20);
            this.label16.TabIndex = 149;
            this.label16.Text = "Broker Share:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(547, 318);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(153, 20);
            this.label19.TabIndex = 148;
            this.label19.Text = "Broker Share In (%):";
            // 
            // tbBrokerSharePercentage
            // 
            this.tbBrokerSharePercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBrokerSharePercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBrokerSharePercentage.Location = new System.Drawing.Point(709, 316);
            this.tbBrokerSharePercentage.Name = "tbBrokerSharePercentage";
            this.tbBrokerSharePercentage.Size = new System.Drawing.Size(269, 26);
            this.tbBrokerSharePercentage.TabIndex = 145;
            this.tbBrokerSharePercentage.TextChanged += new System.EventHandler(this.tbBrokerSharePercentage_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(618, 286);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(87, 20);
            this.label20.TabIndex = 147;
            this.label20.Text = "Total Price:";
            // 
            // tbTotalPrice
            // 
            this.tbTotalPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalPrice.Location = new System.Drawing.Point(709, 284);
            this.tbTotalPrice.Name = "tbTotalPrice";
            this.tbTotalPrice.ReadOnly = true;
            this.tbTotalPrice.Size = new System.Drawing.Size(269, 26);
            this.tbTotalPrice.TabIndex = 146;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(559, 123);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(141, 20);
            this.label18.TabIndex = 144;
            this.label18.Text = "Weight Difference:";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // tbWeightDifference
            // 
            this.tbWeightDifference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWeightDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeightDifference.Location = new System.Drawing.Point(709, 121);
            this.tbWeightDifference.Name = "tbWeightDifference";
            this.tbWeightDifference.ReadOnly = true;
            this.tbWeightDifference.Size = new System.Drawing.Size(269, 26);
            this.tbWeightDifference.TabIndex = 143;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(613, 156);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 20);
            this.label14.TabIndex = 142;
            this.label14.Text = "Trade Unit:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(541, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(162, 20);
            this.label15.TabIndex = 141;
            this.label15.Text = "Weigh Bridge Weight:";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // tbWeighBridgeWeight
            // 
            this.tbWeighBridgeWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeighBridgeWeight.Location = new System.Drawing.Point(709, 89);
            this.tbWeighBridgeWeight.Name = "tbWeighBridgeWeight";
            this.tbWeighBridgeWeight.Size = new System.Drawing.Size(269, 26);
            this.tbWeighBridgeWeight.TabIndex = 138;
            this.tbWeighBridgeWeight.TextChanged += new System.EventHandler(this.tbWeighBridgeWeight_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(595, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 20);
            this.label13.TabIndex = 121;
            this.label13.Text = "Weigh Bridge:";
            // 
            // btnAddWeighBridge
            // 
            this.btnAddWeighBridge.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddWeighBridge.BackgroundImage = global::WinFom.Properties.Resources.Plus_25px;
            this.btnAddWeighBridge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddWeighBridge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddWeighBridge.Location = new System.Drawing.Point(984, 22);
            this.btnAddWeighBridge.Name = "btnAddWeighBridge";
            this.btnAddWeighBridge.Size = new System.Drawing.Size(31, 31);
            this.btnAddWeighBridge.TabIndex = 120;
            this.btnAddWeighBridge.UseVisualStyleBackColor = false;
            this.btnAddWeighBridge.Click += new System.EventHandler(this.btnAddWeighBridge_Click);
            // 
            // cbWeighBridges
            // 
            this.cbWeighBridges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeighBridges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWeighBridges.FormattingEnabled = true;
            this.cbWeighBridges.Location = new System.Drawing.Point(709, 23);
            this.cbWeighBridges.Name = "cbWeighBridges";
            this.cbWeighBridges.Size = new System.Drawing.Size(269, 28);
            this.cbWeighBridges.TabIndex = 119;
            this.cbWeighBridges.SelectedIndexChanged += new System.EventHandler(this.cbWeighBridges_SelectedIndexChanged);
            // 
            // tbDriver
            // 
            this.tbDriver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDriver.Location = new System.Drawing.Point(212, 248);
            this.tbDriver.Name = "tbDriver";
            this.tbDriver.ReadOnly = true;
            this.tbDriver.Size = new System.Drawing.Size(288, 26);
            this.tbDriver.TabIndex = 60;
            // 
            // tbSelector
            // 
            this.tbSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSelector.Location = new System.Drawing.Point(212, 213);
            this.tbSelector.Name = "tbSelector";
            this.tbSelector.ReadOnly = true;
            this.tbSelector.Size = new System.Drawing.Size(288, 26);
            this.tbSelector.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(140, 414);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 20);
            this.label12.TabIndex = 58;
            this.label12.Text = "Dated:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // dtp
            // 
            this.dtp.Enabled = false;
            this.dtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(212, 409);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(288, 26);
            this.dtp.TabIndex = 57;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(853, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(125, 35);
            this.btnClose.TabIndex = 9;
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
            this.btnAdd.Location = new System.Drawing.Point(709, 422);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(138, 35);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Update";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(76, 348);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Loaded Weight:";
            // 
            // tbLoadedWeight
            // 
            this.tbLoadedWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLoadedWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLoadedWeight.Location = new System.Drawing.Point(212, 345);
            this.tbLoadedWeight.Name = "tbLoadedWeight";
            this.tbLoadedWeight.ReadOnly = true;
            this.tbLoadedWeight.Size = new System.Drawing.Size(288, 26);
            this.tbLoadedWeight.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(178, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "Vehicle Weight (Empty):";
            // 
            // tbVehicleWeightEmpty
            // 
            this.tbVehicleWeightEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVehicleWeightEmpty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVehicleWeightEmpty.Location = new System.Drawing.Point(212, 313);
            this.tbVehicleWeightEmpty.Name = "tbVehicleWeightEmpty";
            this.tbVehicleWeightEmpty.ReadOnly = true;
            this.tbVehicleWeightEmpty.Size = new System.Drawing.Size(288, 26);
            this.tbVehicleWeightEmpty.TabIndex = 6;
            this.tbVehicleWeightEmpty.TextChanged += new System.EventHandler(this.tbVehicleWeightEmpty_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(108, 283);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Vehicle No:";
            // 
            // tbVehicleNo
            // 
            this.tbVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVehicleNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVehicleNo.Location = new System.Drawing.Point(212, 281);
            this.tbVehicleNo.Name = "tbVehicleNo";
            this.tbVehicleNo.ReadOnly = true;
            this.tbVehicleNo.Size = new System.Drawing.Size(288, 26);
            this.tbVehicleNo.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(143, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Driver:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(125, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Selector:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(99, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Ready Date:";
            // 
            // tbReadyDate
            // 
            this.tbReadyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReadyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReadyDate.Location = new System.Drawing.Point(212, 181);
            this.tbReadyDate.Name = "tbReadyDate";
            this.tbReadyDate.ReadOnly = true;
            this.tbReadyDate.Size = new System.Drawing.Size(288, 26);
            this.tbReadyDate.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(112, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Deal Date:";
            // 
            // tbDealDate
            // 
            this.tbDealDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealDate.Location = new System.Drawing.Point(212, 149);
            this.tbDealDate.Name = "tbDealDate";
            this.tbDealDate.ReadOnly = true;
            this.tbDealDate.Size = new System.Drawing.Size(288, 26);
            this.tbDealDate.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(123, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Deal Qty:";
            // 
            // tbDealQty
            // 
            this.tbDealQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealQty.Location = new System.Drawing.Point(212, 117);
            this.tbDealQty.Name = "tbDealQty";
            this.tbDealQty.ReadOnly = true;
            this.tbDealQty.Size = new System.Drawing.Size(288, 26);
            this.tbDealQty.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(115, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Deal Item:";
            // 
            // tbDealItem
            // 
            this.tbDealItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealItem.Location = new System.Drawing.Point(212, 85);
            this.tbDealItem.Name = "tbDealItem";
            this.tbDealItem.ReadOnly = true;
            this.tbDealItem.Size = new System.Drawing.Size(288, 26);
            this.tbDealItem.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Broker:";
            // 
            // tbBroker
            // 
            this.tbBroker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBroker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBroker.Location = new System.Drawing.Point(212, 53);
            this.tbBroker.Name = "tbBroker";
            this.tbBroker.ReadOnly = true;
            this.tbBroker.Size = new System.Drawing.Size(288, 26);
            this.tbBroker.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(127, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Deal No:";
            // 
            // tbDealNo
            // 
            this.tbDealNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealNo.Location = new System.Drawing.Point(212, 21);
            this.tbDealNo.Name = "tbDealNo";
            this.tbDealNo.ReadOnly = true;
            this.tbDealNo.Size = new System.Drawing.Size(288, 26);
            this.tbDealNo.TabIndex = 21;
            // 
            // OilDealCompleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 530);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OilDealCompleteForm";
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
        private System.Windows.Forms.TextBox tbDealNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbReadyDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDealDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDealQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDealItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbBroker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLoadedWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbVehicleWeightEmpty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbVehicleNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.TextBox tbDriver;
        private System.Windows.Forms.TextBox tbSelector;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAddWeighBridge;
        private System.Windows.Forms.ComboBox cbWeighBridges;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbWeightDifference;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbWeighBridgeWeight;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbVehicleWeightLoaded;
        private System.Windows.Forms.TextBox tbNetPrice;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbBrokerShare;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbBrokerSharePercentage;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbTotalPrice;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbPerTradeUnitPrice;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbTotalTradeUnits;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbPerTradeUnitQty;
        private System.Windows.Forms.TextBox tbTradeUnit;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbRate;
    }
}