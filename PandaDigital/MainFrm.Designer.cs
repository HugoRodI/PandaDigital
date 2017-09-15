namespace PandaDigital
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.zoomedImgBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.deletePrevBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.deleteAllBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.y2RadioButton = new System.Windows.Forms.RadioButton();
            this.y1RadioButton = new System.Windows.Forms.RadioButton();
            this.y2TextBox = new System.Windows.Forms.TextBox();
            this.y1TextBox = new System.Windows.Forms.TextBox();
            this.x2RadioButton = new System.Windows.Forms.RadioButton();
            this.x1RadioButton = new System.Windows.Forms.RadioButton();
            this.x2TextBox = new System.Windows.Forms.TextBox();
            this.x1TextBox = new System.Windows.Forms.TextBox();
            this.userRadioBtn = new System.Windows.Forms.RadioButton();
            this.axisRadioBtn = new System.Windows.Forms.RadioButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.logRadioButtonY = new System.Windows.Forms.RadioButton();
            this.linearRadioButtonY = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.logRadioButtonX = new System.Windows.Forms.RadioButton();
            this.linearRadioButtonX = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomedImgBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMainMenu,
            this.helpMainMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1193, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileMainMenu
            // 
            this.fileMainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageMainMenu});
            this.fileMainMenu.Name = "fileMainMenu";
            this.fileMainMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMainMenu.Text = "File";
            // 
            // loadImageMainMenu
            // 
            this.loadImageMainMenu.Name = "loadImageMainMenu";
            this.loadImageMainMenu.Size = new System.Drawing.Size(136, 22);
            this.loadImageMainMenu.Text = "Load Image";
            this.loadImageMainMenu.Click += new System.EventHandler(this.loadImageMainMenu_Click);
            // 
            // helpMainMenu
            // 
            this.helpMainMenu.Name = "helpMainMenu";
            this.helpMainMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMainMenu.Text = "Help";
            // 
            // imgBox
            // 
            this.imgBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.imgBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.imgBox.Location = new System.Drawing.Point(6, 19);
            this.imgBox.Name = "imgBox";
            this.imgBox.Size = new System.Drawing.Size(844, 591);
            this.imgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgBox.TabIndex = 1;
            this.imgBox.TabStop = false;
            this.imgBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.imgBox_DragDrop);
            this.imgBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.imgBox_DragEnter);
            this.imgBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imgBox_Paint);
            this.imgBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBox_MouseDown);
            this.imgBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgBox_MouseMove);
            // 
            // zoomedImgBox
            // 
            this.zoomedImgBox.BackColor = System.Drawing.Color.Transparent;
            this.zoomedImgBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomedImgBox.BackgroundImage")));
            this.zoomedImgBox.Location = new System.Drawing.Point(4, 19);
            this.zoomedImgBox.Name = "zoomedImgBox";
            this.zoomedImgBox.Size = new System.Drawing.Size(300, 300);
            this.zoomedImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.zoomedImgBox.TabIndex = 2;
            this.zoomedImgBox.TabStop = false;
            this.zoomedImgBox.Paint += new System.Windows.Forms.PaintEventHandler(this.zoomedImgBox_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imgBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(856, 616);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.zoomedImgBox);
            this.groupBox2.Location = new System.Drawing.Point(870, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 326);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zoomed image:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // deletePrevBtn
            // 
            this.deletePrevBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deletePrevBtn.Location = new System.Drawing.Point(6, 18);
            this.deletePrevBtn.Name = "deletePrevBtn";
            this.deletePrevBtn.Size = new System.Drawing.Size(75, 38);
            this.deletePrevBtn.TabIndex = 5;
            this.deletePrevBtn.Text = "Delete last";
            this.deletePrevBtn.UseVisualStyleBackColor = true;
            this.deletePrevBtn.Click += new System.EventHandler(this.deletePrevBtn_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportBtn.Location = new System.Drawing.Point(4, 18);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(75, 38);
            this.exportBtn.TabIndex = 6;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // deleteAllBtn
            // 
            this.deleteAllBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteAllBtn.Location = new System.Drawing.Point(5, 18);
            this.deleteAllBtn.Name = "deleteAllBtn";
            this.deleteAllBtn.Size = new System.Drawing.Size(75, 38);
            this.deleteAllBtn.TabIndex = 7;
            this.deleteAllBtn.Text = "Delete all";
            this.deleteAllBtn.UseVisualStyleBackColor = true;
            this.deleteAllBtn.Click += new System.EventHandler(this.deleteAllBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.y2RadioButton);
            this.groupBox3.Controls.Add(this.y1RadioButton);
            this.groupBox3.Controls.Add(this.y2TextBox);
            this.groupBox3.Controls.Add(this.y1TextBox);
            this.groupBox3.Controls.Add(this.x2RadioButton);
            this.groupBox3.Controls.Add(this.x1RadioButton);
            this.groupBox3.Controls.Add(this.x2TextBox);
            this.groupBox3.Controls.Add(this.x1TextBox);
            this.groupBox3.Location = new System.Drawing.Point(870, 543);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(311, 108);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Axis coordinates";
            // 
            // y2RadioButton
            // 
            this.y2RadioButton.AutoSize = true;
            this.y2RadioButton.Location = new System.Drawing.Point(164, 74);
            this.y2RadioButton.Name = "y2RadioButton";
            this.y2RadioButton.Size = new System.Drawing.Size(55, 17);
            this.y2RadioButton.TabIndex = 11;
            this.y2RadioButton.Text = "Set y2";
            this.y2RadioButton.UseVisualStyleBackColor = true;
            this.y2RadioButton.CheckedChanged += new System.EventHandler(this.y2RadioButton_CheckedChanged);
            // 
            // y1RadioButton
            // 
            this.y1RadioButton.AutoSize = true;
            this.y1RadioButton.Location = new System.Drawing.Point(164, 34);
            this.y1RadioButton.Name = "y1RadioButton";
            this.y1RadioButton.Size = new System.Drawing.Size(55, 17);
            this.y1RadioButton.TabIndex = 10;
            this.y1RadioButton.Text = "Set y1";
            this.y1RadioButton.UseVisualStyleBackColor = true;
            this.y1RadioButton.CheckedChanged += new System.EventHandler(this.y1RadioButton_CheckedChanged);
            // 
            // y2TextBox
            // 
            this.y2TextBox.Enabled = false;
            this.y2TextBox.Location = new System.Drawing.Point(225, 74);
            this.y2TextBox.Name = "y2TextBox";
            this.y2TextBox.Size = new System.Drawing.Size(78, 20);
            this.y2TextBox.TabIndex = 9;
            // 
            // y1TextBox
            // 
            this.y1TextBox.Enabled = false;
            this.y1TextBox.Location = new System.Drawing.Point(225, 33);
            this.y1TextBox.Name = "y1TextBox";
            this.y1TextBox.Size = new System.Drawing.Size(78, 20);
            this.y1TextBox.TabIndex = 8;
            // 
            // x2RadioButton
            // 
            this.x2RadioButton.AutoSize = true;
            this.x2RadioButton.Location = new System.Drawing.Point(7, 75);
            this.x2RadioButton.Name = "x2RadioButton";
            this.x2RadioButton.Size = new System.Drawing.Size(55, 17);
            this.x2RadioButton.TabIndex = 7;
            this.x2RadioButton.Text = "Set x2";
            this.x2RadioButton.UseVisualStyleBackColor = true;
            this.x2RadioButton.CheckedChanged += new System.EventHandler(this.x2RadioButton_CheckedChanged);
            // 
            // x1RadioButton
            // 
            this.x1RadioButton.AutoSize = true;
            this.x1RadioButton.Checked = true;
            this.x1RadioButton.Location = new System.Drawing.Point(6, 33);
            this.x1RadioButton.Name = "x1RadioButton";
            this.x1RadioButton.Size = new System.Drawing.Size(55, 17);
            this.x1RadioButton.TabIndex = 6;
            this.x1RadioButton.TabStop = true;
            this.x1RadioButton.Text = "Set x1";
            this.x1RadioButton.UseVisualStyleBackColor = true;
            this.x1RadioButton.CheckedChanged += new System.EventHandler(this.x1RadioButton_CheckedChanged);
            // 
            // x2TextBox
            // 
            this.x2TextBox.Enabled = false;
            this.x2TextBox.Location = new System.Drawing.Point(68, 74);
            this.x2TextBox.Name = "x2TextBox";
            this.x2TextBox.Size = new System.Drawing.Size(78, 20);
            this.x2TextBox.TabIndex = 5;
            // 
            // x1TextBox
            // 
            this.x1TextBox.Location = new System.Drawing.Point(68, 33);
            this.x1TextBox.Name = "x1TextBox";
            this.x1TextBox.Size = new System.Drawing.Size(78, 20);
            this.x1TextBox.TabIndex = 4;
            // 
            // userRadioBtn
            // 
            this.userRadioBtn.AutoSize = true;
            this.userRadioBtn.Location = new System.Drawing.Point(12, 21);
            this.userRadioBtn.Name = "userRadioBtn";
            this.userRadioBtn.Size = new System.Drawing.Size(78, 17);
            this.userRadioBtn.TabIndex = 9;
            this.userRadioBtn.Text = "User points";
            this.userRadioBtn.UseVisualStyleBackColor = true;
            this.userRadioBtn.CheckedChanged += new System.EventHandler(this.userRadioBtn_CheckedChanged);
            // 
            // axisRadioBtn
            // 
            this.axisRadioBtn.AutoSize = true;
            this.axisRadioBtn.Checked = true;
            this.axisRadioBtn.Location = new System.Drawing.Point(12, 52);
            this.axisRadioBtn.Name = "axisRadioBtn";
            this.axisRadioBtn.Size = new System.Drawing.Size(75, 17);
            this.axisRadioBtn.TabIndex = 10;
            this.axisRadioBtn.TabStop = true;
            this.axisRadioBtn.Text = "Axis points";
            this.axisRadioBtn.UseVisualStyleBackColor = true;
            this.axisRadioBtn.CheckedChanged += new System.EventHandler(this.axisRadioBtn_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.axisRadioBtn);
            this.groupBox6.Controls.Add(this.userRadioBtn);
            this.groupBox6.Location = new System.Drawing.Point(874, 441);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(96, 81);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Points";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Location = new System.Drawing.Point(977, 441);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(204, 102);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Axis Scale";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.logRadioButtonY);
            this.groupBox7.Controls.Add(this.linearRadioButtonY);
            this.groupBox7.Location = new System.Drawing.Point(102, 20);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(95, 76);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Y";
            // 
            // logRadioButtonY
            // 
            this.logRadioButtonY.AutoSize = true;
            this.logRadioButtonY.Location = new System.Drawing.Point(8, 48);
            this.logRadioButtonY.Name = "logRadioButtonY";
            this.logRadioButtonY.Size = new System.Drawing.Size(79, 17);
            this.logRadioButtonY.TabIndex = 2;
            this.logRadioButtonY.Text = "Logarithmic";
            this.logRadioButtonY.UseVisualStyleBackColor = true;
            // 
            // linearRadioButtonY
            // 
            this.linearRadioButtonY.AutoSize = true;
            this.linearRadioButtonY.Checked = true;
            this.linearRadioButtonY.Location = new System.Drawing.Point(8, 23);
            this.linearRadioButtonY.Name = "linearRadioButtonY";
            this.linearRadioButtonY.Size = new System.Drawing.Size(54, 17);
            this.linearRadioButtonY.TabIndex = 1;
            this.linearRadioButtonY.TabStop = true;
            this.linearRadioButtonY.Text = "Linear";
            this.linearRadioButtonY.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.logRadioButtonX);
            this.groupBox5.Controls.Add(this.linearRadioButtonX);
            this.groupBox5.Location = new System.Drawing.Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(89, 76);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "X";
            // 
            // logRadioButtonX
            // 
            this.logRadioButtonX.AutoSize = true;
            this.logRadioButtonX.Location = new System.Drawing.Point(6, 48);
            this.logRadioButtonX.Name = "logRadioButtonX";
            this.logRadioButtonX.Size = new System.Drawing.Size(79, 17);
            this.logRadioButtonX.TabIndex = 1;
            this.logRadioButtonX.Text = "Logarithmic";
            this.logRadioButtonX.UseVisualStyleBackColor = true;
            // 
            // linearRadioButtonX
            // 
            this.linearRadioButtonX.AutoSize = true;
            this.linearRadioButtonX.Checked = true;
            this.linearRadioButtonX.Location = new System.Drawing.Point(6, 23);
            this.linearRadioButtonX.Name = "linearRadioButtonX";
            this.linearRadioButtonX.Size = new System.Drawing.Size(54, 17);
            this.linearRadioButtonX.TabIndex = 0;
            this.linearRadioButtonX.TabStop = true;
            this.linearRadioButtonX.Text = "Linear";
            this.linearRadioButtonX.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.exportBtn);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(1095, 369);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(86, 63);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Ctrl+S";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.deleteAllBtn);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(986, 369);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(86, 63);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Ctrl+A";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.deletePrevBtn);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(878, 369);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(86, 63);
            this.groupBox10.TabIndex = 17;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Ctrl+D";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 665);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PandaDigital";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrm_KeyDown);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomedImgBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMainMenu;
        private System.Windows.Forms.ToolStripMenuItem loadImageMainMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMainMenu;
        private System.Windows.Forms.PictureBox imgBox;
        private System.Windows.Forms.PictureBox zoomedImgBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button deletePrevBtn;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.Button deleteAllBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton userRadioBtn;
        private System.Windows.Forms.RadioButton axisRadioBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton y2RadioButton;
        private System.Windows.Forms.RadioButton y1RadioButton;
        private System.Windows.Forms.TextBox y2TextBox;
        private System.Windows.Forms.TextBox y1TextBox;
        private System.Windows.Forms.RadioButton x2RadioButton;
        private System.Windows.Forms.RadioButton x1RadioButton;
        private System.Windows.Forms.TextBox x2TextBox;
        private System.Windows.Forms.TextBox x1TextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton logRadioButtonY;
        private System.Windows.Forms.RadioButton linearRadioButtonY;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton logRadioButtonX;
        private System.Windows.Forms.RadioButton linearRadioButtonX;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox10;
    }
}

