using System;
using System.Collections.Generic;
namespace GestionEquipeFootball
{
    public class CarnetPresence
    {
        //private List<Joueur> DerniereListePresenceEntrainement = new List<Joueur>();
        //private List<Joueur> DerniereListePresenceMatch = new List<Joueur>();

        private Dictionary<DateTime,List<Joueur>> ToutePresenceMatch = new Dictionary<DateTime, List<Joueur>>();
        private Dictionary<DateTime,List<Joueur>> ToutePresenceEntrainement = new Dictionary<DateTime, List<Joueur>>();

        public List<Joueur> PresenceMatch(DateTime date)
        {
            return ToutePresenceMatch[date];
        }
        public List<Joueur> PresenceEntrainement(DateTime date)
        {
            return ToutePresenceEntrainement[date] ;
        }


        public void ViderPresenceEntrainement(DateTime date)
        {
            ToutePresenceEntrainement[date].Clear();
        }

        public void ViderPresenceMatch(DateTime date)
        {
            ToutePresenceMatch[date].Clear();
        }

        /*
        public void EstPresentEntrainement(Joueur joueur)
        {
            if (!DerniereListePresenceEntrainement.Contains(joueur))
                DerniereListePresenceEntrainement.Add(joueur);
        }

        public void EstPresentMatch(Joueur joueur)
        {
            if (!DerniereListePresenceMatch.Contains(joueur))
                DerniereListePresenceMatch.Add(joueur);
        }*/
        public bool EtaientPresentAuDernierMatch(Joueur joueur)
        {
            if(ToutePresenceMatch.Count == 0) return false;
            DateTime[] dates = new DateTime[ToutePresenceMatch.Count];
            int counter = 0;
            foreach(DateTime date in ToutePresenceMatch.Keys)
            {
                dates[counter] = date;
                counter++;
            }
            
            return ToutePresenceMatch[Planning.compareDate(dates)].Contains(joueur);
        }

        public bool EtaientPresentAuDernierEntrainement(Joueur joueur)
        {
            if(ToutePresenceEntrainement.Count == 0) return false;
            DateTime[] dates = new DateTime[ToutePresenceEntrainement.Count];
            int counter = 0;
            foreach(DateTime date in ToutePresenceEntrainement.Keys)
            {
                dates[counter] = date;
                counter++;
            }
            return ToutePresenceEntrainement[Planning.compareDate(dates)].Contains(joueur);
        }

        public void SontPresentEntrainement(DateTime date,List<Joueur>  joueurs)
        {
            ToutePresenceEntrainement.Add(date,joueurs);
        }

        public void SontPresentMatch(DateTime date,List<Joueur>  joueurs)
        {

            ToutePresenceMatch.Add(date,joueurs);
        }
    }
}
