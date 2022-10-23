
namespace SmartHomeClientAirTemperature.Sensor
{
    internal class AirHumiditySensor : ISensor
    {
        int? lastData = null;
        readonly Random dataGenerator;

        public AirHumiditySensor()
        {
            dataGenerator = new Random();
        }

        public int ReceivingData()
        {
            if (lastData.HasValue)
            {
                var delta = dataGenerator.Next(-1, 2);
                lastData += delta;
                return lastData.Value;
            }
            lastData = dataGenerator.Next(15, 18);
            return lastData.Value;
        }
    }
}
