using System.IO.Compression;
/*
XmlSerializer serializer = new XmlSerializer(typeof(Cow));
using (FileStream fileStream = new FileStream("/Users/mira/RiderProjects/lab7/ConsoleApp7/bin/Debug/net7.0/lab7.dll", FileMode.Open))
{
    Cow cow = (Cow)serializer.Deserialize(fileStream);
    Console.WriteLine("Deserialized Cow Object:");
    Console.WriteLine("Country: " + cow.Country);
    Console.WriteLine("HideFromOtherAnimals: " + cow.HideFromOtherAnimals);
    Console.WriteLine("Name: " + cow.Name);
    Console.WriteLine("WhatAnimal: " + cow.WhatAnimal);
    Console.WriteLine("Classification Animal: " + cow.GetClassificationAnimal());
    Console.WriteLine("Favorite Food: " + cow.GetFavoriteFood());
    cow.SayHello();
    Console.ReadLine();
}
*/
Console.Write("Путь: ");
string directoryPath = Console.ReadLine();

Console.Write("Имя файла: ");
string fileNameToFind = Console.ReadLine();

// Ищем файл в каталоге и его поддиректориях
string[] foundFiles = Directory.GetFiles(directoryPath, fileNameToFind, SearchOption.AllDirectories);

if (foundFiles.Length == 0)
{
    throw new Exception($"Файл '{fileNameToFind}' не найден.");
}

foreach (string filePath in foundFiles)
{
    Console.WriteLine($"Найден файл: {filePath}");

    // Выводим содержимое файла на консоль с использованием FileStream
    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
    {
        using (StreamReader reader = new StreamReader(fileStream))
        {
            string fileContent = reader.ReadToEnd();
            Console.WriteLine("Содержимое файла:");
            Console.WriteLine(fileContent);
        }
    }

    string compressedFilePath = filePath + ".gz";

    // Сжатие файла с использованием GZipStream
    using (FileStream originalFileStream = new FileStream(filePath, FileMode.Open))
    {
        using (FileStream compressedFileStream = File.Create(compressedFilePath))
        {
            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
            {
                originalFileStream.CopyTo(compressionStream);
                Console.WriteLine($"Файл сжат и сохранен как: {compressedFilePath}");
            }
        }
    }

    Console.ReadKey();
}