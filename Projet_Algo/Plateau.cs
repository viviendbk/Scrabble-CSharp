using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace Projet_Algo
{
    public class Plateau
    {
        #region Attributs

        char[,] plat;

        #endregion

        #region Constructeur
        public Plateau(StreamReader InstancePlateau)
        {
            this.plat = new char[15, 15];
            string line = InstancePlateau.ReadLine();
            int u = 0;
            while (line != null)
            {
                string[] tab = line.Split(";");
                for(int i = 0; i < tab.Length;i++)
                {
                    if(tab[i] == "_")
                    {
                        this.plat[u, i] = ' ';
                    }
                    else
                    {
                        this.plat[u, i] = Convert.ToChar(tab[i]);
                        
                    }
                }
                u ++;
                line = InstancePlateau.ReadLine();
            }
        } 
        #endregion

        #region Méthodes

        public void WhichColor(int l, int c) // méthode qui renvoie la bonne couleur d'une case du plateau
        {
            // si un jeton est posé sur le plateau, la case est de couleur blanche
            if (plat[l, c] != ' ')
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            // remplissage des cases rouges
            else if ((l==0&&c==0)||(l==0&&c==7)|| (l == 0 && c == 14) || //
               (l == 7 && c == 0) || (l == 7 && c == 14) ||
               (l == 14 && c == 0) || (l == 14 && c == 7) || (l == 14 && c == 14))
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            // remplissage des cases bleus
            else if((l == 0 && c == 3) || (l == 0 && c == 11) || (l == 2 && c == 6) || (l == 2 && c == 8) ||
               (l == 3 && c == 0) || (l == 3 && c == 7) || (l == 3 && c == 14) ||
               (l == 6 && c == 2) || (l == 6 && c == 6) || (l == 6 && c == 8) || (l == 6 && c == 12) ||
               (l == 7 && c == 3) || (l == 7 && c == 11) ||
               (l == 8 && c == 2) || (l == 8 && c == 6) || (l == 8 && c == 8) || (l == 8 && c == 12) ||
               (l == 11 && c == 0) || (l == 11 && c == 7) || (l == 11 && c == 14) ||
               (l == 12 && c == 6) || (l == 12 && c == 8) || (l == 14 && c == 3) || (l == 14 && c == 11))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            // remplissages des cases bleu foncé
            else if((l == 1 && c == 5) || (l == 1 && c == 9) ||
               (l == 5 && c == 1) || (l == 5 && c == 5) || (l == 5 && c == 9) || (l == 5 && c == 13) ||
               (l == 9 && c == 1) || (l == 9 && c == 5) || (l == 9 && c == 9) || (l == 9 && c == 13) ||
               (l == 13 && c == 5) || (l == 13 && c == 9))
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            // remplissage des cases roses (la couleur rose n'est pas disponible donc j'ai mis jaune foncé)
            else if((l == 1 && c == 1) || (l == 2 && c == 2) || (l == 3 && c == 3) || (l == 4 && c == 4) ||
               (l == 1 && c == 13) || (l == 2 && c == 12) || (l == 3 && c == 11) || (l == 4 && c == 10) ||
               (l == 7 && c == 7) ||
               (l == 10 && c == 4) || (l == 11 && c == 3) || (l == 12 && c == 2) || (l == 13 && c == 1) ||
               (l == 10 && c == 10) || (l == 11 && c == 11) || (l == 12 && c == 12) || (l == 13 && c == 13))
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            // remplissage des cases vert foncé
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
                

            
        }

        public void toString()
        { 
            for(int i = 0; i < 15; i++)
            {
                for( int j = 0; j < 14; j++)
                {
                    if(j == 0 && i == 0)
                    {
                        Console.Write("    0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 \n0  ");
                    }
                    if(j == 0 && i != 0)
                    {
                        if(i < 10)
                        {
                            Console.Write(i + "  ");
                        }   
                        else
                        {
                            Console.Write(i + " ");
                        }         
                    }
                    
                    WhichColor(i, j);
                    if(plat[i,j] != ' ')
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" " + plat[i, j] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(" " + plat[i, j] + " ");
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                }
                WhichColor(i, 14);
                if(plat[i, 14] != ' ')
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" " + plat[i, 14] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(" " + plat[i, 14] + " ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            
        } // méthode toString

        public string toStringStreamWriter() // méthode qui renvoie le string qui sera écrit dans le fichier de sauvegarde du plateau
        {
            string phrase = null;
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 14; j++)
                {
                    if(plat[i,j] == ' ')
                    {
                        phrase += "_;";
                    }
                    else
                    {
                        phrase += plat[i, j] + ";";
                    }
                }
                phrase += plat[i, 14] + "\n";
            }
            return phrase;
        } 

        public void Enregistrer() // méthode qui enregistre le plateau de la partie
        {
            string fichier = "InstancePlateau.txt";
            Program.créerDossier(fichier);
            StreamWriter txt = new StreamWriter(fichier);
            txt.Write(toStringStreamWriter());
            txt.Close();
        } 
        
        public bool Plateau_Vide() // renvoie true si aucune lettre n'est posée sur le plateau
        {
            bool value = true;
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    if(plat[i,j] != ' ')
                    {
                        value = false;
                    }
                }
            }
            return value;
        }
        
        public bool Test_Plateau(string mot, int ligne, int colonne, char direction, Dictionnaire dico1, Joueur joueur, bool utilisation_joker) // teste si un mot peut être placé sur le plateau à l'endroit donné
        {
            bool eligible = dico1.RechDichoRecursif(mot, 0, dico1.Ensemble_mots.Count);// dico1.RechercheDico(mot); // test si le mot appartient au dictionnaire

            if (((direction == 'b') || (direction == 'd')) && (colonne  <15) && (ligne < 15))
            {
                if (mot == null || mot.Length == 0)
                {
                    Console.WriteLine("le mot n'est pas dans le dictionnaire");
                    eligible = false; // vérifie si le mot n'est pas null
                }
                List<char> charactères_plateau = new List<char>();
                switch (direction)
                {
                    case 'b':
                        if (Plateau_Vide() && (colonne != 7 || ligne + mot.Length - 1 < 7))
                        {
                            Console.WriteLine("[ERREUR] : Une lettre du premier mot doit être placée sur la case centrale du plateau");
                            eligible = false;
                        }
                        // Regarde s'il y a des lettres sur le plateau et vérifie qu'il y a un espace a la fin et au début du mot qu'on veut poser ou que les plateau se fini après ou avant le mot
                        if ((ligne + mot.Length -1 <= 14) && (ligne + mot.Length <= 14 && plat[ligne + mot.Length, colonne] == ' ') && (ligne - 1 != -1 && plat[ligne - 1, colonne] == ' ')) 
                        {
                            for(int i = 0; i < mot.Length; i++)
                            {
                                // Si il y a une lettre sur le plateau qui n'appartient pas au mot ou est mal placée, on arrête
                                if (mot[i] != plat[ligne + i, colonne] && plat[ligne + i, colonne] != ' ')
                                {
                                    Console.WriteLine("[ERREUR] : Les lettres sur le plateau ne permettent pas de poser ce mot à cet endroit");
                                    eligible = false;
                                    break;
                                }
                                else // sinon ajouter le charactère dans une liste
                                {
                                    charactères_plateau.Add(plat[ligne + i, colonne]);
                                }
                                
                            }

                            int Nb_Lettre = 0;
                            for (int i = 0; i < charactères_plateau.Count; i++) // parcours la liste pour déterminer le nombre de lettres qu'il y a dedans
                            {
                                if (charactères_plateau[i] != ' ')
                                {
                                    Nb_Lettre++;
                                }
                            }
                            if (!Plateau_Vide() && (Nb_Lettre < 1) && !Mot_Touche_Un_Autre(mot, ligne, colonne, direction)) // si le plateau n'est pas vide, que la liste ne comporte pas de charactères et que le mot ne touche aucun autre sur le plateau, le mot n'est pas éligible
                            {
                                Console.WriteLine("[ERREUR] : Le mot doit toucher une lettre déjà présente sur le plateau");
                                eligible = false;
                            }
                            // Regarde si les lettres de la main utiles au mot appartiennent bien à la main 
                            for (int i = 0; i < mot.Length; i++) 
                            {
                                if (!Jeton_Exist(mot[i],joueur.Main_courante, utilisation_joker, charactères_plateau) && mot[i] != charactères_plateau[i]) // si le jeton n'est pas présent dans la main courante et n'est pas posé sur le plateau au bon endroit
                                {
                                    eligible = false; 
                                    break;
                                }
                            }
                            // Regarde si les mots perpediculaires sont dans le dictionnaire
                            
                            List<string> mot_trouvé = Créer_Mot(mot, ligne, colonne, 'd');
                            for(int i = 0; i < mot_trouvé.Count; i++)
                            {
                                Console.WriteLine(mot_trouvé[i]);
                            }
                            if (mot_trouvé != null)
                            {
                                for (int i = 0; i < mot_trouvé.Count; i++)
                                {
                                    if (mot_trouvé[i] != null && mot_trouvé[i].Length != 1)
                                    {
                                        if (!dico1.RechDichoRecursif(mot_trouvé[i], 0, dico1.Ensemble_mots.Count) /*!dico1.RechercheDico(mot_trouvé[i])*/) // Si le mot trouvé n'est pas dans le dictionnaire, on arrête la boucle et le mot n'est pas éligible
                                        {
                                            Console.WriteLine("[ERREUR] : Les mots créés perpendiculairement n'appartiennent pas au dictionnaire");
                                            eligible = false;
                                        }
                                    }
                                }
                            }   
                        }
                        else
                        {
                            eligible = false;
                        }

                        break;
                    
                    case 'd':
                        if (Plateau_Vide() && (ligne != 7 || colonne + mot.Length - 1 < 7))
                        {
                            Console.WriteLine("[ERREUR] : Une lettre du premier mot doit être placée sur la case centrale du plateau");
                            eligible = false;
                        }
                        // regarde s'il y a des lettres sur le plateau
                        if ((colonne + mot.Length - 1 <= 14) && (colonne + mot.Length <= 14 && plat[ligne, colonne + mot.Length] == ' ') && (colonne - 1 != -1 && plat[ligne, colonne - 1] == ' '))
                        {
                            for (int i = 0; i < mot.Length; i++)
                            {

                                if (mot[i] != plat[ligne, colonne + i] && plat[ligne, colonne + i] != ' ') // si il y a une lettre sur le plateau qui n'appartient pas au mot ou est mal placée, on arrête
                                {
                                    Console.WriteLine("[ERREUR] : Les lettres sur le plateau ne permettent pas de poser ce mot à cet endroit");
                                    eligible = false;
                                    break;
                                }
                                else // sinon ajouter le charactère dans une liste 
                                {
                                    charactères_plateau.Add(plat[ligne, colonne + i]);
                                }
                            }

                            int Nb_Lettre = 0;
                            for(int i = 0; i < charactères_plateau.Count; i++) // parcours la liste pour déterminer le nombre de lettres qu'il y a dedans
                            {
                                if(charactères_plateau[i] != ' ')
                                {
                                    Nb_Lettre++;
                                }
                            }

                            if (!Plateau_Vide() && (Nb_Lettre < 1) && Mot_Touche_Un_Autre(mot, ligne, colonne, direction) == false) // si le plateau n'est pas vide, que la liste ne comporte pas de charactères et que le mot ne touche aucun autre sur le plateau, le mot n'est pas éligible
                            {
                                Console.WriteLine("[ERREUR] : Le mot doit toucher une lettre déjà présente sur le plateau");
                                eligible = false;
                            }
                            for (int i = 0; i < mot.Length; i++) // Les lettres de la main utiles au mot appartiennent bien à la main 
                            {
                                if (!Jeton_Exist(mot[i], joueur.Main_courante, utilisation_joker, charactères_plateau) && mot[i] != charactères_plateau[i]) // si le jeton n'est pas présent dans la main courante et n'est pas posé sur le plateau au bon endroit
                                {
                                    Console.WriteLine("[ERREUR] : Le jeton n'est pas disponible dans la main courante ou n'est pas bien placé sur le plateau");
                                    eligible = false;
                                    break;
                                }
                            }
                            // regarde si les mots perpediculaires sont dans le dictionnaire
                            List<string> mot_trouvé = Créer_Mot(mot, ligne, colonne, 'b');
                            for (int i = 0; i < mot_trouvé.Count; i++)
                            {
                                Console.WriteLine(mot_trouvé[i]);
                            }
                            if (mot_trouvé != null)
                            {
                                for (int i = 0; i < mot_trouvé.Count; i++)
                                {
                                    if (mot_trouvé[i] != null && mot_trouvé[i].Length > 1)
                                    {
                                        if (!dico1.RechDichoRecursif(mot_trouvé[i], 0, dico1.Ensemble_mots.Count)/*!dico1.RechercheDico(mot_trouvé[i])*/) /// si le mot trouvé n'est pas dans le dictionnaire, on arrête la boucle et le mot n'est pas éligible
                                        {
                                            Console.WriteLine("[ERREUR] : Les mots créés perpendiculairement n'appartiennent pas au dictionnaire");
                                            eligible = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bug");
                            eligible = false;
                        }
                        break;
                }
                // voir si les mots qui se croisent sont éligibles
            }      

            return eligible;
        }
 
        public void ajouterMot( string mot, int ligne, int colonne, char direction, Joueur joueur, char lettre_substituée) // fonction qui ajoute le mot sur le plateau
        {
            switch(direction)
            {
                case 'b':
                    for(int i = 0; i < mot.Length; i++)
                    {
                        if(lettre_substituée != ' ')
                        {
                            if(mot[i] == lettre_substituée)
                            {
                                plat[ligne + i, colonne] = mot[i];
                                joueur.Remove_Main_Courante(Program.Associer_Charactère_Et_Score('*'));
                                lettre_substituée = ' ';
                            }
                            else if(plat[ligne + i, colonne] == ' ')
                            {
                                plat[ligne + i, colonne] = mot[i];
                                joueur.Remove_Main_Courante(Program.Associer_Charactère_Et_Score(mot[i]));
                            }
                        }
                        else if(plat[ligne + i,colonne] == ' ')
                        {
                            plat[ligne + i, colonne] = mot[i];
                            joueur.Remove_Main_Courante(Program.Associer_Charactère_Et_Score(mot[i]));
                        }
                        
                    }
                    break;
                case 'd':
                    for (int i = 0; i < mot.Length; i++)
                    {
                        if (lettre_substituée != ' ')
                        {
                            if (mot[i] == lettre_substituée)
                            {
                                plat[ligne, colonne + i] = mot[i];
                                joueur.Remove_Main_Courante(Program.Associer_Charactère_Et_Score('*'));
                            }
                            else if (plat[ligne, colonne + i] == ' ')
                            {
                                plat[ligne + i, colonne] = mot[i];
                                joueur.Remove_Main_Courante(Program.Associer_Charactère_Et_Score(mot[i]));
                            }
                            lettre_substituée = ' ';
                        }
                        else if (plat[ligne, colonne + i] == ' ')
                        {
                            plat[ligne, colonne + i] = mot[i];
                            joueur.Remove_Main_Courante(Program.Associer_Charactère_Et_Score(mot[i]));
                        }
                    }
                    break;
            }
            joueur.Add_Mot(mot);
            Console.WriteLine("mot ajouté");
        }
        
        public bool Jeton_Exist(char c, List<Jeton> main_courante, bool utilisation_joker, List<char> liste_lettres) // foncion qui renvoie true si le jeton associé au charactère en entrée est dans la main courante
        {
            bool value = false;
            for(int i = 0; i < main_courante.Count; i++)
            {
                if (c == main_courante[i].Lettre)
                {
                    value = true;
                    break;
                }
            }
            if(utilisation_joker)
            {
                bool présent_sur_le_plateau = false;
                for(int i = 0; i < main_courante.Count; i++ )
                {
                    for(int j = 0; j < liste_lettres.Count; j++)
                    {
                        if(main_courante[i].Lettre == liste_lettres[j])
                        {
                            présent_sur_le_plateau = true;
                            value = false;
                        }
                    }
                }
                if(!présent_sur_le_plateau)
                {
                    for (int i = 0; i < main_courante.Count; i++)
                    {
                        if (main_courante[i].Lettre == '*' && value == false)
                        {
                            value = true;
                            Console.WriteLine("utilisation du joker à la place de la lettre " + c);
                            break;
                        }
                    }
                }
            }

            return value;
        } 
        
        public List<string> Créer_Mot(string mot,int ligne, int colonne, char direction) // renvoie une liste de mots qui touchent le mot qu'on veut placer dans le tableau
        {
            List<string> liste_mots = new List<string>();
            string mot2 = null;
            if(direction == 'd')
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if(j == colonne) // si j est égale à la colonne où commence le mot, on ajoute le i-ème charactère de notre mot au mot u'on va créer
                        {
                            mot2 += mot[i]; 
                        }
                        else
                        {
                            if (plat[ligne + i, j] != ' ') // si le charactère est différent d'un espace, on ajoute ce charactère au mot qu'on va créer
                            {
                                mot2 += plat[ligne + i, j];
                            }
                            else if (plat[ligne + i, j] == ' ' && j < colonne) // si il y a un enchainement de plus de 2 charactères puis un espace avant l'emplacement où on veut poser notre mot, on ignore les lettres d'avant donc le mot créée est null
                            {
                                mot2 = null;
                            }
                            else if(plat[ligne + i, j] == ' ' && j > colonne)
                            {
                                break;
                            }
                            
                        }
                    }
                    liste_mots.Add(mot2);
                }
            }
            else if( direction == 'b')
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (j == ligne ) // si j est égale à la ligne où commence le mot, on ajoute le i-ème charactère de notre mot au mot qu'on va créer
                        {
                            mot2 += mot[i];
                        }
                        else
                        {
                            if (plat[j, colonne + i] != ' ') // si le charactère est différent d'un espace, on ajoute ce charactère au mot qu'on va créer
                            {
                                mot2 += plat[j, colonne + i];
                            }
                            else if (plat[j, colonne + i] == ' ' && j < colonne) // si il y a un enchainement de plus de 2 charactères puis un espace avant l'emplacement où on veut poser notre mot, on ignore les lettres d'avant donc le mot créée est null
                            {
                                mot2 = null;
                            }
                            else if (plat[j, colonne + i ] == ' ' && j > colonne)
                            {
                                break;
                            }

                        }
                    }
                    liste_mots.Add(mot2);
                }
            }
            return liste_mots;
        }

        public bool Mot_Touche_Un_Autre(string mot, int ligne, int colonne, char direction) // renvoie true si le mot qu'on veut poser va toucher au moins un jeton du plateau
        {
            bool value = false;
           switch(direction)
            {
                case 'd':
                    if(ligne != 0 && ligne != 14)
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (plat[ligne + 1, colonne + i] != ' ' || plat[ligne - 1, colonne + i] != ' ') // vérifie si le mot qu'on veut poser touche une lettre une case en haut ou une case en bas
                            {
                                value = true;
                            }
                        }                      
                    }
                    else if(ligne == 0)
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (plat[ligne + 1, colonne + i] != ' ') // vérifie si le mot qu'on veut poser touche une lettre une case en haut
                            {
                                value = true;
                            }
                        }
                    }
                    else if( ligne == 14)
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (plat[ligne - 1, colonne + i] != ' ') // vérifie si le mot qu'on veut poser touche une lettre une case en bas
                            {
                                value = true;
                            }
                        }
                    }           
                    break;

                case 'b':
                    if (colonne != 0 && colonne != 14)
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (plat[ligne + i, colonne + 1] != ' ' || plat[ligne + i, colonne - 1] != ' ') // vérifie si le mot qu'on veut poser touche une lettre une case en haut ou une case en bas
                            {
                                value = true;
                            }
                        }
                    }
                    else if (colonne == 0)
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (plat[ligne + i, colonne + 1] != ' ') // vérifie si le mot qu'on veut poser touche une lettre une case à droite
                            {
                                value = true;
                            }
                        }
                    }
                    else if (colonne == 14)
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (plat[ligne +i, colonne - 1] != ' ') // vérifie si le mot qu'on veut poser touche une lettre une case à gauche
                            {
                                value = true;
                            }
                        }
                    }
                    break;
            }

            return value;
        } 

        public int calcul_score(string mot, int ligne, int colonne, char direction, bool utilisation_joker, char lettre_substituée) // calcul le score du mot posé en fonction des cases de couleur
        {
            int score = 0;
            string couleur = null;
            bool rose = false;
            bool rouge = false;
            if(direction == 'd')
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    if (utilisation_joker && mot[i] == lettre_substituée)
                    {
                        Jeton j = Program.Associer_Charactère_Et_Score(mot[i]);
                        couleur = WhichCase(ligne, colonne + i);
                        switch(couleur)
                        {
                            case "vert":
                                Console.WriteLine("vert");
                                score += j.Score;
                                break;
                            case "bleu clair":
                                score += j.Score * 2;
                                break;
                            case "bleu foncé":
                                score += j.Score * 3;
                                break;
                            case "rose":
                                Console.WriteLine("rose");
                                score += j.Score;
                                rose = true;
                                break;
                            case "rouge":
                                score += j.Score;
                                rouge = true;
                                break;
                        }
                        utilisation_joker = false;
                    }
                    else
                    {
                        Jeton j1 = Program.Associer_Charactère_Et_Score(mot[i]);
                        couleur = WhichCase(ligne, colonne + i);
                        switch (couleur)
                        {
                            case "vert":
                                score += j1.Score;
                                break;
                            case "bleu clair":
                                score += j1.Score * 2;
                                break;
                            case "bleu foncé":
                                score += j1.Score * 3;
                                break;
                            case "rose":
                                score += j1.Score;
                                rose = true;
                                break;
                            case "rouge":
                                score += j1.Score;
                                rouge = true;
                                break;
                        }
                    }

                }
            }
            else if(direction == 'b')
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    if (utilisation_joker && mot[i] == lettre_substituée)
                    {
                        Jeton j = Program.Associer_Charactère_Et_Score(mot[i]);
                        couleur = WhichCase(ligne + i, colonne);
                        switch (couleur)
                        {
                            case "vert":
                                score += j.Score;
                                break;
                            case "bleu clair":
                                score += j.Score * 2;
                                break;
                            case "bleu foncé":
                                score += j.Score * 3;
                                break;
                            case "rose":
                                score += j.Score;
                                rose = true;
                                break;
                            case "rouge":
                                score += j.Score;
                                rouge = true;
                                break;
                        }
                        utilisation_joker = false;
                    }
                    else
                    {
                        Jeton j1 = Program.Associer_Charactère_Et_Score(mot[i]);
                        couleur = WhichCase(ligne + i, colonne);
                        switch (couleur)
                        {
                            case "vert":
                                score += j1.Score;
                                break;
                            case "bleu clair":
                                score += j1.Score * 2;
                                break;
                            case "bleu foncé":
                                score += j1.Score * 3;
                                break;
                            case "rose":
                                score += j1.Score;
                                rose = true;
                                break;
                            case "rouge":
                                score += j1.Score;
                                rouge = true;
                                break;
                        }
                    }

                }
            }
            if (rouge)
            {
                score = score * 3;
            }
            if (rose)
            {
                score = score * 2;
            }
            return score;
        }
        
        public string WhichCase(int l, int c) // méthode qui renvoie la bonne couleur d'une case du plateau
        {
            string couleur = null; ;

            // remplissage des cases rouges
            if ((l == 0 && c == 0) || (l == 0 && c == 7) || (l == 0 && c == 14) || //
               (l == 7 && c == 0) || (l == 7 && c == 14) ||
               (l == 14 && c == 0) || (l == 14 && c == 7) || (l == 14 && c == 14))
            {
                couleur = "rouge";
            }
            // remplissage des cases bleus
            else if ((l == 0 && c == 3) || (l == 0 && c == 11) || (l == 2 && c == 6) || (l == 2 && c == 8) ||
               (l == 3 && c == 0) || (l == 3 && c == 7) || (l == 3 && c == 14) ||
               (l == 6 && c == 2) || (l == 6 && c == 6) || (l == 6 && c == 8) || (l == 6 && c == 12) ||
               (l == 7 && c == 3) || (l == 7 && c == 11) ||
               (l == 8 && c == 2) || (l == 8 && c == 6) || (l == 8 && c == 8) || (l == 8 && c == 12) ||
               (l == 11 && c == 0) || (l == 11 && c == 7) || (l == 11 && c == 14) ||
               (l == 12 && c == 6) || (l == 12 && c == 8) || (l == 14 && c == 3) || (l == 14 && c == 11))
            {
                couleur = "bleu clair";
            }
            // remplissages des cases bleu foncé
            else if ((l == 1 && c == 5) || (l == 1 && c == 9) ||
               (l == 5 && c == 1) || (l == 5 && c == 5) || (l == 5 && c == 9) || (l == 5 && c == 13) ||
               (l == 9 && c == 1) || (l == 9 && c == 5) || (l == 9 && c == 9) || (l == 9 && c == 13) ||
               (l == 13 && c == 5) || (l == 13 && c == 9))
            {
                couleur = "bleu foncé";
            }
            // remplissage des cases roses (la couleur rose n'est pas disponible donc j'ai mis jaune foncé)
            else if ((l == 1 && c == 1) || (l == 2 && c == 2) || (l == 3 && c == 3) || (l == 4 && c == 4) ||
               (l == 1 && c == 13) || (l == 2 && c == 12) || (l == 3 && c == 11) || (l == 4 && c == 10) ||
               (l == 7 && c == 7) ||
               (l == 10 && c == 4) || (l == 11 && c == 3) || (l == 12 && c == 2) || (l == 13 && c == 1) ||
               (l == 10 && c == 10) || (l == 11 && c == 11) || (l == 12 && c == 12) || (l == 13 && c == 13))
            {
                couleur = "rose";
            }
            // remplissage des cases vert foncé
            else
            {
                couleur = "vert";
            }
            return couleur;
        }

        #endregion
    }
}
