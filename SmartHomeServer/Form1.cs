using SmartHomeServer.RemoteExchange;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartHomeServer
{
    public partial class Form1 : Form
    {
        readonly Server server;

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart2.Series[0].XValueType = ChartValueType.DateTime;
            server = new Server(new Chart[] {chart1, chart2}, this);
            Task.Run(() => server.Listen("127.0.0.1", 8888));
        }

        public void UpdateChart(Action action)
        {
            BeginInvoke(action);
        }
    }
}
