using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
	public class ClientHandler : IClientHandler
	{
		public TcpClient clientSocket;
		public void Run()
		{
			byte[] data = new byte[clientSocket.ReceiveBufferSize];
			string dataFromClient = null;
			byte[] dataToSend = null;
			string serverResponse = null;
			while (true)
			{
				try
				{
					NetworkStream network = clientSocket.GetStream();
					network.Read(data, 0, clientSocket.ReceiveBufferSize);
					dataFromClient = Encoding.ASCII.GetString(data);
					Console.WriteLine(dataFromClient);
					serverResponse = "Hi there";
					dataToSend = Encoding.ASCII.GetBytes(serverResponse);
					network.Flush();
					Console.WriteLine(serverResponse);
				}
				catch (Exception ex)
				{

					Console.WriteLine(" >> " + ex.ToString());
				}
				
			}
		}
	}
}
