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
            this.label3 = new System.Windows.Forms.Label();
            this.errorPictureBox = new System.Windows.Forms.PictureBox();
            this.HistogramScaleNumeric = new System.Windows.Forms.NumericUpDown();
            this.ComputeErrorBtn = new System.Windows.Forms.Button();
            this.RefreshHistogramBtn = new System.Windows.Forms.Button();
            this.ErrorMatrixListBox = new System.Windows.Forms.ListBox();
            this.HistogramSourceListBox = new System.Windows.Forms.ListBox();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.ScaleNumeric = new System.Windows.Forms.NumericUpDown();
            this.HistogramaPanel = new System.Windows.Forms.Panel();
            this.DecodedImage = new System.Windows.Forms.Panel();
            this.SaveDecodedBtn = new System.Windows.Forms.Button();
            this.DecodeBtn = new System.Windows.Forms.Button();
            this.LoadDecodedBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KNumericUpDown)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramScaleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumeric)).BeginInit();
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
            this.panel2.Location = new System.Drawing.Point(159, 3);
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
            "B+(A-C)/2",
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
            this.EncodeBtn.Click += new System.EventHandler(this.EncodeBtn_Click);
            // 
            // LoadOriginalBtn
            // 
            this.LoadOriginalBtn.Location = new System.Drawing.Point(0, 290);
            this.LoadOriginalBtn.Name = "LoadOriginalBtn";
            this.LoadOriginalBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadOriginalBtn.TabIndex = 1;
            this.LoadOriginalBtn.Text = "Load";
            this.LoadOriginalBtn.UseVisualStyleBackColor = true;
            this.LoadOriginalBtn.Click += new System.EventHandler(this.LoadOriginalBtn_Click);
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
            this.panel3.Controls.Add(this.SaveDecodedBtn);
            this.panel3.Controls.Add(this.DecodedImage);
            this.panel3.Controls.Add(this.DecodeBtn);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.LoadDecodedBtn);
            this.panel3.Controls.Add(this.errorPictureBox);
            this.panel3.Controls.Add(this.HistogramScaleNumeric);
            this.panel3.Controls.Add(this.ComputeErrorBtn);
            this.panel3.Controls.Add(this.RefreshHistogramBtn);
            this.panel3.Controls.Add(this.ErrorMatrixListBox);
            this.panel3.Controls.Add(this.HistogramSourceListBox);
            this.panel3.Controls.Add(this.RefreshBtn);
            this.panel3.Controls.Add(this.ScaleNumeric);
            this.panel3.Controls.Add(this.HistogramaPanel);
            this.panel3.Location = new System.Drawing.Point(507, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(732, 572);
            this.panel3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 466);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Scale";
            // 
            // errorPictureBox
            // 
            this.errorPictureBox.Location = new System.Drawing.Point(3, 3);
            this.errorPictureBox.Name = "errorPictureBox";
            this.errorPictureBox.Size = new System.Drawing.Size(256, 256);
            this.errorPictureBox.TabIndex = 14;
            this.errorPictureBox.TabStop = false;
            // 
            // HistogramScaleNumeric
            // 
            this.HistogramScaleNumeric.DecimalPlaces = 2;
            this.HistogramScaleNumeric.Location = new System.Drawing.Point(41, 482);
            this.HistogramScaleNumeric.Name = "HistogramScaleNumeric";
            this.HistogramScaleNumeric.Size = new System.Drawing.Size(41, 20);
            this.HistogramScaleNumeric.TabIndex = 13;
            this.HistogramScaleNumeric.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // ComputeErrorBtn
            // 
            this.ComputeErrorBtn.Location = new System.Drawing.Point(3, 394);
            this.ComputeErrorBtn.Name = "ComputeErrorBtn";
            this.ComputeErrorBtn.Size = new System.Drawing.Size(117, 23);
            this.ComputeErrorBtn.TabIndex = 13;
            this.ComputeErrorBtn.Text = "Compute error";
            this.ComputeErrorBtn.UseVisualStyleBackColor = true;
            this.ComputeErrorBtn.Click += new System.EventHandler(this.ComputeErrorBtn_Click);
            // 
            // RefreshHistogramBtn
            // 
            this.RefreshHistogramBtn.Location = new System.Drawing.Point(31, 508);
            this.RefreshHistogramBtn.Name = "RefreshHistogramBtn";
            this.RefreshHistogramBtn.Size = new System.Drawing.Size(75, 23);
            this.RefreshHistogramBtn.TabIndex = 12;
            this.RefreshHistogramBtn.Text = "Refresh";
            this.RefreshHistogramBtn.UseVisualStyleBackColor = true;
            this.RefreshHistogramBtn.Click += new System.EventHandler(this.RefreshHistogramBtn_Click);
            // 
            // ErrorMatrixListBox
            // 
            this.ErrorMatrixListBox.FormattingEnabled = true;
            this.ErrorMatrixListBox.Items.AddRange(new object[] {
            "Prediction Error",
            "Q prediction Error"});
            this.ErrorMatrixListBox.Location = new System.Drawing.Point(261, 28);
            this.ErrorMatrixListBox.Name = "ErrorMatrixListBox";
            this.ErrorMatrixListBox.Size = new System.Drawing.Size(87, 30);
            this.ErrorMatrixListBox.TabIndex = 12;
            // 
            // HistogramSourceListBox
            // 
            this.HistogramSourceListBox.FormattingEnabled = true;
            this.HistogramSourceListBox.Items.AddRange(new object[] {
            "Original Image",
            "Prediction Error",
            "Q prediction Error",
            "Decoded Image"});
            this.HistogramSourceListBox.Location = new System.Drawing.Point(3, 319);
            this.HistogramSourceListBox.Name = "HistogramSourceListBox";
            this.HistogramSourceListBox.Size = new System.Drawing.Size(103, 69);
            this.HistogramSourceListBox.TabIndex = 10;
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(261, 232);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(75, 23);
            this.RefreshBtn.TabIndex = 11;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // ScaleNumeric
            // 
            this.ScaleNumeric.DecimalPlaces = 1;
            this.ScaleNumeric.Location = new System.Drawing.Point(276, 206);
            this.ScaleNumeric.Name = "ScaleNumeric";
            this.ScaleNumeric.Size = new System.Drawing.Size(41, 20);
            this.ScaleNumeric.TabIndex = 10;
            this.ScaleNumeric.Value = new decimal(new int[] {
            17,
            0,
            0,
            65536});
            // 
            // HistogramaPanel
            // 
            this.HistogramaPanel.Location = new System.Drawing.Point(126, 265);
            this.HistogramaPanel.Name = "HistogramaPanel";
            this.HistogramaPanel.Size = new System.Drawing.Size(600, 300);
            this.HistogramaPanel.TabIndex = 2;
            // 
            // DecodedImage
            // 
            this.DecodedImage.Location = new System.Drawing.Point(354, 3);
            this.DecodedImage.Name = "DecodedImage";
            this.DecodedImage.Size = new System.Drawing.Size(256, 256);
            this.DecodedImage.TabIndex = 1;
            // 
            // SaveDecodedBtn
            // 
            this.SaveDecodedBtn.Location = new System.Drawing.Point(632, 67);
            this.SaveDecodedBtn.Name = "SaveDecodedBtn";
            this.SaveDecodedBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveDecodedBtn.TabIndex = 12;
            this.SaveDecodedBtn.Text = "Save";
            this.SaveDecodedBtn.UseVisualStyleBackColor = true;
            // 
            // DecodeBtn
            // 
            this.DecodeBtn.Location = new System.Drawing.Point(634, 38);
            this.DecodeBtn.Name = "DecodeBtn";
            this.DecodeBtn.Size = new System.Drawing.Size(75, 23);
            this.DecodeBtn.TabIndex = 11;
            this.DecodeBtn.Text = "Decode";
            this.DecodeBtn.UseVisualStyleBackColor = true;
            // 
            // LoadDecodedBtn
            // 
            this.LoadDecodedBtn.Location = new System.Drawing.Point(632, 9);
            this.LoadDecodedBtn.Name = "LoadDecodedBtn";
            this.LoadDecodedBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadDecodedBtn.TabIndex = 10;
            this.LoadDecodedBtn.Text = "Load";
            this.LoadDecodedBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 578);
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
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramScaleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button SaveOriginalBtn;
        private System.Windows.Forms.Button EncodeBtn;
        private System.Windows.Forms.Button LoadOriginalBtn;
        private System.Windows.Forms.Panel OriginalImage;
        private System.Windows.Forms.Panel DecodedImage;
        private System.Windows.Forms.ListBox PredictionRulesListBox;
        private System.Windows.Forms.ListBox StatisticModelListBox;
        private System.Windows.Forms.NumericUpDown KNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RefreshHistogramBtn;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.NumericUpDown ScaleNumeric;
        private System.Windows.Forms.Panel HistogramaPanel;
        private System.Windows.Forms.Button SaveDecodedBtn;
        private System.Windows.Forms.Button DecodeBtn;
        private System.Windows.Forms.Button LoadDecodedBtn;
        private System.Windows.Forms.ListBox HistogramSourceListBox;
        private System.Windows.Forms.NumericUpDown HistogramScaleNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox ErrorMatrixListBox;
        private System.Windows.Forms.Button ComputeErrorBtn;
        private System.Windows.Forms.PictureBox errorPictureBox;
    }
}

