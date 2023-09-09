  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using ConsoleFileManager.Infrastructure;
using ConsoleFileManager.HelperClasses;

namespace ConsoleFileManager.Display
{
    internal class Display: Infrastructure.Infrastructure
    {


        internal Display()
        
        {
           
            while (true)
            {


                if (Display.R == String.Empty) { DiscOverview(); }

                FolderNavigation();








            };
        
        







        
           void  DiscOverview ()
            {
                DisplayCatalog(); // записываем все диски в массив ManagerPath
                Console.WriteLine($"\n" + "На этом устройсве доступны перечисленные ниже диски");
                Console.WriteLine(ReadingDisksArray()); // читаем все диски из массива в консоль
                Console.WriteLine($"\n" + "В ведите требуемую букву диска");
                String t = Console.ReadLine();
                foreach (DirectoryInfo q in DisplayDirectories(t)) { Console.WriteLine( q.Name + " размер папки - " + FolderSizeCalculation(q.Name) + "  байт" ); };
                foreach (FileInfo q in DisplayFile()) { Console.WriteLine( q +" размер файла - "+q.Length + " байт"); };


            }


            void FolderNavigation ()
            {

                Console.WriteLine($"\n" + "для выбора папки введите наименование папки и нажмите Enter");
                Console.WriteLine($"\n" + "для возврата на 1 шаг назад введите J  и нажмите Enter");
                Console.WriteLine($"\n" + "Для выполнение действий над папками и файлами введите пробел и нажмите Enter");
                string S = Console.ReadLine();
                if (S.Length != null&S!=" "&S!= "J")
                {

                    ArrayAddDirectory(DisplaySubDirectories(), S);
                    Console.WriteLine($"\n" + "сейчас вы находитесь сдесь - " + ConductorPath() + $"\n");
                    foreach (DirectoryInfo q in DisplaySubDirectories()) { Console.WriteLine(q.Name + " размер папки - " + FolderSizeCalculation(q.Name) + "  байт"); };
                    foreach (FileInfo q in DisplayFile()) { Console.WriteLine(q + " размер файла - " + q.Length + " байт"); };
                    //if (DisplayFile().Length > 0) { foreach (FileInfo q in DisplayFile()) { Console.WriteLine(q.Name + " размер файла - " + q.Length + " байт"); }; }



                }
                if (S == "J")
                { 
                    StepBack();
                    if (DisplaySubDirectories().ToArray().Length > 1)
                    {
                        Console.WriteLine($"\n" + "сейчас вы находитесь сдесь - " + ConductorPath() + $"\n");
                        foreach (DirectoryInfo q in DisplaySubDirectories()) { Console.WriteLine(q.Name + " размер папки - " + FolderSizeCalculation(q.Name) + "  байт"); };
                        foreach (FileInfo q in DisplayFile()) { Console.WriteLine(q + " размер файла - " + q.Length + " байт"); }
                    }
                    else { StepBack(); }

                }
                if (S == " ") 
                { 
                    PperationsFoldersFiles();
                }
                




            }



            void PperationsFoldersFiles ()

            {
                void t2() { Console.WriteLine("введите название новой папки"); string s1 = Console.ReadLine(); FolderCreation(s1); }
                void t3() { Console.WriteLine("введите название  папки которую нужно удалить"); string s1 = Console.ReadLine(); FolderDeleting(s1); }
                void t4() { Console.WriteLine("введите имя папки которую нужно переименовать"); string s1 = Console.ReadLine(); Console.WriteLine("введите новое имя папки"); string s2 = Console.ReadLine(); RenamingFolder(s1, s2); }
                void t5() { Console.WriteLine("введите имя папки которую нужно скопировать"); string s1 = Console.ReadLine(); Console.WriteLine("введите путь - куда нужно скопировать"); string s2 = Console.ReadLine(); CopyFolder(s1, s2, true, 1); }
                void t6() { Console.WriteLine("введите имя папки которую нужно найти"); string s1 = Console.ReadLine(); DirectoryInfo[] m = SearhFolder(s1); Console.WriteLine("результаты поиска"); foreach (DirectoryInfo _m in m) { Console.WriteLine(_m.FullName); } }
                void t7() { Console.WriteLine("введите имя папки которую нужно переместить"); string s1 = Console.ReadLine(); Console.WriteLine("Ввведите полный путь - куда нужно переместить папку"); string s2 = Console.ReadLine(); MovingFolder(s1, s2); }
                void t8() { Console.WriteLine("Введите имя файла "); string s1 = Console.ReadLine(); FileCreations(s1); }
                void t9() { Console.WriteLine("введите название название файла для его удаления"); string s1 = Console.ReadLine(); FileDeleting(s1); }
                void t10() { Console.WriteLine("введите имя файла которую нужно переименовать"); string s1 = Console.ReadLine(); Console.WriteLine("введите новое имя файла"); string s2 = Console.ReadLine(); RenamingFile(s1, s2); }
                void t11() { Console.WriteLine("введите имя файла который нужно скопировать"); string s1 = Console.ReadLine(); Console.WriteLine("введите путь - куда нужно скопировать"); string s2 = Console.ReadLine(); CopyFile(s1, s2); }
                void t12() { Console.WriteLine("введите имя файла который нужно переместить"); string s1 = Console.ReadLine(); Console.WriteLine("Ввведите полный путь - куда нужно переместить файл"); string s2 = Console.ReadLine(); MovingFile(s1, s2); }
                void t13() { Console.WriteLine("введите имя файла для его поиска"); string s1 = Console.ReadLine(); FileInfo[] m = SearhFile(s1); Console.WriteLine("результаты поиска"); foreach (FileInfo _m in m) { Console.WriteLine(_m.FullName); } }
                void t14() { Console.WriteLine("введите имя текстового файла с расширением для отоброжения информации о нем"); string s1 = Console.ReadLine(); FileTextData t = DataTextFile(s1); Console.WriteLine($"количество слов -  {t.WordCount}, количество предложений - {t.NumberLines} , Количество пробелов - {t.NumberSpaces}"); }
                void t15() { Console.WriteLine("Переключились в режим навигации по проводнику"); FolderNavigation(); }







                while (true)
                { 
               


                string s = "введите"
                      + $"\n 2 - создание папки" 
                       +$"\n 3 - удаление папки" 
                       +$"\n 4 - переименование папки" 
                       +$"\n 5 - копировать папку" 
                       +$"\n 6 - произвести поиск папки"
                       + $"\n 7 - переместить папку"
                       + $"\n 8 - создание файла" 
                       +$"\n 9 - удаление файла" 
                       +$"\n 10 - переименование файла" 
                       +$"\n 11 - копировать файла"
                       + $"\n 12 - переместить файл"
                       + $"\n 13 - произвести поиск файла" 
                       +$"\n 14 - вывод информации о текстовом файле "
                       +$"\n 15 - переход в режим навигации";

                Console.WriteLine(s);

                    string m =  Console.ReadLine();
                    int m2;
                    bool m1 = int.TryParse(m, out m2);
                    if (m1)
                    {
                        switch (m2)
                        {
                            case 2: t2(); break;
                            case 3: t3(); break;
                            case 4: t4(); break;
                            case 5: t5(); break;
                            case 6: t6(); break;
                            case 7: t7(); break;
                            case 8: t8(); break;
                            case 9: t9(); break;
                            case 10: t10(); break;
                            case 11: t11(); break;
                            case 12: t12(); break;
                            case 13: t13(); break;
                            case 14: t14(); break;
                            case 15: t15(); break;


                        };

                    }


                }

          
                

               
                
               
              











            }







        }



    }
}
