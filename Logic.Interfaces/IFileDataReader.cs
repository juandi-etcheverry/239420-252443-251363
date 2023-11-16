namespace Logic.Interfaces;

public interface IFileDataReader
{
    public string[] GetDirectoryFilePaths(string path);
    public DateTime GetLastModified(string path);
    
}