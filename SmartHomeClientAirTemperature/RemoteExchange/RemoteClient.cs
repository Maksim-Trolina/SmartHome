using SmartHomeClientAirTemperature.Sensor;
using System.Net.Sockets;

namespace SmartHomeClientAirTemperature.RemoteExchange
{
    internal class RemoteClient
    {
        readonly ISensor sensor;
        readonly int delayRequestMilliseconds;
        public RemoteClient(ISensor sensor, int delayRequestOnServerMilliseconds)
        {
            this.sensor = sensor;
            delayRequestMilliseconds = delayRequestOnServerMilliseconds;
        }
        public void Work(string address, int port)
        {
            TcpClient? client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Thread.Sleep(delayRequestMilliseconds);
                    var data = BitConverter.GetBytes(sensor.ReceivingData());
                    stream.Write(data, 0, data.Length);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client?.Close();
            }
        }
    }
}
