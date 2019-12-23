using System;
using System.IO;
using System.Text;

namespace HttpWebServer
{
	public class FileHandler : IFileHandler
	{
		IMimeTypeResolver _mimeType;
		public const string PAGES = "pages";
		public const string IMAGES = "images";
		public FileHandler(IMimeTypeResolver mimeType)
		{
			_mimeType = mimeType;
		}

		public BinaryReader ReadFile(string filePath)
		{

			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

			var reader = new BinaryReader(fileStream, new ASCIIEncoding());

			return reader;

		}

		public string GetFilePath(string fileName)
		{
			var baseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (GetFileMimeType(fileName) == "text/html")
			{
				baseDirectory = $"{baseDirectory}{fileName}";
			}
			else if (GetFileMimeType(fileName) == "image/jpeg")
			{
				baseDirectory = $"{baseDirectory}\\{IMAGES}\\{fileName}";
			}
			else
			{
				throw new NotImplementedException(); // Add here status msg or something
			}

			return baseDirectory;
		}
		public bool CheckIfExists(string filePath)
		{
			if (File.Exists(filePath))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public string GetFileMimeType(string fileName)
		{
			return _mimeType.GetMIMEType(fileName);
		}


	}
}
