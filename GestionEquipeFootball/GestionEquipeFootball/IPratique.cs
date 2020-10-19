using System;

namespace GestionEquipeFootball
{
    public class FactPratique
    {
        public static IPratique Creer(String t)
        {
            if(t == "M") return new Match();
            else if (t == "E") return new Entrainement();
            else throw new ArgumentException("type de pratique Inconnu.");
        }
    }
    
    public interface IPratique
    {
        void Joue(Equipe equipe);
        bool PeuJouer(Equipe equipe);
        int DejaJouer { get; }
    }

}
