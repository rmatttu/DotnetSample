using System.IO;

namespace LARGEWords.DataStore
{
    class FilePathAnalyzer
    {
        readonly char driveLetter;
        readonly string[] folders;
        readonly string fileName;
        readonly string extension;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortcutFileFullPath">ドライブ文字～拡張子まで</param>
        public FilePathAnalyzer(string fileFullPath)
        {
            folders = GetParentFolders(fileFullPath);
            fileName = GetFileName(fileFullPath);
            extension = GetExtension(fileFullPath);
            driveLetter = GetDriveLetter(fileFullPath);
        }

        private string[] GetParentFolders(string fileFullPath)
        {
            char[] separator = { '\\', '/' };
            string[] splited = fileFullPath.Split(separator);
            string[] folders = new string[splited.Length - 2];
            for (int i = 0; i < splited.Length - 2; i++)
            {
                folders[i] = splited[i + 1];
            }
            return folders;
        }

        private string GetFileName(string fileFullPath)
        {
            char[] separator = { '\\', '/' };
            string[] splited = fileFullPath.Split(separator);
            return splited[splited.Length - 1];
        }

        private char GetDriveLetter(string fileFullPath)
        {
            char[] separator = { ':' };
            string[] splited = fileFullPath.Split(separator);
            return splited[0][0];
        }

        private string GetExtension(string fileFullPath)
        {
            char[] separator = { '.' };
            string[] splited = fileFullPath.Split(separator);
            return splited[1].ToLower();
        }

        public char DriveLetter
        {
            get { return driveLetter; }
        }

        public string[] Folders
        {
            get { return folders; }
        }

        public string Extension
        {
            get { return extension; }
        }

        /// <summary>
        /// 自身より上部の階層にいくつフォルダーがあるか。ドライブ文字と自分自身を数えない。 "C:\a\b\c\test.txt" は3を返す
        /// </summary>
        public int FolderDepth
        {
            get { return folders.Length; }
        }

        public string ParentFolderName
        {
            get { return folders[folders.Length - 1]; }
        }

        public string ParentFolderFullPath
        {
            get
            {
                string fullPath = driveLetter + ":\\";
                for (int i = 0; i < folders.Length - 1; i++)
                {
                    fullPath += folders[i] + "\\";
                }
                fullPath += folders[folders.Length - 1];

                return fullPath;
            }
        }

        /// <summary>
        /// FileNameを返す
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        public string FileFullPath
        {
            get
            {
                string filePath = driveLetter + @":\";
                foreach (var item in folders)
                {
                    filePath += item + "\\";
                }
                filePath += fileName;
                return filePath;
            }
        }

        public string FileNameWithoutExtension
        {
            get { return Path.GetFileNameWithoutExtension(fileName); }
        }

        public string FullPathWithoutExtension
        {
            get { return ParentFolderFullPath + "\\" + FileNameWithoutExtension; }
        }
    }
}
