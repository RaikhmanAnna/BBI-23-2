using System;
using System.Net.NetworkInformation;
using System.Xml.Linq;

public struct Profession
{
    private string _sphere;
    private static int _nextUID = 1;
    private int _UID;
    private int _salary;
    private string _description;

    public string Sphere { get { return _sphere; } }
    public string Description
    {
        get { return _description; }
        set
        {
            if (value.Length < 20 && value.Length > 200)
            {
                _description = value;
            }

        }
    }
    public Profession(string sphere, int salary, string description)
    {
        _sphere = sphere;
        _salary = salary;
        _description = description;
        _UID = _nextUID++;
    }

    public void Print()
    {
        Console.WriteLine($"{_sphere,-13} {_UID,-7:F1} {_salary,-10:F1} {_description,-10:F1}");
    }

    public void ChangeDescription(string newDescription)
    {
        if (_sphere == "")
        {
            _description = newDescription;
        }
    }


    public static void GnomeSort(Profession[] professions)
    {
        int i = 1;
        int j = i + 1;
        while (i < professions.Length)
        {
            if (i == 0 || professions[i]._salary <= professions[i - 1]._salary)
            {
                i = j;
                j++;
            }
            else
            {
                var temp = professions[i]; ;
                professions[i] = professions[i - 1];
                professions[i - 1] = temp;
                i--;

            }
        }
    }
}

class Program
{
    static void Main()
    {
        Profession[] professions = new Profession[5]
        {
            new Profession("Медицина", 70000, "Оказание медицинской помощи пациентам."),
            new Profession("Маркетинг", 80000, "Продвижение товаров и услуг на рынке."),
            new Profession("IT", 120000, "Разработка цифровых технологий."),
            new Profession("Финансы", 95000, "Управление финансовыми ресурсами компании."),
            new Profession("Образование", 45000, "Обучение школьников и студентов.")
        };

        foreach (Profession profession in professions)
        {
            if (profession.Sphere == "IT")
            {
                profession.ChangeDescription("Профессия в IT-сфере требует знаний программирования и высоких технических навыков.");
            }
        }

        Profession.GnomeSort(professions);

        Console.WriteLine("Профессия:    УИД:    Зарплата:       Описание:      ");
        foreach (Profession profession in professions)
        {
            profession.Print();
        }
        Console.ReadLine();
    }
}
