using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projet_Algo
{
    public class Dictionnaire
    {

        #region Attributs
        string langue;
        List<string> ensemble_mots;
        #endregion

        #region Constructeurs
        public Dictionnaire(StreamReader dico, string langue) // Constructeur à partir d'un fichier et d'une langue
        {
            this.langue = langue;
            this.ensemble_mots = new List<string>();
            string line = dico.ReadToEnd();
            string[] tab_mots = line.Split(' ', '\n'); // met les valeurs du string dans un tableau
            for(int i = 0; i < tab_mots.Length;i++) // pour chaque case du tableau, si la valeur n'est pas convertible en int, on l'ajoute à la liste de mots
            {
                int convertion = 0;
                bool value = int.TryParse(tab_mots[i], out convertion);
                if(value == false)
                {
                    ensemble_mots.Add(tab_mots[i]);
                }
            }
        }
        #endregion

        #region Propriété
        public string Langue
        {
            get { return this.langue; }
        }
        public List<string> Ensemble_mots
        {
            get { return this.ensemble_mots; }
        }
        #endregion

        #region Méthodes

        public int NbMots(int longueur) // renvoie le nombre de mots présents dans le dictionnaire de longueur donnée en entrée
        {
            int compteur = 0;
            for(int i =0;i<ensemble_mots.Count;i++)
            {
                if(ensemble_mots[i].Length == longueur)
                {
                    compteur += 1;
                }
            }
            return compteur;
        }
        
        public string toString()
        {
            string phrase = "langue du dictionnaire : " + langue+"\nIl y a :";
            
            for( int i = 2; i < 20; i++)
            {
                if(NbMots(i) != 0)
                {
                    phrase += "-"+NbMots(i) + " mots de " + i + " lettres\n\t";
                }
            }
            return phrase;
        }
        
        public bool RechDichoRecursif(string mot, int début, int fin) // méthode recherche dichotomique en récursif
        {
            if (ensemble_mots == null || ensemble_mots.Count == 0)
            {
                return false;
            }
            int milieu = (début + fin) / 2;
            if (début > fin) // si lé début dépasse la fin alors le mot n'appartient pas au dictionnaire
            {
                return false;
            }
            else if (ensemble_mots[milieu].CompareTo(mot) == 0) // si le mot est à la même position dans l'ordre alphabétique qe le mot à l'indice milieu de la liste de mot, alors le mot appartient au dictionnaire
            {
                return true;
            }
            else if (mot.Length > ensemble_mots[milieu].Length) // si la longueur du mot qu'on recherche est supérieur à celle du mot à l'indice milieu de l'ensemble mot, on regarde dans la deuxième partie de la liste
            {
                return RechDichoRecursif(mot, milieu + 1, fin);
            }
            else if (mot.Length < ensemble_mots[milieu].Length) // si la longueur du mot qu'on recherche est inférieur à celle du mot à l'indice milieu de l'ensemble mot, on regarde dans la première partie de la liste
            {
                return RechDichoRecursif(mot, début, milieu - 1);
            }
            else if (mot.Length == ensemble_mots[milieu].Length && mot.CompareTo(ensemble_mots[milieu]) > 0) // si la longueur du mot qu'on recherche est égale à celle du mot à l'indice milieu de l'ensemble mot, on regarde l'ordre alphabétique et on regarde dans la x-ème partie du tableau en fonction de ça
            {
                return RechDichoRecursif(mot, milieu + 1, fin);
            }
            else 
            {
                return RechDichoRecursif(mot, début, milieu - 1);
            }
        } 

        #endregion
    }
}
