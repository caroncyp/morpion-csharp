using System;

class Morpion
{
    static char[,] plateau = new char[3, 3];
    static char joueurActuel = 'X';

    static void Main()
    {
        bool rejouer = true;
        while (rejouer)
        {
            InitialiserPlateau();

            bool jeuTermine = false;
            while (!jeuTermine)
            {
                AfficherPlateau();
                SaisirCoup();
                jeuTermine = VerifierVictoire() || VerifierMatchNul();

                if (!jeuTermine)
                    ChangerJoueur();
            }

            Console.Write("Voulez-vous rejouer ? (o/n) : ");
            rejouer = Console.ReadLine().ToLower() == "o";
        }
    }

    static void InitialiserPlateau()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                plateau[i, j] = ' ';
        joueurActuel = 'X';
    }

    static void AfficherPlateau()
    {
        Console.Clear();
        Console.WriteLine("   1   2   3 ");
        for (int i = 0; i < 3; i++)
        {
            Console.Write((i + 1) + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(" " + plateau[i, j] + " ");
                if (j < 2) Console.Write("|");
            }
            if (i < 2) Console.WriteLine("\n  -----------");
            else Console.WriteLine();
        }
        Console.WriteLine($"\nTour du joueur {joueurActuel}");
    }

    static void SaisirCoup()
    {
        int ligne, colonne;
        bool caseValide = false;

        do
        {
            Console.Write("Choisissez votre case (ligne,colonne) : ");
            string saisie = Console.ReadLine();
            string[] coord = saisie.Split(',');

            if (coord.Length == 2 &&
                int.TryParse(coord[0], out ligne) &&
                int.TryParse(coord[1], out colonne) &&
                ligne >= 1 && ligne <= 3 &&
                colonne >= 1 && colonne <= 3)
            {
                if (plateau[ligne - 1, colonne - 1] == ' ')
                {
                    plateau[ligne - 1, colonne - 1] = joueurActuel;
                    caseValide = true;
                }
                else
                {
                    Console.WriteLine("❌ Case déjà occupée. Essayez encore.");
                }
            }
            else
            {
                Console.WriteLine("❌ Entrée invalide. Format attendu : 2,3");
            }

        } while (!caseValide);
    }

    static void ChangerJoueur()
    {
        joueurActuel = joueurActuel == 'X' ? 'O' : 'X';
    }

    static bool VerifierVictoire()
    {
        char j = joueurActuel;

        // Lignes et colonnes
        for (int i = 0; i < 3; i++)
        {
            if ((plateau[i, 0] == j && plateau[i, 1] == j && plateau[i, 2] == j) ||
                (plateau[0, i] == j && plateau[1, i] == j && plateau[2, i] == j))
            {
                AfficherPlateau();
                Console.WriteLine($"\n🎉 Le joueur {j} a gagné !");
                return true;
            }
        }

        // Diagonales
        if ((plateau[0, 0] == j && plateau[1, 1] == j && plateau[2, 2] == j) ||
            (plateau[0, 2] == j && plateau[1, 1] == j && plateau[2, 0] == j))
        {
            AfficherPlateau();
            Console.WriteLine($"\n🎉 Le joueur {j} a gagné !");
            return true;
        }

        return false;
    }

    static bool VerifierMatchNul()
    {
        foreach (char c in plateau)
        {
            if (c == ' ') return false;
        }

        AfficherPlateau();
        Console.WriteLine("\n🤝 Match nul !");
        return true;
    }
}
