using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Services;

public interface IFileHandler
{
    string ReadFile(string path);
    void WriteFile(string path, string content);
}

public class FileHandler : IFileHandler
{
    public void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }
}
