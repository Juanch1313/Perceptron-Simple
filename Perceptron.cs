using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PerceptronSimple
{
    public partial class Perceptron : Form
    {
        private List<double> valorX = new List<double>();
        private List<double> valorY = new List<double>();

        private double w0 = 1;
        private double w1 = 0;
        private double w2 = 0;
        private double b = 0;

        public Perceptron()
        {
            InitializeComponent();

            //Maximos, minimos y linea cruzada
            chart2.ChartAreas[0].AxisX.Minimum = -10;
            chart2.ChartAreas[0].AxisY.Minimum = -10;
            chart2.ChartAreas[0].AxisX.Maximum = 10;
            chart2.ChartAreas[0].AxisY.Maximum = 10;
            chart2.ChartAreas[0].AxisX.Crossing = 0;
            chart2.ChartAreas[0].AxisY.Crossing = 0;

            //Configuraciones extras de diseño
            chart2.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Triangle;
            chart2.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Triangle;
            chart2.ChartAreas[0].AxisX.LineWidth= 2;
            chart2.ChartAreas[0].AxisY.LineWidth= 2;
            chart2.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 14;
            chart2.ChartAreas[0].AxisY.LabelAutoFitMinFontSize = 14;

            //Colores
            chart2.Series[0].Color = Color.Green;
            chart2.Series[1].Color = Color.Red;
            chart2.Series[2].Color = Color.Black;
            chart2.Series[3].Color = Color.Blue;
        }

        private void chart2_MouseClick(object sender, MouseEventArgs e)
        {
            valorX.Add(chart2.ChartAreas[0].AxisX.PixelPositionToValue(e.X));
            valorY.Add(chart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Y));

            chart2.Series[3].Points.AddXY(valorX.Last(), valorY.Last());
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(tbxPeso1.Text) || String.IsNullOrEmpty(tbxPeso2.Text) || String.IsNullOrEmpty(tbxBias.Text))
            {
                MessageBox.Show("Llene el formulario correctamente");
                return;
            }
            if (chart2.Series[0].Points.Count < 5 &&
                chart2.Series[1].Points.Count < 5 && 
                chart2.Series[3].Points.Count < 5)
            {
                MessageBox.Show("Ingrese almenos 5 puntos en el plano");
                return;
            }

            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart2.Series[3].Points.Clear();

            w1 = double.Parse(tbxPeso1.Text);
            w2 = double.Parse(tbxPeso2.Text);
            b = double.Parse(tbxBias.Text);

            LimpiarTextos();


            for (int i = 0; i < valorX.Count; i++)
            {
                if (FuncionDeActivacion(valorX[i], valorY[i]))
                {
                    chart2.Series[0].Points.AddXY(valorX[i], valorY[i]);
                }
                else
                {
                    chart2.Series[1].Points.AddXY(valorX[i], valorY[i]);
                }
            }

            var y1 = (-b - w1 * (valorX.Min()) / w2);
            var y2 = (-b - w1 * (valorX.Max()) / w2);

            chart2.Series[2].Points.Clear();
            chart2.Series[2].Points.AddXY(valorX.Min(), y1);
            chart2.Series[2].Points.AddXY(valorX.Max(), y2);
        }


        #region Validaciones

        private bool FuncionDeActivacion(double x, double y)
        {
            return (w0 * b) + (x * w1) + (y * w2) >= 0;
        }

        private void LimpiarTextos()
        {
            tbxPeso1.Clear();
            tbxPeso2.Clear();
            tbxBias.Clear();
        }

        #endregion
    }
}
