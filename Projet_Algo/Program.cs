using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Projet_Algo
{
    class Program
    {
        public static void créerDossier(string nom_fichier) // créé un dossier nommé par la string en entré. si le fichier exste déjà, il le supprime et en créé un nouveau
        {
            try
            {
                while (File.Exists(nom_fichier))
                {
                    File.Delete(nom_fichier);
                }
                using (FileStream fileStr = File.Create(nom_fichier)) { }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Jeton Associer_Charactère_Et_Score(char c) // associe un jeton avec le charactère donné en entré
        {
            StreamReader txt = new StreamReader("JetonsInitiales.txt");
            string line = txt.ReadToEnd();
            string[] tab_line = line.Split(';', '\n');
            Jeton j1 = null;
            for (int j = 0; j < tab_line.Length; j++)
            {
                if (tab_line[j].Length == 1 && Convert.ToChar(tab_line[j]) == c) // si le i-ème charactère du tableau est égale au caractère présent dans le fichier texte, on crée le jeton avec ce charactère et le core associé
                {
                    j1 = new Jeton(Convert.ToChar(tab_line[j]), Convert.ToInt32(tab_line[j + 1]));
                    break;
                }
            }
            return j1;
        }

        static int Joueur_Qui_Commence(Joueur[] tab_joueurs) // renvoie le numéro du joueur qui commence pour une nouvelle partie
        {
            int joueur_qui_commence = 0;
            int i = 0;
            int j = 1;
            while (j < tab_joueurs.Length)
            {
                if (Convert.ToString(tab_joueurs[i].Main_courante[0].Lettre).CompareTo(Convert.ToString(tab_joueurs[j].Main_courante[0].Lettre)) <= 0)
                {
                    joueur_qui_commence = i;
                    j++;
                }
                else if (Convert.ToString(tab_joueurs[i].Main_courante[0].Lettre).CompareTo(Convert.ToString(tab_joueurs[j].Main_courante[0].Lettre)) > 0)
                {
                    joueur_qui_commence = j;
                    i = j;
                    j++;
                }
            }
            return joueur_qui_commence;
        }

        static int Joueur_Qui_Commence2(StreamReader joueur) // renvoie la numéro du joueur qui a joué avant d'arrêter la partie
        {
            for (int i = 0; i < 4; i++)
            {
                string poubelle = joueur.ReadLine();
            }
            string tab = joueur.ReadLine();
            joueur.Close();
            return Convert.ToInt32(tab);
        } 

        static int Vainqueur(Joueur[] tab_joueurs) // renvoie le numéro du joueur qui remporte la partie
        {
            int vainqueur = 0;
            int i = 0;
            int j = 0;
            while (j < tab_joueurs.Length)
            {
                if (tab_joueurs[i].Score > tab_joueurs[j].Score)
                {
                    vainqueur = i;
                    j++;
                }
                else if (tab_joueurs[i].Score < tab_joueurs[i].Score)
                {
                    vainqueur = j;
                    i = j;
                    j++;
                }
            }
            return vainqueur;
        }
      
        static int Nombre_Joueurs(StreamReader joueurs) // renvoie le nombre de joueurs présent dans la sauvegarde
        {
            for(int i = 0; i < 3; i++)
            {
                string poubelle = joueurs.ReadLine();
            }
            string tab = joueurs.ReadLine();
            joueurs.Close();
            return Convert.ToInt32(tab);
        }

        static bool EstUnInt(string mot) // renvoie true si le stirng en entrée peut être transformé en int
        {
            int value = 0;
            bool réponse = int.TryParse(mot, out value);
            return réponse;
        } 

        static bool EstUnChar(string mot) // renvoie true si le string en entrée peut être transformé en char
        {
            char lettre = ' ';
            bool réponse = char.TryParse(mot, out lettre);
            return réponse;
        }

        static void Main(string[] args)
        {

            #region variables nécessires au bon fonctionnement du main

            Plateau monplateau = null;
            Sac monsac_jetons = null;
            Joueur[] joueurs = null;
            Dictionnaire mondico = null;
            int joueur_qui_commence = 0;
            bool partie_finie = false;
            int compteur_tours = 0;
            int compteur_passe_tour = 0;
            char lettre_substituée = ' ';
            char lettre = ' ';
            bool value = false;
            bool value2 = false;
            bool possession_joker = false;
            bool utilisation_joker = false;
            int nombre_joueurs = 0;
            string mot = null;
            string ligne0 = null;
            int ligne = 0;
            string colonne0 = null;
            int colonne = 0;
            string direction0 = null;
            char direction = ' ';
            string réponse = null;
            string décision_joueur = null;

            #endregion

            #region choix de recommencer une partie ou en prendre une autre en cours

            Console.Write("Avez vous une partie en cours? (oui/non) : ");
            string décision = Console.ReadLine().ToLower();
            while(décision != "oui" && décision != "non")
            {
                Console.Write("[ERREUR]\nVoulez vous commencer une nouvelle partie ou reprendre votre dernière partie? (oui/non) : ");
                décision = Console.ReadLine().ToLower();
            }

            #endregion

            #region en fonction de la réponse, on initialise la partie en fonction de la sauvegarde ou des paramètres que les joueurs donnent

            if (décision == "oui")
            {
                nombre_joueurs = Nombre_Joueurs(new StreamReader("Joueur0.txt"));

                Console.WriteLine("il y a " + nombre_joueurs + " joueurs");
                joueurs = new Joueur[nombre_joueurs];

                for (int i = 0; i < nombre_joueurs; i++) // créée les joueurs en fonction des fichiers textes
                {
                    Joueur j1 = new Joueur(new StreamReader("Joueur" + i + ".txt"));
                    joueurs[i] = j1;
                }
                joueur_qui_commence = Joueur_Qui_Commence2(new StreamReader("Joueur0.txt")); 

                // créée le dictionnaire, le plateau et le sac de jetons en fonction des fichiers textes
                StreamReader dico1 = new StreamReader("Francais.txt");
                mondico = new Dictionnaire(dico1, "français");
                dico1.Close();
                StreamReader Plateau = new StreamReader("InstancePlateau.txt");
                monplateau = new Plateau(Plateau);
                Plateau.Close();
                StreamReader Sac_Jeton = new StreamReader("Jetons.txt");
                monsac_jetons = new Sac(Sac_Jeton);
                Sac_Jeton.Close();

                Console.WriteLine("Le joueur " + joueurs[joueur_qui_commence].Nom + " avait joué en dernier, il reprends donc la partie");
            }
            else if (décision == "non") 
            {
                // créé le dictionnaire, les joueurs, le plateau et le sac de jeton en fonction de ce que les jueurs disent dans la console et des fichiers textes
                Console.Write("Saisissez un nombre de joueurs (2 à 4) : ");
                nombre_joueurs = Convert.ToInt32(Console.ReadLine());
                while (nombre_joueurs < 2 || nombre_joueurs > 4)
                {
                    Console.WriteLine("[ERREUR] , Saississez de nouveau un nombre de joueur (2 à 4) : ");
                    nombre_joueurs = Convert.ToInt32(Console.ReadLine());
                }
                joueurs = new Joueur[nombre_joueurs];
                Console.Write("Assossiez un prénom à chaque joueur");
                for (int i = 1; i <= nombre_joueurs; i++)
                {
                    Console.Write("\nJoueur " + i + " : ");
                    joueurs[i - 1] = new Joueur(Console.ReadLine());
                }
                monplateau = new Plateau(new StreamReader("InstancePlateauVide.txt"));
                mondico = new Dictionnaire(new StreamReader("Francais.txt"), "français");
                Console.WriteLine("[Distribution des lettres]");
                monsac_jetons = new Sac(new StreamReader("JetonsInitiales.txt"));

                // distribue 7 jetons à chaque joueur
                for (int i = 0; i < joueurs.Length; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        joueurs[i].Add_Main_Courante(monsac_jetons.Retire_Jeton(new Random()));
                    }
                    Console.WriteLine();
                }
                joueur_qui_commence = Joueur_Qui_Commence(joueurs);
                Console.WriteLine("Le joueur " + joueurs[joueur_qui_commence].Nom + " commence");
            }

            #endregion

            #region boucle qui lance le jeu et s'arrête quand la partie est finie

            while (!partie_finie) // lancement de la boucle de jeu
            {
                possession_joker = false;
                compteur_tours++;

                Console.WriteLine("\n-----------------------[tour " + compteur_tours + "]-----------------------");

                for(int i = 0; i < joueurs.Length; i++)
                {
                    monplateau.toString();

                    // sauvegarde automatique
                    Console.WriteLine(joueurs[(joueur_qui_commence + i) % joueurs.Length].toString());
                    monsac_jetons.Enregistrer();
                    monplateau.Enregistrer();
                    for (int j = 0; j < joueurs.Length; j++)
                    {
                        joueurs[j].Enregistrer("Joueur" + j + ".txt", joueurs.Length, (joueur_qui_commence + i) % joueurs.Length);
                    }

                    Stopwatch timer = new Stopwatch();
                    TimeSpan temps = new TimeSpan();
                    timer.Start(); // lancement du timer;

                    // lancement des choix possible du joueur 
                    Console.Write("c'est à vous de jouer, vous avez 3 minute pour jouer\nVoulez vous placer un mot, piocher une lettre ou passer votre tour? (placer/passer/piocher) : ");
                    décision_joueur = Console.ReadLine().ToLower();
                    while (décision_joueur != "passer" && décision_joueur != "placer" && décision_joueur != "piocher")
                    {
                        Console.WriteLine("[ERREUR] Veuillez réessayer (placer/passer/piocher) : ");
                        décision_joueur = Console.ReadLine().ToLower();
                    }
                    if (décision_joueur == "passer")
                    {
                        compteur_passe_tour++;
                    }
                    else if (décision_joueur == "piocher")
                    {
                        Console.Write("quelle lettre voulez vous retirer de votre main courante? ");
                        lettre = Convert.ToChar(Console.ReadLine().ToUpper());
                        for (int j = 0; j < 7; j++)
                        {
                            if (joueurs[(joueur_qui_commence + i) % joueurs.Length].Main_courante[j].Lettre == lettre)
                            {
                                value2 = true;
                            }
                        }
                        while (!value2)
                        {
                            Console.Write("[ERREUR] la lettre n'est pas dans la main courante, saisissez une autre : ");
                            lettre = Convert.ToChar(Console.ReadLine().ToLower());
                            for (int j = 0; j < 7; j++)
                            {
                                if (joueurs[(joueur_qui_commence + i) % joueurs.Length].Main_courante[j].Lettre == lettre)
                                {
                                    value2 = true;
                                }
                            }
                        }
                        joueurs[(joueur_qui_commence + i) % joueurs.Length].Remove_Main_Courante(Associer_Charactère_Et_Score(lettre));
                        joueurs[(joueur_qui_commence + i) % joueurs.Length].Add_Main_Courante(monsac_jetons.Retire_Jeton(new Random()));
                    }
                    else if (décision_joueur == "placer")
                    {
                        compteur_passe_tour = 0;
                        Console.Write("Saisissez un mot, des coordonnées et une direction\n\nmot : ");
                        mot = Console.ReadLine().ToUpper();
                        while (EstUnInt(mot))
                        {
                            Console.WriteLine("Veuillez saisir un mot, non un nombre");
                            mot = Console.ReadLine().ToUpper();
                        }
                        Console.Write("\nabcisse : ");
                        colonne0 = Console.ReadLine();
                        while (!EstUnInt(colonne0))
                        {
                            Console.WriteLine("Veuillez saisir un nombre inférieur à 14.");
                            colonne0 = Console.ReadLine();
                        }
                        colonne = Convert.ToInt32(colonne0);
                        Console.Write("\nordonné : ");
                        ligne0 = Console.ReadLine();
                        while (!EstUnInt(ligne0))
                        {
                            Console.WriteLine("Veuillez saisir un nombre inférieur à 14.");
                            ligne0 = Console.ReadLine();
                        }
                        ligne = Convert.ToInt32(ligne0);
                        Console.Write("\ndirection (d/b) :");
                        direction0 = Console.ReadLine();
                        while (!EstUnChar(direction0) || EstUnInt(direction0))
                        {
                            Console.WriteLine("Veuillez saisir d ou b");
                            direction0 = Console.ReadLine();
                        }
                        direction = Convert.ToChar(direction0);


                        for (int j = 0; j < 7; j++) // vérifie si le joueur possède un joker
                        {
                            if (joueurs[(joueur_qui_commence + i) % joueurs.Length].Main_courante[j].Lettre == '*')
                            {
                                possession_joker = true;
                            }
                        }

                        if (possession_joker)
                        {
                            Console.Write("\nutilisation du joker ? (oui/non) : ");
                            réponse = Console.ReadLine().ToLower();
                            while (réponse != "oui" && réponse != "non")
                            {
                                Console.WriteLine("[ERREUR] : veuillez réessayer (oui/non) :");
                                réponse = Console.ReadLine().ToLower();
                            }
                            if (réponse == "oui")
                            {
                                Console.Write("à la place de la lettre : ");
                                lettre_substituée = Convert.ToChar(Console.ReadLine().ToUpper());
                                value = monplateau.Test_Plateau(mot, ligne, colonne, direction, mondico, joueurs[(joueur_qui_commence + i) % joueurs.Length], true);
                            }
                            else if (réponse == "non")
                            {
                                value = monplateau.Test_Plateau(mot, ligne, colonne, direction, mondico, joueurs[(joueur_qui_commence + i) % joueurs.Length], false);
                            }
                        }
                        else
                        {
                            value = monplateau.Test_Plateau(mot, ligne, colonne, direction, mondico, joueurs[(joueur_qui_commence + i) % joueurs.Length], false);
                        }

                        while (!value)
                        {

                            Console.Write("Saisissez un mot, des coordonnées et une direction\n\nmot : ");
                            mot = Console.ReadLine().ToUpper();
                            while (EstUnInt(mot))
                            {
                                Console.WriteLine("Veuillez saisir un mot, non un nombre");
                                mot = Console.ReadLine().ToUpper();
                            }
                            Console.Write("\nabcisse : ");
                            colonne0 = Console.ReadLine();
                            while (!EstUnInt(colonne0))
                            {
                                Console.WriteLine("Veuillez saisir un nombre inférieur à 14.");
                                colonne0 = Console.ReadLine();
                            }
                            colonne = Convert.ToInt32(colonne0);
                            Console.Write("\nordonné : ");
                            ligne0 = Console.ReadLine();
                            while (!EstUnInt(ligne0))
                            {
                                Console.WriteLine("Veuillez saisir un nombre inférieur à 14.");
                                ligne0 = Console.ReadLine();
                            }
                            ligne = Convert.ToInt32(ligne0);
                            Console.Write("\ndirection (d/b) :");
                            direction0 = Console.ReadLine();
                            while (!EstUnChar(direction0) || EstUnInt(direction0))
                            {
                                Console.WriteLine("Veuillez saisir d ou b");
                                direction0 = Console.ReadLine();
                            }
                            direction = Convert.ToChar(direction0);

                            for (int j = 0; j < 7; j++) // vérifie si le joueur possède un joker
                            {
                                if (joueurs[(joueur_qui_commence + i) % joueurs.Length].Main_courante[j].Lettre == '*')
                                {
                                    possession_joker = true;
                                    Console.Write("\nutilisation du joker ? (oui/non) : ");
                                }
                            }

                            if (possession_joker)
                            {
                                réponse = Console.ReadLine().ToLower();
                                while (réponse != "oui" && réponse != "non")
                                {
                                    Console.WriteLine("[ERREUR] : veuillez réessayer (oui/non) : ");
                                    réponse = Console.ReadLine().ToLower();
                                }
                                if (réponse == "oui")
                                {
                                    utilisation_joker = true;
                                    Console.Write("à la place de la lettre : ");
                                    lettre_substituée = Convert.ToChar(Console.ReadLine().ToUpper());
                                    value = monplateau.Test_Plateau(mot, ligne, colonne, direction, mondico, joueurs[(joueur_qui_commence + i) % joueurs.Length], utilisation_joker);
                                    if (value)
                                    {
                                        possession_joker = false;
                                    }
                                }
                                else if (réponse == "non")
                                {
                                    utilisation_joker = false;
                                    value = monplateau.Test_Plateau(mot, ligne, colonne, direction, mondico, joueurs[(joueur_qui_commence + i) % joueurs.Length], utilisation_joker);
                                }
                            }
                            else
                            {
                                Console.WriteLine("oui");
                                value = monplateau.Test_Plateau(mot, ligne, colonne, direction, mondico, joueurs[(joueur_qui_commence + i) % joueurs.Length], false);
                            }
                        }
                        timer.Stop();
                        temps = timer.Elapsed;
                        if(temps.Minutes < 3) // si le timer dépasse pas 3 minutes, on jouer sinon on fait rien
                        {
                            joueurs[(joueur_qui_commence + i) % joueurs.Length].Add_Score(monplateau.calcul_score(mot, ligne, colonne, direction, utilisation_joker, lettre_substituée));
                            monplateau.ajouterMot(mot, ligne, colonne, direction, joueurs[(joueur_qui_commence + i) % joueurs.Length], lettre_substituée);
                            utilisation_joker = false;
                            lettre_substituée = ' ';
                            for (int j = joueurs[(joueur_qui_commence + i) % joueurs.Length].Main_courante.Count; j < 7; j++)
                            {
                                Jeton jeton_ajouté = new Jeton(monsac_jetons);
                                if (jeton_ajouté == null) // si ce jeton est null
                                {
                                    break;
                                }
                                joueurs[(joueur_qui_commence + i) % joueurs.Length].Add_Main_Courante(jeton_ajouté);
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous avez dépassé la limite de temps, votre action n'a pas été prise en compte");
                        }
                    }
                    if (compteur_passe_tour >= 3*joueurs.Length)
                    {
                        Console.Write("Les joueurs ont tous passé leur tour 3 fois de suite. ");
                        partie_finie = true;
                        break;
                    }
                    if(monsac_jetons.Jetons_disponibles == null  ||  monsac_jetons.Jetons_disponibles.Count == 0)
                    {
                        Console.Write("Le sac de jetons est vide. ");
                        partie_finie = true;
                        break;
                    }

                }
            }

            #endregion

            #region résultat de la partie
            Console.WriteLine("La partie est terminée, voici les scores finaux:");
            
            for (int i = 0; i < joueurs.Length; i++)
            {
                Console.WriteLine(joueurs[i].Nom + " : " + joueurs[i].Score + "points");
            }

            Console.WriteLine("\nLe vainqueur est " + joueurs[Vainqueur(joueurs)] + " avec un total de " + joueurs[Vainqueur(joueurs)].Score + " points");

            #endregion
        }
    }
   
}
