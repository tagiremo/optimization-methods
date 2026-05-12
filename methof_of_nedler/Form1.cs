using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace methof_of_nedler
{
    public partial class Form1 : Form
    {
        private List<TextBox> startPointTextBoxes = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();
            nudVariables.ValueChanged += NudVariables_ValueChanged;
            btnRun.Click += BtnRun_Click;
            UpdateStartPointFields();
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
                    Text = "0"
                };
                pnlStartPoint.Controls.Add(lbl);
                pnlStartPoint.Controls.Add(tb);
                startPointTextBoxes.Add(tb);
            }
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            try
            {
                int dim = (int)nudVariables.Value;
                double[] x0 = new double[dim];

                for (int i = 0; i < dim; i++)
                {
                    if (!double.TryParse(startPointTextBoxes[i].Text, out x0[i]))
                        throw new Exception($"Некорректное значение координаты x{i + 1}");
                }

                double alpha = (double)nudAlpha.Value;
                double beta = (double)nudBeta.Value;
                double gamma = (double)nudGamma.Value;
                double delta = (double)nudDelta.Value;
                double tol = (double)nudTolerance.Value;
                int maxIter = (int)nudMaxIter.Value;

                string expr = tbFunction.Text.Trim();
                if (string.IsNullOrEmpty(expr))
                    throw new Exception("Введите функцию");

                Func<double[], double> func = CreateFunction(expr, dim);

                var optimizationResult = NelderMeadSimplex.Minimize(
                    func, x0, maxIter, tol, alpha, beta, gamma, delta);

                lblResult.Text = $"Минимум: F({string.Join(", ", optimizationResult.BestPoint.Select(v => v.ToString("F6")))}) = {optimizationResult.BestValue:F10}";

                dgvIterations.Rows.Clear();
                for (int i = 0; i < optimizationResult.History.Count; i++)
                {
                    var (pt, val) = optimizationResult.History[i];
                    string pointStr = string.Join("; ", pt.Select(v => v.ToString("F4")));
                    dgvIterations.Rows.Add(i, pointStr, val.ToString("F8"));
                }

                if (dim == 2)
                {
                    pbPlot.Image?.Dispose(); 
                    DrawPlot2D(optimizationResult.History);
                }
                else
                {
                    pbPlot.Image?.Dispose();
                    Bitmap bmp = new Bitmap(pbPlot.Width, pbPlot.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        g.DrawString("График доступен только для 2D",
                            new Font("Arial", 12), Brushes.Gray, 10, 10);
                    }
                    pbPlot.Image = bmp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Func<double[], double> CreateFunction(string expression, int variableCount)
        {
            string processed = ReplacePowerOperator(expression);

            for (int i = variableCount; i >= 1; i--)
                processed = Regex.Replace(processed, $@"\bx{i}\b", $"x[{i - 1}]");

            if (variableCount >= 1)
                processed = Regex.Replace(processed, @"\bx\b(?!\[)", "x[0]");
            if (variableCount >= 2)
                processed = Regex.Replace(processed, @"\by\b(?!\[)", "x[1]");
            if (variableCount >= 3)
                processed = Regex.Replace(processed, @"\bz\b(?!\[)", "x[2]");

            processed = processed.Replace("sin", "Math.Sin");
            processed = processed.Replace("cos", "Math.Cos");
            processed = processed.Replace("tan", "Math.Tan");
            processed = processed.Replace("exp", "Math.Exp");
            processed = processed.Replace("sqrt", "Math.Sqrt");
            processed = processed.Replace("abs", "Math.Abs");
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
            result = Regex.Replace(result, @"\(([^()]+)\)\^(\d+(\.\d+)?|[a-zA-Z]\w*)", "Math.Pow($1,$2)");
            result = Regex.Replace(result, @"(\d+(\.\d+)?)\^(\d+(\.\d+)?)", "Math.Pow($1,$3)");
            result = Regex.Replace(result, @"([a-zA-Z]\w*)\^(\d+(\.\d+)?)", "Math.Pow($1,$2)");
            result = Regex.Replace(result, @"([a-zA-Z]\w*)\^([a-zA-Z]\w*)", "Math.Pow($1,$2)");
            return result;
        }

        private void DrawPlot2D(List<(double[] point, double value)> history)
        {
            if (history.Count < 2)
            {
                // Если истории нет, просто очищаем
                Bitmap emptyBmp = new Bitmap(pbPlot.Width, pbPlot.Height);
                using (Graphics g = Graphics.FromImage(emptyBmp))
                {
                    g.Clear(Color.White);
                    g.DrawString("Недостаточно данных для графика",
                        new Font("Arial", 12), Brushes.Gray, 10, 10);
                }
                pbPlot.Image?.Dispose();
                pbPlot.Image = emptyBmp;
                return;
            }

            Bitmap bmp = new Bitmap(pbPlot.Width, pbPlot.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Определяем границы
                double minX = history.Min(p => p.point[0]);
                double maxX = history.Max(p => p.point[0]);
                double minY = history.Min(p => p.point[1]);
                double maxY = history.Max(p => p.point[1]);

                // Добавляем отступы (минимум 0.5, чтобы не было нулевого диапазона)
                double rangeX = maxX - minX;
                double rangeY = maxY - minY;

                if (rangeX < 0.001) rangeX = 1;
                if (rangeY < 0.001) rangeY = 1;

                double marginX = rangeX * 0.15;
                double marginY = rangeY * 0.15;

                minX -= marginX;
                maxX += marginX;
                minY -= marginY;
                maxY += marginY;

                // Размеры области рисования (с отступами для осей)
                int padding = 40;
                int plotWidth = bmp.Width - padding * 2;
                int plotHeight = bmp.Height - padding * 2;

                // Функции преобразования координат
                int ToScreenX(double x) => padding + (int)((x - minX) / (maxX - minX) * plotWidth);
                int ToScreenY(double y) => padding + plotHeight - (int)((y - minY) / (maxY - minY) * plotHeight);

                // Рисуем сетку
                DrawGrid(g, minX, maxX, minY, maxY, padding, plotWidth, plotHeight, ToScreenX, ToScreenY);

                // Рисуем оси
                using (Pen axisPen = new Pen(Color.Black, 2))
                {
                    // Ось X
                    int yAxisY = ToScreenY(0);
                    if (yAxisY < padding) yAxisY = padding;
                    if (yAxisY > padding + plotHeight) yAxisY = padding + plotHeight;
                    g.DrawLine(axisPen, padding, yAxisY, padding + plotWidth, yAxisY);

                    // Ось Y
                    int xAxisX = ToScreenX(0);
                    if (xAxisX < padding) xAxisX = padding;
                    if (xAxisX > padding + plotWidth) xAxisX = padding + plotWidth;
                    g.DrawLine(axisPen, xAxisX, padding, xAxisX, padding + plotHeight);
                }

                // Рисуем траекторию
                using (Pen pen = new Pen(Color.Blue, 2))
                {
                    for (int i = 0; i < history.Count - 1; i++)
                    {
                        Point p1 = new Point(ToScreenX(history[i].point[0]), ToScreenY(history[i].point[1]));
                        Point p2 = new Point(ToScreenX(history[i + 1].point[0]), ToScreenY(history[i + 1].point[1]));
                        g.DrawLine(pen, p1, p2);
                    }
                }

                // Рисуем точки
                for (int i = 0; i < history.Count; i++)
                {
                    int x = ToScreenX(history[i].point[0]);
                    int y = ToScreenY(history[i].point[1]);

                    if (i == 0) // Начальная точка - красная
                    {
                        g.FillEllipse(Brushes.Red, x - 5, y - 5, 10, 10);
                        g.DrawEllipse(Pens.DarkRed, x - 5, y - 5, 10, 10);
                    }
                    else if (i == history.Count - 1) // Конечная точка - зелёная
                    {
                        g.FillEllipse(Brushes.Green, x - 6, y - 6, 12, 12);
                        g.DrawEllipse(Pens.DarkGreen, x - 6, y - 6, 12, 12);
                    }
                    else // Промежуточные точки - маленькие синие
                    {
                        g.FillEllipse(Brushes.Blue, x - 2, y - 2, 4, 4);
                    }
                }

                // Подписываем начальную и конечную точки
                using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
                {
                    int startX = ToScreenX(history[0].point[0]);
                    int startY = ToScreenY(history[0].point[1]);
                    g.DrawString("Старт", labelFont, Brushes.Red, startX + 8, startY - 20);

                    int endX = ToScreenX(history.Last().point[0]);
                    int endY = ToScreenY(history.Last().point[1]);
                    g.DrawString("Финиш", labelFont, Brushes.Green, endX + 8, endY - 20);
                }
            }

            pbPlot.Image?.Dispose();
            pbPlot.Image = bmp;
        }

        private void DrawGrid(Graphics g, double minX, double maxX, double minY, double maxY,
            int padding, int plotWidth, int plotHeight,
            Func<double, int> toScreenX, Func<double, int> toScreenY)
        {
            // Вычисляем шаг сетки
            double rangeX = maxX - minX;
            double rangeY = maxY - minY;

            double stepX = Math.Pow(10, Math.Floor(Math.Log10(rangeX)));
            double stepY = Math.Pow(10, Math.Floor(Math.Log10(rangeY)));

            if (rangeX / stepX < 4) stepX /= 2;
            if (rangeY / stepY < 4) stepY /= 2;

            using (Pen gridPen = new Pen(Color.LightGray, 1))
            {
                gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                // Вертикальные линии
                double startX = Math.Ceiling(minX / stepX) * stepX;
                for (double x = startX; x <= maxX; x += stepX)
                {
                    int screenX = toScreenX(x);
                    g.DrawLine(gridPen, screenX, padding, screenX, padding + plotHeight);
                }

                // Горизонтальные линии
                double startY = Math.Ceiling(minY / stepY) * stepY;
                for (double y = startY; y <= maxY; y += stepY)
                {
                    int screenY = toScreenY(y);
                    g.DrawLine(gridPen, padding, screenY, padding + plotWidth, screenY);
                }
            }

            // Подписи к осям
            using (Font axisFont = new Font("Arial", 8))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Near;

                double startX = Math.Ceiling(minX / stepX) * stepX;
                for (double x = startX; x <= maxX; x += stepX)
                {
                    int screenX = toScreenX(x);
                    g.DrawString(x.ToString("0.##"), axisFont, Brushes.Black,
                        screenX, padding + plotHeight + 5, sf);
                }

                sf.Alignment = StringAlignment.Far;
                sf.LineAlignment = StringAlignment.Center;

                double startY = Math.Ceiling(minY / stepY) * stepY;
                for (double y = startY; y <= maxY; y += stepY)
                {
                    int screenY = toScreenY(y);
                    g.DrawString(y.ToString("0.##"), axisFont, Brushes.Black,
                        padding - 5, screenY, sf);
                }
            }
        }

        private void btnRun_Click_1(object sender, EventArgs e)
        {

        }
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

            List<double[]> simplex = new List<double[]>();
            simplex.Add((double[])x0.Clone());

            for (int i = 0; i < n; i++)
            {
                double[] point = (double[])x0.Clone();
                point[i] = x0[i] != 0 ? x0[i] * 1.05 + 0.001 : 0.00025;
                simplex.Add(point);
            }

            double[] values = simplex.Select(v => func(v)).ToArray();
            var history = new List<(double[], double)>();
            history.Add(((double[])simplex[0].Clone(), values[0]));

            for (int iter = 0; iter < maxIter; iter++)
            {
                int[] indices = Enumerable.Range(0, n + 1).ToArray();
                Array.Sort(indices, (a, b) => values[a].CompareTo(values[b]));

                var sortedSimplex = indices.Select(i => simplex[i]).ToArray();
                var sortedValues = indices.Select(i => values[i]).ToArray();

                double range = sortedValues[n] - sortedValues[0];
                if (range < tolerance)
                    break;

                double[] centroid = new double[n];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        centroid[j] += sortedSimplex[i][j];
                for (int j = 0; j < n; j++)
                    centroid[j] /= n;

                double[] xr = new double[n];
                for (int j = 0; j < n; j++)
                    xr[j] = centroid[j] + alpha * (centroid[j] - sortedSimplex[n][j]);
                double fxr = func(xr);

                if (fxr < sortedValues[0])
                {
                    double[] xe = new double[n];
                    for (int j = 0; j < n; j++)
                        xe[j] = centroid[j] + gamma * (xr[j] - centroid[j]);
                    double fxe = func(xe);

                    if (fxe < fxr)
                    {
                        simplex[indices[n]] = xe;
                        values[indices[n]] = fxe;
                    }
                    else
                    {
                        simplex[indices[n]] = xr;
                        values[indices[n]] = fxr;
                    }
                }
                else if (fxr < sortedValues[n - 1])
                {
                    simplex[indices[n]] = xr;
                    values[indices[n]] = fxr;
                }
                else
                {
                    if (fxr < sortedValues[n])
                    {
                        double[] xc = new double[n];
                        for (int j = 0; j < n; j++)
                            xc[j] = centroid[j] + beta * (xr[j] - centroid[j]);
                        double fxc = func(xc);

                        if (fxc <= fxr)
                        {
                            simplex[indices[n]] = xc;
                            values[indices[n]] = fxc;
                        }
                        else
                            Shrink(simplex, values, sortedSimplex[0], func, delta, indices);
                    }
                    else
                    {
                        double[] xc = new double[n];
                        for (int j = 0; j < n; j++)
                            xc[j] = centroid[j] + beta * (sortedSimplex[n][j] - centroid[j]);
                        double fxc = func(xc);

                        if (fxc < sortedValues[n])
                        {
                            simplex[indices[n]] = xc;
                            values[indices[n]] = fxc;
                        }
                        else
                            Shrink(simplex, values, sortedSimplex[0], func, delta, indices);
                    }
                }

                double currentBest = values.Min();
                int bestIdx = Array.IndexOf(values, currentBest);
                history.Add(((double[])simplex[bestIdx].Clone(), currentBest));
            }

            double minVal = values.Min();
            int minIdx = Array.IndexOf(values, minVal);

            return new NelderMeadResult
            {
                BestPoint = (double[])simplex[minIdx].Clone(),
                BestValue = minVal,
                History = history
            };
        }

        private static void Shrink(List<double[]> simplex, double[] values,
            double[] bestPoint, Func<double[], double> func, double delta, int[] indices)
        {
            int n = bestPoint.Length;
            for (int i = 0; i < simplex.Count; i++)
            {
                if (i == indices[0]) continue;
                for (int j = 0; j < n; j++)
                    simplex[i][j] = bestPoint[j] + delta * (simplex[i][j] - bestPoint[j]);
                values[i] = func(simplex[i]);
            }
        }
    }
}

