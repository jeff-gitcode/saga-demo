using Rebus.Activation;
using Rebus.Config;

namespace SagaDemo;

class Program
{
    static async Task Main(string[] args)
    {
        using var activator = new BuiltinHandlerActivator();
        Configure.With(activator)
            .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost", "saga-pattern-demo"))
            .Start();
        //var bus = activator.Bus;
        //activator.Register(() => new OrderSaga(bus));
        // Simulate placing an order
        await activator.Bus.Send(new PlaceOrderCommand { OrderId = "123" });
        Console.ReadLine();
    }
}
