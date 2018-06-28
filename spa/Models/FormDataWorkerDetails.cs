using System;
using Newtonsoft.Json;

namespace spa.Models
{
	public class FormDataWorkerDetails
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("lastName")]
		public string LastName { get; set; }
		[JsonProperty("birthDate")]
		public DateTime BirthDate { get; set; }
	}
}