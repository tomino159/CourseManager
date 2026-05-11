using System.Collections.Generic;

List<Kurz> kurzy = new List<Kurz>();
bool online = true;

while (online)
{
    Console.WriteLine("===== MENU =====");
    Console.WriteLine("1 - Pridat kurz");
    Console.WriteLine("2 - Pridat studenta do kurzu");
    Console.WriteLine("3 - Vypisat vsetky kurzy");
    Console.WriteLine("4 - Ukoncit program");
    Console.Write("Zadaj volbu:");

    string vstup = Console.ReadLine();
    int volba = Convert.ToInt32(vstup);

    if (volba == 1)
    {
        Console.WriteLine("Zadaj nazov kurzu:");
        string nazov = Console.ReadLine();

        Console.WriteLine("Zadaj maximalnu kapacitu kurzu:");
        int maxKapacita = Convert.ToInt32(Console.ReadLine());

        var kurz = new Kurz(nazov, maxKapacita);
        kurzy.Add(kurz);
    }
    else if (volba == 2)
    {
        if(kurzy.Count == 0)
        {
            Console.WriteLine("Ziadne kurzy nie su vytvorene.");
        }
        else
        {
            Console.WriteLine("Vyber kurz podla cisla:");
            for(int i = 0;  i < kurzy.Count; i++)
            {
                Console.WriteLine($"{i} - {kurzy[i].Nazov}");
            }
            Console.WriteLine("Vyber cislo kurzu:");
            int indexKurzu = Convert.ToInt32(Console.ReadLine());

            if(indexKurzu < 0 || indexKurzu >= kurzy.Count)
            {
                Console.WriteLine("Neplatny index kurzu.");
            }
            else
            {
                Console.WriteLine("Zadaj meno studenta:");
                string menoStudenta = Console.ReadLine();

                Console.WriteLine("Zadaj vek studenta.");
                int vekStudenta = Convert.ToInt32(Console.ReadLine());

                Student novy = new Student(menoStudenta, vekStudenta);
                bool pridany = kurzy[indexKurzu].PridajStudenta(novy);

                if (pridany)
                {
                    Console.WriteLine("Student bol pridany do kurzu.");
                }
                else
                {
                    Console.WriteLine("Kurz je plny. Studenta nie je mozne pridat.");
                }
            }
        }
    }
    else if (volba == 3)
    {
        if(kurzy.Count == 0)
        {
            Console.WriteLine("Ziadne kurzy nie su vytvorene.");
        }
        else
        {
            foreach(var kurz in kurzy)
            {
                kurz.VypisInfo();
                Console.WriteLine($"");
            }
        }
    }
    else if (volba == 4)
    {
        online = false;
    }
    else
    {
        Console.WriteLine("Neplatna volba");
    }
    Console.WriteLine();
}


class Student
{
    public string Meno {  get; set; }
    public int Vek { get; set; }
    public Student(string meno, int vek) 
    {
        Meno = meno;
        Vek = vek;
    }
    public void VypisInfo()
    {
        Console.WriteLine($"Student: Meno = {Meno}, Vek = {Vek}");
    }
}

class Kurz
{
    public string Nazov { get; set; }
    public int MaxKapacita { get; set; }
    public List<Student> Studenti { get; set; }
    public Kurz(string nazov, int maxKapacita)
    {
        Nazov = nazov;
        MaxKapacita = maxKapacita;
        Studenti = new List<Student>();
    }
    public bool PridajStudenta(Student student)
    {
        if(Studenti.Count < MaxKapacita)
        {
            Studenti.Add(student);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void VypisInfo()
    {
        Console.WriteLine($"Nazov kurzu: {Nazov}, Kapacita kurzu: {MaxKapacita}, Pocet studentov: {Studenti.Count}");
        foreach(var stud in Studenti)
        {
            stud.VypisInfo();
        }
    }
}