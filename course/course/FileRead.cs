using System;
using System.Linq;

namespace course;

internal class FileRead
{
    public string Nev { get; set; }
    public string Nem { get; set; }
    public int Befizetes { get; set; }
    public int[] Eredmeny { get; set; }

    public FileRead(string sor)
    {
        var v = sor.Split(';'); 

        Nev = v[0]; 
        Nem = v[1]; 
        Befizetes = int.Parse(v[2]); 

        Eredmeny = v.Skip(3).Select(int.Parse).ToArray();
    }

    public override string ToString()
    {
        return $"{Nev}, {Nem}, {Befizetes}, [{string.Join(", ", Eredmeny)}]";
    }
}
