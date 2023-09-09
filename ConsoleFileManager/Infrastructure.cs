using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using ConsoleFileManager.HelperClasses;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Runtime.InteropServices;

namespace ConsoleFileManager.Infrastructure
{
    internal class Infrastructure
    {

        internal static string[][] ManagerPath; // Массив для хранения текущего расположения пользователя . Где первфй индекс [1][0] или [2][0] - наименование диска - все остальные пути к файлам и папкам 

        internal static string R = String.Empty; // переменная для хранения актуального имени диска 



        /// <summary>
        /// в конструкторе инициализируем массив
        /// </summary>

         internal Infrastructure()
        
        {

            ManagerPath = new string[100][];

            for (int i = 0; i < ManagerPath.Length; i++) { ManagerPath[i] = new string[100]; }


        
         }
        /// <summary>
        /// читаем и записываем в масссив имена дисков
        /// </summary>
        /// <returns></returns>

        internal void DisplayCatalog()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            int i=0;
            foreach (DriveInfo drive in drives) { ManagerPath[i][0] = drive.Name; i++; }

            
        }

        /// <summary>
        /// читаме директории из интересующего диска
        /// </summary>
        /// <param name="catalog">наименование диска</param>
        /// <returns></returns>

        internal IEnumerable<DirectoryInfo> DisplayDirectories(string catalog)
        {

            string _catalog = ReadCatalogPath(catalog);
            DirectoryInfo directory = new DirectoryInfo(_catalog);
            return directory.EnumerateDirectories();

        }
        /// <summary>
        /// читаем директории пот директории 
        /// </summary>
        /// <param name="directories">директория</param>
        /// <returns></returns>

        internal IEnumerable<DirectoryInfo> DisplaySubDirectories()
        {
            string directories = ConductorPath();

            ;

            DirectoryInfo directory = new DirectoryInfo(directories);

            return directory.EnumerateDirectories();
        }


        /// <summary>
        /// чтение списка файлов в директории 
        /// </summary>
        /// <returns></returns>
        internal FileInfo[] DisplayFile()
        {
            string file = ConductorPath();


            DirectoryInfo directory = new DirectoryInfo(file);


           return directory.GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly);

          // String[] s= new string[Directory.GetFiles(file, "*.*").Length];
          //
          //
          //
          // if (Directory.GetFiles(file, "*.*").Length > 0)
          // {
          //     for (int i = 0; i < s.Length; i++) { s[i] = Directory.GetFiles(file, "*.*")[i]; }
          //
          // }
          //
          // else
          // {
          //     s = new string[1];
          //     s[0] = string.Empty;
          // 
          // }
          //
          //
          //
          //
          //
          //
          // return s;

           
            



        }


       /// <summary>
       /// поиск нужного диска в массиве
       /// </summary>
       /// <param name="catalog">буква диска</param>
       /// <returns></returns>

       internal string ReadCatalogPath(string catalog)
        {

            string t = String.Empty;
            for (int i = 0; i < ManagerPath.Length; i++)
            {
                if (ManagerPath[i][0] != null)
                {

                    if (Regex.Match(ManagerPath[i][0], catalog + @"\W").Success)
                    {
                        t = ManagerPath[i][0];
                        R = t;
                    }


                }



            }
            return t;
        }


        /// <summary>
        /// поиск нужного каталога в массиве
        /// </summary>
        /// <returns></returns>
        string ArraySearchDirectory()
        {

            string directories = string.Empty;
            for (int i = 0; i < ManagerPath.Length; i++)
            {
                if (ManagerPath[i][0] == R)
                {
                    for (int j = 0; j < ManagerPath.Length; j++) { if (ManagerPath[i][j] == null) { directories = ManagerPath[i][j - 1]; break; } }

                }

            }


            return directories;

        }



        /// <summary>
        /// возвращает строку текущего положения в проводнике
        /// </summary>
        /// <returns></returns>
       internal  string ConductorPath()
        {

            StringBuilder conductorPath = new StringBuilder();
            for (int i = 0; i < ManagerPath.Length; i++)
            {

                if (ManagerPath[i][0] == R)

                {
                    for (int j = 0; j < ManagerPath.Length; j++)
                    {
                        if (ManagerPath[i][j] != null) 
                        {
                             conductorPath.Append(ManagerPath[i][j]);
                          
                        }



                    }




                }

            }
            return conductorPath.ToString();


        }

        /// <summary>
        /// Добавляет выбранную пользователем папку в массив
        /// </summary>
        /// <param name="r">перечисление из котрого выбирает пользователь</param>
        /// <param name="t">наименование выбранной папки которую указывает пользователь в консоли</param>
     internal void ArrayAddDirectory(IEnumerable<DirectoryInfo> r , string t) 
        {

            string _t = string.Empty;

            foreach (DirectoryInfo info in r) { if (Regex.Match(info.Name, $@"\w*?{t}\w*?").Success) { _t = info.Name;  } }


            for (int i = 0; i < ManagerPath.Length; i++)
            {
                if (ManagerPath[i][0]==R) 
                {
                    for (int j = 0; j < ManagerPath.Length; j++) 
                    { if (j==1&ManagerPath[i][j]== null) { ManagerPath[i][j] =  Path.Combine(_t); break;  }
                        if (j!=1 & ManagerPath[i][j] == null) { ManagerPath[i][j] = Path.DirectorySeparatorChar + Path.Combine(_t); break; }

                    }


                }
            
            }



        }



        internal void ArrayAddFile(FileInfo[] r, string t)
        {

            string _t = string.Empty;

            foreach (FileInfo info in r) { if (Regex.Match(info.Name, @"...." + t + @"....").Success) { _t = info.Name; } }


            for (int i = 0; i < ManagerPath.Length; i++)
            {
                if (ManagerPath[i][0] == R)
                {
                    for (int j = 0; j < ManagerPath.Length; j++) { if (ManagerPath[i][j] == null) { ManagerPath[i][j] = Path.Combine(_t); } }


                }

            }



        }







        /// <summary>
        /// Читаем диски из массива
        /// </summary>
        /// <returns></returns>

        internal string ReadingDisksArray() 
        {
            string r = string.Empty;

            for (int i = 0; i < ManagerPath.Length; i++)
            {
                if (ManagerPath[i][0]!=null)
                {
                    string _r = $"\n" + ManagerPath[i][0];
                    r += _r;


                }

            }


            return r;


        }
        /// <summary>
        /// создаем папку
        /// </summary>
        /// <param name="NameFolder">наименование папки</param>

        internal void  FolderCreation( string NameFolder) 
        {
            string directories = ConductorPath()+ Path.DirectorySeparatorChar + NameFolder;

            DirectoryInfo di = new DirectoryInfo(directories);

            di.Create();


        }

        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="Neim">наименование файла с расширением</param>

        internal void FileCreations(string Neim) 
        {
            string filePath = ConductorPath() +Path.DirectorySeparatorChar + Neim;
            File.Create(filePath);

        }


        /// <summary>
        /// удаление папки
        /// </summary>
        /// <param name="Neim"> имя папки которую нужно удалить</param>
        internal void FolderDeleting(string Neim)
        {
            string folderPath = ConductorPath() + Path.DirectorySeparatorChar + Neim;

            DirectoryInfo di = new DirectoryInfo(folderPath);

            di.Delete();


       }


        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="Neim">имя файла который нужно удалить</param>

        internal void FileDeleting(string Neim)
        {
            string filePath = ConductorPath() + Path.DirectorySeparatorChar + Neim;
            File.Delete(filePath);
        }

        /// <summary>
        /// перемещение папки 
        /// </summary>
        /// <param name="Neim"> имя папки</param>
        /// <param name="newWay">место куда нужно переместь папку</param>
        internal void MovingFolder(string Neim, string newWay  ) 
        {

            string folderPath = ConductorPath() + Path.DirectorySeparatorChar + Neim;

            DirectoryInfo di = new DirectoryInfo(folderPath);

            di.MoveTo(Path.Combine(newWay, Neim));

        }

        
        /// <summary>
        /// перемещение файла
        /// </summary>
        /// <param name="Neim">Наименование файла</param>
        /// <param name="newWay">место куда нужно переместь файл</param>

        internal void MovingFile(string Neim, string newWay )
        {
            FileInfo t = new FileInfo(Neim);
            string folderPath = ConductorPath();

            DirectoryInfo di = new DirectoryInfo(folderPath);



            FileInfo[] files = di.GetFiles();

            foreach (FileInfo info in files)
            {
                if (info.Name == Neim)
                {
                    t = info;


                }

            }
            string newDestinationDir = Path.Combine(newWay, t.Name);

            t.MoveTo(newDestinationDir);

        }



        /// <summary>
        /// копирование файла
        /// </summary>
        /// <param name="Neim">Наименование файла</param>
        /// <param name="newWay">Место куда копируем</param>
        /// 
        internal void CopyFile(string Neim, string newWay)
        {
            FileInfo t = new FileInfo(Neim);
            string folderPath = ConductorPath();

            DirectoryInfo di = new DirectoryInfo(folderPath);

            

            FileInfo[] files = di.GetFiles();

            foreach (FileInfo info in files)
            {
                if (info.Name== Neim)
                {
                    t = info;


                }
            
            }
            string newDestinationDir = Path.Combine(newWay, t.Name);

            t.CopyTo(newDestinationDir);


        }

        /// <summary>
        /// копирование папки
        /// </summary>
        /// <param name="Neim">Наименование папки</param>
        /// <param name="newWay">куда копируем папку</param>
        /// <param name="recursive">рекурсивно (true) или не рекурсивно (falh) вызывать функцию</param>
        /// <param name="count">при рекурсивноом вызове функции при 1 вызове функции  2 и тд при последующих вызовах функции</param>

        internal void CopyFolder(string Neim, string newWay, bool recursive, int count)
        {
            string folderPath = string.Empty;
            string _folderPath = newWay;

            if (count == 1)
            {
                folderPath = ConductorPath() + Path.DirectorySeparatorChar + Neim;
                _folderPath = newWay + Path.DirectorySeparatorChar + Neim;
            }

            if (count > 1)
            {

                folderPath = Neim;

              
              string [] f = folderPath.Split(Path.DirectorySeparatorChar);

              string _h1 = Path.DirectorySeparatorChar + f[f.Length-1]; _folderPath += _h1;  

            }



            DirectoryInfo di = new DirectoryInfo(folderPath);

            DirectoryInfo[] dirs = di.GetDirectories();



            
            

            Directory.CreateDirectory(_folderPath);

            foreach (FileInfo file in di.GetFiles())
            { 
                string newDestinationDir = Path.Combine(_folderPath, file.Name); file.CopyTo(newDestinationDir);
            
            
            }

            if (recursive) 
            { 
                foreach (DirectoryInfo info in dirs)
                {
                    string newDestinationDir = Path.Combine(_folderPath, info.Name);

                     CopyFolder(info.FullName, newDestinationDir, true, 2); 

                    continue;
                    
                   
                } 
            }

        }

        /// <summary>
        /// подсчет размера папки
        /// </summary>
        /// <param name="Neim">наименование папки</param>
        /// <returns>размер папки в байтах</returns>
        internal float    FolderSizeCalculation(string Neim) 
        {

            string folder = ConductorPath() + Path.DirectorySeparatorChar + Neim;

            DirectoryInfo dir = new DirectoryInfo(folder);

            float folderSize = 0.0f;
            try
            {
                //Checks if the path is valid or not
                if (!dir.Exists)
                    return folderSize;
                else
                {
                    try
                    {
                       
                            foreach (DirectoryInfo t in dir.GetDirectories())
                            {
                                folder = t.FullName;

                                foreach (FileInfo f in t.GetFiles())
                                {
                                    folderSize += f.Length;

                                };

                                
                            };


                        

                      

                    }

                    catch (NotSupportedException e)
                    {
                        Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
               // Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
            }
            return folderSize;


        }


        /// <summary>
        /// Переименование папки
        /// </summary>
        /// <param name="Neim">имя папки</param>
        /// <param name="NewNeim">Новое имя папки</param>
        internal void RenamingFolder(string Neim, string NewNeim) 
        {
            string folderPath = ConductorPath() + Path.DirectorySeparatorChar + Neim;
            

            FileSystem.RenameDirectory(folderPath, NewNeim);



        }

        /// <summary>
        /// Переименование файла
        /// </summary>
        /// <param name="Neim">наименование файла</param>
        /// <param name="NewNeim">новое имя файла</param>
        internal void RenamingFile(string Neim, string NewNeim) 
        {
            string filePath = ConductorPath() + Path.DirectorySeparatorChar + Neim;

            FileSystem.RenameFile(filePath, NewNeim);


        }


        /// <summary>
        /// Поиск папки в  текущем каталоге и поткаталогах
        /// </summary>
        /// <param name="Neim">Имя папки</param>
        /// <returns>Массив DirectoryInfo[] с резудьтатоми поиска </returns>
        internal DirectoryInfo[] SearhFolder(String Neim) 
        {
            string folderPath = ConductorPath();
            string _neim = "*" + Neim + "*";

         DirectoryInfo di = new DirectoryInfo(folderPath);
         DirectoryInfo[] infos =   di.GetDirectories(_neim, System.IO.SearchOption.AllDirectories);
         return infos;


        }

        /// <summary>
        /// Поиск файла в текущем каталоге
        /// </summary>
        /// <param name="Neim">Имя файла</param>
        /// <returns>FileInfo[] с результатами поиска</returns>
        internal FileInfo[] SearhFile(String Neim) 
        {

            string folderPath = ConductorPath();
            string _neim = "*" + Neim + "*";
            DirectoryInfo di = new DirectoryInfo(folderPath);
            FileInfo[] infos = di.GetFiles(_neim, System.IO.SearchOption.AllDirectories);
            return infos;


        }

        /// <summary>
        /// Подсчет количества слов строк и пробелов в текстовых файлах.
        /// </summary>
        /// <param name="Neim">имя файла</param>
        /// <returns>экземпляр класса FileTextData с необходимыми данными</returns>

        internal FileTextData DataTextFile(String Neim) 
        {

            string filePath = ConductorPath() + Path.DirectorySeparatorChar + Neim;


            char[] chars;
            FileTextData textData = new();
            byte[] bytes;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fs.Length];


                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {

                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);
                   

                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }


            }   
                Decoder utf8Decoder = Encoding.UTF8.GetDecoder();
                int charCount = utf8Decoder.GetCharCount(bytes, 0, bytes.Length);
                 chars = new Char[charCount];


                utf8Decoder.GetChars(bytes, 0, bytes.Length, chars, 0);



            

            char[] wordCount = Array.FindAll(chars, x => x.Equals(' '));

            char[] numberLines = Array.FindAll(chars, x => x.Equals('.') & (x + 1).Equals(' ')) ;

            textData.WordCount = wordCount.Length;
            textData.NumberLines = numberLines.Length;
            textData.NumberParagraphs = 0;
            textData.NumberSpaces = wordCount.Length;
           

            return textData;
        }



        internal void StepBack()
        {


            for (int i = 0; i < ManagerPath.Length; i++)
            {
                if (ManagerPath[i][0] == R)
                {
                    for (int j = 0; j < ManagerPath.Length; j++)
                    {
                        

                        if (j!= 0 & ManagerPath[i][j] == null) { ManagerPath[i][j-1] = null; break; }
                        if (j == 0 & ManagerPath[i][j] != null) { Console.WriteLine("Доступны перечисленные диски"); Console.WriteLine(ReadingDisksArray()) ; Console.WriteLine("Укажите требуемую букву диска"); string catalog = Console.ReadLine(); ReadCatalogPath(catalog); break; }


                    }


                }

            }




        }


    }
}
