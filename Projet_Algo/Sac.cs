using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projet_Algo
{
    public class Sac
    {
        #region Attribut
        List<Jeton> jetons_disponibles;
        #endregion

        #region Constructeur
        public Sac(StreamReader txt)
        {
            this.jetons_disponibles = new List<Jeton>();
            string line = txt.ReadLine(); // prend la première ligne du fichier texte et la consomme
            while(line !=null) // tant que le fichier texte n'a pas été entièrement consommé, on crée un jeton à partir du fichier et on l'ajoute dans la liste de jetons
            {
                string[] tab = line.Split(";"); 
                if(tab != null)
                {
                    for (int i = 0; i < Convert.ToInt32(tab[2]); i++)
                    {
                        Jeton j = new Jeton(Convert.ToChar(tab[0]), Convert.ToInt32(tab[1]));
                        jetons_disponibles.Add(j);
                    }
                }
                line = txt.ReadLine();
            }
        }
        #endregion

        #region Propriété
        
        public List<Jeton> Jetons_disponibles
        {
            get { return this.jetons_disponibles; }
        }

        #endregion

        #region Méthodes
        public Jeton Retire_Jeton(Random r) // tirage aléatoire de jetons
        {
            Jeton jeton_selectionne = null;
            if(jetons_disponibles == null || jetons_disponibles.Count == 0)
            {
                Console.WriteLine("Il n'y a plus de jetons dans le sac");
            }
            else
            {
                int selection = r.Next(jetons_disponibles.Count - 1); // prend un nombre aléaoire entre 0 et la longueur de la liste-1
                jeton_selectionne = jetons_disponibles[selection]; // créé un jeton 
                jetons_disponibles.RemoveAt(selection); // retire le jeton du sac
            }
            return jeton_selectionne;
        }
        
        public string toString()
        {
            string phrase = null;
            char lettre = 'A';
            int compteur = 0;
            for (lettre = 'A'; lettre <= 'Z'; lettre++) // regarde le nombre de lettre similaires dans la liste
            {
                for(int i = 0; i < jetons_disponibles.Count; i++)
                {
                    if (lettre == jetons_disponibles[i].Lettre)
                    {
                        compteur += 1;
                    }
                }
                if (compteur >= 1)
                {
                    phrase += "lettre : " + lettre + " ,score : " + jetons_disponibles[lettre].Score + " ,nombre : " + compteur + "\n";
                }
                compteur = 0;
            }
            for(int i = 0; i < jetons_disponibles.Count; i++) // regarde le nombre de jokers dans la liste
            {
                if(jetons_disponibles[i].Lettre == '*')
                {
                    compteur += 1;
                }    
            }
            if(compteur >=1)
            {
                phrase += "lettre : * ,score : 0 ,nombre : " + compteur+"\n";
            }
            return phrase;  
        }     

        public void Enregistrer() // fonction permettant d'enregistrer le sac de jetons dans un fichier texte
        {
            string fichier = "Jetons.txt";
            Program.créerDossier(fichier);
            StreamWriter txt = new StreamWriter("Jetons.txt");
            txt.Write(toStringStreamWriter());
            txt.Close();
        }

        public string toStringStreamWriter() // fonction qui remplie le sac de jeton dans un fichier texte qui sera par la suite enregistré
        {
            string phrase = null;
            char lettre = 'A';
            int compteur = 0;
            for (lettre = 'A'; lettre <= 'Z'; lettre++) // regarde le nombre de lettre similaires dans la liste
            {
                for (int i = 0; i < jetons_disponibles.Count; i++)
                {
                    if (lettre == jetons_disponibles[i].Lettre)
                    {
                        compteur += 1;
                    }
                }
                if(compteur >= 1)
                {
                    phrase += lettre + ";" +Program.Associer_Charactère_Et_Score(lettre).Score + ";" + compteur + "\n";
                }
                compteur = 0;
            }
            for (int i = 0; i < jetons_disponibles.Count; i++) // regarde le nombre de jokers dans la liste
            {
                if (jetons_disponibles[i].Lettre == '*')
                {
                    compteur += 1;
                }
            }
            if (compteur >= 1)
            {
                phrase += "*;0;" + compteur + "\n";
            }
            return phrase;
        }
        #endregion
    }
}
