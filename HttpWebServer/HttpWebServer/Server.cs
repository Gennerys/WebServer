using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class Server
	{
		private TcpListener _server;
		private IConsoleLogger _consoleLogger;
		private bool _isRunning;
		public Server(int port,IConsoleLogger consoleLogger)
		{
			_consoleLogger = consoleLogger;
			_server = new TcpListener(IPAddress.Any, port);
			_server.Start();
			_consoleLogger.WriteLogMessage("Server is running...");
			_isRunning = true;
		}

		public void ClientConnection()
		{
			try
			{
				while (_isRunning)
				{
					//using
					TcpClient client = _server.AcceptTcpClient();
					
					_consoleLogger.WriteLogMessage("Client connected...");
					//ClientHandler clientHandler = new ClientHandler();
					//clientHandler.clientSocket = client;
					//Task clientThread = Task.Run(() => { clientHandler.Run(); });
					//clientThread.Wait();
					//client.Close();
					RequestHandler requestHandler = new RequestHandler(new RequestParser(), client);
					requestHandler.HandleRequest();
					
						
				}
			}
			catch (SocketException e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				_server.Stop();
			}
		}
	}
}
