using Logic.Interfaces;

namespace Logic;

public class FileDataReader : IFileDataReader
{
    public string[] GetDirectoryFilePaths(string path)
    {
        return Directory.GetFiles(path);
    }

    public DateTime GetLastModified(string path)
    {
        return Directory.GetLastWriteTime(path);
    }
}