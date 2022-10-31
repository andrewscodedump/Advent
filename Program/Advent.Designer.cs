namespace Advent
{
    partial class AdventOfCode
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
            this.updDay = new System.Windows.Forms.NumericUpDown();
            this.updPuzzle = new System.Windows.Forms.NumericUpDown();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.lblPuzzle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkTestMode = new System.Windows.Forms.CheckBox();
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnSuperBatch = new System.Windows.Forms.Button();
            this.lblExpected = new System.Windows.Forms.Label();
            this.txtExpected = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.updYear = new System.Windows.Forms.NumericUpDown();
            this.lblTimeTaken = new System.Windows.Forms.Label();
            this.txtTimeTaken = new System.Windows.Forms.TextBox();
            this.prevInput = new System.Windows.Forms.Button();
            this.nextInput = new System.Windows.Forms.Button();
            this.inputNumber = new System.Windows.Forms.TextBox();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.ProgressText = new System.Windows.Forms.TextBox();
            this.BatchWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.updDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updPuzzle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updYear)).BeginInit();
            this.SuspendLayout();
            // 
            // updDay
            // 
            this.updDay.Location = new System.Drawing.Point(238, 33);
            this.updDay.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.updDay.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.updDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updDay.Name = "updDay";
            this.updDay.Size = new System.Drawing.Size(51, 23);
            this.updDay.TabIndex = 3;
            this.updDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updDay.ValueChanged += new System.EventHandler(this.ResetScreenHandler);
            // 
            // updPuzzle
            // 
            this.updPuzzle.Location = new System.Drawing.Point(370, 33);
            this.updPuzzle.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.updPuzzle.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.updPuzzle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updPuzzle.Name = "updPuzzle";
            this.updPuzzle.Size = new System.Drawing.Size(51, 23);
            this.updPuzzle.TabIndex = 5;
            this.updPuzzle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updPuzzle.ValueChanged += new System.EventHandler(this.ResetScreenHandler);
            // 
            // txtInput
            // 
            this.txtInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtInput.Location = new System.Drawing.Point(132, 95);
            this.txtInput.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(455, 23);
            this.txtInput.TabIndex = 8;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(132, 193);
            this.btnProcess.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(88, 27);
            this.btnProcess.TabIndex = 11;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.Process_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(132, 279);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(455, 23);
            this.txtOutput.TabIndex = 15;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(75, 278);
            this.lblOutput.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(45, 15);
            this.lblOutput.TabIndex = 14;
            this.lblOutput.Text = "Output";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(85, 95);
            this.lblInput.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(35, 15);
            this.lblInput.TabIndex = 7;
            this.lblInput.Text = "Input";
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Location = new System.Drawing.Point(197, 33);
            this.lblDay.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(27, 15);
            this.lblDay.TabIndex = 2;
            this.lblDay.Text = "Day";
            // 
            // lblPuzzle
            // 
            this.lblPuzzle.AutoSize = true;
            this.lblPuzzle.Location = new System.Drawing.Point(318, 33);
            this.lblPuzzle.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblPuzzle.Name = "lblPuzzle";
            this.lblPuzzle.Size = new System.Drawing.Size(40, 15);
            this.lblPuzzle.TabIndex = 4;
            this.lblPuzzle.Text = "Puzzle";
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(416, 193);
            this.btnExit.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 27);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // chkTestMode
            // 
            this.chkTestMode.AutoSize = true;
            this.chkTestMode.Location = new System.Drawing.Point(454, 37);
            this.chkTestMode.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.chkTestMode.Name = "chkTestMode";
            this.chkTestMode.Size = new System.Drawing.Size(80, 19);
            this.chkTestMode.TabIndex = 6;
            this.chkTestMode.Text = "Test Mode";
            this.chkTestMode.UseVisualStyleBackColor = true;
            this.chkTestMode.CheckedChanged += new System.EventHandler(this.ResetScreenHandler);
            // 
            // btnBatch
            // 
            this.btnBatch.Location = new System.Drawing.Point(227, 193);
            this.btnBatch.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(88, 27);
            this.btnBatch.TabIndex = 12;
            this.btnBatch.Text = "&Batch";
            this.btnBatch.UseVisualStyleBackColor = true;
            this.btnBatch.Click += new System.EventHandler(this.Batch_Click);
            // 
            // btnSuperBatch
            // 
            this.btnSuperBatch.Location = new System.Drawing.Point(322, 193);
            this.btnSuperBatch.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnSuperBatch.Name = "btnSuperBatch";
            this.btnSuperBatch.Size = new System.Drawing.Size(88, 27);
            this.btnSuperBatch.TabIndex = 12;
            this.btnSuperBatch.Text = "&SuperBatch";
            this.btnSuperBatch.UseVisualStyleBackColor = true;
            this.btnSuperBatch.Click += new System.EventHandler(this.SuperBatch_Click);
            // 
            // lblExpected
            // 
            this.lblExpected.AutoSize = true;
            this.lblExpected.Location = new System.Drawing.Point(65, 142);
            this.lblExpected.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblExpected.Name = "lblExpected";
            this.lblExpected.Size = new System.Drawing.Size(55, 15);
            this.lblExpected.TabIndex = 9;
            this.lblExpected.Text = "Expected";
            // 
            // txtExpected
            // 
            this.txtExpected.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtExpected.Location = new System.Drawing.Point(133, 142);
            this.txtExpected.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.txtExpected.Name = "txtExpected";
            this.txtExpected.Size = new System.Drawing.Size(455, 23);
            this.txtExpected.TabIndex = 10;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(91, 33);
            this.lblYear.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 15);
            this.lblYear.TabIndex = 0;
            this.lblYear.Text = "Year";
            // 
            // updYear
            // 
            this.updYear.Location = new System.Drawing.Point(133, 33);
            this.updYear.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.updYear.Maximum = new decimal(new int[] {
            2022,
            0,
            0,
            0});
            this.updYear.Minimum = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.updYear.Name = "updYear";
            this.updYear.Size = new System.Drawing.Size(51, 23);
            this.updYear.TabIndex = 1;
            this.updYear.Value = new decimal(new int[] {
            2019,
            0,
            0,
            0});
            this.updYear.ValueChanged += new System.EventHandler(this.ResetScreenHandler);
            // 
            // lblTimeTaken
            // 
            this.lblTimeTaken.AutoSize = true;
            this.lblTimeTaken.Location = new System.Drawing.Point(54, 319);
            this.lblTimeTaken.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblTimeTaken.Name = "lblTimeTaken";
            this.lblTimeTaken.Size = new System.Drawing.Size(66, 15);
            this.lblTimeTaken.TabIndex = 16;
            this.lblTimeTaken.Text = "Time Taken";
            // 
            // txtTimeTaken
            // 
            this.txtTimeTaken.Location = new System.Drawing.Point(132, 319);
            this.txtTimeTaken.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.txtTimeTaken.Name = "txtTimeTaken";
            this.txtTimeTaken.Size = new System.Drawing.Size(455, 23);
            this.txtTimeTaken.TabIndex = 17;
            // 
            // prevInput
            // 
            this.prevInput.Location = new System.Drawing.Point(596, 95);
            this.prevInput.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.prevInput.Name = "prevInput";
            this.prevInput.Size = new System.Drawing.Size(24, 27);
            this.prevInput.TabIndex = 18;
            this.prevInput.Text = "<";
            this.prevInput.UseVisualStyleBackColor = true;
            this.prevInput.Click += new System.EventHandler(this.PrevInput_Click);
            // 
            // nextInput
            // 
            this.nextInput.Location = new System.Drawing.Point(628, 95);
            this.nextInput.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.nextInput.Name = "nextInput";
            this.nextInput.Size = new System.Drawing.Size(24, 27);
            this.nextInput.TabIndex = 19;
            this.nextInput.Text = ">";
            this.nextInput.UseVisualStyleBackColor = true;
            this.nextInput.Click += new System.EventHandler(this.NextInput_Click);
            // 
            // inputNumber
            // 
            this.inputNumber.Location = new System.Drawing.Point(596, 142);
            this.inputNumber.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.inputNumber.Name = "inputNumber";
            this.inputNumber.Size = new System.Drawing.Size(55, 23);
            this.inputNumber.TabIndex = 20;
            this.inputNumber.Visible = false;
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(132, 243);
            this.Progress.MarqueeAnimationSpeed = 10;
            this.Progress.Maximum = 50;
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(396, 23);
            this.Progress.Step = 1;
            this.Progress.TabIndex = 21;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(68, 243);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(52, 15);
            this.lblProgress.TabIndex = 22;
            this.lblProgress.Text = "Progress";
            // 
            // ProgressText
            // 
            this.ProgressText.Enabled = false;
            this.ProgressText.Location = new System.Drawing.Point(534, 243);
            this.ProgressText.Name = "ProgressText";
            this.ProgressText.Size = new System.Drawing.Size(53, 23);
            this.ProgressText.TabIndex = 23;
            this.ProgressText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BatchWorker
            // 
            this.BatchWorker.WorkerReportsProgress = true;
            this.BatchWorker.WorkerSupportsCancellation = true;
            this.BatchWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BatchWorker_DoWork);
            this.BatchWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BatchWorker_ProgressChanged);
            this.BatchWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BatchWorker_RunWorkerCompleted);
            // 
            // AdventOfCode
            // 
            this.AcceptButton = this.btnProcess;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 410);
            this.Controls.Add(this.ProgressText);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.inputNumber);
            this.Controls.Add(this.nextInput);
            this.Controls.Add(this.prevInput);
            this.Controls.Add(this.lblTimeTaken);
            this.Controls.Add(this.txtTimeTaken);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.updYear);
            this.Controls.Add(this.lblExpected);
            this.Controls.Add(this.txtExpected);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.btnSuperBatch);
            this.Controls.Add(this.chkTestMode);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblPuzzle);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.updPuzzle);
            this.Controls.Add(this.updDay);
            this.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.Name = "AdventOfCode";
            this.Text = "Advent Of Code";
            ((System.ComponentModel.ISupportInitialize)(this.updDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updPuzzle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown updDay;
        private System.Windows.Forms.NumericUpDown updPuzzle;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblPuzzle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkTestMode;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnSuperBatch;
        private System.Windows.Forms.Label lblExpected;
        private System.Windows.Forms.TextBox txtExpected;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.NumericUpDown updYear;
        private System.Windows.Forms.Label lblTimeTaken;
        private System.Windows.Forms.TextBox txtTimeTaken;
        private System.Windows.Forms.Button prevInput;
        private System.Windows.Forms.Button nextInput;
        private System.Windows.Forms.TextBox inputNumber;
        private ProgressBar Progress;
        private Label lblProgress;
        private TextBox ProgressText;
        private System.ComponentModel.BackgroundWorker BatchWorker;
    }
}

