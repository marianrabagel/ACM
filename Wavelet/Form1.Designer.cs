namespace Wavelet
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
            this.waveletImage = new System.Windows.Forms.Panel();
            this.LoadBmpButton = new System.Windows.Forms.Button();
            this.TestErrorButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.VisualizeWaveletButton = new System.Windows.Forms.Button();
            this.AnH1Button = new System.Windows.Forms.Button();
            this.SyH1Button = new System.Windows.Forms.Button();
            this.SyH5Button = new System.Windows.Forms.Button();
            this.AnH5Button = new System.Windows.Forms.Button();
            this.SyH3Button = new System.Windows.Forms.Button();
            this.AnH3Button = new System.Windows.Forms.Button();
            this.SyH4Button = new System.Windows.Forms.Button();
            this.AnH4Button = new System.Windows.Forms.Button();
            this.SyH2Button = new System.Windows.Forms.Button();
            this.AnH2Button = new System.Windows.Forms.Button();
            this.SyV1Button = new System.Windows.Forms.Button();
            this.AnV1 = new System.Windows.Forms.Button();
            this.SyV2Button = new System.Windows.Forms.Button();
            this.AnV2Button = new System.Windows.Forms.Button();
            this.SyV3Button = new System.Windows.Forms.Button();
            this.AnV3Button = new System.Windows.Forms.Button();
            this.SyV4Button = new System.Windows.Forms.Button();
            this.AnV4Button = new System.Windows.Forms.Button();
            this.SyV5Button = new System.Windows.Forms.Button();
            this.AnV5Button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.AnalysisButton = new System.Windows.Forms.Button();
            this.SynthesisButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MinLabel = new System.Windows.Forms.Label();
            this.MaxLabel = new System.Windows.Forms.Label();
            this.ScaleLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.OffsetLabel = new System.Windows.Forms.Label();
            this.ScaleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.XNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.OffsetNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.YNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LevelesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MaxValueLabel = new System.Windows.Forms.Label();
            this.MinValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LevelesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImagePanel
            // 
            this.OriginalImagePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OriginalImagePanel.Location = new System.Drawing.Point(12, 12);
            this.OriginalImagePanel.Name = "OriginalImagePanel";
            this.OriginalImagePanel.Size = new System.Drawing.Size(512, 512);
            this.OriginalImagePanel.TabIndex = 0;
            // 
            // waveletImage
            // 
            this.waveletImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.waveletImage.Location = new System.Drawing.Point(530, 13);
            this.waveletImage.Name = "waveletImage";
            this.waveletImage.Size = new System.Drawing.Size(512, 512);
            this.waveletImage.TabIndex = 1;
            // 
            // LoadBmpButton
            // 
            this.LoadBmpButton.Location = new System.Drawing.Point(12, 531);
            this.LoadBmpButton.Name = "LoadBmpButton";
            this.LoadBmpButton.Size = new System.Drawing.Size(75, 23);
            this.LoadBmpButton.TabIndex = 2;
            this.LoadBmpButton.Text = "Load";
            this.LoadBmpButton.UseVisualStyleBackColor = true;
            this.LoadBmpButton.Click += new System.EventHandler(this.LoadBmpButton_Click);
            // 
            // TestErrorButton
            // 
            this.TestErrorButton.Location = new System.Drawing.Point(12, 571);
            this.TestErrorButton.Name = "TestErrorButton";
            this.TestErrorButton.Size = new System.Drawing.Size(75, 23);
            this.TestErrorButton.TabIndex = 3;
            this.TestErrorButton.Text = "Test error";
            this.TestErrorButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(530, 531);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(530, 560);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 5;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // VisualizeWaveletButton
            // 
            this.VisualizeWaveletButton.Location = new System.Drawing.Point(653, 530);
            this.VisualizeWaveletButton.Name = "VisualizeWaveletButton";
            this.VisualizeWaveletButton.Size = new System.Drawing.Size(75, 23);
            this.VisualizeWaveletButton.TabIndex = 6;
            this.VisualizeWaveletButton.Text = "Visualize wavelet";
            this.VisualizeWaveletButton.UseVisualStyleBackColor = true;
            this.VisualizeWaveletButton.Click += new System.EventHandler(this.VisualizeWaveletButton_Click);
            // 
            // AnH1Button
            // 
            this.AnH1Button.Location = new System.Drawing.Point(1048, 13);
            this.AnH1Button.Name = "AnH1Button";
            this.AnH1Button.Size = new System.Drawing.Size(57, 23);
            this.AnH1Button.TabIndex = 7;
            this.AnH1Button.Text = "An H1";
            this.AnH1Button.UseVisualStyleBackColor = true;
            this.AnH1Button.Click += new System.EventHandler(this.AnH1Button_Click);
            // 
            // SyH1Button
            // 
            this.SyH1Button.Location = new System.Drawing.Point(1111, 13);
            this.SyH1Button.Name = "SyH1Button";
            this.SyH1Button.Size = new System.Drawing.Size(53, 23);
            this.SyH1Button.TabIndex = 8;
            this.SyH1Button.Text = "Sy H1";
            this.SyH1Button.UseVisualStyleBackColor = true;
            this.SyH1Button.Click += new System.EventHandler(this.SyH1Button_Click);
            // 
            // SyH5Button
            // 
            this.SyH5Button.Location = new System.Drawing.Point(1111, 306);
            this.SyH5Button.Name = "SyH5Button";
            this.SyH5Button.Size = new System.Drawing.Size(53, 23);
            this.SyH5Button.TabIndex = 10;
            this.SyH5Button.Text = "Sy H5";
            this.SyH5Button.UseVisualStyleBackColor = true;
            this.SyH5Button.Click += new System.EventHandler(this.SyH5Button_Click);
            // 
            // AnH5Button
            // 
            this.AnH5Button.Location = new System.Drawing.Point(1048, 306);
            this.AnH5Button.Name = "AnH5Button";
            this.AnH5Button.Size = new System.Drawing.Size(57, 23);
            this.AnH5Button.TabIndex = 9;
            this.AnH5Button.Text = "An H5";
            this.AnH5Button.UseVisualStyleBackColor = true;
            this.AnH5Button.Click += new System.EventHandler(this.AnH5Button_Click);
            // 
            // SyH3Button
            // 
            this.SyH3Button.Location = new System.Drawing.Point(1111, 157);
            this.SyH3Button.Name = "SyH3Button";
            this.SyH3Button.Size = new System.Drawing.Size(53, 23);
            this.SyH3Button.TabIndex = 12;
            this.SyH3Button.Text = "Sy H3";
            this.SyH3Button.UseVisualStyleBackColor = true;
            this.SyH3Button.Click += new System.EventHandler(this.SyH3Button_Click);
            // 
            // AnH3Button
            // 
            this.AnH3Button.Location = new System.Drawing.Point(1048, 157);
            this.AnH3Button.Name = "AnH3Button";
            this.AnH3Button.Size = new System.Drawing.Size(57, 23);
            this.AnH3Button.TabIndex = 11;
            this.AnH3Button.Text = "An H3";
            this.AnH3Button.UseVisualStyleBackColor = true;
            this.AnH3Button.Click += new System.EventHandler(this.AnH3Button_Click);
            // 
            // SyH4Button
            // 
            this.SyH4Button.Location = new System.Drawing.Point(1111, 234);
            this.SyH4Button.Name = "SyH4Button";
            this.SyH4Button.Size = new System.Drawing.Size(53, 23);
            this.SyH4Button.TabIndex = 14;
            this.SyH4Button.Text = "Sy H4";
            this.SyH4Button.UseVisualStyleBackColor = true;
            this.SyH4Button.Click += new System.EventHandler(this.SyH4Button_Click);
            // 
            // AnH4Button
            // 
            this.AnH4Button.Location = new System.Drawing.Point(1048, 234);
            this.AnH4Button.Name = "AnH4Button";
            this.AnH4Button.Size = new System.Drawing.Size(57, 23);
            this.AnH4Button.TabIndex = 13;
            this.AnH4Button.Text = "An H4";
            this.AnH4Button.UseVisualStyleBackColor = true;
            this.AnH4Button.Click += new System.EventHandler(this.AnH4Button_Click);
            // 
            // SyH2Button
            // 
            this.SyH2Button.Location = new System.Drawing.Point(1111, 85);
            this.SyH2Button.Name = "SyH2Button";
            this.SyH2Button.Size = new System.Drawing.Size(53, 23);
            this.SyH2Button.TabIndex = 16;
            this.SyH2Button.Text = "Sy H2";
            this.SyH2Button.UseVisualStyleBackColor = true;
            this.SyH2Button.Click += new System.EventHandler(this.SyH2Button_Click);
            // 
            // AnH2Button
            // 
            this.AnH2Button.Location = new System.Drawing.Point(1048, 85);
            this.AnH2Button.Name = "AnH2Button";
            this.AnH2Button.Size = new System.Drawing.Size(57, 23);
            this.AnH2Button.TabIndex = 15;
            this.AnH2Button.Text = "An H2";
            this.AnH2Button.UseVisualStyleBackColor = true;
            this.AnH2Button.Click += new System.EventHandler(this.AnH2Button_Click);
            // 
            // SyV1Button
            // 
            this.SyV1Button.Location = new System.Drawing.Point(1111, 42);
            this.SyV1Button.Name = "SyV1Button";
            this.SyV1Button.Size = new System.Drawing.Size(53, 23);
            this.SyV1Button.TabIndex = 18;
            this.SyV1Button.Text = "Sy V1";
            this.SyV1Button.UseVisualStyleBackColor = true;
            this.SyV1Button.Click += new System.EventHandler(this.SyV1Button_Click);
            // 
            // AnV1
            // 
            this.AnV1.Location = new System.Drawing.Point(1048, 42);
            this.AnV1.Name = "AnV1";
            this.AnV1.Size = new System.Drawing.Size(57, 23);
            this.AnV1.TabIndex = 17;
            this.AnV1.Text = "An V1";
            this.AnV1.UseVisualStyleBackColor = true;
            this.AnV1.Click += new System.EventHandler(this.AnV1_Click);
            // 
            // SyV2Button
            // 
            this.SyV2Button.Location = new System.Drawing.Point(1111, 114);
            this.SyV2Button.Name = "SyV2Button";
            this.SyV2Button.Size = new System.Drawing.Size(53, 23);
            this.SyV2Button.TabIndex = 20;
            this.SyV2Button.Text = "Sy V2";
            this.SyV2Button.UseVisualStyleBackColor = true;
            this.SyV2Button.Click += new System.EventHandler(this.SyV2Button_Click);
            // 
            // AnV2Button
            // 
            this.AnV2Button.Location = new System.Drawing.Point(1048, 114);
            this.AnV2Button.Name = "AnV2Button";
            this.AnV2Button.Size = new System.Drawing.Size(57, 23);
            this.AnV2Button.TabIndex = 19;
            this.AnV2Button.Text = "An V2";
            this.AnV2Button.UseVisualStyleBackColor = true;
            this.AnV2Button.Click += new System.EventHandler(this.AnV2Button_Click);
            // 
            // SyV3Button
            // 
            this.SyV3Button.Location = new System.Drawing.Point(1111, 186);
            this.SyV3Button.Name = "SyV3Button";
            this.SyV3Button.Size = new System.Drawing.Size(53, 23);
            this.SyV3Button.TabIndex = 22;
            this.SyV3Button.Text = "Sy V3";
            this.SyV3Button.UseVisualStyleBackColor = true;
            this.SyV3Button.Click += new System.EventHandler(this.SyV3Button_Click);
            // 
            // AnV3Button
            // 
            this.AnV3Button.Location = new System.Drawing.Point(1048, 186);
            this.AnV3Button.Name = "AnV3Button";
            this.AnV3Button.Size = new System.Drawing.Size(57, 23);
            this.AnV3Button.TabIndex = 21;
            this.AnV3Button.Text = "An V3";
            this.AnV3Button.UseVisualStyleBackColor = true;
            this.AnV3Button.Click += new System.EventHandler(this.AnV3Button_Click);
            // 
            // SyV4Button
            // 
            this.SyV4Button.Location = new System.Drawing.Point(1111, 263);
            this.SyV4Button.Name = "SyV4Button";
            this.SyV4Button.Size = new System.Drawing.Size(53, 23);
            this.SyV4Button.TabIndex = 24;
            this.SyV4Button.Text = "Sy V4";
            this.SyV4Button.UseVisualStyleBackColor = true;
            this.SyV4Button.Click += new System.EventHandler(this.SyV4Button_Click);
            // 
            // AnV4Button
            // 
            this.AnV4Button.Location = new System.Drawing.Point(1048, 263);
            this.AnV4Button.Name = "AnV4Button";
            this.AnV4Button.Size = new System.Drawing.Size(57, 23);
            this.AnV4Button.TabIndex = 23;
            this.AnV4Button.Text = "An V4";
            this.AnV4Button.UseVisualStyleBackColor = true;
            this.AnV4Button.Click += new System.EventHandler(this.AnV4Button_Click);
            // 
            // SyV5Button
            // 
            this.SyV5Button.Location = new System.Drawing.Point(1111, 335);
            this.SyV5Button.Name = "SyV5Button";
            this.SyV5Button.Size = new System.Drawing.Size(53, 23);
            this.SyV5Button.TabIndex = 26;
            this.SyV5Button.Text = "Sy V5";
            this.SyV5Button.UseVisualStyleBackColor = true;
            this.SyV5Button.Click += new System.EventHandler(this.SyV5Button_Click);
            // 
            // AnV5Button
            // 
            this.AnV5Button.Location = new System.Drawing.Point(1048, 335);
            this.AnV5Button.Name = "AnV5Button";
            this.AnV5Button.Size = new System.Drawing.Size(57, 23);
            this.AnV5Button.TabIndex = 25;
            this.AnV5Button.Text = "An V5";
            this.AnV5Button.UseVisualStyleBackColor = true;
            this.AnV5Button.Click += new System.EventHandler(this.AnV5Button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AnalysisButton
            // 
            this.AnalysisButton.Location = new System.Drawing.Point(1048, 421);
            this.AnalysisButton.Name = "AnalysisButton";
            this.AnalysisButton.Size = new System.Drawing.Size(75, 23);
            this.AnalysisButton.TabIndex = 27;
            this.AnalysisButton.Text = "Analysis";
            this.AnalysisButton.UseVisualStyleBackColor = true;
            this.AnalysisButton.Click += new System.EventHandler(this.AnalysisButton_Click);
            // 
            // SynthesisButton
            // 
            this.SynthesisButton.Location = new System.Drawing.Point(1048, 450);
            this.SynthesisButton.Name = "SynthesisButton";
            this.SynthesisButton.Size = new System.Drawing.Size(75, 23);
            this.SynthesisButton.TabIndex = 28;
            this.SynthesisButton.Text = "Synthesis";
            this.SynthesisButton.UseVisualStyleBackColor = true;
            this.SynthesisButton.Click += new System.EventHandler(this.SynthesisButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1049, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Levels:";
            // 
            // MinLabel
            // 
            this.MinLabel.AutoSize = true;
            this.MinLabel.Location = new System.Drawing.Point(94, 580);
            this.MinLabel.Name = "MinLabel";
            this.MinLabel.Size = new System.Drawing.Size(27, 13);
            this.MinLabel.TabIndex = 30;
            this.MinLabel.Text = "Min:";
            // 
            // MaxLabel
            // 
            this.MaxLabel.AutoSize = true;
            this.MaxLabel.Location = new System.Drawing.Point(94, 596);
            this.MaxLabel.Name = "MaxLabel";
            this.MaxLabel.Size = new System.Drawing.Size(30, 13);
            this.MaxLabel.TabIndex = 31;
            this.MaxLabel.Text = "Max:";
            // 
            // ScaleLabel
            // 
            this.ScaleLabel.AutoSize = true;
            this.ScaleLabel.Location = new System.Drawing.Point(754, 540);
            this.ScaleLabel.Name = "ScaleLabel";
            this.ScaleLabel.Size = new System.Drawing.Size(37, 13);
            this.ScaleLabel.TabIndex = 32;
            this.ScaleLabel.Text = "Scale:";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(861, 541);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(15, 13);
            this.XLabel.TabIndex = 33;
            this.XLabel.Text = "x:";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(861, 566);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(15, 13);
            this.YLabel.TabIndex = 35;
            this.YLabel.Text = "y:";
            // 
            // OffsetLabel
            // 
            this.OffsetLabel.AutoSize = true;
            this.OffsetLabel.Location = new System.Drawing.Point(754, 565);
            this.OffsetLabel.Name = "OffsetLabel";
            this.OffsetLabel.Size = new System.Drawing.Size(38, 13);
            this.OffsetLabel.TabIndex = 34;
            this.OffsetLabel.Text = "Offset:";
            // 
            // ScaleNumericUpDown
            // 
            this.ScaleNumericUpDown.DecimalPlaces = 1;
            this.ScaleNumericUpDown.Location = new System.Drawing.Point(795, 534);
            this.ScaleNumericUpDown.Name = "ScaleNumericUpDown";
            this.ScaleNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.ScaleNumericUpDown.TabIndex = 36;
            this.ScaleNumericUpDown.Value = new decimal(new int[] {
            53,
            0,
            0,
            65536});
            // 
            // XNumericUpDown
            // 
            this.XNumericUpDown.Location = new System.Drawing.Point(882, 534);
            this.XNumericUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.XNumericUpDown.Name = "XNumericUpDown";
            this.XNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.XNumericUpDown.TabIndex = 37;
            this.XNumericUpDown.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // OffsetNumericUpDown
            // 
            this.OffsetNumericUpDown.Location = new System.Drawing.Point(795, 560);
            this.OffsetNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.OffsetNumericUpDown.Name = "OffsetNumericUpDown";
            this.OffsetNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.OffsetNumericUpDown.TabIndex = 38;
            this.OffsetNumericUpDown.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // YNumericUpDown
            // 
            this.YNumericUpDown.Location = new System.Drawing.Point(882, 558);
            this.YNumericUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.YNumericUpDown.Name = "YNumericUpDown";
            this.YNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.YNumericUpDown.TabIndex = 39;
            this.YNumericUpDown.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // LevelesNumericUpDown
            // 
            this.LevelesNumericUpDown.Location = new System.Drawing.Point(1096, 379);
            this.LevelesNumericUpDown.Name = "LevelesNumericUpDown";
            this.LevelesNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.LevelesNumericUpDown.TabIndex = 40;
            this.LevelesNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // MaxValueLabel
            // 
            this.MaxValueLabel.AutoSize = true;
            this.MaxValueLabel.Location = new System.Drawing.Point(144, 597);
            this.MaxValueLabel.Name = "MaxValueLabel";
            this.MaxValueLabel.Size = new System.Drawing.Size(0, 13);
            this.MaxValueLabel.TabIndex = 42;
            // 
            // MinValueLabel
            // 
            this.MinValueLabel.AutoSize = true;
            this.MinValueLabel.Location = new System.Drawing.Point(144, 581);
            this.MinValueLabel.Name = "MinValueLabel";
            this.MinValueLabel.Size = new System.Drawing.Size(0, 13);
            this.MinValueLabel.TabIndex = 41;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 618);
            this.Controls.Add(this.MaxValueLabel);
            this.Controls.Add(this.MinValueLabel);
            this.Controls.Add(this.LevelesNumericUpDown);
            this.Controls.Add(this.YNumericUpDown);
            this.Controls.Add(this.OffsetNumericUpDown);
            this.Controls.Add(this.XNumericUpDown);
            this.Controls.Add(this.ScaleNumericUpDown);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.OffsetLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.ScaleLabel);
            this.Controls.Add(this.MaxLabel);
            this.Controls.Add(this.MinLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SynthesisButton);
            this.Controls.Add(this.AnalysisButton);
            this.Controls.Add(this.SyV5Button);
            this.Controls.Add(this.AnV5Button);
            this.Controls.Add(this.SyV4Button);
            this.Controls.Add(this.AnV4Button);
            this.Controls.Add(this.SyV3Button);
            this.Controls.Add(this.AnV3Button);
            this.Controls.Add(this.SyV2Button);
            this.Controls.Add(this.AnV2Button);
            this.Controls.Add(this.SyV1Button);
            this.Controls.Add(this.AnV1);
            this.Controls.Add(this.SyH2Button);
            this.Controls.Add(this.AnH2Button);
            this.Controls.Add(this.SyH4Button);
            this.Controls.Add(this.AnH4Button);
            this.Controls.Add(this.SyH3Button);
            this.Controls.Add(this.AnH3Button);
            this.Controls.Add(this.SyH5Button);
            this.Controls.Add(this.AnH5Button);
            this.Controls.Add(this.SyH1Button);
            this.Controls.Add(this.AnH1Button);
            this.Controls.Add(this.VisualizeWaveletButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TestErrorButton);
            this.Controls.Add(this.LoadBmpButton);
            this.Controls.Add(this.waveletImage);
            this.Controls.Add(this.OriginalImagePanel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LevelesNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel OriginalImagePanel;
        private System.Windows.Forms.Panel waveletImage;
        private System.Windows.Forms.Button LoadBmpButton;
        private System.Windows.Forms.Button TestErrorButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button VisualizeWaveletButton;
        private System.Windows.Forms.Button AnH1Button;
        private System.Windows.Forms.Button SyH1Button;
        private System.Windows.Forms.Button SyH5Button;
        private System.Windows.Forms.Button AnH5Button;
        private System.Windows.Forms.Button SyH3Button;
        private System.Windows.Forms.Button AnH3Button;
        private System.Windows.Forms.Button SyH4Button;
        private System.Windows.Forms.Button AnH4Button;
        private System.Windows.Forms.Button SyH2Button;
        private System.Windows.Forms.Button AnH2Button;
        private System.Windows.Forms.Button SyV1Button;
        private System.Windows.Forms.Button AnV1;
        private System.Windows.Forms.Button SyV2Button;
        private System.Windows.Forms.Button AnV2Button;
        private System.Windows.Forms.Button SyV3Button;
        private System.Windows.Forms.Button AnV3Button;
        private System.Windows.Forms.Button SyV4Button;
        private System.Windows.Forms.Button AnV4Button;
        private System.Windows.Forms.Button SyV5Button;
        private System.Windows.Forms.Button AnV5Button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button AnalysisButton;
        private System.Windows.Forms.Button SynthesisButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MinLabel;
        private System.Windows.Forms.Label MaxLabel;
        private System.Windows.Forms.Label ScaleLabel;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label OffsetLabel;
        private System.Windows.Forms.NumericUpDown ScaleNumericUpDown;
        private System.Windows.Forms.NumericUpDown XNumericUpDown;
        private System.Windows.Forms.NumericUpDown OffsetNumericUpDown;
        private System.Windows.Forms.NumericUpDown YNumericUpDown;
        private System.Windows.Forms.NumericUpDown LevelesNumericUpDown;
        private System.Windows.Forms.Label MaxValueLabel;
        private System.Windows.Forms.Label MinValueLabel;
    }
}

