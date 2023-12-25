using System;
using System.IO;
using System.Xml;
using System.Text.Json;

namespace lab15_2
{
    public interface ILoggerRepository 
    {
        void LogText(string text); //методы для записи txt/json данных
        void LogJson(object data);

        public class FileLoggerRepository : ILoggerRepository
        {
            private string textFilePath; 
            private string jsonFilePath;

            public FileLoggerRepository(string textPath, string jsonPath) //конструктор, принимающий пути к файлам
            {
                textFilePath = textPath;
                jsonFilePath = jsonPath;
            }

            public void LogText(string text)
            {
                using (StreamWriter writer = new StreamWriter(textFilePath, true))
                {
                    writer.WriteLine(text);
                }
            }

            public void LogJson(object data)
            {
                string json = JsonSerializer.Serialize(data);

                using (StreamWriter writer = new StreamWriter(jsonFilePath, true))
                {
                    writer.WriteLine(json);
                }
            }
        }

        public class MyLogger
        {
            private ILoggerRepository repository;

            public MyLogger(ILoggerRepository repo) //конструктор, принимающий реализацию иинтерфейса
            {
                repository = repo;
            }

            public void LogText(string text) //методы для записей
            {
                repository.LogText(text);
            }

            public void LogJson(object data)
            {
                repository.LogJson(data);
            }
        }

        public class Program
        {
            public static void Main(string[] args)
            {
                string textFilePath = @"C:\Users\amana\OneDrive\Рабочий стол\task2\textFile.txt";
                string jsonFilePath = @"C:\Users\amana\OneDrive\Рабочий стол\task2\file.json";

                ILoggerRepository repository = new FileLoggerRepository(textFilePath, jsonFilePath);
                MyLogger logger = new MyLogger(repository);

                logger.LogText("TEEEXTTT!!!");
                logger.LogJson(new { Name = "LERA", Age = 45 });

                Console.WriteLine("Logs have been written to files.");
                Console.ReadLine();
            }
        }
    }
    }