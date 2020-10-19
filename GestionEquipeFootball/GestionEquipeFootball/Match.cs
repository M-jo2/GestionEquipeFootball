using System;

using System.Collections.Generic;

namespace GestionEquipeFootball
{
    public class Match : IPratique
    {
        private int CmptDejaJouer = 0;

        public int DejaJouer
        {

            get{ return CmptDejaJouer;}
        }

        public void Joue(Equipe equipe) //distribution des penalité, vitalité, blessure, ect..
        {
            if (PeuJouer(equipe))
            {
                int compteur = 0;
                //Presence et attribution des penalite, blessure
                foreach(Joueur joueur in equipe.ToutLesJoueurs.Values)
                {
                    if (equipe.GroupeJoueursPret.Contains(joueur))
                    {
                         blessure(joueur);
                         penalite(joueur);

                         if (equipe.PlanningPratique.ACarnetPresence.EtaientPresentAuDernierMatch(joueur))
                         {
                             joueur.Niveau++;
                             joueur.Vitalite--;
                         }
                         else if (equipe.PlanningPratique.ACarnetPresence.EtaientPresentAuDernierEntrainement(joueur))
                             joueur.Vitalite--;
                        
                    }
                    else
                    {
                        joueur.Gpenalite.UnMatchPasse();
                        joueur.Vitalite++;
                    }
                    compteur++;
                }
                //Presence notée
                equipe.PlanningPratique.ACarnetPresence.SontPresentMatch(Planning.DateCourante,equipe.GroupeJoueursPret);
                this.CmptDejaJouer++;
            }
            else throw new ArgumentException("Cette equipe comporte des joueurs dans l'incapacité de jouer ou pénalisé.");
        }

        public bool PeuJouer(Equipe equipe) 
        {
            bool peuJouer = true;

            if (equipe.GroupeJoueursPret.Count >= 5 && equipe.GroupeJoueursPret.Count <= 7)
            {
                foreach (Joueur joueur in equipe.GroupeJoueursPret)
                {
                    if (!joueur.Gpenalite.EstPurgee()) peuJouer = false;
                    if (joueur.Vitalite < 6) peuJouer = false;
                    if (joueur.TotalPosition() == 0) peuJouer = false;

                }
            }else peuJouer = false;

            return peuJouer;
        }



        private void blessure(Joueur joueur)
        {
            Random rnd = new Random();
            int probabiliteBlessure = 20;

            for (int i = 1; i <= 6; i++)
            {
                if (rnd.Next(0, 100+(10*i)) < probabiliteBlessure)
                {
                    joueur.accident(1);
                }
            }
        }

        private void penalite(Joueur joueur)
        {
            Random rnd = new Random();
            int tirage = rnd.Next(0,100);

            if(tirage >70 && tirage <90) joueur.Gpenalite = new CartonNoir();
            else if (tirage > 90 && tirage <97) joueur.Gpenalite = new CartonRouge();
            else if (tirage >97) joueur.Gpenalite = new CartonNoir();


        }

        public override string ToString()
        {
            return "Match";
        }
    }
}
