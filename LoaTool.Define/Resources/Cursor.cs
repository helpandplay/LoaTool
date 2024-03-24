using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoaTool.Define.Common;
using LoaTool.Define.Enums;
using LoaTool.Define.Interfaces.Enums;

namespace LoaTool.Define.Resources;
public class Cursor : IResource
{
    public string FilePath { get; set; }
    public Enum FileType { get; set; }
    public FilePathType FilePathType { get; set; }

    public Cursor(
        string filePath, CustomCursors fileType)
    {
        FilePath = filePath;
        FileType = fileType;
        FilePathType = IResource.GetFilePath(filePath);
    }
}
