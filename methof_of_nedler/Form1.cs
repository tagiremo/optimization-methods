using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace methof_of_nedler
{
    public partial class Form1 : Form
    {
        private List<TextBox> startPointTextBoxes = new List<TextBox>();
        private bool isRunning;

        public Form1()
        {
            InitializeComponent();
            nudVariables.ValueChanged += NudVariables_ValueChanged;
            btnRun.Click += BtnRun_Click;
            UpdateStartPointFields();
            InitializeNumericUpDowns();

            // Установка начальных значений
            nudVariables.Value = 2;
            tbFunction.Text = "(x1-3)^2 + (x2+2)^2";
        }

        private void InitializeNumericUpDowns()
        {
            nudAlpha.DecimalPlaces = 2;
            nudAlpha.Minimum = 0.1M;
            nudAlpha.Maximum = 5.0M;
            nudAlpha.Increment = 0.1M;
            nudAlpha.Value = 1.0M;

            nudBeta.DecimalPlaces = 2;
            nudBeta.Minimum = 0.1M;
            nudBeta.Maximum = 0.9M;
            nudBeta.Increment = 0.1M;
            nudBeta.Value = 0.5M;

            nudGamma.DecimalPlaces = 2;
            nudGamma.Minimum = 1.0M;
            nudGamma.Maximum = 5.0M;
            nudGamma.Increment = 0.1M;
            nudGamma.Value = 2.0M;

            nudDelta.DecimalPlaces = 2;
            nudDelta.Minimum = 0.1M;
            nudDelta.Maximum = 0.9M;
            nudDelta.Increment = 0.1M;
            nudDelta.Value = 0.5M;

            nudTolerance.DecimalPlaces = 10;
            nudTolerance.Minimum = 0.0000000001M;
            nudTolerance.Maximum = 0.1M;
            nudTolerance.Increment = 0.0000001M;
            nudTolerance.Value = 0.000001M;

            nudMaxIter.DecimalPlaces = 0;
            nudMaxIter.Minimum = 1;
            nudMaxIter.Maximum = 100000;
            nudMaxIter.Increment = 100;
            nudMaxIter.Value = 1000;
        }

        private void NudVariables_ValueChanged(object sender, EventArgs e)
        {
            UpdateStartPointFields();
        }

        private void UpdateStartPointFields()
        {
            int dim = (int)nudVariables.Value;
            pnlStartPoint.Controls.Clear();
            startPointTextBoxes.Clear();

            for (int i = 0; i < dim; i++)
            {
                Label lbl = new Label
                {
                    Text = $"x{i + 1}:",
                    Location = new Point(10, 25 * i + 5),
                    AutoSize = true
                };
                TextBox tb = new TextBox
                {
                    Location = new Point(50, 25 * i + 2),
                    Width = 70,
                    Text = i == 0 ? "0" : "0"
                };
                pnlStartPoint.Controls.Add(lbl);
                pnlStartPoint.Controls.Add(tb);
                startPointTextBoxes.Add(tb);
            }
        }

        private async void BtnRun_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            try
            {
                isRunning = true;
                btnRun.Enabled = false;
                dgvIterations.Rows.Clear();

                // Очистка предыдущего графика
                if (pbPlot.Image != null)
                {
                    pbPlot.Image.Dispose();
                    pbPlot.Image = null;
                }

                var parameters = ParseInputParameters();
                var func = CreateFunction(parameters.Expression, parameters.Dimension);

                var optimizationResult = await Task.Run(() =>
                    NelderMeadSimplex.Minimize(
                        func,
                        parameters.StartPoint,
                        parameters.MaxIter,
                        parameters.Tolerance,
                        parameters.Alpha,
                        parameters.Beta,
                        parameters.Gamma,
                        parameters.Delta));

                DisplayResults(optimizationResult);
                DrawPlot(optimizationResult.History);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isRunning = false;
                btnRun.Enabled = true;
            }
        }

        private OptimizationParameters ParseInputParameters()
        {
            int dim = (int)nudVariables.Value;
            double[] x0 = new double[dim];

            for (int i = 0; i < dim; i++)
            {
                if (!double.TryParse(startPointTextBoxes[i].Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out x0[i]))
                    throw new Exception($"Некорректное значение координаты x{i + 1}. Используйте формат: 10.5 или -3.2");
            }

            string expr = tbFunction.Text.Trim();
            if (string.IsNullOrEmpty(expr))
                throw new Exception("Введите функцию");

            return new OptimizationParameters
            {
                Dimension = dim,
                StartPoint = x0,
                Expression = expr,
                Alpha = (double)nudAlpha.Value,
                Beta = (double)nudBeta.Value,
                Gamma = (double)nudGamma.Value,
                Delta = (double)nudDelta.Value,
                Tolerance = (double)nudTolerance.Value,
                MaxIter = (int)nudMaxIter.Value
            };
        }

        private void DisplayResults(NelderMeadResult result)
        {
            lblResult.Text = $"Минимум: F({string.Join(", ", result.BestPoint.Select(v => v.ToString("F10")))}) = {result.BestValue:F15}";

            dgvIterations.Rows.Clear();
            for (int i = 0; i < result.History.Count; i++)
            {
                var (pt, val) = result.History[i];
                string pointStr = string.Join("; ", pt.Select(v => v.ToString("F6")));
                dgvIterations.Rows.Add(i + 1, pointStr, val.ToString("F10"));
            }
        }

        private void DrawPlot(List<(double[] point, double value)> history)
        {
            int dim = (int)nudVariables.Value;

            if (pbPlot.Image != null)
            {
                pbPlot.Image.Dispose();
            }

            if (dim != 2 || history.Count < 2)
            {
                Bitmap placeholder = new Bitmap(pbPlot.Width, pbPlot.Height);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.White);
                    string message = dim != 2 ? "График доступен только для 2D" : "Недостаточно данных для графика";
                    using (Font font = new Font("Arial", 12))
                    {
                        g.DrawString(message, font, Brushes.Gray, 10, 10);
                    }
                }
                pbPlot.Image = placeholder;
                return;
            }

            Bitmap bmp = new Bitmap(pbPlot.Width, pbPlot.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                var bounds = CalculatePlotBounds(history);
                int padding = 40;
                int plotWidth = bmp.Width - padding * 2;
                int plotHeight = bmp.Height - padding * 2;

                int ToScreenX(double x) => padding + (int)((x - bounds.MinX) / (bounds.MaxX - bounds.MinX) * plotWidth);
                int ToScreenY(double y) => padding + plotHeight - (int)((y - bounds.MinY) / (bounds.MaxY - bounds.MinY) * plotHeight);

                DrawGrid(g, bounds.MinX, bounds.MaxX, bounds.MinY, bounds.MaxY, padding, plotWidth, plotHeight, ToScreenX, ToScreenY);
                DrawAxes(g, bounds, padding, plotWidth, plotHeight, ToScreenX, ToScreenY);
                DrawTrajectory(g, history, ToScreenX, ToScreenY);
                DrawPoints(g, history, ToScreenX, ToScreenY);
                DrawLabels(g, history, ToScreenX, ToScreenY);
            }

            pbPlot.Image = bmp;
        }

        private (double MinX, double MaxX, double MinY, double MaxY) CalculatePlotBounds(List<(double[] point, double value)> history)
        {
            double minX = history.Min(p => p.point[0]);
            double maxX = history.Max(p => p.point[0]);
            double minY = history.Min(p => p.point[1]);
            double maxY = history.Max(p => p.point[1]);

            double rangeX = maxX - minX;
            double rangeY = maxY - minY;

            if (rangeX < 0.001) rangeX = 1;
            if (rangeY < 0.001) rangeY = 1;

            double marginX = rangeX * 0.15;
            double marginY = rangeY * 0.15;

            return (minX - marginX, maxX + marginX, minY - marginY, maxY + marginY);
        }

        private void DrawGrid(Graphics g, double minX, double maxX, double minY, double maxY,
            int padding, int plotWidth, int plotHeight,
            Func<double, int> toScreenX, Func<double, int> toScreenY)
        {
            double rangeX = maxX - minX;
            double rangeY = maxY - minY;

            // Оптимальный шаг сетки
            double stepX = Math.Pow(10, Math.Floor(Math.Log10(rangeX / 5)));
            double stepY = Math.Pow(10, Math.Floor(Math.Log10(rangeY / 5)));

            using (Pen gridPen = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dot })
            using (Font axisFont = new Font("Arial", 8))
            using (StringFormat sf = new StringFormat())
            {
                // Вертикальные линии
                double startX = Math.Ceiling(minX / stepX) * stepX;
                for (double x = startX; x <= maxX; x += stepX)
                {
                    int screenX = toScreenX(x);
                    if (screenX >= padding && screenX <= padding + plotWidth)
                        g.DrawLine(gridPen, screenX, padding, screenX, padding + plotHeight);
                }

                // Горизонтальные линии
                double startY = Math.Ceiling(minY / stepY) * stepY;
                for (double y = startY; y <= maxY; y += stepY)
                {
                    int screenY = toScreenY(y);
                    if (screenY >= padding && screenY <= padding + plotHeight)
                        g.DrawLine(gridPen, padding, screenY, padding + plotWidth, screenY);
                }

                // Подписи осей X
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Near;
                for (double x = startX; x <= maxX; x += stepX)
                {
                    int screenX = toScreenX(x);
                    if (screenX >= padding && screenX <= padding + plotWidth)
                        g.DrawString(x.ToString("0.###"), axisFont, Brushes.Black,
                            screenX, padding + plotHeight + 5, sf);
                }

                // Подписи оси Y
                sf.Alignment = StringAlignment.Far;
                sf.LineAlignment = StringAlignment.Center;
                for (double y = startY; y <= maxY; y += stepY)
                {
                    int screenY = toScreenY(y);
                    if (screenY >= padding && screenY <= padding + plotHeight)
                        g.DrawString(y.ToString("0.###"), axisFont, Brushes.Black,
                            padding - 5, screenY, sf);
                }
            }
        }

        private void DrawAxes(Graphics g, (double MinX, double MaxX, double MinY, double MaxY) bounds,
            int padding, int plotWidth, int plotHeight,
            Func<double, int> toScreenX, Func<double, int> toScreenY)
        {
            using (Pen axisPen = new Pen(Color.Black, 2))
            {
                // Ось X (y=0)
                int yAxisY = toScreenY(0);
                if (yAxisY >= padding && yAxisY <= padding + plotHeight)
                {
                    g.DrawLine(axisPen, padding, yAxisY, padding + plotWidth, yAxisY);
                }

                // Ось Y (x=0)
                int xAxisX = toScreenX(0);
                if (xAxisX >= padding && xAxisX <= padding + plotWidth)
                {
                    g.DrawLine(axisPen, xAxisX, padding, xAxisX, padding + plotHeight);
                }
            }
        }

        private void DrawTrajectory(Graphics g, List<(double[] point, double value)> history,
            Func<double, int> toScreenX, Func<double, int> toScreenY)
        {
            using (Pen pen = new Pen(Color.FromArgb(100, 0, 0, 255), 2))
            {
                for (int i = 0; i < history.Count - 1; i++)
                {
                    Point p1 = new Point(toScreenX(history[i].point[0]), toScreenY(history[i].point[1]));
                    Point p2 = new Point(toScreenX(history[i + 1].point[0]), toScreenY(history[i + 1].point[1]));
                    g.DrawLine(pen, p1, p2);
                }
            }
        }

        private void DrawPoints(Graphics g, List<(double[] point, double value)> history,
            Func<double, int> toScreenX, Func<double, int> toScreenY)
        {
            for (int i = 0; i < history.Count; i++)
            {
                int x = toScreenX(history[i].point[0]);
                int y = toScreenY(history[i].point[1]);

                if (i == 0)
                {
                    using (Brush brush = new SolidBrush(Color.FromArgb(180, 255, 0, 0)))
                    {
                        g.FillEllipse(brush, x - 5, y - 5, 10, 10);
                    }
                    g.DrawEllipse(Pens.DarkRed, x - 5, y - 5, 10, 10);
                }
                else if (i == history.Count - 1)
                {
                    using (Brush brush = new SolidBrush(Color.FromArgb(180, 0, 255, 0)))
                    {
                        g.FillEllipse(brush, x - 6, y - 6, 12, 12);
                    }
                    g.DrawEllipse(Pens.DarkGreen, x - 6, y - 6, 12, 12);
                }
                else if (i % 10 == 0 || i == history.Count - 2)
                {
                    using (Brush brush = new SolidBrush(Color.FromArgb(120, 0, 0, 255)))
                    {
                        g.FillEllipse(brush, x - 3, y - 3, 6, 6);
                    }
                }
            }
        }

        private void DrawLabels(Graphics g, List<(double[] point, double value)> history,
            Func<double, int> toScreenX, Func<double, int> toScreenY)
        {
            using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
            {
                int startX = toScreenX(history[0].point[0]);
                int startY = toScreenY(history[0].point[1]);
                g.DrawString("Старт", labelFont, Brushes.Red, startX + 8, startY - 20);

                int endX = toScreenX(history.Last().point[0]);
                int endY = toScreenY(history.Last().point[1]);
                g.DrawString("Финиш", labelFont, Brushes.Green, endX + 8, endY - 20);
            }
        }

        private Func<double[], double> CreateFunction(string expression, int variableCount)
        {
            string processed = ReplacePowerOperator(expression);

            // Замена переменных x1, x2, ... на x[0], x[1], ...
            for (int i = variableCount; i >= 1; i--)
                processed = Regex.Replace(processed, $@"\bx{i}\b", $"x[{i - 1}]");

            // Поддержка коротких имён для 2D
            if (variableCount >= 1)
                processed = Regex.Replace(processed, @"\bx\b(?!\[)", "x[0]");
            if (variableCount >= 2)
                processed = Regex.Replace(processed, @"\by\b(?!\[)", "x[1]");
            if (variableCount >= 3)
                processed = Regex.Replace(processed, @"\bz\b(?!\[)", "x[2]");

            // Замена математических функций
            processed = processed.Replace("sin", "Math.Sin");
            processed = processed.Replace("cos", "Math.Cos");
            processed = processed.Replace("tan", "Math.Tan");
            processed = processed.Replace("exp", "Math.Exp");
            processed = processed.Replace("sqrt", "Math.Sqrt");
            processed = processed.Replace("abs", "Math.Abs");
            processed = processed.Replace("log10", "Math.Log10");
            processed = processed.Replace("log", "Math.Log");

            string code = $@"
using System;
public static class DynamicFunction
{{
    public static double Evaluate(double[] x)
    {{
        return {processed};
    }}
}}";

            using (CSharpCodeProvider provider = new CSharpCodeProvider())
            {
                CompilerParameters parameters = new CompilerParameters
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false
                };

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {
                    string errors = string.Join("\n",
                        results.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                    throw new Exception("Ошибка компиляции функции:\n" + errors +
                                        "\n\nСгенерированный код:\n" + code);
                }

                var assembly = results.CompiledAssembly;
                var type = assembly.GetType("DynamicFunction");
                var method = type.GetMethod("Evaluate");
                return (Func<double[], double>)Delegate.CreateDelegate(typeof(Func<double[], double>), method);
            }
        }

        private string ReplacePowerOperator(string expr)
        {
            string result = expr;

            // Обработка вложенных скобок
            int maxDepth = 10;
            for (int depth = 0; depth < maxDepth; depth++)
            {
                var match = Regex.Match(result, @"\(([^()]+)\)\^(\d+(?:\.\d+)?|[a-zA-Z]\w*)");
                if (!match.Success) break;
                result = Regex.Replace(result, @"\(([^()]+)\)\^(\d+(?:\.\d+)?|[a-zA-Z]\w*)", "Math.Pow($1,$2)");
            }

            // Обработка чисел в степени
            result = Regex.Replace(result, @"(\d+(?:\.\d+)?)\^(\d+(?:\.\d+)?)", "Math.Pow($1,$2)");

            // Обработка переменных в степени
            result = Regex.Replace(result, @"([a-zA-Z]\w*)\^(\d+(?:\.\d+)?)", "Math.Pow($1,$2)");
            result = Regex.Replace(result, @"([a-zA-Z]\w*)\^([a-zA-Z]\w*)", "Math.Pow($1,$2)");

            return result;
        }
    }

    public class OptimizationParameters
    {
        public int Dimension { get; set; }
        public double[] StartPoint { get; set; }
        public string Expression { get; set; }
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Gamma { get; set; }
        public double Delta { get; set; }
        public double Tolerance { get; set; }
        public int MaxIter { get; set; }
    }

    public class NelderMeadResult
    {
        public double[] BestPoint { get; set; }
        public double BestValue { get; set; }
        public List<(double[] point, double value)> History { get; set; }
    }

    public static class NelderMeadSimplex
    {
        public static NelderMeadResult Minimize(
            Func<double[], double> func,
            double[] x0,
            int maxIter = 1000,
            double tolerance = 1e-6,
            double alpha = 1.0,
            double beta = 0.5,
            double gamma = 2.0,
            double delta = 0.5)
        {
            int n = x0.Length;

            // Инициализация симплекса (исправлено - создание копий)
            List<double[]> simplex = new List<double[]>();
            simplex.Add(x0.ToArray()); // Копия начальной точки

            for (int i = 0; i < n; i++)
            {
                double[] point = x0.ToArray();
                point[i] = point[i] == 0 ? 0.00025 : point[i] * 1.05;
                simplex.Add(point);
            }

            double[] values = simplex.Select(v => func(v)).ToArray();
            var history = new List<(double[], double)>();
            history.Add((simplex[0].ToArray(), values[0]));

            for (int iter = 0; iter < maxIter; iter++)
            {
                // Сортировка симплекса по значениям функции
                var indices = Enumerable.Range(0, n + 1).OrderBy(i => values[i]).ToArray();
                var sortedSimplex = indices.Select(i => simplex[i]).ToArray();
                var sortedValues = indices.Select(i => values[i]).ToArray();

                // Проверка сходимости
                double range = sortedValues[n] - sortedValues[0];
                if (range < tolerance)
                    break;

                // Вычисление центроида (исправлено)
                double[] centroid = new double[n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        centroid[j] += sortedSimplex[i][j];
                }
                for (int j = 0; j < n; j++)
                    centroid[j] /= n;

                // Отражение
                double[] reflected = new double[n];
                for (int j = 0; j < n; j++)
                    reflected[j] = centroid[j] + alpha * (centroid[j] - sortedSimplex[n][j]);
                double fReflected = func(reflected);

                if (fReflected < sortedValues[0])
                {
                    // Растяжение
                    double[] expanded = new double[n];
                    for (int j = 0; j < n; j++)
                        expanded[j] = centroid[j] + gamma * (reflected[j] - centroid[j]);
                    double fExpanded = func(expanded);

                    if (fExpanded < fReflected)
                    {
                        simplex[indices[n]] = expanded;
                        values[indices[n]] = fExpanded;
                    }
                    else
                    {
                        simplex[indices[n]] = reflected;
                        values[indices[n]] = fReflected;
                    }
                }
                else if (fReflected < sortedValues[n - 1])
                {
                    simplex[indices[n]] = reflected;
                    values[indices[n]] = fReflected;
                }
                else
                {
                    // Сжатие
                    double[] contracted;
                    double fContracted;

                    if (fReflected < sortedValues[n])
                    {
                        contracted = new double[n];
                        for (int j = 0; j < n; j++)
                            contracted[j] = centroid[j] + beta * (reflected[j] - centroid[j]);
                        fContracted = func(contracted);

                        if (fContracted <= fReflected)
                        {
                            simplex[indices[n]] = contracted;
                            values[indices[n]] = fContracted;
                        }
                        else
                        {
                            Shrink(simplex, values, sortedSimplex[0], func, delta);
                        }
                    }
                    else
                    {
                        contracted = new double[n];
                        for (int j = 0; j < n; j++)
                            contracted[j] = centroid[j] + beta * (sortedSimplex[n][j] - centroid[j]);
                        fContracted = func(contracted);

                        if (fContracted < sortedValues[n])
                        {
                            simplex[indices[n]] = contracted;
                            values[indices[n]] = fContracted;
                        }
                        else
                        {
                            Shrink(simplex, values, sortedSimplex[0], func, delta);
                        }
                    }
                }

                // Сохранение лучшей точки в истории
                double currentBest = values.Min();
                int bestIdx = Array.IndexOf(values, currentBest);
                history.Add((simplex[bestIdx].ToArray(), currentBest));
            }

            double minVal = values.Min();
            int minIdx = Array.IndexOf(values, minVal);

            return new NelderMeadResult
            {
                BestPoint = simplex[minIdx].ToArray(),
                BestValue = minVal,
                History = history
            };
        }

        private static void Shrink(List<double[]> simplex, double[] values,
            double[] bestPoint, Func<double[], double> func, double delta)
        {
            int n = bestPoint.Length;
            for (int i = 0; i < simplex.Count; i++)
            {
                // Пропускаем лучшую точку
                if (simplex[i] == bestPoint) continue;

                for (int j = 0; j < n; j++)
                    simplex[i][j] = bestPoint[j] + delta * (simplex[i][j] - bestPoint[j]);
                values[i] = func(simplex[i]);
            }
        }
    }
}