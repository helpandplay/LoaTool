using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LoaTool.Define.Interfaces.Enums;

namespace LoaTool.Define.Common
{
    public interface IResource
    {
        public string FilePath { get; set; }
        public Enum FileType { get; set; }
        public FilePathType FilePathType { get; set; }

        protected static FilePathType GetFilePath(string filePath)
        {
            bool isRelativePath = filePath.StartsWith('.');
            if(isRelativePath)
            {
                return FilePathType.Releative;
            }

            bool isPack = filePath.StartsWith("pack");
            if(isPack)
            {
                return FilePathType.Pack;
            }

            bool isAbsolute = Regex.IsMatch(filePath, @"[A-Za-z]{1}:\\/g");
            if(isAbsolute)
            {
                return FilePathType.Absolute;
            }

            throw new NotImplementedException(filePath + " is not matched.");
        }
    }
}
