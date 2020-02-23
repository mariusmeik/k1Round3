//Marinio programa
using System;
using System.Collections.Generic;
using System.IO;


namespace K1Rekursija
{
    class Program
    {
        static void Main(string[] args)
        {
            File.Delete("ats.txt");
            Faculty naujas=InOutUtils.ReadFaculty("data.txt");
            Faculty naujas2 = InOutUtils.ReadFaculty("data1.txt");
            InOutUtils.PrintFaculty(naujas, @"ats.txt", "\npradiniai duomenys (pirmo fak.)");
            InOutUtils.PrintFaculty(naujas2, @"ats.txt", "\npradiniai duomenys (antro fak.)");
            Faculty atrinkti = TaskUtils.Select(naujas);
            Faculty atrinkti2 = TaskUtils.Select(naujas2);
            atrinkti.Sort();
            atrinkti2.Sort();
            InOutUtils.PrintFaculty(atrinkti, @"ats.txt", "\natrinkti ir surusiuoti (pirmo fak.)");
            InOutUtils.PrintFaculty(atrinkti2, @"ats.txt", "\natrinkti ir surusiuoti (antro fak.)");
            if(atrinkti<atrinkti2)
            {
                InOutUtils.PrintMessage( @"ats.txt", "\nantrame fakultete virsijanciu daugiau");
            }
            else if (atrinkti > atrinkti2)
            {
                InOutUtils.PrintMessage(@"ats.txt", "\npirmame fakultete virsijanciu daugiau");
            }
            else
            {
                InOutUtils.PrintMessage(@"ats.txt", "\nabiejuose fakultetuose vienodai");
            }

        }
    }
    class Student
    {
        string Pavarde { get; set; }
        string Vardas { get; set; }
        string Grupe { get; set; }
        List<int> Valandos { get; set; }
        int Suma = 0;
        public Student(string pavarde,string vardas,string grupe,List<int> valandos)
        {
            this.Pavarde = pavarde;
            this.Vardas = vardas;
            this.Grupe = grupe;
            this.Valandos = valandos;

        }
        public string GetString()
        {
            string data=String.Format("|{0,-20}|{1,-20}|{2,-20}", Pavarde, Vardas, Grupe);
            foreach (int val in Valandos)
            {
                data +=" |"+ val;
            }
            return data;
        }
        public static bool operator <(Student stud1, Student stud2)
        {
            if (stud1.Grupe.CompareTo(stud2.Grupe) < 0|| stud1.Grupe.CompareTo(stud2.Grupe)==0&&stud1.Pavarde.CompareTo(stud2.Pavarde)<0)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public static bool operator ==(Student stud1, Student stud2)
        {
            if (stud1.Grupe.CompareTo(stud2.Grupe) == 0&&stud1.Pavarde==stud2.Pavarde)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Suma;
        }
        public static bool operator !=(Student stud1, Student stud2)
        {
            if (stud1.Grupe.CompareTo(stud2.Grupe) != 0 && stud1.Pavarde != stud2.Pavarde)
            {
                return true;
            }
            return false;
        }
        public static bool operator>(Student stud1, Student stud2)
        {
            if (stud1.Grupe.CompareTo(stud2.Grupe) > 0 || stud1.Grupe.CompareTo(stud2.Grupe) == 0 && stud1.Pavarde.CompareTo(stud2.Pavarde) > 0)
            {
                return true;
            }
            return false;
        }
        public int Sum(int ii)
        {
            if(ii==Valandos.Count-1)
            {
                return Valandos[ii];
            }
            return Sum(ii + 1)+Valandos[ii];
        }
        public Student()
        {

        }
    }
    class Faculty
    {
        string fakultetoPavadinimas;
        int kredituSkaicis;
        int moduliuSkaicius;
        public string GetPav()
        {
            return fakultetoPavadinimas;
        }
        public int GetKred()
        {
            return this.kredituSkaicis;
        }
        public int GetmodSK()
        { 
            return this.moduliuSkaicius;
        }
        public string FakInfo()
        {
            return String.Format(fakultetoPavadinimas+" "+kredituSkaicis+" "+moduliuSkaicius );
        }
        public static bool operator <(Faculty fac1,Student stud2)
        {
            return fac1.moduliuSkaicius < stud2.Sum(0);
        }
        public static bool operator >(Faculty fac1, Student stud2)
        {
            return fac1.moduliuSkaicius > stud2.Sum(0);
        }
        public static bool operator ==(Faculty fac1, Student stud2)
        {
            return fac1.kredituSkaicis == stud2.Sum(0);
        }
        public static bool operator !=(Faculty fac1, Student stud2)
        {
            return fac1.kredituSkaicis != stud2.Sum(0);
        }
        public override int GetHashCode()
        {
            return 5;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        Konteineris kont;
        public Konteineris GetKonteiner()
        {
            return kont;
        }
        public static bool operator< (Faculty fac1,Faculty fac2)
        {
            return fac1.GetKonteiner().GetAmmount() < fac2.GetKonteiner().GetAmmount();
        }
        public static bool operator >(Faculty fac1, Faculty fac2)
        {
            return fac1.GetKonteiner().GetAmmount() > fac2.GetKonteiner().GetAmmount();
        }
        public Faculty(string Fakultetas,int kredituSk,int moduliuSk,Konteineris kont)
        {
            this.fakultetoPavadinimas = Fakultetas;
            this.kredituSkaicis = kredituSk;
            this.moduliuSkaicius = moduliuSk;
            this.kont = kont;
        }
        public void Sort()
        {
            for (int i = 0; i < kont.GetAmmount()-1; i++)
            {
                for (int j = i+1; j < kont.GetAmmount(); j++)
                {
                    if(kont.Get(i)<kont.Get(j))
                    {
                        Student temp = kont.Get(i);
                        kont.Replace(i, kont.Get(j));
                        kont.Replace(j, temp);
                    }
                }
            }
        }


    }
    class Konteineris
    {

        Student[] allStudents;
        public Konteineris(List<Student> studentai)
        {
            this.allStudents = new Student[studentai.Count];
            for (int i = 0; i < studentai.Count; i++)
            {
                allStudents[i] = studentai[i];
            }
        }
        public void Add(Student studentas)
        {
            int max = allStudents.Length + 1;
            Student[] newStudents = new Student[max];
            allStudents.CopyTo(newStudents, 0);
            newStudents[max - 1] = studentas;
            allStudents = newStudents;
        }
        public Student Get(int index)
        {
            return allStudents[index];
        }
        public int GetAmmount()
        {
            return allStudents.Length;
        }
        public void Replace(int index,Student value)
        {
            allStudents[index] = value;
        }
    }
    class InOutUtils
    {
        public static Faculty ReadFaculty(string FileName)
        {
            
            StreamReader log = new StreamReader(FileName);
            string line = log.ReadLine();
            string[] val= line.Split(';', StringSplitOptions.RemoveEmptyEntries);

            List<Student> studentai = new List<Student>();
            string eilute;
            while ((eilute=log.ReadLine())!=null)
            {
                List<int> skaiciai = new List<int>();
                string[] values = eilute.Split(';', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 3; i < values.Length; i++)
                {
                    skaiciai.Add(int.Parse(values[i]));
                }
                Student laikinas = new Student(values[0], values[1], values[2], skaiciai);
                studentai.Add(laikinas);
            }
            Konteineris kont = new Konteineris(studentai);
            Faculty fakultetas = new Faculty(val[0],int.Parse(val[1]),int.Parse(val[2]),kont);
            return fakultetas;
        }
        public static void PrintFaculty(Faculty faculty, string fileName, string header)
        {
            StreamWriter log = File.AppendText(fileName);
            log.WriteLine(header);
            log.WriteLine(faculty.FakInfo());
            for (int i = 0; i < faculty.GetKonteiner().GetAmmount(); i++)
            {
                log.WriteLine(faculty.GetKonteiner().Get(i).GetString());
            }
            log.Close();
        }
        public static void PrintMessage( string fileName, string header)
        {
            StreamWriter log = File.AppendText(fileName);
            log.WriteLine(header);
            log.Close();
        }
    }
    class TaskUtils
    {
        public static Faculty Select(Faculty faculty)
        {
            List<Student> empty = new List<Student>();
            Konteineris empt = new Konteineris(empty);
            Faculty pagr = new Faculty(faculty.GetPav(),faculty.GetKred(),faculty.GetmodSK(),empt);
            for (int i = 0; i < faculty.GetKonteiner().GetAmmount(); i++)
            {
                if(faculty>faculty.GetKonteiner().Get(i))
                {
                    pagr.GetKonteiner().Add(faculty.GetKonteiner().Get(i));
                }
            }
            return pagr;
        }
    }
}
