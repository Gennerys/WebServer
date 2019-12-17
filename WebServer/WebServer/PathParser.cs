using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
	public class PathParser : IPathParser
	{
		IMimeType MimeType;
		public const string PAGES = "/pages/";
		public const string IMAGES = "/images/";
		public PathParser(IMimeType _mimeType)
		{
			MimeType = _mimeType;
		}
		public string GetFilePath(string filename)
		{
			string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			if (MimeType.GetMIMEType(filename) == "text/html")
			{
				filePath = filePath + PAGES + filename;
			}
			else if (MimeType.GetMIMEType(filename) == "image/jpeg")
			{
				filePath = filePath + IMAGES + filename;
			}
			else
			{
				throw new NotImplementedException(); // Add here status msg or something
			}
			if (File.Exists(filePath))
			{
				return filePath;
			}
			else
			{
				throw new NotImplementedException(); // Add here status msg or something
			}

		}
	}
}
