using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileVViewer
{
    public static class HelperDir
    {
        static public bool IsFile(string dir) => File.Exists(dir);

        private const string UP_DIR = "[ ... ]";
        static private bool IsRoot(string dir) => Directory.GetCurrentDirectory() == Directory.GetDirectoryRoot(dir);


        static private string GetSmallFileInfo(string fileName)
        {
            StringBuilder fileInfo = new StringBuilder();

            var vs = new FileInfo(fileName);
            fileInfo.AppendLine();
            fileInfo.AppendLine($" Имя файла: {vs.Name}");
            fileInfo.AppendLine();
            fileInfo.AppendLine($" Время создания: {vs.CreationTime}");
            fileInfo.AppendLine();
            fileInfo.AppendLine($" Размер: {vs.Length}");

            return fileInfo.ToString();
        }

        static private string GetSmallDirInfo(string dirName)
        {
            StringBuilder infoText = new StringBuilder();

            DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            infoText.AppendLine();
            infoText.AppendLine($"Название каталога: {dirInfo.Name}");
            infoText.AppendLine();
            infoText.AppendLine($"Полное название каталога: {dirInfo.FullName}");
            infoText.AppendLine();
            infoText.AppendLine($"Время создания каталога: {dirInfo.CreationTime}");
            infoText.AppendLine();
            infoText.AppendLine($"Корневой каталог: {dirInfo.Root}");

            return infoText.ToString();
        }



        static private string SetCurrentDir(string dir)
        {
            if (dir == null)
                dir = Directory.GetCurrentDirectory();

            if (dir == UP_DIR && !IsRoot(dir))
                //dir = Directory.GetParent(dir).FullName;
                dir = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;

            if (IsFile(dir))
                dir = new FileInfo(dir).DirectoryName;
            else
                dir = Path.GetFullPath(dir);

            Directory.SetCurrentDirectory(dir);
            return dir;
        }

        /// <summary>
        /// Заполняет список Директориями и файлами и возвращает его
        /// </summary>
        /// <param name="dir">директория для работы, если null то текущая</param>
        /// <returns></returns>
        static public ObservableCollection<string> GetFileDirList(string dir = null)
        {

            dir = SetCurrentDir(dir);

            var listfile = new ObservableCollection<string>();
            if(!IsRoot(dir))
                listfile.Add(UP_DIR);

            if (Directory.Exists(dir))
            {
                foreach (var textDir in Directory.GetDirectories(dir))
                {
                    listfile.Add(Path.GetFileName(textDir).ToUpper());
                }

                foreach (var textFile in Directory.GetFiles(dir))
                {
                    listfile.Add(Path.GetFileName(textFile));
                }
            }
            return listfile;

        }

        static public string GetFileInfo(string fileName)
        {
            if(IsFile(fileName))
            {
                return GetSmallFileInfo(fileName);
            }
            else 
              return GetSmallDirInfo(fileName);
        }

    }
}
