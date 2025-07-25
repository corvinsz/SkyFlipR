using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Services;

public interface IFileHandler
{
    string BaseFolder { get; }

    bool Exists(string? path);
    string ReadFile(string path);
    void WriteFile(string path, string content);
    void Move(string sourceFileName, string destFileName, bool overwrite = false);
    void Copy(string sourceFileName, string destFileName, bool overwrite = false);
}

public class FileHandler : IFileHandler
{
    public string BaseFolder { get; } = Path.Combine(Path.GetTempPath(), nameof(SkyFlipR));

    public FileHandler()
    {
        EnsureBaseFolderCreated();
    }

    private void EnsureBaseFolderCreated()
        => Directory.CreateDirectory(BaseFolder);

    public void WriteFile(string path, string content)
        => File.WriteAllText(path, content);

    public string ReadFile(string path) 
        => File.ReadAllText(path);

    public bool Exists(string? path)
        => File.Exists(path);
    public void Move(string sourceFileName, string destFileName, bool overwrite = false)
        => File.Move(sourceFileName, destFileName, overwrite);

    public void Copy(string sourceFileName, string destFileName, bool overwrite = false)
        => File.Copy(sourceFileName, destFileName, overwrite);
}
