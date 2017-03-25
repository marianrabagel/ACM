namespace ACM
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.KNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.StatisticModelListBox = new System.Windows.Forms.ListBox();
            this.PredictionRulesListBox = new System.Windows.Forms.ListBox();
            this.SaveOriginalBtn = new System.Windows.Forms.Button();
            this.EncodeBtn = new System.Windows.Forms.Button();
            this.LoadOriginalBtn = new System.Windows.Forms.Button();
            this.OriginalImage = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ErrorImage = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SaveDecodedBtn = new System.Windows.Forms.Button();
            this.DecodedImage = new System.Windows.Forms.Panel();
            this.DecodeBtn = new System.Windows.Forms.Button();
            this.LoadDecodedBtn = new System.Windows.Forms.Button();
            this.ScaleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.ComputeErrorBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KNumericUpDown)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Encode Arithmetic";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Decode Arithmetic";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 78);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Aritmethic coder/decoder";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.KNumericUpDown);
            this.panel2.Controls.Add(this.StatisticModelListBox);
            this.panel2.Controls.Add(this.PredictionRulesListBox);
            this.panel2.Controls.Add(this.SaveOriginalBtn);
            this.panel2.Controls.Add(this.EncodeBtn);
            this.panel2.Controls.Add(this.LoadOriginalBtn);
            this.panel2.Controls.Add(this.OriginalImage);
            this.panel2.Location = new System.Drawing.Point(156, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 447);
            this.panel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "K-value";
            // 
            // KNumericUpDown
            // 
            this.KNumericUpDown.Location = new System.Drawing.Point(180, 300);
            this.KNumericUpDown.Name = "KNumericUpDown";
            this.KNumericUpDown.Size = new System.Drawing.Size(41, 20);
            this.KNumericUpDown.TabIndex = 8;
            this.KNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // StatisticModelListBox
            // 
            this.StatisticModelListBox.FormattingEnabled = true;
            this.StatisticModelListBox.Items.AddRange(new object[] {
            "Fixed 9/16/32",
            "Tabel",
            "Arithmetic"});
            this.StatisticModelListBox.Location = new System.Drawing.Point(168, 362);
            this.StatisticModelListBox.Name = "StatisticModelListBox";
            this.StatisticModelListBox.Size = new System.Drawing.Size(82, 43);
            this.StatisticModelListBox.TabIndex = 7;
            // 
            // PredictionRulesListBox
            // 
            this.PredictionRulesListBox.FormattingEnabled = true;
            this.PredictionRulesListBox.Items.AddRange(new object[] {
            "128",
            "A",
            "B",
            "C",
            "A+B-C",
            "A+(B-C)/2",
            "B + (A-C)/2",
            "(A+B)/2",
            "JPEG-LS"});
            this.PredictionRulesListBox.Location = new System.Drawing.Point(81, 284);
            this.PredictionRulesListBox.Name = "PredictionRulesListBox";
            this.PredictionRulesListBox.Size = new System.Drawing.Size(82, 121);
            this.PredictionRulesListBox.TabIndex = 6;
            // 
            // SaveOriginalBtn
            // 
            this.SaveOriginalBtn.Location = new System.Drawing.Point(0, 348);
            this.SaveOriginalBtn.Name = "SaveOriginalBtn";
            this.SaveOriginalBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveOriginalBtn.TabIndex = 3;
            this.SaveOriginalBtn.Text = "Save";
            this.SaveOriginalBtn.UseVisualStyleBackColor = true;
            // 
            // EncodeBtn
            // 
            this.EncodeBtn.Location = new System.Drawing.Point(0, 319);
            this.EncodeBtn.Name = "EncodeBtn";
            this.EncodeBtn.Size = new System.Drawing.Size(75, 23);
            this.EncodeBtn.TabIndex = 2;
            this.EncodeBtn.Text = "Encode";
            this.EncodeBtn.UseVisualStyleBackColor = true;
            // 
            // LoadOriginalBtn
            // 
            this.LoadOriginalBtn.Location = new System.Drawing.Point(0, 290);
            this.LoadOriginalBtn.Name = "LoadOriginalBtn";
            this.LoadOriginalBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadOriginalBtn.TabIndex = 1;
            this.LoadOriginalBtn.Text = "Load";
            this.LoadOriginalBtn.UseVisualStyleBackColor = true;
            // 
            // OriginalImage
            // 
            this.OriginalImage.Location = new System.Drawing.Point(3, 3);
            this.OriginalImage.Name = "OriginalImage";
            this.OriginalImage.Size = new System.Drawing.Size(256, 256);
            this.OriginalImage.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ComputeErrorBtn);
            this.panel3.Controls.Add(this.listBox2);
            this.panel3.Controls.Add(this.listBox1);
            this.panel3.Controls.Add(this.RefreshBtn);
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.ErrorImage);
            this.panel3.Location = new System.Drawing.Point(507, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 447);
            this.panel3.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1091, 424);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Refresh";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(261, 232);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(75, 23);
            this.RefreshBtn.TabIndex = 11;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(276, 206);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(41, 20);
            this.numericUpDown1.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(127, 300);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(451, 147);
            this.panel5.TabIndex = 2;
            // 
            // ErrorImage
            // 
            this.ErrorImage.Location = new System.Drawing.Point(3, 3);
            this.ErrorImage.Name = "ErrorImage";
            this.ErrorImage.Size = new System.Drawing.Size(256, 256);
            this.ErrorImage.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.DecodedImage);
            this.panel4.Controls.Add(this.SaveDecodedBtn);
            this.panel4.Controls.Add(this.DecodeBtn);
            this.panel4.Controls.Add(this.LoadDecodedBtn);
            this.panel4.Location = new System.Drawing.Point(858, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(345, 294);
            this.panel4.TabIndex = 5;
            // 
            // SaveDecodedBtn
            // 
            this.SaveDecodedBtn.Location = new System.Drawing.Point(165, 268);
            this.SaveDecodedBtn.Name = "SaveDecodedBtn";
            this.SaveDecodedBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveDecodedBtn.TabIndex = 12;
            this.SaveDecodedBtn.Text = "Save";
            this.SaveDecodedBtn.UseVisualStyleBackColor = true;
            // 
            // DecodedImage
            // 
            this.DecodedImage.Location = new System.Drawing.Point(3, 6);
            this.DecodedImage.Name = "DecodedImage";
            this.DecodedImage.Size = new System.Drawing.Size(256, 256);
            this.DecodedImage.TabIndex = 1;
            // 
            // DecodeBtn
            // 
            this.DecodeBtn.Location = new System.Drawing.Point(84, 268);
            this.DecodeBtn.Name = "DecodeBtn";
            this.DecodeBtn.Size = new System.Drawing.Size(75, 23);
            this.DecodeBtn.TabIndex = 11;
            this.DecodeBtn.Text = "Decode";
            this.DecodeBtn.UseVisualStyleBackColor = true;
            // 
            // LoadDecodedBtn
            // 
            this.LoadDecodedBtn.Location = new System.Drawing.Point(3, 268);
            this.LoadDecodedBtn.Name = "LoadDecodedBtn";
            this.LoadDecodedBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadDecodedBtn.TabIndex = 10;
            this.LoadDecodedBtn.Text = "Load";
            this.LoadDecodedBtn.UseVisualStyleBackColor = true;
            // 
            // ScaleNumericUpDown
            // 
            this.ScaleNumericUpDown.Location = new System.Drawing.Point(1113, 398);
            this.ScaleNumericUpDown.Name = "ScaleNumericUpDown";
            this.ScaleNumericUpDown.Size = new System.Drawing.Size(41, 20);
            this.ScaleNumericUpDown.TabIndex = 13;
            this.ScaleNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1110, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Scale";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Original Image",
            "Prediction Error",
            "Q prediction Error",
            "Decoded Image"});
            this.listBox1.Location = new System.Drawing.Point(3, 319);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(103, 69);
            this.listBox1.TabIndex = 10;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Items.AddRange(new object[] {
            "Prediction Error",
            "Q prediction Error"});
            this.listBox2.Location = new System.Drawing.Point(261, 28);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(87, 30);
            this.listBox2.TabIndex = 12;
            // 
            // ComputeErrorBtn
            // 
            this.ComputeErrorBtn.Location = new System.Drawing.Point(3, 394);
            this.ComputeErrorBtn.Name = "ComputeErrorBtn";
            this.ComputeErrorBtn.Size = new System.Drawing.Size(117, 23);
            this.ComputeErrorBtn.TabIndex = 13;
            this.ComputeErrorBtn.Text = "Compute error";
            this.ComputeErrorBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 459);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ScaleNumericUpDown);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KNumericUpDown)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button SaveOriginalBtn;
        private System.Windows.Forms.Button EncodeBtn;
        private System.Windows.Forms.Button LoadOriginalBtn;
        private System.Windows.Forms.Panel OriginalImage;
        private System.Windows.Forms.Panel ErrorImage;
        private System.Windows.Forms.Panel DecodedImage;
        private System.Windows.Forms.ListBox PredictionRulesListBox;
        private System.Windows.Forms.ListBox StatisticModelListBox;
        private System.Windows.Forms.NumericUpDown KNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button SaveDecodedBtn;
        private System.Windows.Forms.Button DecodeBtn;
        private System.Windows.Forms.Button LoadDecodedBtn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown ScaleNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button ComputeErrorBtn;
    }
}

