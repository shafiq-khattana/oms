namespace WinFom.OilDealManaged.Forms
{
    partial class AddOilSelectorForm
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
            this.tbCompany = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbContact = new System.Windows.Forms.TextBox();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.tbMisc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCNIC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pThumbs = new System.Windows.Forms.Panel();
            this.thumb2 = new System.Windows.Forms.PictureBox();
            this.btncap2 = new System.Windows.Forms.Button();
            this.thumbp2 = new System.Windows.Forms.ProgressBar();
            this.thumb1 = new System.Windows.Forms.PictureBox();
            this.btncap1 = new System.Windows.Forms.Button();
            this.thumbp1 = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.bsThumbs = new Bunifu.Framework.UI.BunifuSwitch();
            this.pPics = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCamStart = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.bsPics = new Bunifu.Framework.UI.BunifuSwitch();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            this.pThumbs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thumb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumb1)).BeginInit();
            this.pPics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pMain.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.pHeader.Size = new System.Drawing.Size(803, 37);
            this.pHeader.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(42, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(721, 37);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Add new oil selector";
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
            this.picBtnClose.Location = new System.Drawing.Point(763, 0);
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
            this.pLeft.Size = new System.Drawing.Size(10, 594);
            this.pLeft.TabIndex = 1;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Gray;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(793, 37);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(10, 594);
            this.pRight.TabIndex = 2;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Gray;
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(10, 619);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(783, 12);
            this.pBottom.TabIndex = 3;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Gray;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(10, 37);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(783, 12);
            this.pTop.TabIndex = 4;
            // 
            // tbCompany
            // 
            this.tbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCompany.Location = new System.Drawing.Point(128, 6);
            this.tbCompany.Name = "tbCompany";
            this.tbCompany.Size = new System.Drawing.Size(311, 26);
            this.tbCompany.TabIndex = 0;
            // 
            // tbAddress
            // 
            this.tbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAddress.Location = new System.Drawing.Point(128, 38);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(311, 26);
            this.tbAddress.TabIndex = 1;
            // 
            // tbContact
            // 
            this.tbContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbContact.Location = new System.Drawing.Point(128, 70);
            this.tbContact.Name = "tbContact";
            this.tbContact.Size = new System.Drawing.Size(311, 26);
            this.tbContact.TabIndex = 2;
            // 
            // tbRemarks
            // 
            this.tbRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRemarks.Location = new System.Drawing.Point(128, 134);
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(311, 26);
            this.tbRemarks.TabIndex = 4;
            this.tbRemarks.Text = "N/A";
            // 
            // tbMisc
            // 
            this.tbMisc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMisc.Location = new System.Drawing.Point(128, 166);
            this.tbMisc.Multiline = true;
            this.tbMisc.Name = "tbMisc";
            this.tbMisc.Size = new System.Drawing.Size(311, 74);
            this.tbMisc.TabIndex = 5;
            this.tbMisc.Text = "N/A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Contact:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Remarks:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Misc Info:";
            // 
            // tbCNIC
            // 
            this.tbCNIC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCNIC.Location = new System.Drawing.Point(128, 102);
            this.tbCNIC.Name = "tbCNIC";
            this.tbCNIC.Size = new System.Drawing.Size(311, 26);
            this.tbCNIC.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "CNIC:";
            // 
            // pThumbs
            // 
            this.pThumbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pThumbs.Controls.Add(this.thumb2);
            this.pThumbs.Controls.Add(this.btncap2);
            this.pThumbs.Controls.Add(this.thumbp2);
            this.pThumbs.Controls.Add(this.thumb1);
            this.pThumbs.Controls.Add(this.btncap1);
            this.pThumbs.Controls.Add(this.thumbp1);
            this.pThumbs.Location = new System.Drawing.Point(182, 280);
            this.pThumbs.Name = "pThumbs";
            this.pThumbs.Size = new System.Drawing.Size(257, 219);
            this.pThumbs.TabIndex = 24;
            // 
            // thumb2
            // 
            this.thumb2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thumb2.Location = new System.Drawing.Point(135, 8);
            this.thumb2.Name = "thumb2";
            this.thumb2.Size = new System.Drawing.Size(109, 139);
            this.thumb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thumb2.TabIndex = 53;
            this.thumb2.TabStop = false;
            // 
            // btncap2
            // 
            this.btncap2.AutoEllipsis = true;
            this.btncap2.BackColor = System.Drawing.Color.SteelBlue;
            this.btncap2.FlatAppearance.BorderSize = 4;
            this.btncap2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncap2.ForeColor = System.Drawing.Color.White;
            this.btncap2.Location = new System.Drawing.Point(135, 169);
            this.btncap2.Name = "btncap2";
            this.btncap2.Size = new System.Drawing.Size(109, 40);
            this.btncap2.TabIndex = 1;
            this.btncap2.Text = "Capture";
            this.btncap2.UseVisualStyleBackColor = false;
            this.btncap2.Click += new System.EventHandler(this.btncap2_Click);
            // 
            // thumbp2
            // 
            this.thumbp2.Location = new System.Drawing.Point(135, 151);
            this.thumbp2.Name = "thumbp2";
            this.thumbp2.Size = new System.Drawing.Size(109, 12);
            this.thumbp2.TabIndex = 52;
            // 
            // thumb1
            // 
            this.thumb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thumb1.Location = new System.Drawing.Point(9, 7);
            this.thumb1.Name = "thumb1";
            this.thumb1.Size = new System.Drawing.Size(109, 139);
            this.thumb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thumb1.TabIndex = 50;
            this.thumb1.TabStop = false;
            // 
            // btncap1
            // 
            this.btncap1.AutoEllipsis = true;
            this.btncap1.BackColor = System.Drawing.Color.SteelBlue;
            this.btncap1.FlatAppearance.BorderSize = 4;
            this.btncap1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncap1.ForeColor = System.Drawing.Color.White;
            this.btncap1.Location = new System.Drawing.Point(9, 170);
            this.btncap1.Name = "btncap1";
            this.btncap1.Size = new System.Drawing.Size(109, 40);
            this.btncap1.TabIndex = 0;
            this.btncap1.Text = "Capture";
            this.btncap1.UseVisualStyleBackColor = false;
            this.btncap1.Click += new System.EventHandler(this.btncap1_Click);
            // 
            // thumbp1
            // 
            this.thumbp1.Location = new System.Drawing.Point(9, 150);
            this.thumbp1.Name = "thumbp1";
            this.thumbp1.Size = new System.Drawing.Size(109, 12);
            this.thumbp1.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(205, 20);
            this.label7.TabIndex = 25;
            this.label7.Text = "Capture Thumb Impression:";
            // 
            // bsThumbs
            // 
            this.bsThumbs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bsThumbs.BorderRadius = 5;
            this.bsThumbs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bsThumbs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bsThumbs.Location = new System.Drawing.Point(240, 251);
            this.bsThumbs.Margin = new System.Windows.Forms.Padding(5);
            this.bsThumbs.Name = "bsThumbs";
            this.bsThumbs.Oncolor = System.Drawing.Color.Lime;
            this.bsThumbs.Onoffcolor = System.Drawing.Color.DarkGray;
            this.bsThumbs.Size = new System.Drawing.Size(51, 19);
            this.bsThumbs.TabIndex = 6;
            this.bsThumbs.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bsThumbs.Value = true;
            this.bsThumbs.Click += new System.EventHandler(this.bsThumbs_Click);
            // 
            // pPics
            // 
            this.pPics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pPics.Controls.Add(this.btnBrowse);
            this.pPics.Controls.Add(this.button2);
            this.pPics.Controls.Add(this.button1);
            this.pPics.Controls.Add(this.btnCamStart);
            this.pPics.Controls.Add(this.pictureBox2);
            this.pPics.Controls.Add(this.pictureBox1);
            this.pPics.Location = new System.Drawing.Point(472, 37);
            this.pPics.Name = "pPics";
            this.pPics.Size = new System.Drawing.Size(296, 462);
            this.pPics.TabIndex = 27;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(159, 230);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(121, 24);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Teal;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(16, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 24);
            this.button2.TabIndex = 2;
            this.button2.Text = "Capture";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Crimson;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(159, 200);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "Web cam Stop";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCamStart
            // 
            this.btnCamStart.BackColor = System.Drawing.Color.SlateBlue;
            this.btnCamStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCamStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCamStart.ForeColor = System.Drawing.Color.White;
            this.btnCamStart.Location = new System.Drawing.Point(16, 200);
            this.btnCamStart.Name = "btnCamStart";
            this.btnCamStart.Size = new System.Drawing.Size(121, 24);
            this.btnCamStart.TabIndex = 0;
            this.btnCamStart.Text = "Web cam Start";
            this.btnCamStart.UseVisualStyleBackColor = false;
            this.btnCamStart.Click += new System.EventHandler(this.btnCamStart_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(16, 264);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(264, 185);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(264, 185);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pMain.Controls.Add(this.panel3);
            this.pMain.Controls.Add(this.label8);
            this.pMain.Controls.Add(this.bsPics);
            this.pMain.Controls.Add(this.pPics);
            this.pMain.Controls.Add(this.bsThumbs);
            this.pMain.Controls.Add(this.label7);
            this.pMain.Controls.Add(this.pThumbs);
            this.pMain.Controls.Add(this.label1);
            this.pMain.Controls.Add(this.tbCNIC);
            this.pMain.Controls.Add(this.label6);
            this.pMain.Controls.Add(this.label5);
            this.pMain.Controls.Add(this.label4);
            this.pMain.Controls.Add(this.label3);
            this.pMain.Controls.Add(this.label2);
            this.pMain.Controls.Add(this.tbMisc);
            this.pMain.Controls.Add(this.tbRemarks);
            this.pMain.Controls.Add(this.tbContact);
            this.pMain.Controls.Add(this.tbAddress);
            this.pMain.Controls.Add(this.tbCompany);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.Location = new System.Drawing.Point(10, 49);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(783, 570);
            this.pMain.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Location = new System.Drawing.Point(6, 505);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(771, 59);
            this.panel3.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Salmon;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(412, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(165, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close Me!";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(249, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(157, 35);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(468, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 20);
            this.label8.TabIndex = 28;
            this.label8.Text = "Capture Picture:";
            // 
            // bsPics
            // 
            this.bsPics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bsPics.BorderRadius = 5;
            this.bsPics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bsPics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bsPics.Location = new System.Drawing.Point(599, 13);
            this.bsPics.Margin = new System.Windows.Forms.Padding(5);
            this.bsPics.Name = "bsPics";
            this.bsPics.Oncolor = System.Drawing.Color.Lime;
            this.bsPics.Onoffcolor = System.Drawing.Color.DarkGray;
            this.bsPics.Size = new System.Drawing.Size(51, 19);
            this.bsPics.TabIndex = 7;
            this.bsPics.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bsPics.Value = true;
            this.bsPics.Click += new System.EventHandler(this.bsPics_Click);
            // 
            // AddOilSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 631);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddOilSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Load);
            this.pHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            this.pThumbs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thumb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumb1)).EndInit();
            this.pPics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pHeader;
        private System.Windows.Forms.PictureBox picBtnIcon;
        private System.Windows.Forms.PictureBox picBtnClose;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Label label8;
        private Bunifu.Framework.UI.BunifuSwitch bsPics;
        private System.Windows.Forms.Panel pPics;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuSwitch bsThumbs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pThumbs;
        private System.Windows.Forms.PictureBox thumb2;
        private System.Windows.Forms.Button btncap2;
        private System.Windows.Forms.ProgressBar thumbp2;
        private System.Windows.Forms.PictureBox thumb1;
        private System.Windows.Forms.Button btncap1;
        private System.Windows.Forms.ProgressBar thumbp1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCNIC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMisc;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.TextBox tbContact;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox tbCompany;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCamStart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
    }
}