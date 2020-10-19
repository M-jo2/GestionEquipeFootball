using System;
using System.Collections.Generic;

namespace GestionEquipeFootball
{

    public class Planning
    {
        public static DateTime DateCourante = new DateTime();
        private Dictionary<DateTime, IPratique> Calendrier = new Dictionary<DateTime, IPratique>();
        private CarnetPresence CarnetPresence = new CarnetPresence();


        public static void JourSuivant()
        {
            DateCourante = DateCourante.AddDays(1);
        }
        public static void JourSuivant(int nbJour)
        {
            DateCourante = DateCourante.AddDays(nbJour);
        }
        public static DateTime compareDate(DateTime date1,DateTime date2)
        {
            int cmp = DateTime.Compare(date1,date2);
            if(date1 > date2) return date1;
            else return date2;
        }
        public static DateTime compareDate(DateTime[] dates)
        {   

            DateTime tmp = dates[0];
            for(int i=1 ;i<dates.Length; i++)
            {
                tmp = Planning.compareDate(tmp,dates[i]);
            }
            return tmp;
        }

        public CarnetPresence ACarnetPresence
        {
            get { return CarnetPresence; }
        }

        public Dictionary<DateTime,IPratique> ACalendrier
        {
            get { return Calendrier;}
        }

        public void ajouterDate(IPratique pr,DateTime Date)
        {
            bool libre = JourLibre(Date) ;
            if (!libre) { SupprimerDate(Date); }
            Calendrier.Add(Date, pr);
        }

        public bool SupprimerDate(DateTime Date)//retourne true si est supprimer
        {
            bool libre = JourLibre(Date) ;
            if(!libre)
            {
                Calendrier.Remove(Date);
            }
            return !Calendrier.ContainsKey(Date) && !libre;
        }

        public void DeplacerDate(DateTime DateActuelle, DateTime DateSouhaitee)
        {
            IPratique PratiqueTampon= Calendrier[DateActuelle];
            this.SupprimerDate(DateActuelle);
            this.ajouterDate(PratiqueTampon,DateSouhaitee);
        }

        
        public IPratique PratiqueDuJour(DateTime Date)
        {
            IPratique pratique;
            bool PratiquePrevue = Calendrier.ContainsKey(Date);

            if (PratiquePrevue)
            {
                pratique = Calendrier[Date];
            }
            else { 
                pratique = null;
            }

            return pratique;
        }

        public bool JourLibre(DateTime Date)
        {
            return !Calendrier.ContainsKey(Date);
        }

        public override string ToString()
        {
            string planningStr = "Planning de l'equipe.\n==================\n";
            foreach(DateTime date in Calendrier.Keys)
            {
                planningStr += date.ToLongDateString() + " => "+ Calendrier[date].ToString()+"\n";

            }
            return planningStr;
        }
    }
}
