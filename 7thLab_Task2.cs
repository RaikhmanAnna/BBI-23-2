using System;
using System.IO;
using System.Text.Json.Serialization;
using _7thLab_Task2.Serializers;

namespace _7thLab_Task2
{
    public class Program
    {
        public class Person
        {
            private string _name;
            protected int[] _grades;
            protected static int index = 0;

            public string Name { get { return _name; } set { _name = value; } }
            public int[] Grades { get { return _grades; } set { _grades = value; } }
            public static int Index { get { return index; } }

            public Person() 
            {

            }

            [JsonConstructor]
            public Person(string name, int[] grades)
            {
                _name = name;
                _grades = grades;
                index++;
            }

            public float AverageGrade()
            {
                float res = 0;
                for (int i = 0; i < _grades.Length; i++)
                {
                    if (_grades[i] == 2)
                    {
                        return 0;
                    }
                    res += _grades[i];
                }
                res = res / _grades.Length;
                return res;
            }


            public virtual void Print()
            {
                if (AverageGrade() != 0)
                {
                    Console.WriteLine($"{Name} {AverageGrade()}");
                }
                else
                {
                    Console.WriteLine($"{Name} отчислен");
                }
            }
        }

        public class Student : Person
        {
            private int _id;
            public int ID { get { return _id; }
                set { _id = value; }
                    }

            public Student() : base()
            {
                
            }
            public Student(string name, int[] grades) : base(name, grades)
            {
                _id = index;

            }
            public override void Print()
            {
                if (AverageGrade() != 0)
                {
                    Console.WriteLine($"{Name,-20} {AverageGrade(),-10:F1} {ID,-10}");
                }
                else
                {
                    Console.WriteLine($"{Name,-20}отчислен    {ID,-10}");
                }
            }
        }

        static void Main()
        {
            Student[] students = new Student[5];
            students[0] = new Student("Sergey Ivanov", [3, 4, 5]);
            students[1] = new Student("Jack Sparrow", [3, 3, 3]);
            students[2] = new Student("Valeriy Karpin", [4, 3, 4]);
            students[3] = new Student("Rick Sanchez", [4, 4, 5]);
            students[4] = new Student("Morty", [3, 2, 5]);

            GnomeSort(students);

            MySerializers[] serializers = new MySerializers[2] { new MyJsonSerializer(), new MyXmlSerializer() };
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "9Lab2");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string[] files = new string[2]
            {
                "first.json",
                "second.xml"
            };
            for (int i = 0; i < serializers.Length; i++)
            {
                serializers[i].Write<Student[]>(students, Path.Combine(path, files[i]));
            }

            for (int i = 0; i < serializers.Length; i++)
            {
                students = serializers[i].Read<Student[]>(Path.Combine(path, files[i]));
                Console.WriteLine("Name           Average Grade    ID");
                Console.WriteLine("------------------------------------");
                foreach (Student student in students)
                {
                    student.Print();
                }
            }
            Console.ReadLine();


        }
        static void GnomeSort(Student[] students)
        {
            int i = 1;
            int j = i + 1;
            while (i < students.Length)
            {
                if (i == 0 || students[i].AverageGrade() <= students[i - 1].AverageGrade())
                {
                    i = j;
                    j++;
                }
                else
                {
                    Student temp = students[i]; ;
                    students[i] = students[i - 1];
                    students[i - 1] = temp;
                    i--;
                }
            }
        }
    }
}