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
            this.Iteration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResult = new System.Windows.Forms.Label();
            this.pbPlot = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
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
            this.tbFunction.Location = new System.Drawing.Point(105, 8);
            this.tbFunction.Name = "tbFunction";
            this.tbFunction.Size = new System.Drawing.Size(275, 20);
            this.tbFunction.TabIndex = 0;
            // 
            // nudVariables
            // 
            this.nudVariables.Location = new System.Drawing.Point(105, 36);
            this.nudVariables.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVariables.Name = "nudVariables";
            this.nudVariables.Size = new System.Drawing.Size(46, 20);
            this.nudVariables.TabIndex = 1;
            this.nudVariables.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // pnlStartPoint
            // 
            this.pnlStartPoint.Location = new System.Drawing.Point(105, 68);
            this.pnlStartPoint.Name = "pnlStartPoint";
            this.pnlStartPoint.Size = new System.Drawing.Size(211, 100);
            this.pnlStartPoint.TabIndex = 2;
            // 
            // nudAlpha
            // 
            this.nudAlpha.Location = new System.Drawing.Point(95, 174);
            this.nudAlpha.Name = "nudAlpha";
            this.nudAlpha.Size = new System.Drawing.Size(120, 20);
            this.nudAlpha.TabIndex = 3;
            // 
            // nudBeta
            // 
            this.nudBeta.Location = new System.Drawing.Point(95, 200);
            this.nudBeta.Name = "nudBeta";
            this.nudBeta.Size = new System.Drawing.Size(120, 20);
            this.nudBeta.TabIndex = 4;
            // 
            // nudGamma
            // 
            this.nudGamma.Location = new System.Drawing.Point(95, 278);
            this.nudGamma.Name = "nudGamma";
            this.nudGamma.Size = new System.Drawing.Size(120, 20);
            this.nudGamma.TabIndex = 7;
            // 
            // nudDelta
            // 
            this.nudDelta.Location = new System.Drawing.Point(95, 252);
            this.nudDelta.Name = "nudDelta";
            this.nudDelta.Size = new System.Drawing.Size(120, 20);
            this.nudDelta.TabIndex = 6;
            // 
            // nudTolerance
            // 
            this.nudTolerance.Location = new System.Drawing.Point(95, 226);
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(120, 20);
            this.nudTolerance.TabIndex = 5;
            // 
            // nudMaxIter
            // 
            this.nudMaxIter.Location = new System.Drawing.Point(95, 304);
            this.nudMaxIter.Name = "nudMaxIter";
            this.nudMaxIter.Size = new System.Drawing.Size(120, 20);
            this.nudMaxIter.TabIndex = 8;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(95, 330);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Начать";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // dgvIterations
            // 
            this.dgvIterations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIterations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Iteration,
            this.Point,
            this.Value});
            this.dgvIterations.Location = new System.Drawing.Point(513, 68);
            this.dgvIterations.Name = "dgvIterations";
            this.dgvIterations.Size = new System.Drawing.Size(344, 150);
            this.dgvIterations.TabIndex = 10;
            // 
            // Iteration
            // 
            this.Iteration.HeaderText = "№";
            this.Iteration.Name = "Iteration";
            this.Iteration.Width = 50;
            // 
            // Point
            // 
            this.Point.HeaderText = "Точка";
            this.Point.Name = "Point";
            this.Point.Width = 150;
            // 
            // Value
            // 
            this.Value.HeaderText = "Значение функции";
            this.Value.Name = "Value";
            this.Value.Width = 120;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(510, 15);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(35, 13);
            this.lblResult.TabIndex = 11;
            this.lblResult.Text = "label1";
            // 
            // pbPlot
            // 
            this.pbPlot.Location = new System.Drawing.Point(259, 226);
            this.pbPlot.Name = "pbPlot";
            this.pbPlot.Size = new System.Drawing.Size(598, 323);
            this.pbPlot.TabIndex = 12;
            this.pbPlot.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Размерность";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Функция";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Начальная точка";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "alpha";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "beta";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 285);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "gamma";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "delta";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 233);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "точность";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 311);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "число итераций";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(410, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Решение:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
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
            this.Text = "Метод Нелдера-Мида";
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}