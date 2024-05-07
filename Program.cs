//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Подготовка
//{
//    class Program
//    {
//        static void Main()
//        {
//            // Исходное предложение
//            string sentence = "Двигатель самолета – это сложная инженерная конструкция, обеспечивающая подъем, управление и движение в воздухе. Он состоит из множества компонентов, каждый из которых играет важную роль в общей работе механизма. Внутреннее устройство двигателя включает в себя компрессор, камеру сгорания, турбину и системы управления и охлаждения. Принцип работы основан на воздушно-топливной смеси, которая подвергается сжатию, воспламенению и расширению, обеспечивая движение воздушного судна..";

//            // Разделяем предложение на слова и знаки препинания
//            string[] words = sentence.Split(' ', ',', '.', '!', '?');
//            int punctuationMarksCount = 0;

//            // Подсчитываем знаки препинания
//            foreach (char c in sentence)
//            {
//                if (Char.IsPunctuation(c))
//                {
//                    punctuationMarksCount++;
//                }
//            }

//            // Вычисляем сложность предложения
//            int complexity = words.Length + punctuationMarksCount;

//            // Выводим сложность предложения
//            Console.WriteLine($"Сложность предложения: {complexity}");
//        }
//    }
//}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using System.Xml.Linq;


public abstract class Task
{
    protected string text;

    public Task(string text)
    {
        this.text = text;
    }

    public override string ToString()
    {
        return text;
    }

}

public class Task_2 : Task
{
    public Task_2(string text) : base(text)
    {
    }

    public string Encrypt(string text)
    {
        string[] words = text.Split(' ');
        for (int i = 0; i < words.Length - 1; i++)
        {
            if (!Char.IsLetter(words[i][words[i].Length - 1]))
            {

                char[] word = words[i].ToCharArray(0, words[i].Length - 1);
                string rest = words[i].Substring(words[i].Length - 1);
                Array.Reverse(word);
                words[i] = new string(word) + rest;
            }
            else
            {
                char[] word = words[i].ToCharArray();
                Array.Reverse(word);
                words[i] = new string(word);
            }
        }

        string reversedText = string.Join(" ", words);
        return reversedText;

    }
}

public class Task_4 : Task
{
    public Task_4(string text) : base(text)
    {
    }

    public int Complexity()
    {
        int countwords = text.Split( ' ', '.', ',', '!', '?' ).Length;
        int countpunct = text.Count(char.IsPunctuation);
        int res = countwords + countpunct;

        return res;
    }

    public override string ToString()
    {
        return Complexity().ToString();
    }
}

public class Task_6 : Task
{
    public Task_6(string text) : base(text)
    {
    }
    public void Syllables()
    {
        text = text.ToLower();
        string[] words = text.Split(' ', ',', '.', '!', '?');
        Dictionary<int, int> syllableCount = new Dictionary<int, int>();

        foreach (string word in words)
        {
            if (word.Length > 0) 
            {
                int syllables = CountSyllables(word);
                if (syllableCount.ContainsKey(syllables))
                {
                    syllableCount[syllables]++;
                }
                else
                {
                    syllableCount[syllables] = 1;
                }
            }
        }

        foreach (var pair in syllableCount)
        {
            Console.WriteLine($"Количество слог: {pair.Key}, количество слов {pair.Value}.");
        }
    }

    static int CountSyllables(string word)
    {
        string vowels = "аеёиоуыэюяaeiouy";
        int count = 0;
        bool prevVowel = false;

        foreach (char c in word.ToLower())
        {
            if (vowels.Contains(c))
            {
                if (!prevVowel)
                {
                    count++;
                }
                prevVowel = true;
            }
            else
            {
                prevVowel = false;
            }
        }
        if (count == 0 && word.Length > 0)//одиночные гласные
        {
            count++;
        }

        return count;
    }

}

public class Task_8 : Task
{
    public Task_8(string text) : base(text)
    {
    }
    public void SplitText(int m)
    {
        string[] words = text.Split(' ');
        List<string> lines = new List<string>();
        string currentline = " ";

        foreach (string word in words)
        {
            if ((currentline + word).Length + 1 <= m)
            {
                currentline += word + " ";
            }
            else
            {
                lines.Add(currentline.Trim());
                currentline = word + " ";
            }
        }

        lines.Add(currentline.Trim());

        foreach (string line in lines)
        {
            string[] linewords = line.Split(' ');
            int wordslength = linewords.Sum(word => word.Length);
            int numSpaces = linewords.Length - 1;

            if (numSpaces != 0)
            {
                int numberspaces = m - wordslength;
                int basespace = numberspaces / numSpaces;
                int extraSpaces = numberspaces % numSpaces;

                string formattedLine = "";
                for (int i = 0; i < linewords.Length - 1; i++)
                {
                    formattedLine += linewords[i] + new string(' ', basespace);
                    if (extraSpaces > 0)
                    {
                        formattedLine += " ";
                        extraSpaces--;
                    }
                }
                formattedLine += linewords[linewords.Length - 1];
                Console.WriteLine(formattedLine);
            }
            //    else
            //    {
            //        Console.WriteLine("Error!");
            //    }
            //}
        }
    }



    public override string ToString()
    {
        return text;
    }

}




public class Task_9 : Task
{
    private Dictionary<string, string> symbols;
    public string newtext;

    public Task_9(string text) : base(text)
    {
        symbols = new Dictionary<string, string>();
    }

    public override string ToString()
    {
        return newtext;
    }

    public void ParseText(string text)
    {
        var pairs = new Dictionary<string, int>();
        for (int i = 0; i < text.Length - 1; i++)
        {
            if (char.IsLetter(text[i]) && char.IsLetter(text[i + 1]))
            {
                var sequence = text.Substring(i, 2);
                if (!pairs.ContainsKey(sequence))
                {
                    pairs[sequence] = 0;
                }
                pairs[sequence]++;
            }
        }

        var topSequences = pairs.OrderByDescending(x => x.Value).Take(5).ToList();
        char code = '[';
        foreach (var sequence in topSequences)
        {
            symbols[sequence.Key] = code.ToString();
            text = text.Replace(sequence.Key, code.ToString());
            code++;
        }
        newtext = text;
    }

    public void DisplayCodesTable()
    {
        Console.WriteLine("Таблица кодов:");
        foreach (var codePair in symbols)
        {
            Console.WriteLine($"Пара букв: {codePair.Key} -> Знак шифра: {codePair.Value}");
        }
    }
}

class Task_10 : Task
{
    private Dictionary<string, string> symbols;
    private string firsttext;

    public Task_10(string text) : base(text)
    {
        symbols = new Dictionary<string, string>();
        firsttext = text;
    }


    public override string ToString()
    {
        return firsttext;
    }

    public void ParseText(string text)
    {
        foreach (var codePair in symbols)
        {
            firsttext = firsttext.Replace(codePair.Value, codePair.Key);
        }
    }
}

class Program
{
    static void Main()
    {
        string text = "Первое кругосветное путешествие было осуществлено флотом, возглавляемым португальским исследователем Фернаном Магелланом. Путешествие началось 20 сентября 1519 года, когда флот, состоящий из пяти кораблей и примерно 270 человек, отправился из порту Сан-Лукас в Испании. Хотя Магеллан не закончил свое путешествие из-за гибели в битве на Филиппинах в 1521 году, его экспедиция стала первой, которая успешно обогнула Землю и доказала ее круглую форму. Это путешествие открыло новые морские пути и имело огромное значение для картографии и географических открытий. ";
        //string text = "Привет всем.";
        Task_2 task2 = new Task_2(text);
        string rr = task2.Encrypt(text);
        Console.WriteLine("Зашифрованное сообщение: " + task2.Encrypt(text));
        Console.WriteLine();
        Console.WriteLine("Расшифрованное сообщение: " + task2.Encrypt(rr));
        Console.WriteLine();

        Task_4 task4 = new Task_4(text);
        Console.WriteLine("Cложность предложения: " + task4);
        Console.WriteLine();

        Task_6 task6 = new Task_6(text);
        task6.Syllables();
        Console.WriteLine();

        Task_8 task8 = new Task_8(text);
        Console.WriteLine("Разделенный текст:");
        task8.SplitText(50);
        Console.WriteLine();


        Task_9 task9 = new Task_9(text);
        task9.ParseText(text);
        Console.WriteLine("Зашифрованное сообщение:");
        Console.WriteLine(task9.ToString());
        Console.WriteLine();
        task9.DisplayCodesTable();
        Console.WriteLine();

        Task_10 task10 = new Task_10(text);
        task10.ParseText(task9.ToString());
        Console.WriteLine("Расшифрованное сообщение:");
        Console.WriteLine(task10.ToString());

        Console.ReadKey();
    }
}









