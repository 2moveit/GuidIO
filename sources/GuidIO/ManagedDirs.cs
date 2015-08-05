using System;
using System.IO;
using System.Linq;

namespace GuidIO
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
        private readonly string guidFormat;

        /// <param name="size">Characters used for a directory which determines the size of each directory level. Default = 2</param>
        /// <param name="depth">Directory tree depth. Default = 2</param>
        /// <param name="ignoreChar">Characters to ignore for directories.</param>
        /// <param name="guidFormat">A single format specifier that indicates how to format the value of this Guid. The format parameter can be "N", "D", "B", "P", or "X". If format is null or an empty string (""), "D" is used. https://msdn.microsoft.com/en-us/library/97af8hh4%28v=vs.110%29.aspx</param>
        public ManagedDirs(int size = 2, int depth = 2, char[] ignoreChar = null, string guidFormat = "D")
        {
            this.size = size;
            this.depth = depth;
            this.guidFormat = guidFormat;
            this.ignoreChar = ignoreChar ?? new char[0];
        }

        /// <summary>
        /// Max numbers of directory on each level.
        /// </summary>
        /// <param name="charQuantity">Character quantity of file to determine max directory number for.</param>
        /// <returns>Number of max directories on one level.</returns>
        public int MaxDirsPerLevel(int charQuantity = 16)
        {
            return (int) Math.Pow(charQuantity, size);
        }

        /// <summary>
        /// Man number of directories.
        /// </summary>
        /// <returns>Max number of directories.</returns>
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
            return GetDirPath(guid.ToString(guidFormat).ToUpper(), rootDir);
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
        /// <param name="guid">GUID to derive the directory structure and create the file for.</param>
        /// <param name="rootDir">>Root directory for the managed file structure.</param>
        /// <param name="extension">Extesion part of the file.</param>
        /// <returns></returns>
        public FileStream Create(Guid guid, string rootDir = ".", string extension = "")
        {
            string fileName = guid.ToString(guidFormat).ToUpper();
            if (!string.IsNullOrEmpty(extension))
                fileName = string.Format("{0}.{1}", fileName, extension);
            return Create(fileName, rootDir);
        }

        /// <summary>
        ///  Determines the directory under the specified root directory, creates a new file with the specified string in the file,
        ///  and closes the file. If the target already exists, it is overritten.
        /// </summary>
        /// <param name="fileName">File name to derive the directory structure for.</param>
        /// <param name="content">The string to write to the file.</param>
        /// <param name="rootDir">Root directory for the managed file structure.</param>
        public void WriteAllText(string fileName, string content, string rootDir = ".")
        {
            string path = Path.Combine(rootDir, GetDirPath(fileName));
            EnsureDirExists(path);
            File.WriteAllText(Path.Combine(path, fileName), content);
        }

        /// <summary>
        ///  Determines the directory under the specified root directory, creates a new file with the specified string in the file,
        ///  and closes the file. If the target already exists, it is overritten.
        /// </summary>
        /// <param name="guid">GUID to derive the directory structure and create the file for.</param>
        /// <param name="content">The string to write to the file.</param>
        /// <param name="rootDir">Root directory for the managed file structure.</param>
        /// <param name="extension">Extesion part of the file.</param>
        public void WriteAllText(Guid guid, string content, string rootDir = ".", string extension = "")
        {
            string fileName = guid.ToString(guidFormat).ToUpper();
            if (!string.IsNullOrEmpty(extension))
                fileName = string.Format("{0}.{1}", fileName, extension);
            WriteAllText(fileName, content, rootDir);
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