using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projet_Algo
{
    public class Jeton
    {

        #region Attributs
        char lettre;
        int score;
        #endregion

        #region Constructeur
        
        public Jeton(char lettre,int score) // créé la classe jeton à partir de la lettre et du score
        {
            this.lettre = lettre;
            this.score = score;
        }
        
        public Jeton(Sac s1) // créé la classe jeton à partir du sac
        {
            Jeton jeton1 = s1.Retire_Jeton(new Random());
            if(jeton1 != null)
            {
                this.lettre = jeton1.Lettre;
                this.score = jeton1.Score;
            }
        }
        #endregion

        #region Propriétés
        public char Lettre
        {
            get { return this.lettre; }
        }
        public int Score
        {
            get { return this.score; }
        }
        
        #endregion

        #region Méthodes
        
        public string toString() 
        {
            return "Lettre : "+lettre+", score : "+score;
        }
        #endregion
    }
}
