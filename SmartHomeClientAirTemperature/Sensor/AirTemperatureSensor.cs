
namespace SmartHomeClientAirTemperature.Sensor
{
    internal class AirTemperatureSensor : ISensor
    {
        readonly Random dataGenerator;
        int? lastData;
        public AirTemperatureSensor()
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
            lastData = dataGenerator.Next(45, 48);
            return lastData.Value;
        }
    }
}
