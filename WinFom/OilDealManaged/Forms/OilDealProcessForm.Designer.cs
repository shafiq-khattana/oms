namespace WinFom.OilDealManaged.Forms
{
    partial class OilDealProcessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OilDealProcessForm));
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
            this.label16 = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbLaborChargesDescription = new System.Windows.Forms.TextBox();
            this.tbLaborCharges = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.bswLaborCharges = new Bunifu.Framework.UI.BunifuSwitch();
            this.label12 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.numUDCopies = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.bsPrinterState = new Bunifu.Framework.UI.BunifuSwitch();
            this.label18 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLoadedWeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbVehicleWeightEmpty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbVehicleNo = new System.Windows.Forms.TextBox();
            this.btnAddDriver = new System.Windows.Forms.Button();
            this.btnAddSelector = new System.Windows.Forms.Button();
            this.cbDrivers = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSelectors = new System.Windows.Forms.ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.numUDCopies)).BeginInit();
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
            this.pHeader.Size = new System.Drawing.Size(1100, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1018, 37);
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
            this.picBtnClose.Location = new System.Drawing.Point(1060, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 379);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(1090, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 379);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 404);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(1080, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1080, 12);
            this.pTop.TabIndex = 4;
            this.pTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pTop_Paint);
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.label16);
            this.pMain.Controls.Add(this.tbRate);
            this.pMain.Controls.Add(this.label14);
            this.pMain.Controls.Add(this.tbLaborChargesDescription);
            this.pMain.Controls.Add(this.tbLaborCharges);
            this.pMain.Controls.Add(this.label15);
            this.pMain.Controls.Add(this.label13);
            this.pMain.Controls.Add(this.bswLaborCharges);
            this.pMain.Controls.Add(this.label12);
            this.pMain.Controls.Add(this.dtp);
            this.pMain.Controls.Add(this.numUDCopies);
            this.pMain.Controls.Add(this.label19);
            this.pMain.Controls.Add(this.bsPrinterState);
            this.pMain.Controls.Add(this.label18);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.label11);
            this.pMain.Controls.Add(this.tbLoadedWeight);
            this.pMain.Controls.Add(this.label10);
            this.pMain.Controls.Add(this.tbVehicleWeightEmpty);
            this.pMain.Controls.Add(this.label9);
            this.pMain.Controls.Add(this.tbVehicleNo);
            this.pMain.Controls.Add(this.btnAddDriver);
            this.pMain.Controls.Add(this.btnAddSelector);
            this.pMain.Controls.Add(this.cbDrivers);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.cbSelectors);
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
            this.pMain.Size = new System.Drawing.Size(1080, 355);
            this.pMain.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(146, 285);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 20);
            this.label16.TabIndex = 87;
            this.label16.Text = "Rate:";
            // 
            // tbRate
            // 
            this.tbRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRate.Location = new System.Drawing.Point(212, 283);
            this.tbRate.Name = "tbRate";
            this.tbRate.ReadOnly = true;
            this.tbRate.Size = new System.Drawing.Size(288, 26);
            this.tbRate.TabIndex = 86;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(651, 220);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 20);
            this.label14.TabIndex = 85;
            this.label14.Text = "Description:";
            // 
            // tbLaborChargesDescription
            // 
            this.tbLaborChargesDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLaborChargesDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLaborChargesDescription.Location = new System.Drawing.Point(762, 214);
            this.tbLaborChargesDescription.Name = "tbLaborChargesDescription";
            this.tbLaborChargesDescription.Size = new System.Drawing.Size(288, 26);
            this.tbLaborChargesDescription.TabIndex = 84;
            this.tbLaborChargesDescription.Text = "N/A";
            // 
            // tbLaborCharges
            // 
            this.tbLaborCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLaborCharges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLaborCharges.Location = new System.Drawing.Point(762, 182);
            this.tbLaborCharges.Name = "tbLaborCharges";
            this.tbLaborCharges.Size = new System.Drawing.Size(200, 26);
            this.tbLaborCharges.TabIndex = 83;
            this.tbLaborCharges.Text = "0.0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(626, 184);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(118, 20);
            this.label15.TabIndex = 82;
            this.label15.Text = "Labor Charges:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(562, 153);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(182, 20);
            this.label13.TabIndex = 79;
            this.label13.Text = "Enable Labor Expenses:";
            // 
            // bswLaborCharges
            // 
            this.bswLaborCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswLaborCharges.BorderRadius = 5;
            this.bswLaborCharges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswLaborCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswLaborCharges.Location = new System.Drawing.Point(762, 156);
            this.bswLaborCharges.Margin = new System.Windows.Forms.Padding(10, 11, 10, 11);
            this.bswLaborCharges.Name = "bswLaborCharges";
            this.bswLaborCharges.Oncolor = System.Drawing.Color.Lime;
            this.bswLaborCharges.Onoffcolor = System.Drawing.Color.DarkGray;
            this.bswLaborCharges.Size = new System.Drawing.Size(51, 19);
            this.bswLaborCharges.TabIndex = 78;
            this.bswLaborCharges.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswLaborCharges.Value = true;
            this.bswLaborCharges.Click += new System.EventHandler(this.bswLaborCharges_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(687, 125);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 20);
            this.label12.TabIndex = 58;
            this.label12.Text = "Dated:";
            // 
            // dtp
            // 
            this.dtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(762, 119);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(200, 26);
            this.dtp.TabIndex = 57;
            // 
            // numUDCopies
            // 
            this.numUDCopies.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDCopies.Location = new System.Drawing.Point(989, 250);
            this.numUDCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDCopies.Name = "numUDCopies";
            this.numUDCopies.Size = new System.Drawing.Size(61, 27);
            this.numUDCopies.TabIndex = 56;
            this.numUDCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUDCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(882, 253);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 20);
            this.label19.TabIndex = 55;
            this.label19.Text = "No of copies:";
            // 
            // bsPrinterState
            // 
            this.bsPrinterState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bsPrinterState.BorderRadius = 5;
            this.bsPrinterState.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bsPrinterState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bsPrinterState.Location = new System.Drawing.Point(762, 255);
            this.bsPrinterState.Margin = new System.Windows.Forms.Padding(7);
            this.bsPrinterState.Name = "bsPrinterState";
            this.bsPrinterState.Oncolor = System.Drawing.Color.Lime;
            this.bsPrinterState.Onoffcolor = System.Drawing.Color.DarkGray;
            this.bsPrinterState.Size = new System.Drawing.Size(51, 19);
            this.bsPrinterState.TabIndex = 54;
            this.bsPrinterState.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bsPrinterState.Value = true;
            this.bsPrinterState.Click += new System.EventHandler(this.bsPrinterState_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(642, 252);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(102, 20);
            this.label18.TabIndex = 53;
            this.label18.Text = "Printer State:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(925, 296);
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
            this.btnAdd.Location = new System.Drawing.Point(762, 296);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(157, 35);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Update";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(623, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Loaded Weight:";
            // 
            // tbLoadedWeight
            // 
            this.tbLoadedWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLoadedWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLoadedWeight.Location = new System.Drawing.Point(762, 87);
            this.tbLoadedWeight.Name = "tbLoadedWeight";
            this.tbLoadedWeight.Size = new System.Drawing.Size(288, 26);
            this.tbLoadedWeight.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(566, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(178, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "Vehicle Weight (Empty):";
            // 
            // tbVehicleWeightEmpty
            // 
            this.tbVehicleWeightEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVehicleWeightEmpty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVehicleWeightEmpty.Location = new System.Drawing.Point(762, 55);
            this.tbVehicleWeightEmpty.Name = "tbVehicleWeightEmpty";
            this.tbVehicleWeightEmpty.Size = new System.Drawing.Size(288, 26);
            this.tbVehicleWeightEmpty.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(655, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Vehicle No:";
            // 
            // tbVehicleNo
            // 
            this.tbVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVehicleNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVehicleNo.Location = new System.Drawing.Point(762, 23);
            this.tbVehicleNo.Name = "tbVehicleNo";
            this.tbVehicleNo.Size = new System.Drawing.Size(288, 26);
            this.tbVehicleNo.TabIndex = 5;
            // 
            // btnAddDriver
            // 
            this.btnAddDriver.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddDriver.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDriver.BackgroundImage")));
            this.btnAddDriver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddDriver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDriver.Location = new System.Drawing.Point(469, 246);
            this.btnAddDriver.Name = "btnAddDriver";
            this.btnAddDriver.Size = new System.Drawing.Size(31, 31);
            this.btnAddDriver.TabIndex = 4;
            this.btnAddDriver.UseVisualStyleBackColor = false;
            this.btnAddDriver.Click += new System.EventHandler(this.btnAddDriver_Click);
            // 
            // btnAddSelector
            // 
            this.btnAddSelector.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddSelector.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddSelector.BackgroundImage")));
            this.btnAddSelector.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddSelector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddSelector.Location = new System.Drawing.Point(469, 211);
            this.btnAddSelector.Name = "btnAddSelector";
            this.btnAddSelector.Size = new System.Drawing.Size(31, 31);
            this.btnAddSelector.TabIndex = 2;
            this.btnAddSelector.UseVisualStyleBackColor = false;
            this.btnAddSelector.Click += new System.EventHandler(this.btnAddSelector_Click);
            // 
            // cbDrivers
            // 
            this.cbDrivers.BackColor = System.Drawing.Color.Gainsboro;
            this.cbDrivers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDrivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDrivers.FormattingEnabled = true;
            this.cbDrivers.Location = new System.Drawing.Point(212, 246);
            this.cbDrivers.Name = "cbDrivers";
            this.cbDrivers.Size = new System.Drawing.Size(251, 28);
            this.cbDrivers.TabIndex = 3;
            this.cbDrivers.SelectedIndexChanged += new System.EventHandler(this.cbDrivers_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(140, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Driver:";
            // 
            // cbSelectors
            // 
            this.cbSelectors.BackColor = System.Drawing.Color.Gainsboro;
            this.cbSelectors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectors.FormattingEnabled = true;
            this.cbSelectors.Location = new System.Drawing.Point(212, 212);
            this.cbSelectors.Name = "cbSelectors";
            this.cbSelectors.Size = new System.Drawing.Size(251, 28);
            this.cbSelectors.TabIndex = 1;
            this.cbSelectors.SelectedIndexChanged += new System.EventHandler(this.cbSelectors_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(122, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Selector:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(96, 183);
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
            this.label6.Location = new System.Drawing.Point(109, 151);
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
            this.label3.Location = new System.Drawing.Point(120, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Deal Qty:";
            // 
            // tbDealQty
            // 
            this.tbDealQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealQty.Enabled = false;
            this.tbDealQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealQty.Location = new System.Drawing.Point(212, 117);
            this.tbDealQty.Name = "tbDealQty";
            this.tbDealQty.Size = new System.Drawing.Size(288, 26);
            this.tbDealQty.TabIndex = 24;
            this.tbDealQty.TextChanged += new System.EventHandler(this.tbDealQty_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(112, 87);
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
            this.label1.Location = new System.Drawing.Point(134, 55);
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
            this.label2.Location = new System.Drawing.Point(124, 23);
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
            // OilDealProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 416);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OilDealProcessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCopies)).EndInit();
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
        private System.Windows.Forms.Button btnAddDriver;
        private System.Windows.Forms.Button btnAddSelector;
        private System.Windows.Forms.ComboBox cbDrivers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSelectors;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown numUDCopies;
        private System.Windows.Forms.Label label19;
        private Bunifu.Framework.UI.BunifuSwitch bsPrinterState;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Label label13;
        private Bunifu.Framework.UI.BunifuSwitch bswLaborCharges;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbLaborChargesDescription;
        private System.Windows.Forms.TextBox tbLaborCharges;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbRate;
    }
}