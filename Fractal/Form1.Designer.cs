namespace Fractal
{
    partial class Form1
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
            this.OriginalImagePanel = new System.Windows.Forms.Panel();
            this.DecodedImagePanel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LoadBmpButton = new System.Windows.Forms.Button();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadInitialButton = new System.Windows.Forms.Button();
            this.LoadFrcButton = new System.Windows.Forms.Button();
            this.SaveDecodedButton = new System.Windows.Forms.Button();
            this.DecodeNbStepsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NbOfStepsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.PSNRValueLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.xdvalue = new System.Windows.Forms.Label();
            this.ydValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.izoValue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.scaleValue = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.offsetValue = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NbOfStepsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImagePanel
            // 
            this.OriginalImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OriginalImagePanel.Location = new System.Drawing.Point(12, 12);
            this.OriginalImagePanel.Name = "OriginalImagePanel";
            this.OriginalImagePanel.Size = new System.Drawing.Size(512, 512);
            this.OriginalImagePanel.TabIndex = 0;
            this.OriginalImagePanel.Click += new System.EventHandler(this.OriginalImagePanel_Click);
            // 
            // DecodedImagePanel
            // 
            this.DecodedImagePanel.Location = new System.Drawing.Point(549, 12);
            this.DecodedImagePanel.Name = "DecodedImagePanel";
            this.DecodedImagePanel.Size = new System.Drawing.Size(512, 512);
            this.DecodedImagePanel.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 530);
            this.progressBar1.Maximum = 4096;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1049, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // LoadBmpButton
            // 
            this.LoadBmpButton.Location = new System.Drawing.Point(13, 560);
            this.LoadBmpButton.Name = "LoadBmpButton";
            this.LoadBmpButton.Size = new System.Drawing.Size(75, 23);
            this.LoadBmpButton.TabIndex = 3;
            this.LoadBmpButton.Text = "Load";
            this.LoadBmpButton.UseVisualStyleBackColor = true;
            this.LoadBmpButton.Click += new System.EventHandler(this.LoadBmpButton_Click);
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(13, 589);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessButton.TabIndex = 4;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(13, 618);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadInitialButton
            // 
            this.LoadInitialButton.Location = new System.Drawing.Point(549, 559);
            this.LoadInitialButton.Name = "LoadInitialButton";
            this.LoadInitialButton.Size = new System.Drawing.Size(75, 23);
            this.LoadInitialButton.TabIndex = 6;
            this.LoadInitialButton.Text = "Load initial";
            this.LoadInitialButton.UseVisualStyleBackColor = true;
            this.LoadInitialButton.Click += new System.EventHandler(this.LoadInitialButton_Click);
            // 
            // LoadFrcButton
            // 
            this.LoadFrcButton.Location = new System.Drawing.Point(666, 559);
            this.LoadFrcButton.Name = "LoadFrcButton";
            this.LoadFrcButton.Size = new System.Drawing.Size(75, 23);
            this.LoadFrcButton.TabIndex = 7;
            this.LoadFrcButton.Text = "Load";
            this.LoadFrcButton.UseVisualStyleBackColor = true;
            this.LoadFrcButton.Click += new System.EventHandler(this.LoadFrcButton_Click);
            // 
            // SaveDecodedButton
            // 
            this.SaveDecodedButton.Location = new System.Drawing.Point(771, 559);
            this.SaveDecodedButton.Name = "SaveDecodedButton";
            this.SaveDecodedButton.Size = new System.Drawing.Size(75, 23);
            this.SaveDecodedButton.TabIndex = 8;
            this.SaveDecodedButton.Text = "Save";
            this.SaveDecodedButton.UseVisualStyleBackColor = true;
            // 
            // DecodeNbStepsButton
            // 
            this.DecodeNbStepsButton.Location = new System.Drawing.Point(666, 588);
            this.DecodeNbStepsButton.Name = "DecodeNbStepsButton";
            this.DecodeNbStepsButton.Size = new System.Drawing.Size(75, 23);
            this.DecodeNbStepsButton.TabIndex = 9;
            this.DecodeNbStepsButton.Text = "Decode number of steps";
            this.DecodeNbStepsButton.UseVisualStyleBackColor = true;
            this.DecodeNbStepsButton.Click += new System.EventHandler(this.DecodeNbStepsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(550, 594);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Nb of steps";
            // 
            // NbOfStepsNumericUpDown
            // 
            this.NbOfStepsNumericUpDown.Location = new System.Drawing.Point(617, 589);
            this.NbOfStepsNumericUpDown.Name = "NbOfStepsNumericUpDown";
            this.NbOfStepsNumericUpDown.Size = new System.Drawing.Size(43, 20);
            this.NbOfStepsNumericUpDown.TabIndex = 11;
            this.NbOfStepsNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(878, 569);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "PSNR";
            // 
            // PSNRValueLabel
            // 
            this.PSNRValueLabel.AutoSize = true;
            this.PSNRValueLabel.Location = new System.Drawing.Point(933, 565);
            this.PSNRValueLabel.Name = "PSNRValueLabel";
            this.PSNRValueLabel.Size = new System.Drawing.Size(0, 13);
            this.PSNRValueLabel.TabIndex = 13;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 569);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "xd";
            // 
            // xdvalue
            // 
            this.xdvalue.AutoSize = true;
            this.xdvalue.Location = new System.Drawing.Point(196, 570);
            this.xdvalue.Name = "xdvalue";
            this.xdvalue.Size = new System.Drawing.Size(45, 13);
            this.xdvalue.TabIndex = 15;
            this.xdvalue.Text = "xdValue";
            // 
            // ydValue
            // 
            this.ydValue.AutoSize = true;
            this.ydValue.Location = new System.Drawing.Point(196, 589);
            this.ydValue.Name = "ydValue";
            this.ydValue.Size = new System.Drawing.Size(35, 13);
            this.ydValue.TabIndex = 17;
            this.ydValue.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 588);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "yd";
            // 
            // izoValue
            // 
            this.izoValue.AutoSize = true;
            this.izoValue.Location = new System.Drawing.Point(196, 611);
            this.izoValue.Name = "izoValue";
            this.izoValue.Size = new System.Drawing.Size(35, 13);
            this.izoValue.TabIndex = 19;
            this.izoValue.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(155, 610);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "izo";
            // 
            // scaleValue
            // 
            this.scaleValue.AutoSize = true;
            this.scaleValue.Location = new System.Drawing.Point(345, 570);
            this.scaleValue.Name = "scaleValue";
            this.scaleValue.Size = new System.Drawing.Size(35, 13);
            this.scaleValue.TabIndex = 21;
            this.scaleValue.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(304, 569);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "scale";
            // 
            // offsetValue
            // 
            this.offsetValue.AutoSize = true;
            this.offsetValue.Location = new System.Drawing.Point(345, 594);
            this.offsetValue.Name = "offsetValue";
            this.offsetValue.Size = new System.Drawing.Size(41, 13);
            this.offsetValue.TabIndex = 23;
            this.offsetValue.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(304, 593);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "offset";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 632);
            this.Controls.Add(this.offsetValue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.scaleValue);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.izoValue);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ydValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.xdvalue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PSNRValueLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NbOfStepsNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DecodeNbStepsButton);
            this.Controls.Add(this.SaveDecodedButton);
            this.Controls.Add(this.LoadFrcButton);
            this.Controls.Add(this.LoadInitialButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.LoadBmpButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.DecodedImagePanel);
            this.Controls.Add(this.OriginalImagePanel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.NbOfStepsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel OriginalImagePanel;
        private System.Windows.Forms.Panel DecodedImagePanel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button LoadBmpButton;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadInitialButton;
        private System.Windows.Forms.Button LoadFrcButton;
        private System.Windows.Forms.Button SaveDecodedButton;
        private System.Windows.Forms.Button DecodeNbStepsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NbOfStepsNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PSNRValueLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label xdvalue;
        private System.Windows.Forms.Label ydValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label izoValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label scaleValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label offsetValue;
        private System.Windows.Forms.Label label12;
    }
}

