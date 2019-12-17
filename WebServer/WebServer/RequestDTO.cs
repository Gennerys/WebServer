using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
	public class RequestDTO
	{
		public string Method { get; set; }
		public string Version { get; set; }
		public string Host { get; set; }
		public string UserAgent { get; set; }
		public byte[] BodyData { get; set; }
	}
}
