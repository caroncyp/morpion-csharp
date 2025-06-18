using System;

class Morpion
{
    // Déclaration d'un tableau 2D pour représenter le plateau 3x3
    static char[,] plateau = new char[3, 3];

    // Variable pour suivre quel joueur joue actuellement (X ou O)
    static char joueurActuel = 'X';

    static void Main()
    {
        bool rejouer = true;

        // Boucle principale du jeu (permet de rejouer plusieurs parties)
        while (rejouer)
        {
            InitialiserPlateau(); // Réinitialise le plateau à vide

            bool jeuTermine = false;

            // Boucle de jeu jusqu’à ce que quelqu’un gagne ou que ce soit un match nul
            while (!jeuTermine)
            {
                AfficherPlateau(); // Affiche le plateau à chaque tour
                SaisirCoup();      // Demande au joueur de faire un mouvement

                // Vérifie si le joueur a gagné ou s’il y a un match nul
                jeuTermine = VerifierVictoire() || VerifierMatchNul();

                if (!jeuTermine)
                    ChangerJoueur(); // Alterne entre les joueurs X et O
            }

            // Propose de rejouer une partie
            Console.Write("Voulez-vous rejouer ? (o/n) : ");
            rejouer = Console.ReadLine().ToLower() == "o";
        }
    }

    // Initialise le plateau avec des cases vides
    static void InitialiserPlateau()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                plateau[i, j] = ' '; // Remplit chaque case avec un espace vide
        joueurActuel = 'X'; // Le joueur X commence toujours
    }

    // Affiche le plateau 3x3 dans la console avec les coordonnées
    static void AfficherPlateau()
    {
        Console.Clear(); // Efface la console à chaque tour
        Console.WriteLine("   1   2   3 ");
        for (int i = 0; i < 3; i++)
        {
            Console.Write((i + 1) + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(" " + plateau[i, j] + " ");
                if (j < 2) Console.Write("|"); // Séparateur vertical entre les colonnes
            }
            if (i < 2) Console.WriteLine("\n  -----------"); // Séparateur horizontal entre les lignes
            else Console.WriteLine();
        }

        // Affiche de quel joueur c’est le tour
        Console.WriteLine($"\nTour du joueur {joueurActuel}");
    }

    // Permet au joueur de saisir les coordonnées de son coup
    static void SaisirCoup()
    {
        int ligne, colonne;
        bool caseValide = false;

        do
        {
            Console.Write("Choisissez votre case (ligne,colonne) : ");
            string saisie = Console.ReadLine();
            string[] coord = saisie.Split(',');

            // Vérifie que la saisie est bien composée de deux nombres séparés par une virgule
            if (coord.Length == 2 &&
                int.TryParse(coord[0], out ligne) &&
                int.TryParse(coord[1], out colonne) &&
                ligne >= 1 && ligne <= 3 &&
                colonne >= 1 && colonne <= 3)
            {
                // Vérifie que la case choisie est vide
                if (plateau[ligne - 1, colonne - 1] == ' ')
                {
                    plateau[ligne - 1, colonne - 1] = joueurActuel; // Place le symbole du joueur
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

        } while (!caseValide); // Répète tant qu'une case valide n'est pas choisie
    }

    // Change le joueur actif (de X à O ou de O à X)
    static void ChangerJoueur()
    {
        joueurActuel = joueurActuel == 'X' ? 'O' : 'X';
    }

    // Vérifie si le joueur actuel a gagné
    static bool VerifierVictoire()
    {
        char j = joueurActuel;

        // Vérifie les lignes et les colonnes
        for (int i = 0; i < 3; i++)
        {
            if ((plateau[i, 0] == j && plateau[i, 1] == j && plateau[i, 2] == j) || // Ligne
                (plateau[0, i] == j && plateau[1, i] == j && plateau[2, i] == j))   // Colonne
            {
                AfficherPlateau();
                Console.WriteLine($"\n🎉 Le joueur {j} a gagné !");
                return true;
            }
        }

        // Vérifie les 2 diagonales
        if ((plateau[0, 0] == j && plateau[1, 1] == j && plateau[2, 2] == j) ||     // Diagonale \
            (plateau[0, 2] == j && plateau[1, 1] == j && plateau[2, 0] == j))       // Diagonale /
        {
            AfficherPlateau();
            Console.WriteLine($"\n🎉 Le joueur {j} a gagné !");
            return true;
        }

        return false; // Aucune victoire détectée
    }

    // Vérifie si toutes les cases sont remplies sans qu’il y ait de gagnant
    static bool VerifierMatchNul()
    {
        foreach (char c in plateau)
        {
            if (c == ' ') return false; // Si au moins une case est vide, ce n’est pas un match nul
        }

        AfficherPlateau();
        Console.WriteLine("\n🤝 Match nul !");
        return true;
    }
}
