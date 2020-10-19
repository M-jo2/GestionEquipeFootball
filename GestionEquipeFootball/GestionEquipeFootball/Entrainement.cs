using System;
namespace GestionEquipeFootball
{
    public class Entrainement : IPratique
    {

        private int CmptDejaJouer=0;

        public int DejaJouer
        {

            get{ return CmptDejaJouer;}
        }

        public void Joue(Equipe equipe)
        {
            if (this.PeuJouer(equipe))
            {

                //Presence et attribution des penalite, blessure
                foreach(Joueur joueur in equipe.ToutLesJoueurs.Values)
                {
                    if (equipe.GroupeJoueursPret.Contains(joueur))
                    {
                        blessure(joueur);

                        joueur.Niveau++;
                        if (equipe.PlanningPratique.ACarnetPresence.EtaientPresentAuDernierEntrainement(joueur)
                            || equipe.PlanningPratique.ACarnetPresence.EtaientPresentAuDernierMatch(joueur))
                        {
                            joueur.Vitalite--;
                        }
                    }
                    else
                    {
                        if(!equipe.PlanningPratique.ACarnetPresence.EtaientPresentAuDernierEntrainement(joueur))
                            joueur.Niveau--;

                        joueur.Vitalite++;
                    }
                }
                //Presence notée
                equipe.PlanningPratique.ACarnetPresence.SontPresentEntrainement(Planning.DateCourante,equipe.GroupeJoueursPret);
                this.CmptDejaJouer++;
            }
            else throw new ArgumentException("Cette equipe comporte des joueurs dans l'incapacité de jouer.");
        }
        public bool PeuJouer(Equipe equipe) 
        {
            bool peuJouer = true;

            foreach(Joueur joueur in equipe.GroupeJoueursPret)
            {
                if (joueur.Vitalite < 4)
                {
                    peuJouer = false;
                    break;
                }
            }
            return peuJouer;
        }


        private void blessure(Joueur joueur)
        {
            Random rnd = new Random();
            int probabiliteBlessure = 10;

            for (int i = 1; i <= 6; i++)
            {
                if (rnd.Next(0, 100+(10*i)) < probabiliteBlessure)
                {
                    joueur.accident(1);
                }
            }
        }
        public override string ToString()
        {
            return "Entrainement";
        }
    }
}
