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
            updDay = new NumericUpDown();
            updPuzzle = new NumericUpDown();
            txtInput = new TextBox();
            btnProcess = new Button();
            txtOutput = new TextBox();
            lblOutput = new Label();
            lblInput = new Label();
            lblDay = new Label();
            lblPuzzle = new Label();
            btnExit = new Button();
            chkTestMode = new CheckBox();
            btnBatch = new Button();
            lblExpected = new Label();
            txtExpected = new TextBox();
            lblYear = new Label();
            updYear = new NumericUpDown();
            lblTimeTaken = new Label();
            txtTimeTaken = new TextBox();
            prevInput = new Button();
            nextInput = new Button();
            inputNumber = new TextBox();
            Progress = new ProgressBar();
            lblProgress = new Label();
            ProgressText = new TextBox();
            BatchWorker = new System.ComponentModel.BackgroundWorker();
            FullInput = new Button();
            Website = new Button();
            Description = new TextBox();
            ((System.ComponentModel.ISupportInitialize)updDay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)updPuzzle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)updYear).BeginInit();
            SuspendLayout();
            // 
            // updDay
            // 
            updDay.Location = new Point(251, 33);
            updDay.Margin = new Padding(9, 10, 9, 10);
            updDay.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            updDay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            updDay.Name = "updDay";
            updDay.Size = new Size(51, 23);
            updDay.TabIndex = 3;
            updDay.Value = new decimal(new int[] { 1, 0, 0, 0 });
            updDay.ValueChanged += ResetScreenHandler;
            // 
            // updPuzzle
            // 
            updPuzzle.Location = new Point(369, 33);
            updPuzzle.Margin = new Padding(9, 10, 9, 10);
            updPuzzle.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            updPuzzle.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            updPuzzle.Name = "updPuzzle";
            updPuzzle.Size = new Size(51, 23);
            updPuzzle.TabIndex = 5;
            updPuzzle.Value = new decimal(new int[] { 1, 0, 0, 0 });
            updPuzzle.ValueChanged += ResetScreenHandler;
            // 
            // txtInput
            // 
            txtInput.Cursor = Cursors.IBeam;
            txtInput.Location = new Point(133, 184);
            txtInput.Margin = new Padding(9, 10, 9, 10);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(455, 23);
            txtInput.TabIndex = 8;
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(133, 282);
            btnProcess.Margin = new Padding(9, 10, 9, 10);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(88, 27);
            btnProcess.TabIndex = 11;
            btnProcess.Text = "&Process";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += Process_Click;
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(133, 368);
            txtOutput.Margin = new Padding(9, 10, 9, 10);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(455, 23);
            txtOutput.TabIndex = 15;
            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(76, 367);
            lblOutput.Margin = new Padding(9, 0, 9, 0);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(45, 15);
            lblOutput.TabIndex = 14;
            lblOutput.Text = "Output";
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.Location = new Point(86, 184);
            lblInput.Margin = new Padding(9, 0, 9, 0);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(35, 15);
            lblInput.TabIndex = 7;
            lblInput.Text = "Input";
            // 
            // lblDay
            // 
            lblDay.AutoSize = true;
            lblDay.Location = new Point(214, 33);
            lblDay.Margin = new Padding(9, 0, 9, 0);
            lblDay.Name = "lblDay";
            lblDay.Size = new Size(27, 15);
            lblDay.TabIndex = 2;
            lblDay.Text = "Day";
            // 
            // lblPuzzle
            // 
            lblPuzzle.AutoSize = true;
            lblPuzzle.Location = new Point(318, 33);
            lblPuzzle.Margin = new Padding(9, 0, 9, 0);
            lblPuzzle.Name = "lblPuzzle";
            lblPuzzle.Size = new Size(40, 15);
            lblPuzzle.TabIndex = 4;
            lblPuzzle.Text = "Puzzle";
            // 
            // chkTestMode
            // 
            chkTestMode.AutoSize = true;
            chkTestMode.Location = new Point(454, 37);
            chkTestMode.Margin = new Padding(9, 10, 9, 10);
            chkTestMode.Name = "chkTestMode";
            chkTestMode.Size = new Size(80, 19);
            chkTestMode.TabIndex = 6;
            chkTestMode.Text = "Test Mode";
            chkTestMode.UseVisualStyleBackColor = true;
            chkTestMode.CheckedChanged += ResetScreenHandler;
            // 
            // btnBatch
            // 
            btnBatch.Location = new Point(228, 282);
            btnBatch.Margin = new Padding(9, 10, 9, 10);
            btnBatch.Name = "btnBatch";
            btnBatch.Size = new Size(88, 27);
            btnBatch.TabIndex = 12;
            btnBatch.Text = "&Batch";
            btnBatch.UseVisualStyleBackColor = true;
            btnBatch.Click += Batch_Click;
            // 
            // btnExit
            // 
            btnExit.DialogResult = DialogResult.Cancel;
            btnExit.Location = new Point(323, 282);
            btnExit.Margin = new Padding(9, 10, 9, 10);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(88, 27);
            btnExit.TabIndex = 12;
            btnExit.Text = "E&xit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += Exit_Click;
            // 
            // lblExpected
            // 
            lblExpected.AutoSize = true;
            lblExpected.Location = new Point(66, 231);
            lblExpected.Margin = new Padding(9, 0, 9, 0);
            lblExpected.Name = "lblExpected";
            lblExpected.Size = new Size(55, 15);
            lblExpected.TabIndex = 9;
            lblExpected.Text = "Expected";
            // 
            // txtExpected
            // 
            txtExpected.Cursor = Cursors.IBeam;
            txtExpected.Location = new Point(134, 231);
            txtExpected.Margin = new Padding(9, 10, 9, 10);
            txtExpected.Name = "txtExpected";
            txtExpected.Size = new Size(455, 23);
            txtExpected.TabIndex = 10;
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(91, 33);
            lblYear.Margin = new Padding(9, 0, 9, 0);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(29, 15);
            lblYear.TabIndex = 0;
            lblYear.Text = "Year";
            // 
            // updYear
            // 
            updYear.Location = new Point(133, 33);
            updYear.Margin = new Padding(9, 10, 9, 10);
            updYear.Maximum = new decimal(new int[] { 2023, 0, 0, 0 });
            updYear.Minimum = new decimal(new int[] { 2015, 0, 0, 0 });
            updYear.Name = "updYear";
            updYear.Size = new Size(51, 23);
            updYear.TabIndex = 1;
            updYear.Value = new decimal(new int[] { 2019, 0, 0, 0 });
            updYear.ValueChanged += ResetScreenHandler;
            // 
            // lblTimeTaken
            // 
            lblTimeTaken.AutoSize = true;
            lblTimeTaken.Location = new Point(55, 408);
            lblTimeTaken.Margin = new Padding(9, 0, 9, 0);
            lblTimeTaken.Name = "lblTimeTaken";
            lblTimeTaken.Size = new Size(66, 15);
            lblTimeTaken.TabIndex = 16;
            lblTimeTaken.Text = "Time Taken";
            // 
            // txtTimeTaken
            // 
            txtTimeTaken.Location = new Point(133, 408);
            txtTimeTaken.Margin = new Padding(9, 10, 9, 10);
            txtTimeTaken.Name = "txtTimeTaken";
            txtTimeTaken.Size = new Size(455, 23);
            txtTimeTaken.TabIndex = 17;
            // 
            // prevInput
            // 
            prevInput.Location = new Point(640, 184);
            prevInput.Margin = new Padding(9, 10, 9, 10);
            prevInput.Name = "prevInput";
            prevInput.Size = new Size(24, 23);
            prevInput.TabIndex = 18;
            prevInput.Text = "<";
            prevInput.UseVisualStyleBackColor = true;
            prevInput.Click += PrevInput_Click;
            // 
            // nextInput
            // 
            nextInput.Location = new Point(672, 184);
            nextInput.Margin = new Padding(9, 10, 9, 10);
            nextInput.Name = "nextInput";
            nextInput.Size = new Size(24, 23);
            nextInput.TabIndex = 19;
            nextInput.Text = ">";
            nextInput.UseVisualStyleBackColor = true;
            nextInput.Click += NextInput_Click;
            // 
            // inputNumber
            // 
            inputNumber.Location = new Point(597, 231);
            inputNumber.Margin = new Padding(9, 10, 9, 10);
            inputNumber.Name = "inputNumber";
            inputNumber.Size = new Size(55, 23);
            inputNumber.TabIndex = 20;
            inputNumber.Visible = false;
            // 
            // Progress
            // 
            Progress.Location = new Point(133, 332);
            Progress.MarqueeAnimationSpeed = 10;
            Progress.Maximum = 50;
            Progress.Name = "Progress";
            Progress.Size = new Size(396, 23);
            Progress.Step = 1;
            Progress.TabIndex = 21;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(69, 332);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(52, 15);
            lblProgress.TabIndex = 22;
            lblProgress.Text = "Progress";
            // 
            // ProgressText
            // 
            ProgressText.Enabled = false;
            ProgressText.Location = new Point(535, 332);
            ProgressText.Name = "ProgressText";
            ProgressText.Size = new Size(53, 23);
            ProgressText.TabIndex = 23;
            ProgressText.TextAlign = HorizontalAlignment.Right;
            // 
            // BatchWorker
            // 
            BatchWorker.WorkerReportsProgress = true;
            BatchWorker.WorkerSupportsCancellation = true;
            BatchWorker.DoWork += BatchWorker_DoWork;
            BatchWorker.ProgressChanged += BatchWorker_ProgressChanged;
            BatchWorker.RunWorkerCompleted += BatchWorker_RunWorkerCompleted;
            // 
            // FullInput
            // 
            FullInput.Location = new Point(600, 183);
            FullInput.Name = "FullInput";
            FullInput.Size = new Size(34, 24);
            FullInput.TabIndex = 24;
            FullInput.Text = "&Full";
            FullInput.UseVisualStyleBackColor = true;
            FullInput.Click += FullInput_Click;
            // 
            // Website
            // 
            Website.Location = new Point(600, 70);
            Website.Name = "Website";
            Website.Size = new Size(75, 23);
            Website.TabIndex = 26;
            Website.Text = "&Website";
            Website.UseVisualStyleBackColor = true;
            Website.Click += Website_Click;
            // 
            // Description
            // 
            Description.Cursor = Cursors.IBeam;
            Description.Location = new Point(133, 70);
            Description.Margin = new Padding(9, 10, 9, 10);
            Description.Multiline = true;
            Description.Name = "Description";
            Description.ScrollBars = ScrollBars.Vertical;
            Description.Size = new Size(455, 94);
            Description.TabIndex = 27;
            // 
            // AdventOfCode
            // 
            AcceptButton = btnProcess;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(748, 474);
            Controls.Add(Description);
            Controls.Add(Website);
            Controls.Add(FullInput);
            Controls.Add(ProgressText);
            Controls.Add(lblProgress);
            Controls.Add(Progress);
            Controls.Add(inputNumber);
            Controls.Add(nextInput);
            Controls.Add(prevInput);
            Controls.Add(lblTimeTaken);
            Controls.Add(txtTimeTaken);
            Controls.Add(lblYear);
            Controls.Add(updYear);
            Controls.Add(lblExpected);
            Controls.Add(txtExpected);
            Controls.Add(btnBatch);
            Controls.Add(btnSuperBatch);
            Controls.Add(chkTestMode);
            Controls.Add(btnExit);
            Controls.Add(lblPuzzle);
            Controls.Add(lblDay);
            Controls.Add(lblInput);
            Controls.Add(lblOutput);
            Controls.Add(txtOutput);
            Controls.Add(btnProcess);
            Controls.Add(txtInput);
            Controls.Add(updPuzzle);
            Controls.Add(updDay);
            Margin = new Padding(9, 10, 9, 10);
            Name = "AdventOfCode";
            Text = "Advent Of Code";
            ((System.ComponentModel.ISupportInitialize)updDay).EndInit();
            ((System.ComponentModel.ISupportInitialize)updPuzzle).EndInit();
            ((System.ComponentModel.ISupportInitialize)updYear).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown updDay;
        private NumericUpDown updPuzzle;
        private TextBox txtInput;
        private Button btnProcess;
        private TextBox txtOutput;
        private Label lblOutput;
        private Label lblInput;
        private Label lblDay;
        private Label lblPuzzle;
        private Button btnExit;
        private CheckBox chkTestMode;
        private Button btnBatch;
        private Button btnSuperBatch;
        private Label lblExpected;
        private TextBox txtExpected;
        private Label lblYear;
        private NumericUpDown updYear;
        private Label lblTimeTaken;
        private TextBox txtTimeTaken;
        private Button prevInput;
        private Button nextInput;
        private TextBox inputNumber;
        private ProgressBar Progress;
        private Label lblProgress;
        private TextBox ProgressText;
        private System.ComponentModel.BackgroundWorker BatchWorker;
        private Button FullInput;
        private Button Website;
        private TextBox Description;
    }
}

