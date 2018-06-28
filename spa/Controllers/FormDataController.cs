using AutoMapper;
using Bus.Service;
using Bus.Service.Models;
using Microsoft.AspNetCore.Mvc;
using spa.Models;

namespace spa.Controllers
{
	[Route("api")]
    public class FormDataController
    {
	    private readonly IMapper _mapper;
	    private readonly IWorkerBusService _workerBusService;
		public FormDataController(IMapper mapper, IWorkerBusService workerBusService)
		{
			_mapper = mapper;
			_workerBusService = workerBusService;
		}

	    [HttpPost("postFormData")]
		public void SaveWorkerDetails([FromBody] FormDataWorkerDetails dataWorkerDetails)
		{
			_workerBusService.PublishMessage(_mapper.Map<WorkerDetailsPosted>(dataWorkerDetails));
		}

		[HttpGet("workerDetails")]
		public DataWorkerDetails GetWorkerDetails()
		{
			var workerDetails = _workerBusService.ConsumeMessage();
			return _mapper.Map<DataWorkerDetails>(workerDetails);
		}

	}
}
