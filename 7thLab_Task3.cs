
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _7thLab_Task3.Serializer;

namespace _7thLab_Task3
{
    public class Person
    {

        private string _name;
        private int _pos;

        public string Name { get { return _name; } set { _name = value; } }
        public int Pos { get { return _pos; } set { _pos = value; } }

        public Person() 
        {

        }
        public Person(string name, int pos)
        {
            _name = name;
            _pos = pos;
        }

    }

    public class Program
    {
        public class Team
        {

            private string _name;
            private Person[] _persons;

            public string Name { get { return _name; } set { _name = value; } }
            public Person[] Persons { get { return _persons; } set { _persons = value; } }

            public Team()
            {

            }
            public Team(string name, Person[] persons)
            {
                _name = name;
                _persons = persons;
            }

            protected int TeamPoints()
            {
                int points = 0;
                foreach (Person person in Persons)
                {
                    if (person.Pos >= 1 && person.Pos <= 5) points += 5 - (person.Pos - 1);
                }
                return points;
            }
            public static void Sort(Team[] teams)
            {
                int i = 1;
                int j = i + 1;
                while (i < teams.Length)
                {
                    if (i == 0 || teams[i].TeamPoints() <= teams[i - 1].TeamPoints())
                    {
                        i = j;
                        i++;
                    }
                    else if (teams[i].TeamPoints() > teams[i - 1].TeamPoints())
                    {
                        Team temp = teams[i - 1];
                        teams[i - 1] = teams[i];
                        teams[i] = temp;
                    }
                    else if (teams[i].TeamPoints() == teams[i - 1].TeamPoints())
                    {
                        if (Favorite(teams[i]))
                        {
                            Team temp = teams[i];
                            teams[i] = teams[i - 1];
                            teams[i - 1] = temp;
                        }
                        else { continue; }
                    }
                }
            }
            static bool Favorite(Team team)
            {
                foreach (Person person in team.Persons)
                {
                    if (person.Pos == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            public virtual void Print() => Console.WriteLine($"Team: {Name} Points: {TeamPoints()}");
        }

        public class MaleTeam : Team
        {
            private string _name;
            private Person[] _persons;

            //public string Name { get { return _name; } set { _name = value; } }
            //public Person[] Persons { get { return _persons; } set { _persons = value; } }

            public MaleTeam()
            {

            }
            public MaleTeam(string name, Person[] persons) : base(name, persons)
            {
                _name = name;
                _persons = persons;
            }
            public override void Print()
            {
                Console.WriteLine($"Мale Team: {Name} Points: {TeamPoints()}");
            }

        }
        public class FemaleTeam : Team
        {
            private string _name;
            private Person[] _persons;

            //public string Name { get { return _name; } set { _name = value; } }

            public FemaleTeam()
            {

            }
            public FemaleTeam(string name, Person[] persons) : base(name, persons)
            {
                _name = name;
                _persons = persons;
            }
            public override void Print()
            {
                Console.WriteLine($"Female Team: {Name} Points: {TeamPoints()}");
            }

        }

        static void Main()
        {

            Person[] teamBlack = new Person[6];

            teamBlack[0] = new Person("Ivanov", 3);
            teamBlack[1] = new Person("Petrov", 6);
            teamBlack[2] = new Person("Sidorov", 8);
            teamBlack[3] = new Person("Smirnov", 14);
            teamBlack[4] = new Person("Sparrow", 16);
            teamBlack[5] = new Person("Sanchez", 15);

            Person[] teamWhite = new Person[6];

            teamWhite[0] = new Person("Rai", 1);
            teamWhite[1] = new Person("Dai", 7);
            teamWhite[2] = new Person("Nai", 5);
            teamWhite[3] = new Person("Vai", 10);
            teamWhite[4] = new Person("Tay", 11);
            teamWhite[5] = new Person("May", 12);

            Person[] teamBlue = new Person[6];

            teamBlue[0] = new Person("Ret", 2);
            teamBlue[1] = new Person("Ter", 4);
            teamBlue[2] = new Person("Mer", 9);
            teamBlue[3] = new Person("Ser", 18);
            teamBlue[4] = new Person("Ley", 17);
            teamBlue[5] = new Person("Pop", 13);

            MaleTeam[] teams = new MaleTeam[3];
            teams[0] = new MaleTeam("Black Team", teamBlack);
            teams[1] = new MaleTeam("White Team", teamWhite);
            teams[2] = new MaleTeam("Blue Team", teamBlue);

            Team.Sort(teams);

            MySerializers[] serializers = new MySerializers[2] { new MyJsonSerializer(), new MyXmlSerializer() };
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "9Lab3");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string[] files = new string[2]
            {
                "first.json",
                "second.xml"
            };
            for (int i = 0; i < serializers.Length; i++)
            {
                serializers[i].Write<MaleTeam[]>(teams, Path.Combine(path, files[i]));
            }

            for (int i = 0; i < serializers.Length; i++)
            {
                teams = serializers[i].Read<MaleTeam[]>(Path.Combine(path, files[i]));
                Console.WriteLine("Name          Result1   Result2   BestResult");
                Console.WriteLine("----------------------------------------------");
                teams[0].Print();
            }
            Console.ReadLine();

        }
    }
}
   




