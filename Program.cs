
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!Char.IsLetter(words[i][words[i].Length - 1])) //берёт слово и проверяет последний символ слова и если это не буква
            {

                char[] word = words[i].ToCharArray(0, words[i].Length - 1); //переводит в массив символов с первого символа до последнего, но не берёт последний символ
                string rest = words[i].Substring(words[i].Length - 1); //сохраняет последний символ 
                Array.Reverse(word); //переворачивает массив элементов 
                words[i] = new string(word) + rest; //перезаписывает слово с сохранённым символом
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
        int countwords = text.Split(' ', '.', ',', '!', '?').Length;
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
    public struct SyllableCount
    {
        public int _syllables;
        public int wordcount;
    }

    public void Syllables()
    {
        text = text.ToLower();
        string[] words = text.Split(' ', '.', ',', '!', '?');
        List<SyllableCount> Count = new List<SyllableCount>();

        foreach (string word in words)
        {
            if (word.Length > 0)
            {
                int syllables = CountSyllables(word);
                var syllablecount = Count.FirstOrDefault(x => x._syllables == syllables);
                bool ishere = false;
                int k = 0;
                for(int i = 0; i < Count.Count; i++)
                {
                    if (Count[i]._syllables == syllablecount._syllables)
                    { ishere = true; k = i; }
                }
                if (!ishere)
                {
                    Count.Add(new SyllableCount { _syllables = syllables, wordcount = 1 });
                }
                else
                {
                    syllablecount.wordcount++;
                    Count[k] = syllablecount;
                }
            }
        }

        foreach (var pair in Count)
        {
            Console.WriteLine($"Количество слог: {pair._syllables}, количество слов {pair.wordcount}.");
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
        if (count == 0 && word.Length > 0)
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
    public void SplitText(int width)
    {
        string[] words = text.Split(' ');
        List<string> lines = new List<string>(); //лист сохраняет линии
        string currentline = " ";

        foreach (string word in words)
        {
            if ((currentline + word).Length + 1 <= width) //проверяет можно ли добавить слово, 1 - пробел после слова
            {
                currentline += word + " ";
            }
            else //если не можем добавить слово
            {
                lines.Add(currentline.Trim()); //добавляет предложение в лист
                currentline = word + " "; //новая строка
            }
        }

        lines.Add(currentline.Trim()); //чтобы добавить последнюю строку текста

        foreach (string line in lines) //перебираем лист
        {
            string[] linewords = line.Split(' ');
            int wordslength = linewords.Sum(word => word.Length); //суммирует длину всех слов
            int sumSpaces = linewords.Length - 1; //количество пробелов кооторые есть сейчас

            if (sumSpaces != 0)
            {
                int numberspaces = width - wordslength; //необходимые пробелы
                int basespace = numberspaces / sumSpaces;//доп пробелы между словами
                int extraSpaces = numberspaces % sumSpaces; //остаточные доп пробелы

                string formattedLine = "";
                for (int i = 0; i < linewords.Length - 1; i++)//убирает последнее слово, чтобы оно было замыкающим
                {
                    formattedLine += linewords[i] + new string(' ', basespace); //пробелы и их количество
                    if (extraSpaces > 0)
                    {
                        formattedLine += " "; //добавляет в конец строки
                        extraSpaces--;
                    }
                }
                formattedLine += linewords[linewords.Length - 1]; //обратно добавляет последнее слово
                Console.WriteLine(formattedLine);
            }
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
                var sequence = text.Substring(i, 2); //разделяет на пары
                if (!pairs.ContainsKey(sequence))//если в словаре нет последовательности, то добавляет новую пару
                {
                    pairs[sequence] = 0;
                }
                pairs[sequence]++;
            }
        }

        var topSequences = pairs.OrderByDescending(x => x.Value).Take(5).ToList(); //сортирует пары по убыванию частоты вхождения и выбирает пять самых частых пар.
        char code = '[';
        foreach (var sequence in topSequences)
        {
            symbols[sequence.Key] = code.ToString(); //меняет буквы на код
            text = text.Replace(sequence.Key, code.ToString()); //меняет текст
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

    public void ParseText(string firsttext)
    {
        foreach (var codePair in symbols)
        {
            firsttext = firsttext.Replace(codePair.Value, codePair.Key);//заменяет знак на соответствующую пару букв
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
        string rr = task2.Encrypt(text); //сохраняем перевёрнутый текст, чтобы опять перевернуть 
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



