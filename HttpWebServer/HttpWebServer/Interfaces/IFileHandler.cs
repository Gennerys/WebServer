using System.IO;

namespace HttpWebServer
{
	public interface IFileHandler
	{
		bool CheckIfExists(string filePath);
		string GetFileMimeType(string fileName);
		string GetFilePath(string fileName);
		BinaryReader ReadFile(string filePath);
	}
}