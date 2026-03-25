namespace tp_agence_logement;

using System;
using System.Collections.Generic;

public class Logement
{
    protected string reference;
    protected string adresse;
    protected int surface;
    protected double loyerBase;
    protected bool disponible;

    public Logement(string reference, string adresse, int surface, double loyerBase)
    {
        if (surface <= 0)
        {
            Console.WriteLine("Surface invalide !");
            return;
        }

        if (loyerBase < 0)
        {
            Console.WriteLine("Loyer invalide !");
            return;
        }

        this.reference = reference;
        this.adresse = adresse;
        this.surface = surface;
        this.loyerBase = loyerBase;
        this.disponible = true;
    }

    public virtual void Afficher()
    {
        Console.WriteLine($"Ref: {reference}, Adresse: {adresse}, Surface: {surface}m², Loyer: {loyerBase}€, Disponible: {disponible}");
    }

    public virtual double CalculerLoyer()
    {
        return loyerBase;
    }

    public bool EstDisponible()
    {
        return disponible;
    }

    public void SetIndisponible()
    {
        disponible = false;
    }
}

public class Studio : Logement
{
    private bool meuble;

    public Studio(string reference, string adresse, int surface, double loyerBase, bool meuble)
        : base(reference, adresse, surface, loyerBase)
    {
        this.meuble = meuble;
    }

    public override double CalculerLoyer()
    {
        if (meuble)
            return loyerBase + 50;
        return loyerBase;
    }

    public override void Afficher()
    {
        base.Afficher();
        Console.WriteLine($"Meublé: {meuble}");
    }
}

public class Appartement : Logement
{
    private int nombrePieces;

    public Appartement(string reference, string adresse, int surface, double loyerBase, int nombrePieces)
        : base(reference, adresse, surface, loyerBase)
    {
        if (nombrePieces < 1)
        {
            Console.WriteLine("Nombre de pièces invalide !");
            return;
        }

        this.nombrePieces = nombrePieces;
    }

    public override double CalculerLoyer()
    {
        return loyerBase + (100 * nombrePieces);
    }

    public override void Afficher()
    {
        base.Afficher();
        Console.WriteLine($"Pièces: {nombrePieces}");
    }
}

public class Maison : Logement
{
    private int surfaceJardin;

    public Maison(string reference, string adresse, int surface, double loyerBase, int surfaceJardin)
        : base(reference, adresse, surface, loyerBase)
    {
        if (surfaceJardin < 0)
        {
            Console.WriteLine("Surface jardin invalide !");
            return;
        }

        this.surfaceJardin = surfaceJardin;
    }

    public override double CalculerLoyer()
    {
        return loyerBase + (10 * surfaceJardin);
    }

    public override void Afficher()
    {
        base.Afficher();
        Console.WriteLine($"Jardin: {surfaceJardin}m²");
    }
}

public class Locataire
{
    private int id;
    private string nom;
    private string telephone;

    public Locataire(int id, string nom, string telephone)
    {
        this.id = id;
        this.nom = nom;
        this.telephone = telephone;
    }

    public void Afficher()
    {
        Console.WriteLine($"ID: {id}, Nom: {nom}, Tel: {telephone}");
    }
}

public class ContratLocation
{
    private int numero;
    private Locataire locataire;
    private Logement logement;
    private int nombreJours;
    private double tarifJournalier;

    public ContratLocation(int numero, Locataire locataire, Logement logement, int nombreJours)
    {
        if (nombreJours <= 0)
        {
            Console.WriteLine("Durée invalide !");
            return;
        }

        if (!logement.EstDisponible())
        {
            Console.WriteLine("Logement déjà loué !");
            return;
        }

        this.numero = numero;
        this.locataire = locataire;
        this.logement = logement;
        this.nombreJours = nombreJours;
        this.tarifJournalier = logement.CalculerLoyer();

        logement.SetIndisponible();
    }

    public double CalculerMontantTotal()
    {
        return nombreJours * tarifJournalier;
    }

    public void Afficher()
    {
        Console.WriteLine($"Contrat {numero} - Montant: {CalculerMontantTotal()}€");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Studio s = new Studio("S1", "Paris", 20, 500, true);
        Appartement a = new Appartement("A1", "Lyon", 60, 800, 3);
        Maison m = new Maison("M1", "Lille", 100, 1200, 50);

        Console.WriteLine("=== AFFICHAGE ===");
        s.Afficher();
        a.Afficher();
        m.Afficher();

        Console.WriteLine("\n=== LOYERS ===");
        Console.WriteLine(s.CalculerLoyer());
        Console.WriteLine(a.CalculerLoyer());
        Console.WriteLine(m.CalculerLoyer());

        // Polymorphisme
        List<Logement> logements = new List<Logement>();
        logements.Add(s);
        logements.Add(a);
        logements.Add(m);

        Console.WriteLine("\n=== POLYMORPHISME ===");
        foreach (Logement log in logements)
        {
            log.Afficher();
            Console.WriteLine("Loyer: " + log.CalculerLoyer());
        }

        // Locataire
        Locataire l1 = new Locataire(1, "Alice", "123");
        l1.Afficher();

        // Contrat
        ContratLocation c1 = new ContratLocation(101, l1, s, 5);
        c1.Afficher();
    }
}
