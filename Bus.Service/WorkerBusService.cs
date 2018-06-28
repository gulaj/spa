using System.Text;
using Bus.Service.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Bus.Service
{
	public class WorkerBusService : IWorkerBusService
	{
		private const string QueueName = "spa.worker.details.posted";
		public void PublishMessage(WorkerDetailsPosted workerDetailsPosted)
		{
			using (var conn = new ConnectionFactory
				{
					HostName = "localhost",
					UserName = "guest",
					Password = "guest"
				}
				.CreateConnection())
			{
				using (var channel = conn.CreateModel())
				{

					channel.QueueDeclare(queue: QueueName,
						durable: false,
						exclusive: false,
						autoDelete: false,
						arguments: null);

					var message = JsonConvert.SerializeObject(workerDetailsPosted);
					var body = Encoding.UTF8.GetBytes(message);

					channel.BasicPublish(exchange: "",
						routingKey: QueueName,
						basicProperties: null,
						body: body);
				}
			}
		}

		public WorkerDetailsPosted ConsumeMessage()
		{
			var factory = new ConnectionFactory() {HostName = "localhost"};
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				var message = channel.BasicGet(QueueName, true);
				return message!=null ? JsonConvert.DeserializeObject<WorkerDetailsPosted>(Encoding.UTF8.GetString(message.Body)) : new WorkerDetailsPosted();
			}
		}
	}
}
