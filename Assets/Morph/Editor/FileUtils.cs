using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Morph.Editor
{
    public static class FileUtils
    {
        /// <summary>
        /// Test if a folder exists, and create it if not
        /// </summary>
        /// <param name="folderPath">Path of the folder</param>
        /// <returns>Info of the folder, null if no folder is created or found</returns>
        public static DirectoryInfo CreateFolderIfNeeded(string folderPath)
        {
            bool hasDirectory = Directory.Exists(folderPath);
            if (!hasDirectory)
            {
                Debug.LogWarningFormat("Morph couldn't find directory {0}. Creating one.", folderPath);
                DirectoryInfo info = Directory.CreateDirectory(folderPath);
                if (!info.Exists)
                {
                    Debug.LogErrorFormat("Morph failed to create directory at path {0}", folderPath);
                    return null;
                }

                return info;
            }

            return new DirectoryInfo(folderPath);
        }
    }
}
