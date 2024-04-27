using Rebus.Sagas;

namespace SagaDemo
{
    public class OrderSagaData : ISagaData
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public Guid Id { get; set; }
        public int Revision { get; set; }
    }
}