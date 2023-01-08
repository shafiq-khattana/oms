namespace WinFom.Retail.Forms
{
    partial class OrderProcessForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.bswCustomer = new Bunifu.Framework.UI.BunifuSwitch();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRemainingAmount = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numCopies = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.bswPrinterState = new Bunifu.Framework.UI.BunifuSwitch();
            this.label1 = new System.Windows.Forms.Label();
            this.panelWalkIn = new System.Windows.Forms.Panel();
            this.walkInCellTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.walkInAddressTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.walkInNameTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ordTypeLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbChangeGiven = new System.Windows.Forms.TextBox();
            this.tbAmountGiven = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPaymentToReceive = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.partialRadioButton = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.creditRadioBtn = new System.Windows.Forms.RadioButton();
            this.cashRadioBtn = new System.Windows.Forms.RadioButton();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCopies)).BeginInit();
            this.panelWalkIn.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.pHeader.Size = new System.Drawing.Size(379, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(297, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Order Process Form";
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
            this.picBtnClose.Location = new System.Drawing.Point(339, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 683);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(369, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 683);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 708);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(359, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(359, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.panel2);
            this.pMain.Controls.Add(this.label10);
            this.pMain.Controls.Add(this.tbRemainingAmount);
            this.pMain.Controls.Add(this.panel1);
            this.pMain.Controls.Add(this.panelWalkIn);
            this.pMain.Controls.Add(this.ordTypeLabel);
            this.pMain.Controls.Add(this.button1);
            this.pMain.Controls.Add(this.btnOk);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Controls.Add(this.tbChangeGiven);
            this.pMain.Controls.Add(this.tbAmountGiven);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.tbPaymentToReceive);
            this.pMain.Controls.Add(this.groupBox1);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(359, 659);
            this.pMain.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.bswCustomer);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Location = new System.Drawing.Point(14, 555);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 40);
            this.panel2.TabIndex = 35;
            // 
            // bswCustomer
            // 
            this.bswCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswCustomer.BorderRadius = 5;
            this.bswCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswCustomer.Location = new System.Drawing.Point(273, 11);
            this.bswCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.bswCustomer.Name = "bswCustomer";
            this.bswCustomer.Oncolor = System.Drawing.Color.Lime;
            this.bswCustomer.Onoffcolor = System.Drawing.Color.DarkGray;
            this.bswCustomer.Size = new System.Drawing.Size(51, 19);
            this.bswCustomer.TabIndex = 28;
            this.bswCustomer.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswCustomer.Value = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(167, 21);
            this.label12.TabIndex = 29;
            this.label12.Text = "Print Customer Copy";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 440);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 21);
            this.label10.TabIndex = 34;
            this.label10.Text = "Remaining Amount:";
            // 
            // tbRemainingAmount
            // 
            this.tbRemainingAmount.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tbRemainingAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemainingAmount.Location = new System.Drawing.Point(14, 464);
            this.tbRemainingAmount.Name = "tbRemainingAmount";
            this.tbRemainingAmount.ReadOnly = true;
            this.tbRemainingAmount.Size = new System.Drawing.Size(330, 35);
            this.tbRemainingAmount.TabIndex = 33;
            this.tbRemainingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.numCopies);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.bswPrinterState);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(14, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 40);
            this.panel1.TabIndex = 32;
            // 
            // numCopies
            // 
            this.numCopies.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCopies.Location = new System.Drawing.Point(151, 8);
            this.numCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCopies.Name = "numCopies";
            this.numCopies.Size = new System.Drawing.Size(56, 27);
            this.numCopies.TabIndex = 31;
            this.numCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(218, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 21);
            this.label8.TabIndex = 30;
            this.label8.Text = "No of copies";
            // 
            // bswPrinterState
            // 
            this.bswPrinterState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswPrinterState.BorderRadius = 5;
            this.bswPrinterState.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswPrinterState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswPrinterState.Location = new System.Drawing.Point(84, 13);
            this.bswPrinterState.Margin = new System.Windows.Forms.Padding(5);
            this.bswPrinterState.Name = "bswPrinterState";
            this.bswPrinterState.Oncolor = System.Drawing.Color.Lime;
            this.bswPrinterState.Onoffcolor = System.Drawing.Color.DarkGray;
            this.bswPrinterState.Size = new System.Drawing.Size(51, 19);
            this.bswPrinterState.TabIndex = 28;
            this.bswPrinterState.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswPrinterState.Value = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 21);
            this.label1.TabIndex = 29;
            this.label1.Text = "Printer:";
            // 
            // panelWalkIn
            // 
            this.panelWalkIn.BackColor = System.Drawing.Color.Gainsboro;
            this.panelWalkIn.Controls.Add(this.walkInCellTB);
            this.panelWalkIn.Controls.Add(this.label6);
            this.panelWalkIn.Controls.Add(this.walkInAddressTB);
            this.panelWalkIn.Controls.Add(this.label7);
            this.panelWalkIn.Controls.Add(this.walkInNameTB);
            this.panelWalkIn.Controls.Add(this.label5);
            this.panelWalkIn.Location = new System.Drawing.Point(17, 121);
            this.panelWalkIn.Name = "panelWalkIn";
            this.panelWalkIn.Size = new System.Drawing.Size(327, 121);
            this.panelWalkIn.TabIndex = 27;
            // 
            // walkInCellTB
            // 
            this.walkInCellTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.walkInCellTB.Location = new System.Drawing.Point(178, 23);
            this.walkInCellTB.Name = "walkInCellTB";
            this.walkInCellTB.Size = new System.Drawing.Size(145, 29);
            this.walkInCellTB.TabIndex = 1;
            this.walkInCellTB.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(175, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cell:";
            // 
            // walkInAddressTB
            // 
            this.walkInAddressTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.walkInAddressTB.Location = new System.Drawing.Point(4, 77);
            this.walkInAddressTB.Name = "walkInAddressTB";
            this.walkInAddressTB.Size = new System.Drawing.Size(319, 29);
            this.walkInAddressTB.TabIndex = 2;
            this.walkInAddressTB.Text = "N/A";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Address:";
            // 
            // walkInNameTB
            // 
            this.walkInNameTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.walkInNameTB.Location = new System.Drawing.Point(4, 23);
            this.walkInNameTB.Name = "walkInNameTB";
            this.walkInNameTB.Size = new System.Drawing.Size(168, 29);
            this.walkInNameTB.TabIndex = 0;
            this.walkInNameTB.Text = "N/A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Name:";
            // 
            // ordTypeLabel
            // 
            this.ordTypeLabel.BackColor = System.Drawing.Color.Gray;
            this.ordTypeLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ordTypeLabel.ForeColor = System.Drawing.Color.White;
            this.ordTypeLabel.Location = new System.Drawing.Point(17, 87);
            this.ordTypeLabel.Name = "ordTypeLabel";
            this.ordTypeLabel.Size = new System.Drawing.Size(327, 30);
            this.ordTypeLabel.TabIndex = 26;
            this.ordTypeLabel.Text = "Customer:";
            this.ordTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.AutoEllipsis = true;
            this.button1.BackColor = System.Drawing.Color.DarkRed;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderSize = 4;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(184, 607);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 42);
            this.button1.TabIndex = 18;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoEllipsis = true;
            this.btnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOk.FlatAppearance.BorderSize = 4;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(14, 607);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(160, 42);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 376);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 21);
            this.label3.TabIndex = 20;
            this.label3.Text = "Change";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 21);
            this.label2.TabIndex = 21;
            this.label2.Text = "Amount Given:";
            // 
            // tbChangeGiven
            // 
            this.tbChangeGiven.BackColor = System.Drawing.Color.LightPink;
            this.tbChangeGiven.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbChangeGiven.Location = new System.Drawing.Point(14, 401);
            this.tbChangeGiven.Name = "tbChangeGiven";
            this.tbChangeGiven.ReadOnly = true;
            this.tbChangeGiven.Size = new System.Drawing.Size(330, 35);
            this.tbChangeGiven.TabIndex = 19;
            this.tbChangeGiven.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbAmountGiven
            // 
            this.tbAmountGiven.BackColor = System.Drawing.Color.Honeydew;
            this.tbAmountGiven.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAmountGiven.Location = new System.Drawing.Point(14, 338);
            this.tbAmountGiven.Name = "tbAmountGiven";
            this.tbAmountGiven.Size = new System.Drawing.Size(330, 35);
            this.tbAmountGiven.TabIndex = 15;
            this.tbAmountGiven.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbAmountGiven.TextChanged += new System.EventHandler(this.tbAmountGiven_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 21);
            this.label4.TabIndex = 22;
            this.label4.Text = "Payment to receive:";
            // 
            // tbPaymentToReceive
            // 
            this.tbPaymentToReceive.BackColor = System.Drawing.Color.MediumTurquoise;
            this.tbPaymentToReceive.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPaymentToReceive.Location = new System.Drawing.Point(14, 274);
            this.tbPaymentToReceive.Name = "tbPaymentToReceive";
            this.tbPaymentToReceive.ReadOnly = true;
            this.tbPaymentToReceive.Size = new System.Drawing.Size(330, 35);
            this.tbPaymentToReceive.TabIndex = 25;
            this.tbPaymentToReceive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.partialRadioButton);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.creditRadioBtn);
            this.groupBox1.Controls.Add(this.cashRadioBtn);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 68);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order Mode";
            // 
            // partialRadioButton
            // 
            this.partialRadioButton.AutoSize = true;
            this.partialRadioButton.Location = new System.Drawing.Point(239, 37);
            this.partialRadioButton.Name = "partialRadioButton";
            this.partialRadioButton.Size = new System.Drawing.Size(73, 25);
            this.partialRadioButton.TabIndex = 3;
            this.partialRadioButton.Text = "Partial";
            this.partialRadioButton.UseVisualStyleBackColor = true;
            this.partialRadioButton.CheckedChanged += new System.EventHandler(this.partialRadioButton_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(2, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 24);
            this.label9.TabIndex = 2;
            // 
            // creditRadioBtn
            // 
            this.creditRadioBtn.AutoSize = true;
            this.creditRadioBtn.Location = new System.Drawing.Point(138, 37);
            this.creditRadioBtn.Name = "creditRadioBtn";
            this.creditRadioBtn.Size = new System.Drawing.Size(73, 25);
            this.creditRadioBtn.TabIndex = 1;
            this.creditRadioBtn.Text = "Credit";
            this.creditRadioBtn.UseVisualStyleBackColor = true;
            this.creditRadioBtn.CheckedChanged += new System.EventHandler(this.creditRadioBtn_CheckedChanged);
            // 
            // cashRadioBtn
            // 
            this.cashRadioBtn.AutoSize = true;
            this.cashRadioBtn.Checked = true;
            this.cashRadioBtn.Location = new System.Drawing.Point(58, 37);
            this.cashRadioBtn.Name = "cashRadioBtn";
            this.cashRadioBtn.Size = new System.Drawing.Size(62, 25);
            this.cashRadioBtn.TabIndex = 0;
            this.cashRadioBtn.TabStop = true;
            this.cashRadioBtn.Text = "Cash";
            this.cashRadioBtn.UseVisualStyleBackColor = true;
            this.cashRadioBtn.CheckedChanged += new System.EventHandler(this.cashRadioBtn_CheckedChanged);
            // 
            // OrderProcessForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(379, 720);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "OrderProcessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OrderProcessForm_KeyDown);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCopies)).EndInit();
            this.panelWalkIn.ResumeLayout(false);
            this.panelWalkIn.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Panel panelWalkIn;
        private System.Windows.Forms.TextBox walkInCellTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox walkInAddressTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox walkInNameTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ordTypeLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbChangeGiven;
        private System.Windows.Forms.TextBox tbAmountGiven;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPaymentToReceive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton creditRadioBtn;
        private System.Windows.Forms.RadioButton cashRadioBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numCopies;
        private System.Windows.Forms.Label label8;
        private Bunifu.Framework.UI.BunifuSwitch bswPrinterState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRemainingAmount;
        private System.Windows.Forms.RadioButton partialRadioButton;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuSwitch bswCustomer;
        private System.Windows.Forms.Label label12;
    }
}