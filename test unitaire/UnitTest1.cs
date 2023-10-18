using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Projet_Algo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dictionnaire dico = new Dictionnaire(new StreamReader("Francais.txt"), "français");
            bool result = dico.RechDichoRecursif("AA", 0, dico.Ensemble_mots.Count);
            Assert.AreEqual(result, true);
        }
        public void TestMethod2()
        {
            Joueur j1 = new Joueur("vivien");
            j1.Add_Score(2);
            Assert.AreEqual(j1.Score, 2);
        }
        public void TestMethod3()
        {
            Joueur j1 = new Joueur("vivien");
            Jeton jeton1 = new Jeton('A', 1);
            j1.Add_Main_Courante(jeton1);
            Assert.AreEqual(j1.Main_courante[0].Lettre, jeton1.Lettre);
        }
        public void TestMethod4()
        {
            Joueur j1 = new Joueur("vivien");
            Jeton jeton1 = new Jeton('A', 1);
            j1.Add_Main_Courante(jeton1);
            j1.Remove_Main_Courante(jeton1);
            Assert.AreEqual(j1.Main_courante[0].Lettre, ' ');

        }
        public void TestMethod5()
        {
            Joueur j1 = new Joueur("vivien");
            j1.Add_Mot("mot");
            Assert.AreEqual(j1.Mot_trouvés[0], "mot");

        }
    }
}
