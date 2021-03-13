using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitGate.Application.ViewModels
{
    public class Response
    {
		public string statuscode { get; set; }
		public string message { get; set; }
		public object payload { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}


   
}
