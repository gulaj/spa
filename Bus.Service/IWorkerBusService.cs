using Bus.Service.Models;

namespace Bus.Service
{
	public interface IWorkerBusService
	{
		WorkerDetailsPosted ConsumeMessage();
		void PublishMessage(WorkerDetailsPosted workerDetailsPosted);
	}
}