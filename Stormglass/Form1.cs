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

namespace Stormglass
{
    public partial class Form1 : Form
    {
        stormglassData sgData;
        public Form1()
        {
            InitializeComponent();
            sgData = new stormglassData();
            sgData.Request();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var objChart = chart.ChartAreas[0];

            // TimeDate
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            objChart.AxisX.Minimum = sgData.response.meta.start.ToOADate();
            objChart.AxisX.Maximum = sgData.response.meta.end.ToOADate();

            // Tide
            objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisY.Minimum = -3;
            objChart.AxisY.Maximum = 3;

            //Clear
            chart.Series.Clear();

            Series newSeries = new Series("Tide");
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 3;
            newSeries.Color = Color.Pink;
            newSeries.XValueType = ChartValueType.DateTime;
            chart.Series.Add(newSeries);

            for (int i = 0; i < sgData.response.data.Count; i++)
            {
                chart.Series[0].Points.AddXY(sgData.response.data[i].time, sgData.response.data[i].height);
                Refresh();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
