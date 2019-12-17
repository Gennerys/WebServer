using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Server server = new Server(1234);
			server.ClientConnection();
			Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
			MimeTypeResolver mime = new MimeTypeResolver();
			Console.WriteLine(mime.GetMIMEType("index.html"));
			PathParser pathParser = new PathParser(new MimeTypeResolver());
			Console.WriteLine(pathParser.GetFilePath("index.html"));
			Console.WriteLine((int)StatusCode.OK);
		}
	}
}
