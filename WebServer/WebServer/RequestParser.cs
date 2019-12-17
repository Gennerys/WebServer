using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
	public class RequestParser
	{
		private TcpClient _client;
		byte [] _buffer;
		public RequestParser(TcpClient client)
		{
			_client = client;
		}

		public void Parse()
		{
			_buffer = new byte[_client.ReceiveBufferSize];
			string message = "";
			NetworkStream network = _client.GetStream();
			network.Read(_buffer, 0, _buffer.Length);
			message = Encoding.ASCII.GetString(_buffer);
			RequestDTO requestDTO = new RequestDTO();
			string[] parts = message.Split('\n');
			requestDTO.Method = parts[0].Substring(0,3);
			requestDTO.Version = parts[0].Substring(parts[0].IndexOf("HTTP"), parts[0].Length - parts[0].IndexOf("HTTP"));
			requestDTO.Host = parts[1].Substring(6, parts[1].Length - 6);
			//requestDTO.UserAgent = parts[2].Substring(parts[2].IndexOf("User-Agent"), parts[2].Length - parts[2].IndexOf("User-Agent"));
			
			Console.WriteLine(requestDTO.Method + requestDTO.Version);
			Console.WriteLine(requestDTO.Host);
			//Console.WriteLine(requestDTO.UserAgent);
			Console.WriteLine(message);
			network.Flush();

		}
	}
}
