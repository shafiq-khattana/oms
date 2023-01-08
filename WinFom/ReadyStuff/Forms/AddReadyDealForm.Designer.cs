namespace WinFom.ReadyStuff.Forms
{
    partial class AddReadyDealForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddReadyDealForm));
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
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPerTradeUnitPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbPerTradeUnitQty = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.btnAddBharthiType = new System.Windows.Forms.Button();
            this.cbTradeUnits = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRemaining = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehiclesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readyDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDealDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddDealItem = new System.Windows.Forms.Button();
            this.btnAddBroker = new System.Windows.Forms.Button();
            this.cbDealItems = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBrokers = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVehiclesCount = new System.Windows.Forms.TextBox();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schVMBindingSource)).BeginInit();
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
            this.pHeader.Size = new System.Drawing.Size(800, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(718, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Add Deal";
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
            this.picBtnClose.Location = new System.Drawing.Point(760, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 663);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(790, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 663);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 688);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(780, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(780, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.label8);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.tbPerTradeUnitPrice);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.tbDescription);
            this.pMain.Controls.Add(this.tbPerTradeUnitQty);
            this.pMain.Controls.Add(this.label11);
            this.pMain.Controls.Add(this.btnAddSchedule);
            this.pMain.Controls.Add(this.btnAddBharthiType);
            this.pMain.Controls.Add(this.cbTradeUnits);
            this.pMain.Controls.Add(this.label10);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.tbRemaining);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.dgv);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Controls.Add(this.dtpDealDate);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.btnAddDealItem);
            this.pMain.Controls.Add(this.btnAddBroker);
            this.pMain.Controls.Add(this.cbDealItems);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.cbBrokers);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.tbVehiclesCount);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(780, 639);
            this.pMain.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(550, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 20);
            this.label8.TabIndex = 141;
            this.label8.Text = "Kg";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(347, 26);
            this.label7.TabIndex = 140;
            this.label7.Text = "Packing Types:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPerTradeUnitPrice
            // 
            this.tbPerTradeUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitPrice.Location = new System.Drawing.Point(372, 252);
            this.tbPerTradeUnitPrice.Name = "tbPerTradeUnitPrice";
            this.tbPerTradeUnitPrice.Size = new System.Drawing.Size(268, 26);
            this.tbPerTradeUnitPrice.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(176, 284);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 26);
            this.label6.TabIndex = 138;
            this.label6.Text = "Description:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDescription
            // 
            this.tbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDescription.Location = new System.Drawing.Point(372, 284);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(269, 90);
            this.tbDescription.TabIndex = 10;
            this.tbDescription.Text = "N/A";
            // 
            // tbPerTradeUnitQty
            // 
            this.tbPerTradeUnitQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbPerTradeUnitQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPerTradeUnitQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPerTradeUnitQty.Location = new System.Drawing.Point(372, 220);
            this.tbPerTradeUnitQty.Name = "tbPerTradeUnitQty";
            this.tbPerTradeUnitQty.ReadOnly = true;
            this.tbPerTradeUnitQty.Size = new System.Drawing.Size(172, 26);
            this.tbPerTradeUnitQty.TabIndex = 134;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(345, 26);
            this.label11.TabIndex = 133;
            this.label11.Text = "Packing Types:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddSchedule
            // 
            this.btnAddSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddSchedule.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddSchedule.BackgroundImage")));
            this.btnAddSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddSchedule.Location = new System.Drawing.Point(699, 364);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(54, 37);
            this.btnAddSchedule.TabIndex = 11;
            this.btnAddSchedule.UseVisualStyleBackColor = false;
            this.btnAddSchedule.Click += new System.EventHandler(this.btnAddSchedule_Click);
            // 
            // btnAddBharthiType
            // 
            this.btnAddBharthiType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddBharthiType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddBharthiType.BackgroundImage")));
            this.btnAddBharthiType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddBharthiType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBharthiType.Location = new System.Drawing.Point(647, 183);
            this.btnAddBharthiType.Name = "btnAddBharthiType";
            this.btnAddBharthiType.Size = new System.Drawing.Size(31, 31);
            this.btnAddBharthiType.TabIndex = 8;
            this.btnAddBharthiType.UseVisualStyleBackColor = false;
            this.btnAddBharthiType.Click += new System.EventHandler(this.btnAddBharthiType_Click);
            // 
            // cbTradeUnits
            // 
            this.cbTradeUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTradeUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTradeUnits.FormattingEnabled = true;
            this.cbTradeUnits.Location = new System.Drawing.Point(372, 186);
            this.cbTradeUnits.Name = "cbTradeUnits";
            this.cbTradeUnits.Size = new System.Drawing.Size(269, 28);
            this.cbTradeUnits.TabIndex = 7;
            this.cbTradeUnits.SelectedIndexChanged += new System.EventHandler(this.cbTradeUnits_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(281, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 20);
            this.label10.TabIndex = 132;
            this.label10.Text = "Trade Unit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(279, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 73;
            this.label4.Text = "Remaining:";
            // 
            // tbRemaining
            // 
            this.tbRemaining.Enabled = false;
            this.tbRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemaining.Location = new System.Drawing.Point(372, 153);
            this.tbRemaining.Name = "tbRemaining";
            this.tbRemaining.Size = new System.Drawing.Size(95, 26);
            this.tbRemaining.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(591, 591);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(162, 42);
            this.btnClose.TabIndex = 13;
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
            this.btnAdd.Location = new System.Drawing.Point(402, 591);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(183, 42);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add Deal";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.vehiclesDataGridViewTextBoxColumn,
            this.readyDateDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.schVMBindingSource;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv.Location = new System.Drawing.Point(36, 404);
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(717, 180);
            this.dgv.TabIndex = 14;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEnter);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.FillWeight = 15.22843F;
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vehiclesDataGridViewTextBoxColumn
            // 
            this.vehiclesDataGridViewTextBoxColumn.DataPropertyName = "Vehicles";
            this.vehiclesDataGridViewTextBoxColumn.FillWeight = 142.3858F;
            this.vehiclesDataGridViewTextBoxColumn.HeaderText = "Vehicles";
            this.vehiclesDataGridViewTextBoxColumn.Name = "vehiclesDataGridViewTextBoxColumn";
            this.vehiclesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // readyDateDataGridViewTextBoxColumn
            // 
            this.readyDateDataGridViewTextBoxColumn.DataPropertyName = "ReadyDate";
            this.readyDateDataGridViewTextBoxColumn.FillWeight = 142.3858F;
            this.readyDateDataGridViewTextBoxColumn.HeaderText = "ReadyDate";
            this.readyDateDataGridViewTextBoxColumn.Name = "readyDateDataGridViewTextBoxColumn";
            this.readyDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // schVMBindingSource
            // 
            this.schVMBindingSource.DataSource = typeof(Model.ReadyStuff.ViewModel.SchVM);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(283, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 71;
            this.label2.Text = "Deal Date:";
            // 
            // dtpDealDate
            // 
            this.dtpDealDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDealDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDealDate.Location = new System.Drawing.Point(372, 122);
            this.dtpDealDate.Name = "dtpDealDate";
            this.dtpDealDate.Size = new System.Drawing.Size(268, 26);
            this.dtpDealDate.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(257, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 69;
            this.label1.Text = "No of vehicles:";
            // 
            // btnAddDealItem
            // 
            this.btnAddDealItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddDealItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDealItem.BackgroundImage")));
            this.btnAddDealItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddDealItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDealItem.Location = new System.Drawing.Point(646, 56);
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
            this.btnAddBroker.Location = new System.Drawing.Point(646, 21);
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
            this.cbDealItems.Location = new System.Drawing.Point(372, 56);
            this.cbDealItems.Name = "cbDealItems";
            this.cbDealItems.Size = new System.Drawing.Size(268, 28);
            this.cbDealItems.TabIndex = 2;
            this.cbDealItems.SelectedIndexChanged += new System.EventHandler(this.cbDealItems_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(286, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 67;
            this.label5.Text = "Deal Item:";
            // 
            // cbBrokers
            // 
            this.cbBrokers.BackColor = System.Drawing.Color.Gainsboro;
            this.cbBrokers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrokers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBrokers.FormattingEnabled = true;
            this.cbBrokers.Location = new System.Drawing.Point(372, 22);
            this.cbBrokers.Name = "cbBrokers";
            this.cbBrokers.Size = new System.Drawing.Size(268, 28);
            this.cbBrokers.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(308, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 68;
            this.label3.Text = "Broker:";
            // 
            // tbVehiclesCount
            // 
            this.tbVehiclesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVehiclesCount.Location = new System.Drawing.Point(372, 90);
            this.tbVehiclesCount.Name = "tbVehiclesCount";
            this.tbVehiclesCount.Size = new System.Drawing.Size(95, 26);
            this.tbVehiclesCount.TabIndex = 4;
            this.tbVehiclesCount.TextChanged += new System.EventHandler(this.tbVehiclesCount_TextChanged);
            // 
            // AddReadyDealForm
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddReadyDealForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schVMBindingSource)).EndInit();
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
        private System.Windows.Forms.Button btnAddDealItem;
        private System.Windows.Forms.Button btnAddBroker;
        private System.Windows.Forms.ComboBox cbDealItems;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbBrokers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVehiclesCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDealDate;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehiclesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn readyDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource schVMBindingSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRemaining;
        private System.Windows.Forms.TextBox tbPerTradeUnitQty;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAddBharthiType;
        private System.Windows.Forms.ComboBox cbTradeUnits;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAddSchedule;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPerTradeUnitPrice;
        private System.Windows.Forms.Label label8;
    }
}