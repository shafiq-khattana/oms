namespace WinFom.ReadyStuff.Forms
{
    partial class ScheduleCompleteForm
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
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TextBox();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tbEmptyVehicleWeight = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbFullVehicleWeight = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.tbWeightDifference = new System.Windows.Forms.TextBox();
            this.tbNetPrice = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbBrokerShare = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbBrokerSharePercentage = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbTotalPrice = new System.Windows.Forms.TextBox();
            this.tbPerTradeUnitPrice = new System.Windows.Forms.TextBox();
            this.tbTotalTradeUnits = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbPerTradeUnitQty = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbTradeUnits = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAddWeighBridge = new System.Windows.Forms.Button();
            this.cbWeighBridges = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGrossWeight = new System.Windows.Forms.TextBox();
            this.tbWeighBridgeWeight = new System.Windows.Forms.TextBox();
            this.sparklineEdit1 = new DevExpress.XtraEditors.SparklineEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.tbVehicleNo = new System.Windows.Forms.TextBox();
            this.tbReadyDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDriver = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDealItem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBroker = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbScheduleNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pMain.SuspendLayout();
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
            this.pHeader.Size = new System.Drawing.Size(1200, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1118, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Schedule Complete";
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
            this.picBtnClose.Location = new System.Drawing.Point(1160, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 443);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(1190, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 443);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 468);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(1180, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1180, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.label28);
            this.pMain.Controls.Add(this.label27);
            this.pMain.Controls.Add(this.label26);
            this.pMain.Controls.Add(this.label25);
            this.pMain.Controls.Add(this.label24);
            this.pMain.Controls.Add(this.label23);
            this.pMain.Controls.Add(this.label22);
            this.pMain.Controls.Add(this.tbRate);
            this.pMain.Controls.Add(this.dtp);
            this.pMain.Controls.Add(this.label21);
            this.pMain.Controls.Add(this.label20);
            this.pMain.Controls.Add(this.tbEmptyVehicleWeight);
            this.pMain.Controls.Add(this.label19);
            this.pMain.Controls.Add(this.tbFullVehicleWeight);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.label18);
            this.pMain.Controls.Add(this.tbWeightDifference);
            this.pMain.Controls.Add(this.tbNetPrice);
            this.pMain.Controls.Add(this.label17);
            this.pMain.Controls.Add(this.tbBrokerShare);
            this.pMain.Controls.Add(this.label16);
            this.pMain.Controls.Add(this.label15);
            this.pMain.Controls.Add(this.tbBrokerSharePercentage);
            this.pMain.Controls.Add(this.label14);
            this.pMain.Controls.Add(this.label13);
            this.pMain.Controls.Add(this.tbTotalPrice);
            this.pMain.Controls.Add(this.tbPerTradeUnitPrice);
            this.pMain.Controls.Add(this.tbTotalTradeUnits);
            this.pMain.Controls.Add(this.label12);
            this.pMain.Controls.Add(this.tbPerTradeUnitQty);
            this.pMain.Controls.Add(this.label11);
            this.pMain.Controls.Add(this.cbTradeUnits);
            this.pMain.Controls.Add(this.label10);
            this.pMain.Controls.Add(this.label9);
            this.pMain.Controls.Add(this.btnAddWeighBridge);
            this.pMain.Controls.Add(this.cbWeighBridges);
            this.pMain.Controls.Add(this.label8);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.tbGrossWeight);
            this.pMain.Controls.Add(this.tbWeighBridgeWeight);
            this.pMain.Controls.Add(this.sparklineEdit1);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.tbVehicleNo);
            this.pMain.Controls.Add(this.tbReadyDate);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.tbDriver);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.tbDealItem);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.tbBroker);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.tbScheduleNo);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(1180, 419);
            this.pMain.TabIndex = 1;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(454, 116);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 20);
            this.label28.TabIndex = 151;
            this.label28.Text = "Kg";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(454, 283);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(28, 20);
            this.label27.TabIndex = 150;
            this.label27.Text = "Kg";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(454, 249);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(28, 20);
            this.label26.TabIndex = 149;
            this.label26.Text = "Kg";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(454, 217);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(28, 20);
            this.label25.TabIndex = 148;
            this.label25.Text = "Kg";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(454, 148);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(28, 20);
            this.label24.TabIndex = 147;
            this.label24.Text = "Kg";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(1110, 116);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(28, 20);
            this.label23.TabIndex = 146;
            this.label23.Text = "Kg";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(159, 383);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 20);
            this.label22.TabIndex = 145;
            this.label22.Text = "Rate:";
            // 
            // tbRate
            // 
            this.tbRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRate.Location = new System.Drawing.Point(213, 381);
            this.tbRate.Name = "tbRate";
            this.tbRate.ReadOnly = true;
            this.tbRate.Size = new System.Drawing.Size(269, 26);
            this.tbRate.TabIndex = 144;
            // 
            // dtp
            // 
            this.dtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(213, 349);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(149, 26);
            this.dtp.TabIndex = 143;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(150, 354);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 20);
            this.label21.TabIndex = 142;
            this.label21.Text = "Dated:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(29, 149);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(178, 20);
            this.label20.TabIndex = 141;
            this.label20.Text = "Vehicel Weight (Empty):";
            // 
            // tbEmptyVehicleWeight
            // 
            this.tbEmptyVehicleWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEmptyVehicleWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEmptyVehicleWeight.Location = new System.Drawing.Point(213, 146);
            this.tbEmptyVehicleWeight.Name = "tbEmptyVehicleWeight";
            this.tbEmptyVehicleWeight.ReadOnly = true;
            this.tbEmptyVehicleWeight.Size = new System.Drawing.Size(235, 26);
            this.tbEmptyVehicleWeight.TabIndex = 140;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(53, 218);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(154, 20);
            this.label19.TabIndex = 139;
            this.label19.Text = "Vehicle Weight(Full):";
            // 
            // tbFullVehicleWeight
            // 
            this.tbFullVehicleWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFullVehicleWeight.Location = new System.Drawing.Point(213, 214);
            this.tbFullVehicleWeight.Name = "tbFullVehicleWeight";
            this.tbFullVehicleWeight.Size = new System.Drawing.Size(235, 26);
            this.tbFullVehicleWeight.TabIndex = 2;
            this.tbFullVehicleWeight.TextChanged += new System.EventHandler(this.tbFullVehicleWeight_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1042, 376);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 35);
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
            this.btnAdd.Location = new System.Drawing.Point(869, 376);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(167, 35);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Complete";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(66, 284);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(141, 20);
            this.label18.TabIndex = 137;
            this.label18.Text = "Weight Difference:";
            // 
            // tbWeightDifference
            // 
            this.tbWeightDifference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWeightDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeightDifference.Location = new System.Drawing.Point(213, 281);
            this.tbWeightDifference.Name = "tbWeightDifference";
            this.tbWeightDifference.ReadOnly = true;
            this.tbWeightDifference.Size = new System.Drawing.Size(235, 26);
            this.tbWeightDifference.TabIndex = 136;
            // 
            // tbNetPrice
            // 
            this.tbNetPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNetPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNetPrice.Location = new System.Drawing.Point(869, 302);
            this.tbNetPrice.Name = "tbNetPrice";
            this.tbNetPrice.ReadOnly = true;
            this.tbNetPrice.Size = new System.Drawing.Size(269, 26);
            this.tbNetPrice.TabIndex = 135;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(787, 304);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 20);
            this.label17.TabIndex = 134;
            this.label17.Text = "Net Price:";
            // 
            // tbBrokerShare
            // 
            this.tbBrokerShare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBrokerShare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBrokerShare.Location = new System.Drawing.Point(869, 272);
            this.tbBrokerShare.Name = "tbBrokerShare";
            this.tbBrokerShare.ReadOnly = true;
            this.tbBrokerShare.Size = new System.Drawing.Size(269, 26);
            this.tbBrokerShare.TabIndex = 133;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(757, 276);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(107, 20);
            this.label16.TabIndex = 132;
            this.label16.Text = "Broker Share:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(711, 244);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(153, 20);
            this.label15.TabIndex = 131;
            this.label15.Text = "Broker Share In (%):";
            // 
            // tbBrokerSharePercentage
            // 
            this.tbBrokerSharePercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBrokerSharePercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBrokerSharePercentage.Location = new System.Drawing.Point(869, 242);
            this.tbBrokerSharePercentage.Name = "tbBrokerSharePercentage";
            this.tbBrokerSharePercentage.Size = new System.Drawing.Size(269, 26);
            this.tbBrokerSharePercentage.TabIndex = 5;
            this.tbBrokerSharePercentage.TextChanged += new System.EventHandler(this.tbBrokerSharePercentage_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(777, 212);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 20);
            this.label14.TabIndex = 129;
            this.label14.Text = "Total Price:";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(672, 177);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(192, 26);
            this.label13.TabIndex = 128;
            this.label13.Text = "Packing Types:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbTotalPrice
            // 
            this.tbTotalPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalPrice.Location = new System.Drawing.Point(869, 210);
            this.tbTotalPrice.Name = "tbTotalPrice";
            this.tbTotalPrice.ReadOnly = true;
            this.tbTotalPrice.Size = new System.Drawing.Size(269, 26);
            this.tbTotalPrice.TabIndex = 127;
            // 
            // tbPerTradeUnitPrice
            // 
            this.tbPerTradeUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitPrice.Location = new System.Drawing.Point(869, 178);
            this.tbPerTradeUnitPrice.Name = "tbPerTradeUnitPrice";
            this.tbPerTradeUnitPrice.ReadOnly = true;
            this.tbPerTradeUnitPrice.Size = new System.Drawing.Size(269, 26);
            this.tbPerTradeUnitPrice.TabIndex = 4;
            this.tbPerTradeUnitPrice.TextChanged += new System.EventHandler(this.tbPerTradeUnitPrice_TextChanged);
            // 
            // tbTotalTradeUnits
            // 
            this.tbTotalTradeUnits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalTradeUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalTradeUnits.Location = new System.Drawing.Point(869, 146);
            this.tbTotalTradeUnits.Name = "tbTotalTradeUnits";
            this.tbTotalTradeUnits.ReadOnly = true;
            this.tbTotalTradeUnits.Size = new System.Drawing.Size(269, 26);
            this.tbTotalTradeUnits.TabIndex = 125;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(672, 145);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(192, 26);
            this.label12.TabIndex = 124;
            this.label12.Text = "Packing Types:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPerTradeUnitQty
            // 
            this.tbPerTradeUnitQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitQty.Location = new System.Drawing.Point(869, 114);
            this.tbPerTradeUnitQty.Name = "tbPerTradeUnitQty";
            this.tbPerTradeUnitQty.ReadOnly = true;
            this.tbPerTradeUnitQty.Size = new System.Drawing.Size(235, 26);
            this.tbPerTradeUnitQty.TabIndex = 123;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(672, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(192, 26);
            this.label11.TabIndex = 122;
            this.label11.Text = "Packing Types:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTradeUnits
            // 
            this.cbTradeUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTradeUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTradeUnits.FormattingEnabled = true;
            this.cbTradeUnits.Location = new System.Drawing.Point(213, 313);
            this.cbTradeUnits.Name = "cbTradeUnits";
            this.cbTradeUnits.Size = new System.Drawing.Size(269, 28);
            this.cbTradeUnits.TabIndex = 4;
            this.cbTradeUnits.SelectedIndexChanged += new System.EventHandler(this.cbTradeUnits_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(120, 317);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 20);
            this.label10.TabIndex = 119;
            this.label10.Text = "Trade Unit:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(99, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 20);
            this.label9.TabIndex = 118;
            this.label9.Text = "Weigh Bridge:";
            // 
            // btnAddWeighBridge
            // 
            this.btnAddWeighBridge.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddWeighBridge.BackgroundImage = global::WinFom.Properties.Resources.Plus_25px;
            this.btnAddWeighBridge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddWeighBridge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddWeighBridge.Location = new System.Drawing.Point(488, 179);
            this.btnAddWeighBridge.Name = "btnAddWeighBridge";
            this.btnAddWeighBridge.Size = new System.Drawing.Size(31, 31);
            this.btnAddWeighBridge.TabIndex = 1;
            this.btnAddWeighBridge.UseVisualStyleBackColor = false;
            this.btnAddWeighBridge.Click += new System.EventHandler(this.btnAddWeighBridge_Click);
            // 
            // cbWeighBridges
            // 
            this.cbWeighBridges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeighBridges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWeighBridges.FormattingEnabled = true;
            this.cbWeighBridges.Location = new System.Drawing.Point(213, 180);
            this.cbWeighBridges.Name = "cbWeighBridges";
            this.cbWeighBridges.Size = new System.Drawing.Size(269, 28);
            this.cbWeighBridges.TabIndex = 0;
            this.cbWeighBridges.SelectedIndexChanged += new System.EventHandler(this.cbWeighBridges_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(45, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 20);
            this.label8.TabIndex = 46;
            this.label8.Text = "Weigh Bridge Weight:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(73, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "Schedule Weight:";
            // 
            // tbGrossWeight
            // 
            this.tbGrossWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbGrossWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGrossWeight.Location = new System.Drawing.Point(213, 113);
            this.tbGrossWeight.Name = "tbGrossWeight";
            this.tbGrossWeight.ReadOnly = true;
            this.tbGrossWeight.Size = new System.Drawing.Size(235, 26);
            this.tbGrossWeight.TabIndex = 44;
            // 
            // tbWeighBridgeWeight
            // 
            this.tbWeighBridgeWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeighBridgeWeight.Location = new System.Drawing.Point(213, 246);
            this.tbWeighBridgeWeight.Name = "tbWeighBridgeWeight";
            this.tbWeighBridgeWeight.Size = new System.Drawing.Size(235, 26);
            this.tbWeighBridgeWeight.TabIndex = 3;
            this.tbWeighBridgeWeight.TextChanged += new System.EventHandler(this.tbWeighBridgeWeight_TextChanged);
            // 
            // sparklineEdit1
            // 
            this.sparklineEdit1.Location = new System.Drawing.Point(-7, 93);
            this.sparklineEdit1.Name = "sparklineEdit1";
            this.sparklineEdit1.Properties.View = lineSparklineView1;
            this.sparklineEdit1.Size = new System.Drawing.Size(1194, 2);
            this.sparklineEdit1.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(776, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 41;
            this.label7.Text = "Vehicle No:";
            // 
            // tbVehicleNo
            // 
            this.tbVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVehicleNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVehicleNo.Location = new System.Drawing.Point(869, 48);
            this.tbVehicleNo.Name = "tbVehicleNo";
            this.tbVehicleNo.ReadOnly = true;
            this.tbVehicleNo.Size = new System.Drawing.Size(269, 26);
            this.tbVehicleNo.TabIndex = 40;
            // 
            // tbReadyDate
            // 
            this.tbReadyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReadyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReadyDate.Location = new System.Drawing.Point(126, 48);
            this.tbReadyDate.Name = "tbReadyDate";
            this.tbReadyDate.ReadOnly = true;
            this.tbReadyDate.Size = new System.Drawing.Size(236, 26);
            this.tbReadyDate.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.TabIndex = 38;
            this.label6.Text = "Ready Date:";
            // 
            // tbDriver
            // 
            this.tbDriver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDriver.Location = new System.Drawing.Point(869, 17);
            this.tbDriver.Name = "tbDriver";
            this.tbDriver.ReadOnly = true;
            this.tbDriver.Size = new System.Drawing.Size(269, 26);
            this.tbDriver.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(811, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Driver:";
            // 
            // tbDealItem
            // 
            this.tbDealItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealItem.Location = new System.Drawing.Point(504, 46);
            this.tbDealItem.Name = "tbDealItem";
            this.tbDealItem.ReadOnly = true;
            this.tbDealItem.Size = new System.Drawing.Size(237, 26);
            this.tbDealItem.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(416, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 34;
            this.label4.Text = "Deal Item:";
            // 
            // tbBroker
            // 
            this.tbBroker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBroker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBroker.Location = new System.Drawing.Point(504, 13);
            this.tbBroker.Name = "tbBroker";
            this.tbBroker.ReadOnly = true;
            this.tbBroker.Size = new System.Drawing.Size(237, 26);
            this.tbBroker.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(438, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "Broker:";
            // 
            // tbScheduleNo
            // 
            this.tbScheduleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbScheduleNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScheduleNo.Location = new System.Drawing.Point(126, 17);
            this.tbScheduleNo.Name = "tbScheduleNo";
            this.tbScheduleNo.ReadOnly = true;
            this.tbScheduleNo.Size = new System.Drawing.Size(236, 26);
            this.tbScheduleNo.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Schedule No.";
            // 
            // ScheduleCompleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 480);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScheduleCompleteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbVehicleNo;
        private System.Windows.Forms.TextBox tbReadyDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDriver;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDealItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBroker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbScheduleNo;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SparklineEdit sparklineEdit1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbGrossWeight;
        private System.Windows.Forms.TextBox tbWeighBridgeWeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAddWeighBridge;
        private System.Windows.Forms.ComboBox cbWeighBridges;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbTotalPrice;
        private System.Windows.Forms.TextBox tbPerTradeUnitPrice;
        private System.Windows.Forms.TextBox tbTotalTradeUnits;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbPerTradeUnitQty;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbTradeUnits;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbNetPrice;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbBrokerShare;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbBrokerSharePercentage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbWeightDifference;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbEmptyVehicleWeight;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbFullVehicleWeight;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.TextBox tbRate;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
    }
}