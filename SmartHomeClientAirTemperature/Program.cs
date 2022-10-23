using SmartHomeClientAirTemperature.RemoteExchange;
using SmartHomeClientAirTemperature.Sensor;

class Program
{
    static async Task Main(string[] args)
    {
        var airTemperatureSensor = new AirTemperatureSensor();
        var airHumiditySensor = new AirHumiditySensor();

        var airTemperatureSensorClient = new RemoteClient(airTemperatureSensor, 1000);
        var airHumiditySensorClient = new RemoteClient(airHumiditySensor, 1000);

        var temperatureSensorClientTask =  Task.Run(() => airTemperatureSensorClient.Work("127.0.0.1", 8888));
        await Task.Delay(3000);
        var humiditySensorClientTask = Task.Run(() => airHumiditySensorClient.Work("127.0.0.1", 8888));

        await Task.WhenAll(temperatureSensorClientTask, humiditySensorClientTask);
    }
}
