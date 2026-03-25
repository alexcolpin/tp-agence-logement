namespace tp_agence_logement;

using System;

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

class Program
{
    
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
