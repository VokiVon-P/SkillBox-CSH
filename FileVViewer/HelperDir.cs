using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileVViewer
{
    /// <summary>
    /// Вспомогательный статический класс скрывающий логику работы с файлами и файловой системой
    /// </summary>
    public static class HelperDir
    {
        static public bool IsFile(string dir) => File.Exists(dir);

        private const string UP_DIR = "[ ... ]";

        private static readonly string[] extTxt = new string[] { ".txt", ".ini", ".cmd", ".cfg", ".config", ".cs", ".sql" };
        
        static private bool IsRoot(string dir) => Directory.GetCurrentDirectory() == Directory.GetDirectoryRoot(dir);

        /// <summary>
        /// Проверяет существует ли файл и принадлежит ли множеству обрабатываемых текстовых файлов
        /// </summary>
        /// <param name="pathFile"> файл для проверки </param>
        /// <returns> true - если можем обработать как текстовый </returns>
        static public bool IsTxtFile(string pathFile)
        {
            if (!IsFile(pathFile))
                return false;

            string extFile = new FileInfo(pathFile).Extension;

            return Array.Exists(extTxt, element => element == extFile);
        }
        
        /// <summary>
        /// Получение небольшой системной информации о файле
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> Форматированная строка с информацией </returns>
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
            fileInfo.AppendLine();

            return fileInfo.ToString();
        }

        /// <summary>
        /// Получение небольшой системной информации о директории
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns> Форматированная строка с информацией </returns>
        static private string GetSmallDirInfo(string dirName)
        {
            if (dirName == UP_DIR)
                return "Родительская директория";

            if (string.IsNullOrEmpty(dirName))
                return "";

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
        
        /// <summary>
        /// Служебная функция для установки текущей директории для приложения
        /// </summary>
        /// <param name="dir"> путь который нужно сделать текущей директорией </param>
        /// <returns> путь новой текущей директории </returns>
        static private string SetCurrentDir(string dir)
        {
            if (dir == null)
                dir = Directory.GetCurrentDirectory();

            if (dir == UP_DIR && !IsRoot(dir))
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
        /// <returns> заполненный список </returns>
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

        /// <summary>
        /// Формирование системной информации о файле/директории
        /// </summary>
        /// <param name="pathName"> путь для обработки </param>
        /// <returns> Форматированную строку с информацией </returns>
        static public string GetFileInfo(string pathName)
        {
            if(IsFile(pathName))
            {
                string text = GetSmallFileInfo(pathName);
                if (IsTxtFile(pathName))
                    text += $"Содержимое файла:\n\n{ReadTxtFile(pathName)}";

                return text;
            }
            else 
              return GetSmallDirInfo(pathName);
        }

        /// <summary>
        /// Получение текстового содержимого файла
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> строка с необработанных содержимым файла </returns>
        static public string ReadTxtFile(string fileName)
        {
            string fullPathFile = new FileInfo(fileName).FullName;
            return File.ReadAllText(fullPathFile);
        }

    }
}
