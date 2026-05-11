using System.Collections.Generic;
using System.IO;

List<Kurz> kurzy = new List<Kurz>();
bool online = true;

while (online)
{
    Console.WriteLine("===== MENU =====");
    Console.WriteLine("1 - Pridat kurz");
    Console.WriteLine("2 - Pridat studenta do kurzu");
    Console.WriteLine("3 - Vypisat vsetky kurzy");
    Console.WriteLine("4 - Ukoncit program");
    Console.WriteLine("5 - Ulozit kurzy do suboru");
    Console.WriteLine("6 - Nacitat kurzy zo suboru");
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
    else if (volba == 5)
    {
        if(kurzy.Count == 0)
        {
            Console.WriteLine("Nie su ziadne kurzy na ulozenie.");
        }
        else
        {
            string cesta = "kurzy.txt";
            try
            {
                string data = "";
                foreach (var kurz in kurzy)
                {
                    data += $"KURZ;{kurz.Nazov};{kurz.MaxKapacita}\n";

                    foreach (var student in kurz.Studenti)
                    {
                        data += $"STUDENT;{student.Meno};{student.Vek}\n";
                    }
                }
                File.WriteAllText(cesta, data);
                Console.WriteLine("Kurzy boli uspesne ulozene do suboru.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba pri zapise do suboru: {ex.Message}");
            }
        }
    }
    else if(volba == 6)
    {
        string cesta = "kurzy.txt";

        if (!File.Exists(cesta))
        {
            Console.WriteLine("Subor s kurzami neexistuje.");
        } else
        {
            try
            {
                kurzy.Clear();

                string[] riadky = File.ReadAllLines(cesta);
                Kurz aktualnyKurz = null;
                foreach (string riadok in riadky)
                {
                    if (string.IsNullOrWhiteSpace(riadok))
                        continue;

                    string[] casti = riadok.Split(';');

                    if (casti[0] == "KURZ")
                    {
                        string nazov = casti[1];
                        int maxKapacita = Convert.ToInt32(casti[2]);
                        aktualnyKurz = new Kurz(nazov, maxKapacita);
                        kurzy.Add(aktualnyKurz);
                    }
                    else if (casti[0] == "STUDENT" && aktualnyKurz != null)
                    {
                        string meno = casti[1];
                        int vek = Convert.ToInt32(casti[2]);

                        Student s = new Student(meno, vek);
                        aktualnyKurz.PridajStudenta(s);
                    }
                }
                Console.WriteLine("Kurzy boli uspesne nacitane zo suboru.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba pri citani zo suboru: {ex.Message}");
            }
        }
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
