using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class Response
	{
		private TcpClient _client;
		private IFileHandler _fileHandler;
		private IStatusCodeResponse _statusCodeResponse;
		public Response(TcpClient client, IFileHandler fileHandler, IStatusCodeResponse statusCodeResponse)
		{
			_client = client;
			_fileHandler = fileHandler;
			_statusCodeResponse = statusCodeResponse;
		}
		public void TransferToClient(string fileName)
		{
			string physicalPath = _fileHandler.GetFilePath(fileName);
			string contentType = _fileHandler.GetFileMimeType(fileName);
			if (_fileHandler.CheckIfExists(physicalPath))
			{
				
				BinaryReader reader = _fileHandler.ReadFile(physicalPath);
				SendResponse(reader, _statusCodeResponse.GetResponse(StatusCode.OK), contentType);
			}
			else
			{
				SendErrorResponse(_statusCodeResponse.GetResponse(StatusCode.NOT_FOUND), contentType);
			}
		}

		private void SendResponse(BinaryReader reader, string responseCode, string contentType)
		{
			//reader = new BinaryReader(_client.GetStream());
			int bufferChunk = 1024;
			byte[] buffer = CreateHeader(responseCode,bufferChunk,contentType);
			string test = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
			Console.WriteLine(test);
			NetworkStream network = _client.GetStream();
			//using (NetworkStream network = _client.GetStream())
			{
				
				//using (reader)
				{
					int byteCount;
					while ((byteCount = reader.Read(buffer, 0, buffer.Length)) != 0)
					{
						network.Write(buffer, 0, byteCount);
					}
				}

			}
		}
		private void SendErrorResponse(string responseCode, string contentType)
		{
			SendResponse(null, responseCode, contentType);
		}

		private byte[] CreateHeader(string responseCode, int contentLength, string contentType)
		{
			return Encoding.ASCII.GetBytes("HTTP/1.1 " + responseCode + "\r\n"
								  + "Server: Simple Web Server\r\n"
								  + "Content-Length: " + contentLength + "\r\n"
								  + "Connection: close\r\n"
								  + "Content-Type: " + contentType + "\r\n\r\n");
		}

	}
}
