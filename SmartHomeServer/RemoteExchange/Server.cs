using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartHomeServer.RemoteExchange
{
    internal class Server
    {
        TcpListener listener;

        readonly Chart[] charts;
        readonly Form1 form;
        int chartIndex = 0;
        public Server(Chart[] charts, Form1 form)
        {
            this.charts = charts;
            this.form = form;
        }
        public void Listen(string address, int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(address), port);
                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client, charts[chartIndex], form);
                    chartIndex++;
                    Task.Run(() => clientObject.Process());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                listener?.Stop();
            }
        }
    }
}
