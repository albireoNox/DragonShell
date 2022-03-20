using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.io
{
    public static class FileSystem
    {
        public static DirectoryInfo currentDirectory()
        {
            return new DirectoryInfo(Directory.GetCurrentDirectory());
        }

        public static DirectoryInfo getSubdirectory(this DirectoryInfo dir, string subdirName)
        {
            var subdirs = dir.GetDirectories(subdirName);
            if (subdirs.Length == 0)
            {
                throw new IOException($"Could not find directory '{subdirName}' in '{dir.FullName}'");
            }
            else if (subdirs.Length > 1)
            {
                throw new IOException($"Ambiguous name '{subdirName}' in '{dir.FullName}'");
            }

            return subdirs[0];
        }

        public static FileInfo getFile(this DirectoryInfo dir, string fileName)
        {
            var files = dir.GetFiles(fileName);
            if (files.Length == 0)
            {
                throw new IOException($"Could not find file '{fileName}' in '{dir.FullName}'");
            }
            else if (files.Length > 1)
            {
                throw new IOException($"Ambiguous name '{fileName}' in '{dir.FullName}'");
            }

            return files[0];
        }

    }
}
