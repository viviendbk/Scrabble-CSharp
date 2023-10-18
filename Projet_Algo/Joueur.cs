using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projet_Algo
{
    public class Joueur
    {
        #region Attributs
        string nom;
        int score;
        List<string> mots_trouvés;
        List<Jeton> main_courante;
        #endregion

        #region Constructeur
        public Joueur(string nom)
        {
       
            this.nom = char.ToUpper(nom[0]) + nom.Substring(1).ToLower();
            this.score = 0;
            this.mots_trouvés = null;
            this.main_courante = null;
        }
 
        public Joueur(StreamReader joueur) // constructeur de jouer à partir d'un fichier texte
        {
            
            this.main_courante = new List<Jeton>();
            this.mots_trouvés = new List<string>();
            string line = joueur.ReadLine();
            string[] tab = line.Split(";");
            this.nom = tab[0]; // ajout du nom
            this.score = Convert.ToInt32(tab[1]); // ajout du score
            line = joueur.ReadLine(); // lit la première ligne du fichier texte et la consomme
            string[] tab2 = line.Split(";");
            for(int i =0;i<tab2.Length;i++) // ajout de mots
            {
                Add_Mot(tab2[i]);
            }
            line = joueur.ReadLine();
            string[] tab3 = line.Split(";");
           
            StreamReader jeton = new StreamReader("JetonsInitiales.txt");
            CreationjetonEtAjout(jeton, tab3);
            jeton.Close();
            joueur.Close();
        }

        public void CreationjetonEtAjout(StreamReader txt, string[] tab) // compare le tableau de jeton obtenu avec le fichier Jetons.txt pour avoir le score de chaque jeton
        {
            string line = txt.ReadToEnd();
            string[] tab_line = line.Split(';', '\n');
            for (int i = 0; i < tab.Length; i++)
            {
                for(int j = 0; j < tab_line.Length; j++)
                {
                    if (tab_line[j] == tab[i]) // si le i-ème charactère du tableau est égale au caractère présent dans le fichier texte, on crée le jeton avec ce charactère et le core associé
                    {
                        Jeton j1 = new Jeton(Convert.ToChar(tab_line[j]), Convert.ToInt32(tab_line[j+1])/*, 1*/);
                        
                        Add_Main_Courante(j1);
                    }
                }
            }
        }
        #endregion

        #region Propriétés

        public List<Jeton> Main_courante
        {
            get { return this.main_courante; }
        }

        public string Nom
        {
            get { return this.nom; }
        }

        public int Score
        {
            get { return this.score; }
        }

        public List<string> Mot_trouvés
        {
            get { return this.mots_trouvés; }
        }

        #endregion

        #region Méthodes
        public void Add_Mot(string mot) //  ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie 
        {
            if(mot!=null || mot.Length !=0)
            {
                if(mots_trouvés != null)
                {
                    mots_trouvés.Add(mot);
                }
                else
                {
                    mots_trouvés = new List<string>();
                    mots_trouvés.Add(mot);
                }
                
            }
            else
            {
                Console.WriteLine("entrez un mot valide.");
            }       
        }
      
        public string toString() // retourne une chaîne de caractères qui décrit un joueur.
        {
            string phrase = "Nom : " + nom + "\nScore : " + score + "\nMot(s) trouvé(s) : ";
            if(mots_trouvés == null || mots_trouvés.Count == 0)
            {
                phrase += "0";
            }
            else
            {
                for (int i = 0; i < mots_trouvés.Count - 1; i++)
                {
                    phrase += mots_trouvés[i] + ", ";
                }
                phrase += mots_trouvés[mots_trouvés.Count - 1];
            }
            phrase += "\nMain courrante : ";
            if(main_courante != null )
            {
                for (int i = 0; i < main_courante.Count; i++)
                {
                    phrase += main_courante[i].toString() + "\n\t\t ";
                }
            }
            return phrase;
        }
       
        public string toStringStreamWriter() // retourne la chaine de charactères qui sera utile pour remplir le fichier texte pour sauvegarder
        {
            string phrase = nom + ";" + score + "\n";
            if(mots_trouvés == null || mots_trouvés.Count == 0)
            {
                phrase += "\n";
            }
            else
            {
                for(int i = 0; i < mots_trouvés.Count - 1; i++)
                {
                    phrase += mots_trouvés[i] + ";";
                }
                phrase += mots_trouvés[mots_trouvés.Count - 1] + "\n";
            }
            for(int i = 0; i < main_courante.Count - 1; i++)
            {
                phrase += main_courante[i].Lettre + ";";
            }
            phrase += main_courante[main_courante.Count - 1].Lettre + "\n";
            return phrase;
        }

        public void Enregistrer(string fichier, int nombre_joueurs, int dernier_joueur_à_jouer)
        {
            Program.créerDossier(fichier);
            StreamWriter txt = new StreamWriter(fichier);
            txt.Write(toStringStreamWriter());
            if (fichier == "Joueur0.txt")
            {
                txt.Write(nombre_joueurs + "\n" + dernier_joueur_à_jouer);
            }
            txt.Close();
        }

        public void Add_Score(int val) // ajoute une valeur au score 
        {
            score += val;
        }
        
        public void Add_Main_Courante(Jeton monjeton) // ajoute un jeton à la main courante 
        {
            if(main_courante == null) // si la liste n'est pas créée, on la créée
            {
                main_courante = new List<Jeton>();
            }
            main_courante.Add(monjeton);
            Console.WriteLine("le jeton [" + monjeton.toString()+"] a été ajouté à la main courante du joueur "+nom);
        }

        public void Remove_Main_Courante(Jeton monjeton) // retire un jeton de la main courante
        {
            bool value = false;
            for(int i = 0; i< main_courante.Count; i++) // regarde si le jeton en entrée est présent dans la main courante
            {
                if(main_courante[i].Lettre == monjeton.Lettre)
                {
                    main_courante.RemoveAt(i);
                    Console.WriteLine("le jeton [" + monjeton.toString() + "] a été retiré de la main courante du joueur " + nom);
                    value = true;
                    break;
                }
            }
            if(!value)
            {
                Console.WriteLine("le jeton [" + monjeton.toString  () + "] n'est pas dans la main courante du joueur " + nom);
            }
           
        }
        #endregion
    }
}
