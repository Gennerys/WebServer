using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class RequestHandler
	{
		private TcpClient _client;
		private IParser _requestParser;
		public RequestHandler(IParser requestParser, TcpClient client)
		{
			_requestParser = requestParser;
			_client = client;
		}
		public void HandleRequest()
		{
			NetworkStream requestMessage = GetRequest();
			RequestDTO requestDTO = _requestParser.Parse(requestMessage);

			if (requestDTO.Method.Equals("GET"))
			{
				var createResponse = new Response(_client,new FileHandler(new MimeTypeResolver()),new StatusCodeResponse());
				createResponse.TransferToClient(requestDTO.Url);
			}
			else if (requestDTO.Method.Equals("POST"))
			{
				Console.WriteLine("TODO");
			}
			else
			{
				Console.WriteLine("TODO");
			}

		}
		public NetworkStream GetRequest()
		{
			NetworkStream network = _client.GetStream();
			return network;
		}
	}
}
