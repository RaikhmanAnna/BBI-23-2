using System;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CW_2_Raikhman
{
    abstract class Task
    {
        protected string text;
        public string Text
        {
            get { return text; }
            protected set { text = value; }
        }
        public Task(string text)
        {
            this.text = text;
        }
    }
    class Task1 : Task
    {
        [JsonConstructor]
        public Task1(string text) : base(text)
        {
            string[] array = text.Split('.');
            string[] lastwords = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                string[] words = array[i].Split(' ');
                lastwords[i] = words[words.Length - 1];
            }
        }
        public override string ToString()
        {
            string output = "";
            for(int i = 0; i < lastwords.Length; i++)
            {
                output += lastwords[i] + "\n";
            }
            return output;
        }


    }
    class Task2 : Task
    {
        public Task2(string text) : base(text)
        {
            this.text = text;
        }
        public override string ToString()
        {
            return text;
        }
    }
    class JSONManager
    {
        public static void Write<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, obj);
            }
        }
        public static T Read<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<T>(fs);
            }
        }
    }
    class Program
    {
        static void Main()
        {
            string text = "За окном светит солнышко. Я беру с собой портфель и иду на море. Надеюсь, вода в речке будет тёплой.";
            Task[] tasks = { new Task1(text), new Task2(text) };
            Console.WriteLine(tasks[0]);
            Console.WriteLine(tasks[1]);


            string path = "C:\\Users\\m2303931";
            string folderName = "Answer";
            path = Path.Combine(path, folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string file1 = "cw2_1.json";
            string file2 = "cw2_2.json";


            if (!File.Exists(file2))
            {
                JSONManager.Write<Task>((Task2)tasks[1], file2);
            }
            else
            {
                var task1 = JSONManager.Read<Task>(file1);
                var task2 = JSONManager.Read<Task>(file2);
                Console.WriteLine(task1);
                Console.WriteLine(task2);
            }
        }
    }
}
