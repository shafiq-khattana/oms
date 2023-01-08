namespace WinFom.Deal.Forms
{
    partial class DealListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.tbReceivedQtySchedules = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLossPercentage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbLossInCash = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbQtyDifference = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbReceivedQtyActual = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbRemainingSchedules = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbReceivedSchedules = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTotalSchedules = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brokerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packingUnitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tradeUnitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalQtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isCompletedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dealStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completionStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.tbTotalLossInCash = new System.Windows.Forms.TextBox();
            this.tbTotalQtyLoss = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAllDeals = new System.Windows.Forms.Button();
            this.btnDateRangeView = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealVMBindingSource)).BeginInit();
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
            // pHeader
            // 
            this.pHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.pHeader.Controls.Add(this.lblHeading);
            this.pHeader.Controls.Add(this.picBtnIcon);
            this.pHeader.Controls.Add(this.picBtnClose);
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(1300, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1218, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Deal List";
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
            this.picBtnClose.Location = new System.Drawing.Point(1260, 0);
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
            this.pRight.Location = new System.Drawing.Point(1290, 37);
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
            this.pBottom.Size = new System.Drawing.Size(1280, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1280, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.panel2);
            this.pMain.Controls.Add(this.dgv);
            this.pMain.Controls.Add(this.panel1);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(1280, 639);
            this.pMain.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.tbReceivedQtySchedules);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.tbLossPercentage);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.tbLossInCash);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tbQtyDifference);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tbReceivedQtyActual);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.tbRemainingSchedules);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbReceivedSchedules);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbTotalSchedules);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 550);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1280, 89);
            this.panel2.TabIndex = 3;
            // 
            // tbReceivedQtySchedules
            // 
            this.tbReceivedQtySchedules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReceivedQtySchedules.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReceivedQtySchedules.Location = new System.Drawing.Point(857, 12);
            this.tbReceivedQtySchedules.Name = "tbReceivedQtySchedules";
            this.tbReceivedQtySchedules.ReadOnly = true;
            this.tbReceivedQtySchedules.Size = new System.Drawing.Size(138, 26);
            this.tbReceivedQtySchedules.TabIndex = 28;
            this.tbReceivedQtySchedules.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(756, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 20);
            this.label7.TabIndex = 27;
            this.label7.Text = "Loaded Qty:";
            // 
            // tbLossPercentage
            // 
            this.tbLossPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLossPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLossPercentage.Location = new System.Drawing.Point(1101, 12);
            this.tbLossPercentage.Name = "tbLossPercentage";
            this.tbLossPercentage.ReadOnly = true;
            this.tbLossPercentage.Size = new System.Drawing.Size(76, 26);
            this.tbLossPercentage.TabIndex = 24;
            this.tbLossPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1024, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 20);
            this.label9.TabIndex = 23;
            this.label9.Text = "Loss(%):";
            // 
            // tbLossInCash
            // 
            this.tbLossInCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLossInCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLossInCash.Location = new System.Drawing.Point(1001, 57);
            this.tbLossInCash.Name = "tbLossInCash";
            this.tbLossInCash.ReadOnly = true;
            this.tbLossInCash.Size = new System.Drawing.Size(176, 26);
            this.tbLossInCash.TabIndex = 22;
            this.tbLossInCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(894, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Loss in cash:";
            // 
            // tbQtyDifference
            // 
            this.tbQtyDifference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbQtyDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQtyDifference.Location = new System.Drawing.Point(719, 57);
            this.tbQtyDifference.Name = "tbQtyDifference";
            this.tbQtyDifference.ReadOnly = true;
            this.tbQtyDifference.Size = new System.Drawing.Size(163, 26);
            this.tbQtyDifference.TabIndex = 20;
            this.tbQtyDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(587, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Qty Difference:";
            // 
            // tbReceivedQtyActual
            // 
            this.tbReceivedQtyActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReceivedQtyActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReceivedQtyActual.Location = new System.Drawing.Point(390, 57);
            this.tbReceivedQtyActual.Name = "tbReceivedQtyActual";
            this.tbReceivedQtyActual.ReadOnly = true;
            this.tbReceivedQtyActual.Size = new System.Drawing.Size(184, 26);
            this.tbReceivedQtyActual.TabIndex = 18;
            this.tbReceivedQtyActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(218, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Received Qty (Actual):";
            // 
            // tbRemainingSchedules
            // 
            this.tbRemainingSchedules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRemainingSchedules.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemainingSchedules.Location = new System.Drawing.Point(580, 12);
            this.tbRemainingSchedules.Name = "tbRemainingSchedules";
            this.tbRemainingSchedules.ReadOnly = true;
            this.tbRemainingSchedules.Size = new System.Drawing.Size(72, 26);
            this.tbRemainingSchedules.TabIndex = 16;
            this.tbRemainingSchedules.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(406, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Remaining Schedules:";
            // 
            // tbReceivedSchedules
            // 
            this.tbReceivedSchedules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReceivedSchedules.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReceivedSchedules.Location = new System.Drawing.Point(353, 12);
            this.tbReceivedSchedules.Name = "tbReceivedSchedules";
            this.tbReceivedSchedules.ReadOnly = true;
            this.tbReceivedSchedules.Size = new System.Drawing.Size(47, 26);
            this.tbReceivedSchedules.TabIndex = 14;
            this.tbReceivedSchedules.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(189, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Received Schedules:";
            // 
            // tbTotalSchedules
            // 
            this.tbTotalSchedules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalSchedules.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalSchedules.Location = new System.Drawing.Point(139, 12);
            this.tbTotalSchedules.Name = "tbTotalSchedules";
            this.tbTotalSchedules.ReadOnly = true;
            this.tbTotalSchedules.Size = new System.Drawing.Size(44, 26);
            this.tbTotalSchedules.TabIndex = 12;
            this.tbTotalSchedules.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Schedules:";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoGenerateColumns = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.companyDataGridViewTextBoxColumn,
            this.brokerDataGridViewTextBoxColumn,
            this.itemDataGridViewTextBoxColumn,
            this.packingUnitsDataGridViewTextBoxColumn,
            this.tradeUnitsDataGridViewTextBoxColumn,
            this.totalQtyDataGridViewTextBoxColumn,
            this.totalPriceDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.isCompletedDataGridViewCheckBoxColumn,
            this.dealStatusDataGridViewTextBoxColumn,
            this.completionStatusDataGridViewTextBoxColumn,
            this.rateDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.dealVMBindingSource;
            this.dgv.Location = new System.Drawing.Point(0, 108);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(1274, 436);
            this.dgv.TabIndex = 2;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.FillWeight = 32.99492F;
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // companyDataGridViewTextBoxColumn
            // 
            this.companyDataGridViewTextBoxColumn.DataPropertyName = "Company";
            this.companyDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.companyDataGridViewTextBoxColumn.HeaderText = "Company";
            this.companyDataGridViewTextBoxColumn.Name = "companyDataGridViewTextBoxColumn";
            this.companyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // brokerDataGridViewTextBoxColumn
            // 
            this.brokerDataGridViewTextBoxColumn.DataPropertyName = "Broker";
            this.brokerDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.brokerDataGridViewTextBoxColumn.HeaderText = "Broker";
            this.brokerDataGridViewTextBoxColumn.Name = "brokerDataGridViewTextBoxColumn";
            this.brokerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemDataGridViewTextBoxColumn
            // 
            this.itemDataGridViewTextBoxColumn.DataPropertyName = "Item";
            this.itemDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.itemDataGridViewTextBoxColumn.HeaderText = "Item";
            this.itemDataGridViewTextBoxColumn.Name = "itemDataGridViewTextBoxColumn";
            this.itemDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // packingUnitsDataGridViewTextBoxColumn
            // 
            this.packingUnitsDataGridViewTextBoxColumn.DataPropertyName = "PackingUnits";
            this.packingUnitsDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.packingUnitsDataGridViewTextBoxColumn.HeaderText = "Packing Units";
            this.packingUnitsDataGridViewTextBoxColumn.Name = "packingUnitsDataGridViewTextBoxColumn";
            this.packingUnitsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tradeUnitsDataGridViewTextBoxColumn
            // 
            this.tradeUnitsDataGridViewTextBoxColumn.DataPropertyName = "TradeUnits";
            this.tradeUnitsDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.tradeUnitsDataGridViewTextBoxColumn.HeaderText = "Trade Units";
            this.tradeUnitsDataGridViewTextBoxColumn.Name = "tradeUnitsDataGridViewTextBoxColumn";
            this.tradeUnitsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalQtyDataGridViewTextBoxColumn
            // 
            this.totalQtyDataGridViewTextBoxColumn.DataPropertyName = "TotalQty";
            this.totalQtyDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.totalQtyDataGridViewTextBoxColumn.HeaderText = "Total Qty";
            this.totalQtyDataGridViewTextBoxColumn.Name = "totalQtyDataGridViewTextBoxColumn";
            this.totalQtyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalPriceDataGridViewTextBoxColumn
            // 
            this.totalPriceDataGridViewTextBoxColumn.DataPropertyName = "TotalPrice";
            this.totalPriceDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.totalPriceDataGridViewTextBoxColumn.HeaderText = "Total Price";
            this.totalPriceDataGridViewTextBoxColumn.Name = "totalPriceDataGridViewTextBoxColumn";
            this.totalPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.dateDataGridViewTextBoxColumn.HeaderText = "Deal Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isCompletedDataGridViewCheckBoxColumn
            // 
            this.isCompletedDataGridViewCheckBoxColumn.DataPropertyName = "IsCompleted";
            this.isCompletedDataGridViewCheckBoxColumn.FillWeight = 105.5838F;
            this.isCompletedDataGridViewCheckBoxColumn.HeaderText = "Completed";
            this.isCompletedDataGridViewCheckBoxColumn.Name = "isCompletedDataGridViewCheckBoxColumn";
            this.isCompletedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // dealStatusDataGridViewTextBoxColumn
            // 
            this.dealStatusDataGridViewTextBoxColumn.DataPropertyName = "DealStatus";
            this.dealStatusDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.dealStatusDataGridViewTextBoxColumn.HeaderText = "Deal Status";
            this.dealStatusDataGridViewTextBoxColumn.Name = "dealStatusDataGridViewTextBoxColumn";
            this.dealStatusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // completionStatusDataGridViewTextBoxColumn
            // 
            this.completionStatusDataGridViewTextBoxColumn.DataPropertyName = "CompletionStatus";
            this.completionStatusDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.completionStatusDataGridViewTextBoxColumn.HeaderText = "Comp Status";
            this.completionStatusDataGridViewTextBoxColumn.Name = "completionStatusDataGridViewTextBoxColumn";
            this.completionStatusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rateDataGridViewTextBoxColumn
            // 
            this.rateDataGridViewTextBoxColumn.DataPropertyName = "Rate";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.rateDataGridViewTextBoxColumn.FillWeight = 105.5838F;
            this.rateDataGridViewTextBoxColumn.HeaderText = "Rate";
            this.rateDataGridViewTextBoxColumn.Name = "rateDataGridViewTextBoxColumn";
            this.rateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dealVMBindingSource
            // 
            this.dealVMBindingSource.DataSource = typeof(Model.Deal.ViewModel.DealVM);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.tbTotalLossInCash);
            this.panel1.Controls.Add(this.tbTotalQtyLoss);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnAllDeals);
            this.panel1.Controls.Add(this.btnDateRangeView);
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 102);
            this.panel1.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(803, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(143, 20);
            this.label14.TabIndex = 54;
            this.label14.Text = "Total Loss in Cash:";
            // 
            // tbTotalLossInCash
            // 
            this.tbTotalLossInCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalLossInCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalLossInCash.Location = new System.Drawing.Point(952, 70);
            this.tbTotalLossInCash.Name = "tbTotalLossInCash";
            this.tbTotalLossInCash.ReadOnly = true;
            this.tbTotalLossInCash.Size = new System.Drawing.Size(198, 26);
            this.tbTotalLossInCash.TabIndex = 53;
            this.tbTotalLossInCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbTotalQtyLoss
            // 
            this.tbTotalQtyLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalQtyLoss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalQtyLoss.Location = new System.Drawing.Point(580, 70);
            this.tbTotalQtyLoss.Name = "tbTotalQtyLoss";
            this.tbTotalQtyLoss.ReadOnly = true;
            this.tbTotalQtyLoss.Size = new System.Drawing.Size(217, 26);
            this.tbTotalQtyLoss.TabIndex = 52;
            this.tbTotalQtyLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(454, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 51;
            this.label8.Text = "Total Qty Loss:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Red;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(728, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 36);
            this.btnClose.TabIndex = 50;
            this.btnClose.Text = "Close me!";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1068, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 30);
            this.label10.TabIndex = 49;
            this.label10.Text = "Count:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1006, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 20);
            this.label11.TabIndex = 48;
            this.label11.Text = "Count:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(574, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 36);
            this.button2.TabIndex = 47;
            this.button2.Text = "Completed";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAllDeals
            // 
            this.btnAllDeals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAllDeals.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllDeals.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllDeals.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllDeals.ForeColor = System.Drawing.Color.White;
            this.btnAllDeals.Location = new System.Drawing.Point(410, 16);
            this.btnAllDeals.Name = "btnAllDeals";
            this.btnAllDeals.Size = new System.Drawing.Size(158, 36);
            this.btnAllDeals.TabIndex = 46;
            this.btnAllDeals.Text = "All Deals";
            this.btnAllDeals.UseVisualStyleBackColor = false;
            this.btnAllDeals.Click += new System.EventHandler(this.btnAllDeals_Click);
            // 
            // btnDateRangeView
            // 
            this.btnDateRangeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDateRangeView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDateRangeView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDateRangeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDateRangeView.ForeColor = System.Drawing.Color.White;
            this.btnDateRangeView.Location = new System.Drawing.Point(63, 62);
            this.btnDateRangeView.Name = "btnDateRangeView";
            this.btnDateRangeView.Size = new System.Drawing.Size(284, 36);
            this.btnDateRangeView.TabIndex = 45;
            this.btnDateRangeView.Text = "View";
            this.btnDateRangeView.UseVisualStyleBackColor = false;
            this.btnDateRangeView.Click += new System.EventHandler(this.btnDateRangeView_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(226, 21);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(120, 26);
            this.dtpTo.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(189, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 20);
            this.label12.TabIndex = 43;
            this.label12.Text = "To:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(63, 22);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(120, 26);
            this.dtpFrom.TabIndex = 42;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 20);
            this.label13.TabIndex = 41;
            this.label13.Text = "From:";
            // 
            // DealListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DealListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealVMBindingSource)).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbReceivedQtySchedules;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLossPercentage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbLossInCash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbQtyDifference;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbReceivedQtyActual;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbRemainingSchedules;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbReceivedSchedules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTotalSchedules;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnAllDeals;
        private System.Windows.Forms.Button btnDateRangeView;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbTotalLossInCash;
        private System.Windows.Forms.TextBox tbTotalQtyLoss;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.BindingSource dealVMBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brokerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packingUnitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tradeUnitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalQtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCompletedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn completionStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rateDataGridViewTextBoxColumn;
    }
}