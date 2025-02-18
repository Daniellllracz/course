using course;

class Program
{
    static void Main()
    {
        string inputFilePath = "../../../src/course.txt"; 
        List<FileRead> hallgatok = new List<FileRead>();
        Console.WriteLine("1.feladat");
        try
        {

            foreach (var sor in File.ReadAllLines(inputFilePath))
            {
                hallgatok.Add(new FileRead(sor));
            }

            Console.WriteLine($"A fájl {hallgatok.Count} hallgató adatait tartalmazza.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt a fájl beolvasása közben: {ex.Message}");
        }
        Console.WriteLine("2.feladat");

        var f2 = hallgatok
            .Average(x => x.Eredmeny[3]);
        Console.WriteLine($"a hallgatók {f2} átlaga backend programozó");

        Console.WriteLine("3.feladat");

        var f3 = hallgatok
            .MaxBy(x => x.Eredmeny.Sum());
        Console.WriteLine($"A legjobb hallgató név:{f3.Nev}");

        Console.WriteLine("4. feladat");

        int ferfiakSzama = hallgatok.Count(x => x.Nem == "m");


        double ferfiArany = (double)ferfiakSzama / hallgatok.Count * 100;


        Console.WriteLine($"A férfiak aránya: {ferfiArany:F2}%");

        Console.WriteLine("5. feladat");

        var f5 = hallgatok
                .Where(x => x.Nem == "f") 
                .MaxBy(x => x.Eredmeny[2] + x.Eredmeny[3]); 
        if (f5 != null)
        {
            Console.WriteLine($"A legjobb női webfejlesztő: {f5.Nev}");
        }
        else
        {
            Console.WriteLine("Nincs női hallgató az adatok között.");
        }

        Console.WriteLine("6. feladat");

        var befizetettHallgatok = hallgatok.Where(x => x.Befizetes == 2600).ToList();

        if (befizetettHallgatok.Any())
        {
            Console.WriteLine("A tanfolyamot előfinanszírozó hallgatók:");
            foreach (var h in befizetettHallgatok) 
            {
                Console.WriteLine(h.Nev);
            }
        }
        else
        {
            Console.WriteLine("Nincs olyan hallgató, aki előre kifizette volna a tanfolyamot.");
        }

        Console.WriteLine("7. feladat");
        Console.Write("Adja meg egy hallgató nevét: ");
        string keresettNev = Console.ReadLine();
        var hallgato = hallgatok.FirstOrDefault(x => x.Nev.Equals(keresettNev, StringComparison.OrdinalIgnoreCase));

        if (hallgato != null)
        {
            List<string> javitoVizsga = new List<string>();
            string[] tantargyak = { "Alapok", "Mobil", "Frontend", "Backend" };

            for (int i = 0; i < hallgato.Eredmeny.Length; i++)
            {
                if (hallgato.Eredmeny[i] < 51)
                {
                    javitoVizsga.Add(tantargyak[i]);
                }
            }

            if (javitoVizsga.Any())
            {
                Console.WriteLine($"{hallgato.Nev} az alábbi tantárgyakból kell javítóvizsgát tennie:");
                Console.WriteLine(string.Join(", ", javitoVizsga));
            }
            else
            {
                Console.WriteLine($"{hallgato.Nev} nem kell javítóvizsgát tennie.");
            }
        }
        else
        {
            Console.WriteLine("Nincs ilyen nevű hallgató az adatbázisban.");
        }
        Console.WriteLine("8. feladat");

        int kitunoHallgatokSzama = hallgatok.Count(h =>
            h.Eredmeny.Any(e => e == 100) &&  
            h.Eredmeny.All(e => e >= 51)      
        );

        Console.WriteLine($"Azon hallgatók száma, akik legalább egy modulból 100%-ot teljesítettek, és egyik modulból sem kell javítóvizsgát tenniük: {kitunoHallgatokSzama}");
        Console.WriteLine("9. feladat");


        string[] modulNevek = { "Alapok", "Mobil", "Frontend", "Backend" };


        int[] javitoVizsgak = new int[modulNevek.Length];

        foreach (var student in hallgatok) 
        {
            for (int i = 0; i < student.Eredmeny.Length; i++)
            {
                if (student.Eredmeny[i] < 51)
                {
                    javitoVizsgak[i]++;
                }
            }
        }


        for (int i = 0; i < modulNevek.Length; i++)
        {
            Console.WriteLine($"{modulNevek[i]}: {javitoVizsgak[i]} hallgatónak kell javítóvizsgát tennie.");
        }
        Console.WriteLine("10. feladat");

        string outputFilePath = "../../../src/sorted_students.txt";


        var rendezettHallgatok = hallgatok
            .OrderBy(h => h.Nev.Split(' ')[1]) 
            .Select(h => $"{h.Nev};{h.Eredmeny.Average():F2}") 
            .ToList();

        try
        {
            File.WriteAllLines(outputFilePath, rendezettHallgatok);
            Console.WriteLine($"A rendezett hallgatói lista kiírásra került: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt a fájl írása közben: {ex.Message}");
        }


    }
}

