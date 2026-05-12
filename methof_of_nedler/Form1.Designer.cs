namespace methof_of_nedler
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbFunction = new System.Windows.Forms.TextBox();
            this.nudVariables = new System.Windows.Forms.NumericUpDown();
            this.pnlStartPoint = new System.Windows.Forms.Panel();
            this.nudAlpha = new System.Windows.Forms.NumericUpDown();
            this.nudBeta = new System.Windows.Forms.NumericUpDown();
            this.nudGamma = new System.Windows.Forms.NumericUpDown();
            this.nudDelta = new System.Windows.Forms.NumericUpDown();
            this.nudTolerance = new System.Windows.Forms.NumericUpDown();
            this.nudMaxIter = new System.Windows.Forms.NumericUpDown();
            this.btnRun = new System.Windows.Forms.Button();
            this.dgvIterations = new System.Windows.Forms.DataGridView();
            this.lblResult = new System.Windows.Forms.Label();
            this.pbPlot = new System.Windows.Forms.PictureBox();
            this.Iteration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBeta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlot)).BeginInit();
            this.SuspendLayout();
            // 
            // tbFunction
            // 
            this.tbFunction.Location = new System.Drawing.Point(62, 12);
            this.tbFunction.Name = "tbFunction";
            this.tbFunction.Size = new System.Drawing.Size(275, 20);
            this.tbFunction.TabIndex = 0;
            // 
            // nudVariables
            // 
            this.nudVariables.Location = new System.Drawing.Point(62, 38);
            this.nudVariables.Name = "nudVariables";
            this.nudVariables.Size = new System.Drawing.Size(46, 20);
            this.nudVariables.TabIndex = 1;
            // 
            // pnlStartPoint
            // 
            this.pnlStartPoint.Location = new System.Drawing.Point(62, 68);
            this.pnlStartPoint.Name = "pnlStartPoint";
            this.pnlStartPoint.Size = new System.Drawing.Size(200, 100);
            this.pnlStartPoint.TabIndex = 2;
            // 
            // nudAlpha
            // 
            this.nudAlpha.Location = new System.Drawing.Point(62, 174);
            this.nudAlpha.Name = "nudAlpha";
            this.nudAlpha.Size = new System.Drawing.Size(120, 20);
            this.nudAlpha.TabIndex = 0;
            // 
            // nudBeta
            // 
            this.nudBeta.Location = new System.Drawing.Point(62, 200);
            this.nudBeta.Name = "nudBeta";
            this.nudBeta.Size = new System.Drawing.Size(120, 20);
            this.nudBeta.TabIndex = 3;
            // 
            // nudGamma
            // 
            this.nudGamma.Location = new System.Drawing.Point(62, 278);
            this.nudGamma.Name = "nudGamma";
            this.nudGamma.Size = new System.Drawing.Size(120, 20);
            this.nudGamma.TabIndex = 4;
            // 
            // nudDelta
            // 
            this.nudDelta.Location = new System.Drawing.Point(62, 252);
            this.nudDelta.Name = "nudDelta";
            this.nudDelta.Size = new System.Drawing.Size(120, 20);
            this.nudDelta.TabIndex = 5;
            // 
            // nudTolerance
            // 
            this.nudTolerance.Location = new System.Drawing.Point(62, 226);
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(120, 20);
            this.nudTolerance.TabIndex = 6;
            // 
            // nudMaxIter
            // 
            this.nudMaxIter.Location = new System.Drawing.Point(62, 304);
            this.nudMaxIter.Name = "nudMaxIter";
            this.nudMaxIter.Size = new System.Drawing.Size(120, 20);
            this.nudMaxIter.TabIndex = 7;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(62, 330);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "button1";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click_1);
            // 
            // dgvIterations
            // 
            this.dgvIterations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIterations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Iteration,
            this.Point,
            this.Value});
            this.dgvIterations.Location = new System.Drawing.Point(289, 38);
            this.dgvIterations.Name = "dgvIterations";
            this.dgvIterations.Size = new System.Drawing.Size(344, 150);
            this.dgvIterations.TabIndex = 9;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(690, 45);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(35, 13);
            this.lblResult.TabIndex = 10;
            this.lblResult.Text = "label1";
            // 
            // pbPlot
            // 
            this.pbPlot.Location = new System.Drawing.Point(259, 226);
            this.pbPlot.Name = "pbPlot";
            this.pbPlot.Size = new System.Drawing.Size(598, 323);
            this.pbPlot.TabIndex = 11;
            this.pbPlot.TabStop = false;
            // 
            // Iteration
            // 
            this.Iteration.HeaderText = "№";
            this.Iteration.Name = "Iteration";
            // 
            // Point
            // 
            this.Point.HeaderText = "Точка";
            this.Point.Name = "Point";
            // 
            // Value
            // 
            this.Value.HeaderText = "Значение функции";
            this.Value.Name = "Value";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.pbPlot);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.dgvIterations);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.nudMaxIter);
            this.Controls.Add(this.nudTolerance);
            this.Controls.Add(this.nudDelta);
            this.Controls.Add(this.nudGamma);
            this.Controls.Add(this.nudBeta);
            this.Controls.Add(this.nudAlpha);
            this.Controls.Add(this.pnlStartPoint);
            this.Controls.Add(this.nudVariables);
            this.Controls.Add(this.tbFunction);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBeta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFunction;
        private System.Windows.Forms.NumericUpDown nudVariables;
        private System.Windows.Forms.Panel pnlStartPoint;
        private System.Windows.Forms.NumericUpDown nudAlpha;
        private System.Windows.Forms.NumericUpDown nudBeta;
        private System.Windows.Forms.NumericUpDown nudGamma;
        private System.Windows.Forms.NumericUpDown nudDelta;
        private System.Windows.Forms.NumericUpDown nudTolerance;
        private System.Windows.Forms.NumericUpDown nudMaxIter;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.DataGridView dgvIterations;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.PictureBox pbPlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Point;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}

