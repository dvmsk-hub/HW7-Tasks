
// Прочитать 3 файла параллельно и вычислить количество пробелов в них (через Task).

using System.Diagnostics;

var listFiles = new List<string> // список файлов
{
    "file1.txt",
    "file2.txt",
    "file3.txt"
};

var tasks = new Task[3]; // массив Task

// Запуск трех Task для чтения файлов 
for(var i = 0; i < 3; i++)
{
    var index = i;
    tasks[i] = new Task(() =>
    { 
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var numberSpaces = CountNumberSpacesInFile(listFiles[index]);
        stopWatch.Stop();
        var ts = stopWatch.Elapsed;
        Console.WriteLine(numberSpaces + " пробелов в файле " + listFiles[index] + " , время выполнения " + ts);
    });
    tasks[i].Start();   // запускаем задачу
}

Task.WaitAll(tasks); // ожидаем завершения всех задач

// Написать функцию, принимающую в качестве аргумента путь к папке. Из этой папки параллельно прочитать все файлы и вычислить количество пробелов в них.

var path = "d:/test/"; // путь к папке

CountNumberSpacesInFileInDirectory(path);

return;

// функция чтения файла и подсчета кол-ва пробелов
int CountNumberSpacesInFile(string filename)
{
    
    using var reader = new StreamReader(filename);
    var text = reader.ReadToEnd();
   
    return text.Count(x => x == ' ');
}

// функция чтения файлов в папке и подсчета кол-ва пробелов 
void CountNumberSpacesInFileInDirectory(string path)
{
    var listFilesFromDirectory = Directory.GetFiles(path);
    
    var tasksIn = new Task[listFilesFromDirectory.Length]; 
    
    for(var i = 0; i < listFilesFromDirectory.Length; i++)
    {
        var index = i;
        tasksIn[i] = new Task(() =>
        { 
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var numberSpaces = CountNumberSpacesInFile(listFilesFromDirectory[index]);
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine(numberSpaces + " пробелов в файле " + listFilesFromDirectory[index] + " , время выполнения " + ts);
        });
        tasksIn[i].Start();   // запускаем задачу
    }

    Task.WaitAll(tasksIn); // ожидаем завершения всех задач
}
 













