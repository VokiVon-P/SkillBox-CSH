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

        private const string UP_DIR = "[ .. ]";

        static private bool IsRoot(string dir) => Directory.GetCurrentDirectory() == Directory.GetDirectoryRoot(dir);
        static public bool IsFile(string dir) => File.Exists(dir);

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
            return "OK";
        }

    }
}
