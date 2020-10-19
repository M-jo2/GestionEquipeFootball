using System;
namespace GestionEquipeFootball
{
    public class Penalite
    {
        protected int  CompteurAttenteMatch = 0;

        public int MatchRestant
        {
            get { return CompteurAttenteMatch;}
        }

        public virtual void UnMatchPasse() { 
            if(CompteurAttenteMatch > 0) CompteurAttenteMatch--; 
        }

        public bool EstPurgee()
        {
            return CompteurAttenteMatch == 0;
        }
    }

    public class CartonRouge : Penalite
    {
        public CartonRouge (){
            CompteurAttenteMatch = 2;
        }

        public override string  ToString()
        {
            return $"Carton Rouge : {CompteurAttenteMatch} match restant";
        }
    }

    public class CartonJaune: Penalite
    {
        public CartonJaune (){
            CompteurAttenteMatch = 1;
        }

        public override string  ToString()
        {
            return $"Carton Jaune: {CompteurAttenteMatch} match restant";
        }
    }

    public class CartonNoir: Penalite
    {
        private Random rnd = new Random();
        private int ChanceDepenalisation;
        public CartonNoir (){
            CompteurAttenteMatch = rnd.Next(50);
            ChanceDepenalisation = rnd.Next(100);
        }

        sealed public override void UnMatchPasse() {
            if (CompteurAttenteMatch > 0)
            {
                if(rnd.Next(100)>ChanceDepenalisation)
                    CompteurAttenteMatch=0;
                else CompteurAttenteMatch--;
            }
        }

        public override string  ToString()
        {
            return $"Carton Noir: {CompteurAttenteMatch} match restant";
        }
    }
}
