using System;
using System.IO;
using System.Linq;

namespace KCT.GuidIO
{
    /// <summary>
    ///     GuidIO manages the directory structure for files.
    /// </summary>
    /// <remarks>Pronounced Gidio like Guido</remarks>
    public class ManagedDirs
    {
        private readonly int depth = 2;
        private readonly char[] ignoreChar;
        private readonly int size = 2;

        /// <param name="size">Characters used for a directory which determines the size of each directory level. Default = 2</param>
        /// <param name="depth">Directory tree depth. Default = 2</param>
        /// <param name="ignoreChar">Characters to ignore for directories.</param>
        public ManagedDirs(int size = 2, int depth = 2, char[] ignoreChar = null)
        {
            this.size = size;
            this.depth = depth;
            this.ignoreChar = ignoreChar ?? new char[0];
        }

        public int MaxDirsPerLevel(int charQuantity = 16)
        {
            return (int) Math.Pow(charQuantity, size);
        }

        public int MaxDirs()
        {
            return (int) Math.Pow(MaxDirsPerLevel(), depth);
        }

        /// <summary>
        ///     Gets the directory path for a GUID.
        /// </summary>
        /// <param name="guid">GUID  to derive the directory structure for.</param>
        /// <param name="rootDir">Root directory for the managed file structure.</param>
        /// <returns>Path of the directory</returns>
        public string GetDirPath(Guid guid, string rootDir = ".")
        {
            return GetDirPath(guid.ToString("N"), rootDir);
        }

        /// <summary>
        ///     Gets the directory path for a file.
        /// </summary>
        /// <param name="fileName">File name to derive the directory structure for.</param>
        /// <param name="rootDir">Root directory for the managed file structure.</param>
        /// <returns>Path of the directory</returns>
        public string GetDirPath(string fileName, string rootDir = ".")
        {
            var name = ignoreChar.Aggregate(fileName, (current, t) => current.Replace(t.ToString(), ""));
            string path = rootDir;
            for (int index = 0; index < depth; index++)
            {
                if (index == 0)
                    path = Path.Combine(path, name.Substring(0, size));
                else
                {
                    int startIndex = index * size;
                    if (startIndex >= name.Length) break;
                    int nextSize = size;
                    if (startIndex * size > name.Length)
                        nextSize = name.Length - startIndex;
                    path = Path.Combine(path, name.Substring(startIndex, nextSize));
                }
            }
            return path;
        }

        /// <summary>
        ///     Determines the directory under the specified root directory and creates or overwrites the file.
        /// </summary>
        /// <param name="fileName">File name to derive the directory structure for.</param>
        /// <param name="rootDir">Root directory for the managed file structure.</param>
        /// <returns></returns>
        public FileStream Create(string fileName, string rootDir = ".")
        {
            string path = Path.Combine(rootDir, GetDirPath(fileName));
            EnsureDirExists(path);
            return File.Create(Path.Combine(path, fileName));
        }

        /// <summary>
        ///     Determines the directory under the specified root directory and creates or overwrites the file.
        /// </summary>
        /// <param name="guid">GUID  to derive the directory structure and create the file for .</param>
        /// <param name="rootDir">>Root directory for the managed file structure.</param>
        /// <param name="extension">Extesion part of the file</param>
        /// <returns></returns>
        public FileStream Create(Guid guid, string rootDir = ".", string extension = "")
        {
            string fileName = guid.ToString("N");
            if (!string.IsNullOrEmpty(extension))
                fileName = string.Format("{0}.{1}", fileName, extension);
            return Create(fileName, rootDir);
        }

        private static void EnsureDirExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}