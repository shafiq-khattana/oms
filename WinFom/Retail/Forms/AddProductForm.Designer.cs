namespace WinFom.Retail.Forms
{
    partial class AddProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProductForm));
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
            this.label15 = new System.Windows.Forms.Label();
            this.bswApplyLaborExpense = new Bunifu.Framework.UI.BunifuSwitch();
            this.bswDeductBardana = new Bunifu.Framework.UI.BunifuSwitch();
            this.label14 = new System.Windows.Forms.Label();
            this.tbWholeSalePrice = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.bswAlertOnSale = new Bunifu.Framework.UI.BunifuSwitch();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bswIsDiscountable = new Bunifu.Framework.UI.BunifuSwitch();
            this.btnAddProductsize = new System.Windows.Forms.Button();
            this.cbProductSize = new System.Windows.Forms.ComboBox();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.cbCategories = new System.Windows.Forms.ComboBox();
            this.tbPoints = new System.Windows.Forms.TextBox();
            this.tbProductNetPrice = new System.Windows.Forms.TextBox();
            this.tbProductDiscount = new System.Windows.Forms.TextBox();
            this.tbDiscountPercentage = new System.Windows.Forms.TextBox();
            this.tbBarcode = new System.Windows.Forms.TextBox();
            this.tbCustomerPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbProductTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.pHeader.Size = new System.Drawing.Size(512, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(430, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Add New Product";
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
            this.picBtnClose.Location = new System.Drawing.Point(472, 0);
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
            this.pRight.Location = new System.Drawing.Point(502, 37);
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
            this.pBottom.Size = new System.Drawing.Size(492, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(492, 12);
            this.pTop.TabIndex = 4;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.label16);
            this.pMain.Controls.Add(this.label15);
            this.pMain.Controls.Add(this.bswApplyLaborExpense);
            this.pMain.Controls.Add(this.bswDeductBardana);
            this.pMain.Controls.Add(this.label14);
            this.pMain.Controls.Add(this.tbWholeSalePrice);
            this.pMain.Controls.Add(this.label13);
            this.pMain.Controls.Add(this.label12);
            this.pMain.Controls.Add(this.tbWeight);
            this.pMain.Controls.Add(this.btnClose);
            this.pMain.Controls.Add(this.btnAdd);
            this.pMain.Controls.Add(this.bswAlertOnSale);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.label11);
            this.pMain.Controls.Add(this.label10);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.label9);
            this.pMain.Controls.Add(this.bswIsDiscountable);
            this.pMain.Controls.Add(this.btnAddProductsize);
            this.pMain.Controls.Add(this.cbProductSize);
            this.pMain.Controls.Add(this.btnAddCategory);
            this.pMain.Controls.Add(this.cbCategories);
            this.pMain.Controls.Add(this.tbPoints);
            this.pMain.Controls.Add(this.tbProductNetPrice);
            this.pMain.Controls.Add(this.tbProductDiscount);
            this.pMain.Controls.Add(this.tbDiscountPercentage);
            this.pMain.Controls.Add(this.tbBarcode);
            this.pMain.Controls.Add(this.tbCustomerPrice);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.label8);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.tbProductTitle);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(492, 539);
            this.pMain.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(20, 470);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(163, 20);
            this.label16.TabIndex = 34;
            this.label16.Text = "Apply Labor Expense:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(53, 444);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(130, 20);
            this.label15.TabIndex = 33;
            this.label15.Text = "Deduct Bardana:";
            // 
            // bswApplyLaborExpense
            // 
            this.bswApplyLaborExpense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswApplyLaborExpense.BorderRadius = 5;
            this.bswApplyLaborExpense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswApplyLaborExpense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswApplyLaborExpense.Location = new System.Drawing.Point(189, 473);
            this.bswApplyLaborExpense.Margin = new System.Windows.Forms.Padding(6);
            this.bswApplyLaborExpense.Name = "bswApplyLaborExpense";
            this.bswApplyLaborExpense.Oncolor = System.Drawing.Color.Lime;
            this.bswApplyLaborExpense.Onoffcolor = System.Drawing.Color.Red;
            this.bswApplyLaborExpense.Size = new System.Drawing.Size(51, 19);
            this.bswApplyLaborExpense.TabIndex = 32;
            this.bswApplyLaborExpense.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswApplyLaborExpense.Value = true;
            // 
            // bswDeductBardana
            // 
            this.bswDeductBardana.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswDeductBardana.BorderRadius = 5;
            this.bswDeductBardana.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswDeductBardana.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswDeductBardana.Location = new System.Drawing.Point(189, 446);
            this.bswDeductBardana.Margin = new System.Windows.Forms.Padding(6);
            this.bswDeductBardana.Name = "bswDeductBardana";
            this.bswDeductBardana.Oncolor = System.Drawing.Color.Lime;
            this.bswDeductBardana.Onoffcolor = System.Drawing.Color.Red;
            this.bswDeductBardana.Size = new System.Drawing.Size(51, 19);
            this.bswDeductBardana.TabIndex = 31;
            this.bswDeductBardana.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswDeductBardana.Value = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(50, 414);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(133, 20);
            this.label14.TabIndex = 18;
            this.label14.Text = "Whole Sale Price:";
            // 
            // tbWholeSalePrice
            // 
            this.tbWholeSalePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWholeSalePrice.Location = new System.Drawing.Point(189, 411);
            this.tbWholeSalePrice.Name = "tbWholeSalePrice";
            this.tbWholeSalePrice.Size = new System.Drawing.Size(236, 26);
            this.tbWholeSalePrice.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(397, 382);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 20);
            this.label13.TabIndex = 17;
            this.label13.Text = "Kg";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(61, 382);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 20);
            this.label12.TabIndex = 19;
            this.label12.Text = "Product Weight:";
            // 
            // tbWeight
            // 
            this.tbWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWeight.Location = new System.Drawing.Point(189, 379);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(202, 26);
            this.tbWeight.TabIndex = 13;
            this.tbWeight.Leave += new System.EventHandler(this.tbWeight_Leave);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(329, 504);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 29);
            this.btnClose.TabIndex = 16;
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
            this.btnAdd.Location = new System.Drawing.Point(189, 504);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(134, 29);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // bswAlertOnSale
            // 
            this.bswAlertOnSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswAlertOnSale.BorderRadius = 5;
            this.bswAlertOnSale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswAlertOnSale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswAlertOnSale.Location = new System.Drawing.Point(189, 190);
            this.bswAlertOnSale.Margin = new System.Windows.Forms.Padding(6);
            this.bswAlertOnSale.Name = "bswAlertOnSale";
            this.bswAlertOnSale.Oncolor = System.Drawing.Color.Lime;
            this.bswAlertOnSale.Onoffcolor = System.Drawing.Color.Red;
            this.bswAlertOnSale.Size = new System.Drawing.Size(51, 19);
            this.bswAlertOnSale.TabIndex = 7;
            this.bswAlertOnSale.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswAlertOnSale.Value = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(67, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "Product Points:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(33, 318);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(150, 20);
            this.label11.TabIndex = 21;
            this.label11.Text = "Customer Net Price:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(48, 286);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "Product Discount:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "Product Discount (%):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(76, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 20);
            this.label9.TabIndex = 25;
            this.label9.Text = "Alert On Sale:";
            // 
            // bswIsDiscountable
            // 
            this.bswIsDiscountable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bswIsDiscountable.BorderRadius = 5;
            this.bswIsDiscountable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bswIsDiscountable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswIsDiscountable.Location = new System.Drawing.Point(189, 160);
            this.bswIsDiscountable.Margin = new System.Windows.Forms.Padding(5);
            this.bswIsDiscountable.Name = "bswIsDiscountable";
            this.bswIsDiscountable.Oncolor = System.Drawing.Color.Lime;
            this.bswIsDiscountable.Onoffcolor = System.Drawing.Color.Red;
            this.bswIsDiscountable.Size = new System.Drawing.Size(51, 19);
            this.bswIsDiscountable.TabIndex = 6;
            this.bswIsDiscountable.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bswIsDiscountable.Value = false;
            this.bswIsDiscountable.Click += new System.EventHandler(this.bswIsDiscountable_Click);
            // 
            // btnAddProductsize
            // 
            this.btnAddProductsize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddProductsize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddProductsize.BackgroundImage")));
            this.btnAddProductsize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddProductsize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddProductsize.Location = new System.Drawing.Point(431, 91);
            this.btnAddProductsize.Name = "btnAddProductsize";
            this.btnAddProductsize.Size = new System.Drawing.Size(31, 31);
            this.btnAddProductsize.TabIndex = 4;
            this.btnAddProductsize.UseVisualStyleBackColor = false;
            this.btnAddProductsize.Click += new System.EventHandler(this.btnAddProductsize_Click);
            // 
            // cbProductSize
            // 
            this.cbProductSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProductSize.FormattingEnabled = true;
            this.cbProductSize.Location = new System.Drawing.Point(189, 92);
            this.cbProductSize.Name = "cbProductSize";
            this.cbProductSize.Size = new System.Drawing.Size(236, 28);
            this.cbProductSize.TabIndex = 3;
            this.cbProductSize.SelectedIndexChanged += new System.EventHandler(this.cbProductSize_SelectedIndexChanged);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddCategory.BackgroundImage")));
            this.btnAddCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCategory.Location = new System.Drawing.Point(431, 25);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(31, 31);
            this.btnAddCategory.TabIndex = 1;
            this.btnAddCategory.UseVisualStyleBackColor = false;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // cbCategories
            // 
            this.cbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Location = new System.Drawing.Point(189, 26);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(236, 28);
            this.cbCategories.TabIndex = 0;
            this.cbCategories.SelectedIndexChanged += new System.EventHandler(this.cbCategories_SelectedIndexChanged);
            // 
            // tbPoints
            // 
            this.tbPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPoints.Location = new System.Drawing.Point(189, 347);
            this.tbPoints.Name = "tbPoints";
            this.tbPoints.Size = new System.Drawing.Size(236, 26);
            this.tbPoints.TabIndex = 12;
            // 
            // tbProductNetPrice
            // 
            this.tbProductNetPrice.Enabled = false;
            this.tbProductNetPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProductNetPrice.Location = new System.Drawing.Point(189, 315);
            this.tbProductNetPrice.Name = "tbProductNetPrice";
            this.tbProductNetPrice.ReadOnly = true;
            this.tbProductNetPrice.Size = new System.Drawing.Size(236, 26);
            this.tbProductNetPrice.TabIndex = 11;
            // 
            // tbProductDiscount
            // 
            this.tbProductDiscount.Enabled = false;
            this.tbProductDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProductDiscount.Location = new System.Drawing.Point(189, 283);
            this.tbProductDiscount.Name = "tbProductDiscount";
            this.tbProductDiscount.ReadOnly = true;
            this.tbProductDiscount.Size = new System.Drawing.Size(236, 26);
            this.tbProductDiscount.TabIndex = 10;
            // 
            // tbDiscountPercentage
            // 
            this.tbDiscountPercentage.Enabled = false;
            this.tbDiscountPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDiscountPercentage.Location = new System.Drawing.Point(189, 251);
            this.tbDiscountPercentage.Name = "tbDiscountPercentage";
            this.tbDiscountPercentage.ReadOnly = true;
            this.tbDiscountPercentage.Size = new System.Drawing.Size(236, 26);
            this.tbDiscountPercentage.TabIndex = 9;
            this.tbDiscountPercentage.Text = "0";
            this.tbDiscountPercentage.TextChanged += new System.EventHandler(this.tbDiscountPercentage_TextChanged);
            // 
            // tbBarcode
            // 
            this.tbBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBarcode.Location = new System.Drawing.Point(189, 219);
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.Size = new System.Drawing.Size(236, 26);
            this.tbBarcode.TabIndex = 8;
            // 
            // tbCustomerPrice
            // 
            this.tbCustomerPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCustomerPrice.Location = new System.Drawing.Point(189, 126);
            this.tbCustomerPrice.Name = "tbCustomerPrice";
            this.tbCustomerPrice.Size = new System.Drawing.Size(236, 26);
            this.tbCustomerPrice.TabIndex = 5;
            this.tbCustomerPrice.TextChanged += new System.EventHandler(this.tbCustomerPrice_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(64, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "Apply Discount:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(80, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 20);
            this.label8.TabIndex = 28;
            this.label8.Text = "Product Size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Barcode/Access Code:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Customer Total Price:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(82, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 29;
            this.label1.Text = "Product Title:";
            // 
            // tbProductTitle
            // 
            this.tbProductTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProductTitle.Location = new System.Drawing.Point(189, 60);
            this.tbProductTitle.Name = "tbProductTitle";
            this.tbProductTitle.Size = new System.Drawing.Size(236, 26);
            this.tbProductTitle.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Category:";
            // 
            // AddProductForm
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(512, 600);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddProductForm";
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
        private System.Windows.Forms.TextBox tbBarcode;
        private System.Windows.Forms.TextBox tbCustomerPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbProductTitle;
        private System.Windows.Forms.ComboBox cbCategories;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnAddProductsize;
        private System.Windows.Forms.ComboBox cbProductSize;
        private Bunifu.Framework.UI.BunifuSwitch bswIsDiscountable;
        private Bunifu.Framework.UI.BunifuSwitch bswAlertOnSale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPoints;
        private System.Windows.Forms.TextBox tbDiscountPercentage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbProductNetPrice;
        private System.Windows.Forms.TextBox tbProductDiscount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbWholeSalePrice;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private Bunifu.Framework.UI.BunifuSwitch bswApplyLaborExpense;
        private Bunifu.Framework.UI.BunifuSwitch bswDeductBardana;
    }
}