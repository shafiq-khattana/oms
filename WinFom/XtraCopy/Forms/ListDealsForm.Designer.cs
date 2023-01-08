namespace WinFom.XtraCopy.Forms
{
    partial class ListDealsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnInCompleted = new System.Windows.Forms.Button();
            this.btnCompleted = new System.Windows.Forms.Button();
            this.tbDealCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brokerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brokerAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brokerAmountPercentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealItemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fareAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalUnitPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealReadyDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.bunifuDragControl1.TargetControl = null;
            this.bunifuDragControl1.Vertical = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 36);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(44, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1312, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add new deal item";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(13, 44);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1374, 643);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 589);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1374, 54);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.tbDealCount);
            this.panel4.Controls.Add(this.btnCompleted);
            this.panel4.Controls.Add(this.btnInCompleted);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1374, 62);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.Controls.Add(this.dgv);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 62);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1374, 527);
            this.panel5.TabIndex = 2;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.companyDataGridViewTextBoxColumn,
            this.brokerDataGridViewTextBoxColumn,
            this.brokerAmountDataGridViewTextBoxColumn,
            this.brokerAmountPercentageDataGridViewTextBoxColumn,
            this.selectorDataGridViewTextBoxColumn,
            this.dealItemDataGridViewTextBoxColumn,
            this.packingDataGridViewTextBoxColumn,
            this.qtyDataGridViewTextBoxColumn,
            this.dealPriceDataGridViewTextBoxColumn,
            this.unitPriceDataGridViewTextBoxColumn,
            this.taxDataGridViewTextBoxColumn,
            this.fareAmountDataGridViewTextBoxColumn,
            this.totalPriceDataGridViewTextBoxColumn,
            this.totalUnitPriceDataGridViewTextBoxColumn,
            this.dealDateDataGridViewTextBoxColumn,
            this.dealReadyDateDataGridViewTextBoxColumn,
            this.addedByDataGridViewTextBoxColumn,
            this.updatedByDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.dealVMBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(1374, 527);
            this.dgv.TabIndex = 1;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // btnInCompleted
            // 
            this.btnInCompleted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnInCompleted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInCompleted.ForeColor = System.Drawing.Color.White;
            this.btnInCompleted.Location = new System.Drawing.Point(3, 12);
            this.btnInCompleted.Name = "btnInCompleted";
            this.btnInCompleted.Size = new System.Drawing.Size(157, 35);
            this.btnInCompleted.TabIndex = 29;
            this.btnInCompleted.Text = "In Completed";
            this.btnInCompleted.UseVisualStyleBackColor = false;
            this.btnInCompleted.Click += new System.EventHandler(this.btnInCompleted_Click);
            // 
            // btnCompleted
            // 
            this.btnCompleted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCompleted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompleted.ForeColor = System.Drawing.Color.White;
            this.btnCompleted.Location = new System.Drawing.Point(166, 12);
            this.btnCompleted.Name = "btnCompleted";
            this.btnCompleted.Size = new System.Drawing.Size(157, 35);
            this.btnCompleted.TabIndex = 30;
            this.btnCompleted.Text = "Completed";
            this.btnCompleted.UseVisualStyleBackColor = false;
            this.btnCompleted.Click += new System.EventHandler(this.btnCompleted_Click);
            // 
            // tbDealCount
            // 
            this.tbDealCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDealCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDealCount.Location = new System.Drawing.Point(459, 17);
            this.tbDealCount.Name = "tbDealCount";
            this.tbDealCount.ReadOnly = true;
            this.tbDealCount.Size = new System.Drawing.Size(62, 26);
            this.tbDealCount.TabIndex = 31;
            this.tbDealCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(364, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "Deal Count";
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // companyDataGridViewTextBoxColumn
            // 
            this.companyDataGridViewTextBoxColumn.DataPropertyName = "Company";
            this.companyDataGridViewTextBoxColumn.HeaderText = "Company";
            this.companyDataGridViewTextBoxColumn.Name = "companyDataGridViewTextBoxColumn";
            this.companyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // brokerDataGridViewTextBoxColumn
            // 
            this.brokerDataGridViewTextBoxColumn.DataPropertyName = "Broker";
            this.brokerDataGridViewTextBoxColumn.HeaderText = "Broker";
            this.brokerDataGridViewTextBoxColumn.Name = "brokerDataGridViewTextBoxColumn";
            this.brokerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // brokerAmountDataGridViewTextBoxColumn
            // 
            this.brokerAmountDataGridViewTextBoxColumn.DataPropertyName = "BrokerAmount";
            this.brokerAmountDataGridViewTextBoxColumn.HeaderText = "Broker Amount";
            this.brokerAmountDataGridViewTextBoxColumn.Name = "brokerAmountDataGridViewTextBoxColumn";
            this.brokerAmountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // brokerAmountPercentageDataGridViewTextBoxColumn
            // 
            this.brokerAmountPercentageDataGridViewTextBoxColumn.DataPropertyName = "BrokerAmountPercentage";
            this.brokerAmountPercentageDataGridViewTextBoxColumn.HeaderText = "Broker Amount-%";
            this.brokerAmountPercentageDataGridViewTextBoxColumn.Name = "brokerAmountPercentageDataGridViewTextBoxColumn";
            this.brokerAmountPercentageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // selectorDataGridViewTextBoxColumn
            // 
            this.selectorDataGridViewTextBoxColumn.DataPropertyName = "Selector";
            this.selectorDataGridViewTextBoxColumn.HeaderText = "Selector";
            this.selectorDataGridViewTextBoxColumn.Name = "selectorDataGridViewTextBoxColumn";
            this.selectorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dealItemDataGridViewTextBoxColumn
            // 
            this.dealItemDataGridViewTextBoxColumn.DataPropertyName = "DealItem";
            this.dealItemDataGridViewTextBoxColumn.HeaderText = "Deal Item";
            this.dealItemDataGridViewTextBoxColumn.Name = "dealItemDataGridViewTextBoxColumn";
            this.dealItemDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // packingDataGridViewTextBoxColumn
            // 
            this.packingDataGridViewTextBoxColumn.DataPropertyName = "Packing";
            this.packingDataGridViewTextBoxColumn.HeaderText = "Packing";
            this.packingDataGridViewTextBoxColumn.Name = "packingDataGridViewTextBoxColumn";
            this.packingDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            this.qtyDataGridViewTextBoxColumn.DataPropertyName = "Qty";
            this.qtyDataGridViewTextBoxColumn.HeaderText = "Qty";
            this.qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            this.qtyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dealPriceDataGridViewTextBoxColumn
            // 
            this.dealPriceDataGridViewTextBoxColumn.DataPropertyName = "DealPrice";
            this.dealPriceDataGridViewTextBoxColumn.HeaderText = "Deal Price";
            this.dealPriceDataGridViewTextBoxColumn.Name = "dealPriceDataGridViewTextBoxColumn";
            this.dealPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unitPriceDataGridViewTextBoxColumn
            // 
            this.unitPriceDataGridViewTextBoxColumn.DataPropertyName = "UnitPrice";
            this.unitPriceDataGridViewTextBoxColumn.HeaderText = "Unit Price";
            this.unitPriceDataGridViewTextBoxColumn.Name = "unitPriceDataGridViewTextBoxColumn";
            this.unitPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // taxDataGridViewTextBoxColumn
            // 
            this.taxDataGridViewTextBoxColumn.DataPropertyName = "Tax";
            this.taxDataGridViewTextBoxColumn.HeaderText = "Tax";
            this.taxDataGridViewTextBoxColumn.Name = "taxDataGridViewTextBoxColumn";
            this.taxDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fareAmountDataGridViewTextBoxColumn
            // 
            this.fareAmountDataGridViewTextBoxColumn.DataPropertyName = "FareAmount";
            this.fareAmountDataGridViewTextBoxColumn.HeaderText = "Fare Amount";
            this.fareAmountDataGridViewTextBoxColumn.Name = "fareAmountDataGridViewTextBoxColumn";
            this.fareAmountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalPriceDataGridViewTextBoxColumn
            // 
            this.totalPriceDataGridViewTextBoxColumn.DataPropertyName = "TotalPrice";
            this.totalPriceDataGridViewTextBoxColumn.HeaderText = "Toral Price";
            this.totalPriceDataGridViewTextBoxColumn.Name = "totalPriceDataGridViewTextBoxColumn";
            this.totalPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalUnitPriceDataGridViewTextBoxColumn
            // 
            this.totalUnitPriceDataGridViewTextBoxColumn.DataPropertyName = "TotalUnitPrice";
            this.totalUnitPriceDataGridViewTextBoxColumn.HeaderText = "Total Unit Price";
            this.totalUnitPriceDataGridViewTextBoxColumn.Name = "totalUnitPriceDataGridViewTextBoxColumn";
            this.totalUnitPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dealDateDataGridViewTextBoxColumn
            // 
            this.dealDateDataGridViewTextBoxColumn.DataPropertyName = "DealDate";
            this.dealDateDataGridViewTextBoxColumn.HeaderText = "Deal Date";
            this.dealDateDataGridViewTextBoxColumn.Name = "dealDateDataGridViewTextBoxColumn";
            this.dealDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dealReadyDateDataGridViewTextBoxColumn
            // 
            this.dealReadyDateDataGridViewTextBoxColumn.DataPropertyName = "DealReadyDate";
            this.dealReadyDateDataGridViewTextBoxColumn.HeaderText = "Deal Ready Date";
            this.dealReadyDateDataGridViewTextBoxColumn.Name = "dealReadyDateDataGridViewTextBoxColumn";
            this.dealReadyDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // addedByDataGridViewTextBoxColumn
            // 
            this.addedByDataGridViewTextBoxColumn.DataPropertyName = "AddedBy";
            this.addedByDataGridViewTextBoxColumn.HeaderText = "Added By";
            this.addedByDataGridViewTextBoxColumn.Name = "addedByDataGridViewTextBoxColumn";
            this.addedByDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedByDataGridViewTextBoxColumn
            // 
            this.updatedByDataGridViewTextBoxColumn.DataPropertyName = "UpdatedBy";
            this.updatedByDataGridViewTextBoxColumn.HeaderText = "Updated By";
            this.updatedByDataGridViewTextBoxColumn.Name = "updatedByDataGridViewTextBoxColumn";
            this.updatedByDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dealVMBindingSource
            // 
            //this.dealVMBindingSource.DataSource = typeof(WinFom.XtraCopy.ViewModel.DealVM2);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = global::WinFom.Properties.Resources.Delete_k52px;
            this.pictureBox2.Location = new System.Drawing.Point(1356, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(44, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::WinFom.Properties.Resources.icons8_robot_48;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ListDealsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ListDealsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SampleForm";
            this.Load += new System.EventHandler(this.ListDealsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brokerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brokerAmountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brokerAmountPercentageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealItemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fareAmountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalUnitPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealReadyDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource dealVMBindingSource;
        private System.Windows.Forms.Button btnCompleted;
        private System.Windows.Forms.Button btnInCompleted;
        private System.Windows.Forms.TextBox tbDealCount;
        private System.Windows.Forms.Label label2;
    }
}