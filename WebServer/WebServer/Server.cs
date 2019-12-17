using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer
{
	public class Server
	{
		private TcpListener _server;
		private bool _isRunning;
		public Server(int port)
		{
			_server = new TcpListener(IPAddress.Any, port);
			_server.Start();

			_isRunning = true;
		}

		public void ClientConnection()
		{
			while (_isRunning)
			{
				//using
				TcpClient client = _server.AcceptTcpClient();
				ClientHandler clientHandler = new ClientHandler();
				//Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClients));
				//clientThread.Start(client);
				clientHandler.clientSocket = client;
				//Task clientThread = Task.Run(() => { clientHandler.Run(); });
				//clientThread.Wait();
				//client.Close();
				RequestParser request = new RequestParser(client);
				request.Parse();
				
			}
		}

	}
}
