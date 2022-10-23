using System;
using System.Net.Sockets;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartHomeServer.RemoteExchange
{
    public class ClientObject
    {
        readonly TcpClient client;
        readonly Chart chart;
        readonly Form1 form;
        public ClientObject(TcpClient tcpClient, Chart chart, Form1 form)
        {
            client = tcpClient;
            this.chart = chart;
            this.form = form;
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[sizeof(int)];
                int result;
                while (true)
                {
                    do
                    {
                        stream.Read(data, 0, data.Length);
                        result = BitConverter.ToInt32(data, 0);
                    }
                    while (stream.DataAvailable);
                    Action updateAction = () =>
                    {
                        chart.Series[0].Points.AddXY(DateTime.Now, result);
                    };
                    form.UpdateChart(updateAction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                stream?.Close();
                client?.Close();
            }
        }
    }
}
