using System;
using System.IO;
using System.Xml.Linq;
using System.Text.Json;
using System.IO.Compression;

namespace Krutov_Aleksey_laba1_OS
{
  class Person
  {
    public string Name { get; set; }
    public string SurName { get; set; }
    public int Year { get; set; }
    public string Country { get; set; }
  }

  class Program
  {
    /// <summary>
    /// Выводит информацию о дисках в консоль 
    /// </summary>
    static void getDiskInformation()
    {
      Console.WriteLine("1.Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы. ");
      DriveInfo[] drives = DriveInfo.GetDrives();
      //получить информацию о дисках
      foreach (DriveInfo drive in drives)
      {
        Console.WriteLine($"\tНазвание: {drive.Name}");
        Console.WriteLine($"\tТип: {drive.DriveType}");
        if (drive.IsReady)
        {
          Console.WriteLine($"\tОбъем диска: {drive.TotalSize}");
          Console.WriteLine($"\tСвободное пространство: {drive.TotalFreeSpace}");
          Console.WriteLine($"\tМетка: {drive.VolumeLabel}");
        }
        Console.WriteLine();
      }
    }


    /// <summary>
    /// Создает текстовый файл по path.
    /// </summary>
    /// <param name="path"></param>
    static void processTextFile(string path)
    {
      Console.WriteLine("2.Работа с файлами ");
      FileInfo fileInf = new FileInfo(path);
      try
      {
        using (FileStream fStream = File.Create(path))
        {
          Console.WriteLine($"\tФайл, создан по пути: {path}");
          //если файл создан, получить информацию о файле
          if (fileInf.Exists)
          {
            Console.WriteLine("\tИмя файла: {0}", fileInf.Name);
            Console.WriteLine("\tВремя создания: {0}", fileInf.CreationTime);
            Console.WriteLine("\tРазмер: {0}", fileInf.Length);
            Console.WriteLine();
          }
        }
        //перезаписывает файл, добавляя строку
        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
        {
          Console.Write("\tВведите строку для записи в файл: ");
          sw.WriteLine(Console.ReadLine());
        }
        //Открыть поток и прочитать файл
        using (StreamReader sr = new StreamReader(path))
        {
          Console.Write("\tИнформация из файла: ");
          Console.WriteLine(sr.ReadToEnd());
        }
        //если файл создан, удалить его
        Console.Write("Хотите удалить файл? (1/0): ");
        int sigh = int.Parse(Console.ReadLine());
        if (sigh == 1)
        {
          if (fileInf.Exists)
          {
            fileInf.Delete();
            Console.WriteLine($"\tФайл по пути {path} удален.");
            Console.WriteLine();
          }
        }
        else
        {
          Console.WriteLine($"\tФайл по пути {path} удален.");
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// Создает файл JSON по path
    /// </summary>
    /// <param name="path"></param>
    static void processJsonFile(string path)
    {
      Console.WriteLine("3.Работа с форматом JSON ");
      FileInfo fileJSON = new FileInfo(path);
      try
      {
        using (FileStream fStream = File.Create(path))
        {
          Console.WriteLine($"\tФайл, создан по пути: {path}");
          //если файл создан, получить информацию о файле
          if (fileJSON.Exists)
          {
            Console.WriteLine("\tИмя файла: {0}", fileJSON.Name);
            Console.WriteLine("\tВремя создания: {0}", fileJSON.CreationTime);
            Console.WriteLine("\tРазмер: {0}", fileJSON.Length);
            Console.WriteLine();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      try
      {
        //запись данных
        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
        {
          Person person = new Person();
          Console.Write("\tВведите имя человека: ");
          person.Name = Console.ReadLine();
          Console.Write("\tВведите фамилию человека: ");
          person.SurName = Console.ReadLine();
          Console.Write("\tВведите страну: ");
          person.Country = Console.ReadLine();
          while (true)
          {
            Console.Write("\tВведите год рождения: ");
            string year = Console.ReadLine();
            if (int.TryParse(year, out int number))
            {
              person.Year = number;
              break;
            }
            Console.Write("Вы ввели не число, введите число еще раз: ");
          }
          sw.WriteLine(JsonSerializer.Serialize<Person>(person));
        }
        //чтение данных
        using (StreamReader sr = new StreamReader(path))
        {
          Console.Write("\tИнформация из файла:\n ");
          Person restoredPerson = JsonSerializer.Deserialize<Person>(sr.ReadToEnd());
          Console.WriteLine($"\t\tName: {restoredPerson.Name}\n\t\tSurname: {restoredPerson.SurName}");
          Console.WriteLine($"\t\tCountry: {restoredPerson.Country}\n\t\tYear: {restoredPerson.Year}");
        }
        //если файл создан, удалить его
        Console.Write("Хотите удалить файл? (1/0): ");
        int sigh = int.Parse(Console.ReadLine());
        if (sigh == 1)
        {
          if (fileJSON.Exists)
          {
            fileJSON.Delete();
            Console.WriteLine($"\tФайл по пути {path} удален.");
            Console.WriteLine();
          }
        }
        else
        {
          Console.WriteLine($"\tФайл по пути {path} не удален.");
        }

      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

    }

    /// <summary>
    ///  Создает файл XML с названием fileName
    /// </summary>
    /// <param name="path"></param>
    static void processXMLFile(string path)
    {
      Console.WriteLine("4.Работа с форматом XML ");
      FileInfo fileXML = new FileInfo(path);
      try
      {
        using (FileStream fStream = File.Create(path))
        {
          Console.WriteLine($"\tФайл, создан по пути: {path}");
          //если файл создан, получить информацию о файле
          if (fileXML.Exists)
          {
            Console.WriteLine("\tИмя файла: {0}", fileXML.Name);
            Console.WriteLine("\tВремя создания: {0}", fileXML.CreationTime);
            Console.WriteLine("\tРазмер: {0}", fileXML.Length);
            Console.WriteLine();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      XDocument xDoc = new XDocument();
      //создаем элемент - студент
      XElement person = new XElement("person");
      Console.Write("\tВведите имя человека: ");
      XAttribute nameXAttr = new XAttribute("name", Console.ReadLine());
      Console.Write("\tВведите фамилию человека: ");
      XAttribute surnameXAttr = new XAttribute("surname", Console.ReadLine());
      Console.Write("\tВведите год рождения: ");
      XElement yearXElm = new XElement("year", Console.ReadLine());
      Console.Write("\tВведите страну: ");
      XElement countryXElm = new XElement("country", Console.ReadLine());
      //добавим выше введенные данные к person
      person.Add(nameXAttr);
      person.Add(surnameXAttr);
      person.Add(yearXElm);
      person.Add(countryXElm);
      //создадим корневой элемент
      XElement persons = new XElement("persons");
      //добавим в корневой элемент введеннго студента
      persons.Add(person);
      // добавляем корневой элемент в документ
      xDoc.Add(persons);
      //сохраняем документ
      xDoc.Save(path);

      //загружаем документ
      XDocument xDocLoad = XDocument.Load(path);
      XElement personXElement = xDocLoad.Element("persons").Element("person");
      //получаем информацию из документа
      nameXAttr = personXElement.Attribute("name");
      surnameXAttr = personXElement.Attribute("surname");
      yearXElm = personXElement.Element("year");
      countryXElm = personXElement.Element("country");
      //вывод информации на консоль
      Console.WriteLine("\tИнформация в файле: ");
      Console.WriteLine($"\t\tИмя и фамилия человека: {nameXAttr.Value} {surnameXAttr.Value}");
      Console.WriteLine($"\t\tГод рождения: {yearXElm.Value}");
      Console.WriteLine($"\t\tСтрана: {countryXElm.Value}");
      //если файл создан, удалить его
      Console.Write("Хотите удалить файл? (1/0): ");
      int sigh = int.Parse(Console.ReadLine());
      if (sigh == 1)
      {
        if (fileXML.Exists)
        {
          fileXML.Delete();
          Console.WriteLine($"\tФайл по пути {path} удален.");
          Console.WriteLine();
        }
      }
      else
      {
        Console.WriteLine($"\tФайл по пути {path} не удален.");
      }
    }

    /// <summary>
    /// Создает архив по pathArchive, добавляет файл pathFile в архив и выводит его на консоль 
    /// </summary>
    /// <param name="pathArchive"></param>
    /// <param name="pathFile"></param>
    static void processZIPArchive(string pathArchive, string pathFile)
    {
      Console.WriteLine("5.Создание zip архива, добавление туда файла, определение размера архива ");
      try
      {
        using (FileStream fStream = File.Create(pathArchive))
        {
          Console.WriteLine($"\tФайл, создан по пути: {pathArchive}");
          FileInfo fileInf = new FileInfo(pathArchive);
          //если файл создан, получить информацию о файле
          if (fileInf.Exists)
          {
            Console.WriteLine("\tИмя файла: {0}", fileInf.Name);
            Console.WriteLine("\tВремя создания: {0}", fileInf.CreationTime);
            Console.WriteLine("\tРазмер: {0}", fileInf.Length);
            Console.WriteLine();
          }
        }
        //запись файла в архив
        using (FileStream zipToOpen = new FileStream(pathArchive, FileMode.Open))
        {
          using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
          {
            ZipArchiveEntry fileText = archive.CreateEntry(pathFile);
            using (StreamWriter writer = new StreamWriter(fileText.Open()))
            {
              Console.Write($"\tВведите данные в файл для добавления его в архив {pathArchive}: ");
              writer.WriteLine(Console.ReadLine());
            }
          }
        }
        FileInfo fileInfArchive = new FileInfo(pathArchive);
        //если файл создан, получить информацию о файле
        if (fileInfArchive.Exists)
        {
          Console.WriteLine("\tИмя файла: {0}", fileInfArchive.Name);
          Console.WriteLine("\tВремя создания: {0}", fileInfArchive.CreationTime);
          Console.WriteLine("\tРазмер: {0}", fileInfArchive.Length);
          Console.WriteLine();
        }
        //разархивация файла в архиве
        using (ZipArchive zip = ZipFile.OpenRead(pathArchive))
        {
          zip.ExtractToDirectory("./");
        }
        Console.WriteLine("\tДанные из разархифированного файла: ");
        FileInfo fileInfText = new FileInfo(pathFile);
        //если файл создан, получить информацию о файле
        if (fileInfText.Exists)
        {
          Console.WriteLine("\tИмя файла: {0}", fileInfText.Name);
          Console.WriteLine("\tВремя создания: {0}", fileInfText.CreationTime);
          Console.WriteLine("\tРазмер: {0}", fileInfText.Length);
          Console.WriteLine();
        }
        //Открыть поток и прочитать файл
        using (StreamReader sr = new StreamReader(pathFile))
        {
          Console.Write("\tИнформация из файла: ");
          Console.WriteLine(sr.ReadToEnd());
        }
        Console.Write("Хотите удалить файл? (1/0): ");
        int sign = int.Parse(Console.ReadLine());
        if (sign == 1)
        {
          //если файл создан, удалить его
          if (fileInfText.Exists)
          {
            fileInfText.Delete();
            Console.WriteLine($"\tРазархивированный файл по пути {pathFile} удален.");
            Console.WriteLine();
          }
          else
          {
            Console.WriteLine($"\tРазархивированный файл по пути {pathFile} не был удален.");
          }
        }
        //удалить файл из архива
        Console.Write("Хотите удалить файл? (1/0): ");
        sign = int.Parse(Console.ReadLine());
        if (sign == 1)
        {
          using (ZipArchive archive = ZipFile.Open(pathArchive, ZipArchiveMode.Update))
          {
            ZipArchiveEntry archiveEntry = archive.Entries[0];
            archiveEntry.Delete();
          }
          Console.WriteLine($"\tФайл {pathFile} в архиве {pathArchive} был удален.");
        }
        else
        {
          Console.WriteLine($"\tФайл {pathFile} в архиве {pathArchive} не был удален.");
        }

        FileInfo fileInfArchiveEmpty = new FileInfo(pathArchive);
        //если файл создан, получить информацию о файле
        if (fileInfArchiveEmpty.Exists)
        {
          Console.WriteLine("\tИмя файла: {0}", fileInfArchiveEmpty.Name);
          Console.WriteLine("\tВремя создания: {0}", fileInfArchiveEmpty.CreationTime);
          Console.WriteLine("\tРазмер: {0}", fileInfArchiveEmpty.Length);
          Console.WriteLine();
        }
        //если файл создан, удалить его
        Console.Write("Хотите удалить файл? (1/0): ");
        sign = int.Parse(Console.ReadLine());
        if (sign == 1)
        {
          if (fileInfArchive.Exists)
          {
            fileInfArchive.Delete();
            Console.WriteLine($"\tФайл по пути {pathArchive} удален.");
            Console.WriteLine();
          }
        }
        else
        {
          Console.WriteLine($"\tФайл по пути {pathArchive} не удален.");
        }

      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }

    /// <summary>
    /// Выводит меню для работы с пользователем
    /// </summary>
    static void printMenu()
    {
      bool flag = true;
      while (flag)
      {
        Console.WriteLine("Введите цифру для доступа к заданиям:");
        Console.WriteLine("1.Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы. ");
        Console.WriteLine("2.Работа с файлами ");
        Console.WriteLine("3.Работа с форматом JSON ");
        Console.WriteLine("4.Работа с форматом XML ");
        Console.WriteLine("5.Создание zip архива, добавление туда файла, определение размера архива ");
        Console.WriteLine("6.Очистить консоль");
        Console.WriteLine("7.Выход из меню");
        int input = int.Parse(Console.ReadLine());
        switch (input)
        {
          case 1:
            getDiskInformation();
            break;
          case 2:
            string pathTXT = "text.txt";
            processTextFile(pathTXT);
            break;
          case 3:
            string pathJSON = "student.json";
            processJsonFile(pathJSON);
            break;
          case 4:
            string pathXML = "students.xml";
            processXMLFile(pathXML);
            break;
          case 5:
            string pathZIP = "fzip.zip";
            string pathZIPFile = "fzip.txt";
            processZIPArchive(pathZIP, pathZIPFile);
            break;
          case 6:
            Console.Clear();
            break;
          case 7:
            flag = false;
            break;
          default:
            Console.WriteLine("Данного пункта нет в меню");
            break;
        }
      }
      Console.Write("Нажмите ENTER, чтобы выйти из программы.");
      Console.Read();
    }

    static void Main(string[] args)
    {
      printMenu();
    }
  }
}