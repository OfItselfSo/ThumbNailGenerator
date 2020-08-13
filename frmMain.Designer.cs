/// +------------------------------------------------------------------------------------------------------------------------------+
/// ¦                                                   TERMS OF USE: MIT License                                                  ¦
/// +------------------------------------------------------------------------------------------------------------------------------¦
/// ¦Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation    ¦
/// ¦files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,    ¦
/// ¦modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software¦
/// ¦is furnished to do so, subject to the following conditions:                                                                   ¦
/// ¦                                                                                                                              ¦
/// ¦The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.¦
/// ¦                                                                                                                              ¦
/// ¦THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE          ¦
/// ¦WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR         ¦
/// ¦COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,   ¦
/// ¦ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                         ¦
/// +------------------------------------------------------------------------------------------------------------------------------+


namespace ThumbNailGenerator
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pictureBox600x600 = new System.Windows.Forms.PictureBox();
            this.pictureBoxScaled = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonGoBack = new System.Windows.Forms.Button();
            this.buttonSkipImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSetInputDirectory = new System.Windows.Forms.Button();
            this.textBoxInputDirectory = new System.Windows.Forms.TextBox();
            this.buttonGetImage = new System.Windows.Forms.Button();
            this.textBoxImageName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonVPNudge10 = new System.Windows.Forms.RadioButton();
            this.radioButtonVPNudge5 = new System.Windows.Forms.RadioButton();
            this.radioButtonVPNudge3 = new System.Windows.Forms.RadioButton();
            this.radioButtonVPNudge1 = new System.Windows.Forms.RadioButton();
            this.buttonVPLeft = new System.Windows.Forms.Button();
            this.buttonVPDown = new System.Windows.Forms.Button();
            this.buttonVPUp = new System.Windows.Forms.Button();
            this.buttonVPRight = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxScaleLevel = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonSaveAndDuplicateImage = new System.Windows.Forms.Button();
            this.checkBoxAutoLoadNext = new System.Windows.Forms.CheckBox();
            this.textBoxThumbnailImageSuffix = new System.Windows.Forms.TextBox();
            this.textBoxCroppedImageSuffix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSetOutputDirectory = new System.Windows.Forms.Button();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.buttonSaveImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox600x600)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScaled)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 646);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // pictureBox600x600
            // 
            this.pictureBox600x600.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox600x600.Location = new System.Drawing.Point(9, 12);
            this.pictureBox600x600.Name = "pictureBox600x600";
            this.pictureBox600x600.Size = new System.Drawing.Size(600, 600);
            this.pictureBox600x600.TabIndex = 1;
            this.pictureBox600x600.TabStop = false;
            // 
            // pictureBoxScaled
            // 
            this.pictureBoxScaled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxScaled.Location = new System.Drawing.Point(650, 12);
            this.pictureBoxScaled.Name = "pictureBoxScaled";
            this.pictureBoxScaled.Size = new System.Drawing.Size(302, 202);
            this.pictureBoxScaled.TabIndex = 3;
            this.pictureBoxScaled.TabStop = false;
            this.pictureBoxScaled.Click += new System.EventHandler(this.pictureBoxScaled_Click);
            this.pictureBoxScaled.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxScaled_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonGoBack);
            this.groupBox2.Controls.Add(this.buttonSkipImage);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonSetInputDirectory);
            this.groupBox2.Controls.Add(this.textBoxInputDirectory);
            this.groupBox2.Controls.Add(this.buttonGetImage);
            this.groupBox2.Location = new System.Drawing.Point(624, 384);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 91);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image Read";
            // 
            // buttonGoBack
            // 
            this.buttonGoBack.Location = new System.Drawing.Point(204, 59);
            this.buttonGoBack.Name = "buttonGoBack";
            this.buttonGoBack.Size = new System.Drawing.Size(75, 23);
            this.buttonGoBack.TabIndex = 85;
            this.buttonGoBack.Text = "Go Back";
            this.buttonGoBack.UseVisualStyleBackColor = true;
            this.buttonGoBack.Click += new System.EventHandler(this.buttonGoBack_Click);
            // 
            // buttonSkipImage
            // 
            this.buttonSkipImage.Location = new System.Drawing.Point(123, 59);
            this.buttonSkipImage.Name = "buttonSkipImage";
            this.buttonSkipImage.Size = new System.Drawing.Size(75, 23);
            this.buttonSkipImage.TabIndex = 84;
            this.buttonSkipImage.Text = "Skip Image";
            this.buttonSkipImage.UseVisualStyleBackColor = true;
            this.buttonSkipImage.Click += new System.EventHandler(this.buttonSkipImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Input Directory";
            // 
            // buttonSetInputDirectory
            // 
            this.buttonSetInputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetInputDirectory.Location = new System.Drawing.Point(293, 33);
            this.buttonSetInputDirectory.Name = "buttonSetInputDirectory";
            this.buttonSetInputDirectory.Size = new System.Drawing.Size(25, 20);
            this.buttonSetInputDirectory.TabIndex = 66;
            this.buttonSetInputDirectory.Text = "...";
            this.buttonSetInputDirectory.UseVisualStyleBackColor = true;
            this.buttonSetInputDirectory.Click += new System.EventHandler(this.buttonSetInputDirectory_Click);
            // 
            // textBoxInputDirectory
            // 
            this.textBoxInputDirectory.Location = new System.Drawing.Point(26, 33);
            this.textBoxInputDirectory.Name = "textBoxInputDirectory";
            this.textBoxInputDirectory.ReadOnly = true;
            this.textBoxInputDirectory.Size = new System.Drawing.Size(261, 20);
            this.textBoxInputDirectory.TabIndex = 64;
            // 
            // buttonGetImage
            // 
            this.buttonGetImage.Location = new System.Drawing.Point(36, 59);
            this.buttonGetImage.Name = "buttonGetImage";
            this.buttonGetImage.Size = new System.Drawing.Size(75, 23);
            this.buttonGetImage.TabIndex = 63;
            this.buttonGetImage.Text = "Get Image";
            this.buttonGetImage.UseVisualStyleBackColor = true;
            this.buttonGetImage.Click += new System.EventHandler(this.buttonGetImage_Click);
            // 
            // textBoxImageName
            // 
            this.textBoxImageName.Location = new System.Drawing.Point(84, 618);
            this.textBoxImageName.Name = "textBoxImageName";
            this.textBoxImageName.ReadOnly = true;
            this.textBoxImageName.Size = new System.Drawing.Size(525, 20);
            this.textBoxImageName.TabIndex = 65;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBoxScaleLevel);
            this.groupBox3.Location = new System.Drawing.Point(624, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(342, 154);
            this.groupBox3.TabIndex = 74;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image Scale and Crop";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonVPNudge10);
            this.groupBox1.Controls.Add(this.radioButtonVPNudge5);
            this.groupBox1.Controls.Add(this.radioButtonVPNudge3);
            this.groupBox1.Controls.Add(this.radioButtonVPNudge1);
            this.groupBox1.Controls.Add(this.buttonVPLeft);
            this.groupBox1.Controls.Add(this.buttonVPDown);
            this.groupBox1.Controls.Add(this.buttonVPUp);
            this.groupBox1.Controls.Add(this.buttonVPRight);
            this.groupBox1.Location = new System.Drawing.Point(38, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 106);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View Port Nudge";
            // 
            // radioButtonVPNudge10
            // 
            this.radioButtonVPNudge10.AutoSize = true;
            this.radioButtonVPNudge10.Location = new System.Drawing.Point(91, 78);
            this.radioButtonVPNudge10.Name = "radioButtonVPNudge10";
            this.radioButtonVPNudge10.Size = new System.Drawing.Size(37, 17);
            this.radioButtonVPNudge10.TabIndex = 79;
            this.radioButtonVPNudge10.Text = "10";
            this.radioButtonVPNudge10.UseVisualStyleBackColor = true;
            this.radioButtonVPNudge10.CheckedChanged += new System.EventHandler(this.radioButtonVPNudge10_CheckedChanged);
            // 
            // radioButtonVPNudge5
            // 
            this.radioButtonVPNudge5.AutoSize = true;
            this.radioButtonVPNudge5.Checked = true;
            this.radioButtonVPNudge5.Location = new System.Drawing.Point(91, 58);
            this.radioButtonVPNudge5.Name = "radioButtonVPNudge5";
            this.radioButtonVPNudge5.Size = new System.Drawing.Size(31, 17);
            this.radioButtonVPNudge5.TabIndex = 78;
            this.radioButtonVPNudge5.TabStop = true;
            this.radioButtonVPNudge5.Text = "5";
            this.radioButtonVPNudge5.UseVisualStyleBackColor = true;
            this.radioButtonVPNudge5.CheckedChanged += new System.EventHandler(this.radioButtonVPNudge5_CheckedChanged);
            // 
            // radioButtonVPNudge3
            // 
            this.radioButtonVPNudge3.AutoSize = true;
            this.radioButtonVPNudge3.Location = new System.Drawing.Point(91, 38);
            this.radioButtonVPNudge3.Name = "radioButtonVPNudge3";
            this.radioButtonVPNudge3.Size = new System.Drawing.Size(31, 17);
            this.radioButtonVPNudge3.TabIndex = 77;
            this.radioButtonVPNudge3.Text = "3";
            this.radioButtonVPNudge3.UseVisualStyleBackColor = true;
            this.radioButtonVPNudge3.CheckedChanged += new System.EventHandler(this.radioButtonVPNudge3_CheckedChanged);
            // 
            // radioButtonVPNudge1
            // 
            this.radioButtonVPNudge1.AutoSize = true;
            this.radioButtonVPNudge1.Location = new System.Drawing.Point(91, 18);
            this.radioButtonVPNudge1.Name = "radioButtonVPNudge1";
            this.radioButtonVPNudge1.Size = new System.Drawing.Size(31, 17);
            this.radioButtonVPNudge1.TabIndex = 76;
            this.radioButtonVPNudge1.Text = "1";
            this.radioButtonVPNudge1.UseVisualStyleBackColor = true;
            this.radioButtonVPNudge1.CheckedChanged += new System.EventHandler(this.radioButtonVPNudge1_CheckedChanged);
            // 
            // buttonVPLeft
            // 
            this.buttonVPLeft.Location = new System.Drawing.Point(14, 45);
            this.buttonVPLeft.Name = "buttonVPLeft";
            this.buttonVPLeft.Size = new System.Drawing.Size(25, 25);
            this.buttonVPLeft.TabIndex = 75;
            this.buttonVPLeft.Text = "<";
            this.buttonVPLeft.UseVisualStyleBackColor = true;
            this.buttonVPLeft.Click += new System.EventHandler(this.buttonVPLeft_Click);
            // 
            // buttonVPDown
            // 
            this.buttonVPDown.Location = new System.Drawing.Point(37, 68);
            this.buttonVPDown.Name = "buttonVPDown";
            this.buttonVPDown.Size = new System.Drawing.Size(25, 25);
            this.buttonVPDown.TabIndex = 74;
            this.buttonVPDown.Text = "v";
            this.buttonVPDown.UseVisualStyleBackColor = true;
            this.buttonVPDown.Click += new System.EventHandler(this.buttonVPDown_Click);
            // 
            // buttonVPUp
            // 
            this.buttonVPUp.Location = new System.Drawing.Point(37, 21);
            this.buttonVPUp.Name = "buttonVPUp";
            this.buttonVPUp.Size = new System.Drawing.Size(25, 25);
            this.buttonVPUp.TabIndex = 73;
            this.buttonVPUp.Text = "^";
            this.buttonVPUp.UseVisualStyleBackColor = true;
            this.buttonVPUp.Click += new System.EventHandler(this.buttonVPUp_Click);
            // 
            // buttonVPRight
            // 
            this.buttonVPRight.Location = new System.Drawing.Point(60, 45);
            this.buttonVPRight.Name = "buttonVPRight";
            this.buttonVPRight.Size = new System.Drawing.Size(25, 25);
            this.buttonVPRight.TabIndex = 72;
            this.buttonVPRight.Text = ">";
            this.buttonVPRight.UseVisualStyleBackColor = true;
            this.buttonVPRight.Click += new System.EventHandler(this.buttonVPRight_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 74;
            this.label2.Text = "Scale Factor";
            // 
            // comboBoxScaleLevel
            // 
            this.comboBoxScaleLevel.FormattingEnabled = true;
            this.comboBoxScaleLevel.Location = new System.Drawing.Point(205, 72);
            this.comboBoxScaleLevel.Name = "comboBoxScaleLevel";
            this.comboBoxScaleLevel.Size = new System.Drawing.Size(83, 21);
            this.comboBoxScaleLevel.TabIndex = 73;
            this.comboBoxScaleLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxScaleLevel_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonSaveAndDuplicateImage);
            this.groupBox4.Controls.Add(this.checkBoxAutoLoadNext);
            this.groupBox4.Controls.Add(this.textBoxThumbnailImageSuffix);
            this.groupBox4.Controls.Add(this.textBoxCroppedImageSuffix);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.buttonSetOutputDirectory);
            this.groupBox4.Controls.Add(this.textBoxOutputDirectory);
            this.groupBox4.Controls.Add(this.buttonSaveImage);
            this.groupBox4.Location = new System.Drawing.Point(624, 480);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(342, 158);
            this.groupBox4.TabIndex = 75;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image Write";
            // 
            // buttonSaveAndDuplicateImage
            // 
            this.buttonSaveAndDuplicateImage.Location = new System.Drawing.Point(27, 104);
            this.buttonSaveAndDuplicateImage.Name = "buttonSaveAndDuplicateImage";
            this.buttonSaveAndDuplicateImage.Size = new System.Drawing.Size(109, 31);
            this.buttonSaveAndDuplicateImage.TabIndex = 84;
            this.buttonSaveAndDuplicateImage.Text = "Save+Dup Image";
            this.buttonSaveAndDuplicateImage.UseVisualStyleBackColor = true;
            this.buttonSaveAndDuplicateImage.Click += new System.EventHandler(this.buttonSaveAndDuplicateImage_Click);
            // 
            // checkBoxAutoLoadNext
            // 
            this.checkBoxAutoLoadNext.AutoSize = true;
            this.checkBoxAutoLoadNext.Checked = true;
            this.checkBoxAutoLoadNext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoLoadNext.Location = new System.Drawing.Point(142, 70);
            this.checkBoxAutoLoadNext.Name = "checkBoxAutoLoadNext";
            this.checkBoxAutoLoadNext.Size = new System.Drawing.Size(135, 17);
            this.checkBoxAutoLoadNext.TabIndex = 83;
            this.checkBoxAutoLoadNext.Text = "Auto load next on save";
            this.checkBoxAutoLoadNext.UseVisualStyleBackColor = true;
            // 
            // textBoxThumbnailImageSuffix
            // 
            this.textBoxThumbnailImageSuffix.Location = new System.Drawing.Point(161, 127);
            this.textBoxThumbnailImageSuffix.Name = "textBoxThumbnailImageSuffix";
            this.textBoxThumbnailImageSuffix.Size = new System.Drawing.Size(62, 20);
            this.textBoxThumbnailImageSuffix.TabIndex = 82;
            // 
            // textBoxCroppedImageSuffix
            // 
            this.textBoxCroppedImageSuffix.Location = new System.Drawing.Point(161, 101);
            this.textBoxCroppedImageSuffix.Name = "textBoxCroppedImageSuffix";
            this.textBoxCroppedImageSuffix.Size = new System.Drawing.Size(62, 20);
            this.textBoxCroppedImageSuffix.TabIndex = 81;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Thumbnail Image Suffix";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Cropped Image Suffix";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 78;
            this.label4.Text = "Output Directory";
            // 
            // buttonSetOutputDirectory
            // 
            this.buttonSetOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetOutputDirectory.Location = new System.Drawing.Point(293, 36);
            this.buttonSetOutputDirectory.Name = "buttonSetOutputDirectory";
            this.buttonSetOutputDirectory.Size = new System.Drawing.Size(25, 20);
            this.buttonSetOutputDirectory.TabIndex = 77;
            this.buttonSetOutputDirectory.Text = "...";
            this.buttonSetOutputDirectory.UseVisualStyleBackColor = true;
            this.buttonSetOutputDirectory.Click += new System.EventHandler(this.buttonSetOutputDirectory_Click);
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(26, 36);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.ReadOnly = true;
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(261, 20);
            this.textBoxOutputDirectory.TabIndex = 76;
            // 
            // buttonSaveImage
            // 
            this.buttonSaveImage.Location = new System.Drawing.Point(27, 66);
            this.buttonSaveImage.Name = "buttonSaveImage";
            this.buttonSaveImage.Size = new System.Drawing.Size(109, 31);
            this.buttonSaveImage.TabIndex = 64;
            this.buttonSaveImage.Text = "Save Image";
            this.buttonSaveImage.UseVisualStyleBackColor = true;
            this.buttonSaveImage.Click += new System.EventHandler(this.buttonSaveImage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 621);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "Current Image:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 646);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.textBoxImageName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBoxScaled);
            this.Controls.Add(this.pictureBox600x600);
            this.Controls.Add(this.splitter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ThumbNailGenerator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox600x600)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScaled)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.PictureBox pictureBox600x600;
        private System.Windows.Forms.PictureBox pictureBoxScaled;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSetInputDirectory;
        private System.Windows.Forms.TextBox textBoxImageName;
        private System.Windows.Forms.TextBox textBoxInputDirectory;
        private System.Windows.Forms.Button buttonGetImage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonVPNudge10;
        private System.Windows.Forms.RadioButton radioButtonVPNudge5;
        private System.Windows.Forms.RadioButton radioButtonVPNudge3;
        private System.Windows.Forms.RadioButton radioButtonVPNudge1;
        private System.Windows.Forms.Button buttonVPLeft;
        private System.Windows.Forms.Button buttonVPDown;
        private System.Windows.Forms.Button buttonVPUp;
        private System.Windows.Forms.Button buttonVPRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxScaleLevel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSetOutputDirectory;
        private System.Windows.Forms.TextBox textBoxOutputDirectory;
        private System.Windows.Forms.Button buttonSaveImage;
        private System.Windows.Forms.Button buttonSkipImage;
        private System.Windows.Forms.CheckBox checkBoxAutoLoadNext;
        private System.Windows.Forms.TextBox textBoxThumbnailImageSuffix;
        private System.Windows.Forms.TextBox textBoxCroppedImageSuffix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonGoBack;
        private System.Windows.Forms.Button buttonSaveAndDuplicateImage;
    }
}

