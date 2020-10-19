using System;

namespace GestionEquipeFootball
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            GestionEquipeConsole gec = GestionEquipeConsole.GetInstance();
            gec.MainGestionEquipe();
            /*Equipe equipe = new Equipe(7,"les Diablotins");
            equipe.JoueurPret(equipe.ToutLesJoueurs[0]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[1]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[2]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[3]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[4]);
            equipe.GroupeJoueursPret[0].AjoutPosition(PosTer.GARDIEN);
            equipe.GroupeJoueursPret[1].AjoutPosition(PosTer.ATT_D);
            equipe.GroupeJoueursPret[2].AjoutPosition(PosTer.ATT_G);
            equipe.GroupeJoueursPret[3].AjoutPosition(PosTer.DEF_D);
            equipe.GroupeJoueursPret[4].AjoutPosition(PosTer.DEF_G);
            Console.WriteLine(equipe.ToString());

            equipe.PlanningPratique.ajouterDate(new Match(),new DateTime(2020,1,1));
            Pratique prat = equipe.PlanningPratique.PratiqueDuJour(new DateTime(2020,1,1));
            if(prat.PeuJouer(equipe)){
                prat.Joue(equipe);
            }else Console.WriteLine("pas ok");

            Console.WriteLine(equipe.ToString());*/
        }
    }
}
