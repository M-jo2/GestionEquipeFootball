using System;
using System.Collections.Generic;

namespace GestionEquipeFootball
{
    public class Equipe
    {
        
        private const int NB_JOUEUR_TERRAIN = 5; 
        private Planning planningPratique = new Planning();
        private Dictionary<int, Joueur> Joueurs = new Dictionary<int, Joueur>();
        private List<Joueur> JoueursPret = new List<Joueur>(); 
        /* pour un match :5 premier joueur : sont sur le terrain. ls potentiels deux suivant sont en reserve
        *  pour un entrainement : Tout les joueurs contenu dans cette liste participent à l'entrainement
        *  Les joueurs qui ne sont pas dans la liste sont tout simplement absent       
        */

        
        private String nomEquipe;



        public Planning PlanningPratique
        {
            get { return planningPratique; }
        }

        public String NomEquipe
        {
            get { return nomEquipe; }
            set { this.nomEquipe = value; }
        }

        public List<Joueur> GroupeJoueursPret
        {
            get { return JoueursPret;}
        }

        public Dictionary<int,Joueur> ToutLesJoueurs
        {
            get { return Joueurs ;}
        }
        //======================CONSTRUCTEURS===========================
        private void Init(int nbJoueur)
        {
            for (int i = 0; i < nbJoueur; i++)
            {
                Joueur jouTemp = new Joueur(2, 7);
                Joueurs.Add(jouTemp.ID, jouTemp);
            }
        }

        public Equipe()
        {
            Init(7);
            this.nomEquipe = "equipeSimple";
        }

        public Equipe(int nbJoueur)
        {
            Init(nbJoueur);
            this.nomEquipe = $"equipe{nbJoueur}";
        }

        public Equipe(int nbJoueur,String nom)
        {
            Init(nbJoueur);
            this.nomEquipe = nom;
        }


        //=========================GESTION_EQUIPE========================
        public bool AjouterUnJoueurEquipe(Joueur joueur)
        {
            bool ajout = !Joueurs.ContainsKey(joueur.ID);
            if (ajout)
            {
                Joueurs.Add(joueur.ID, joueur);
            }
            return ajout;
        }

        public bool SupprimerJoueurEquipe(Joueur joueur)
        {
            bool supprime = Joueurs.ContainsKey(joueur.ID);
            if (supprime)
            {
                Joueurs.Remove(joueur.ID);
            }
            return supprime;
        }

        public void RemoveTousLesJoueursPret()
        {
            Joueurs.Clear();
        }

        public void JoueurPret(Joueur joueur) 
        //Le joueur doit faire parti de l'equipe avant d'être ajouté à cette liste.
        {
            if (Joueurs.ContainsKey(joueur.ID))
            {
                if (!JoueursPret.Contains(joueur))
                {
                    JoueursPret.Add(joueur);
                }
            }
            else throw new ArgumentException($"l'ID {joueur.ID} ne correspondent à aucun joueur de cette equipe");
        }

        public bool JoueurPasPret(Joueur joueur)
        {
            bool supprime = JoueursPret.Contains(joueur);
            if (supprime)
            {
                JoueursPret.Remove(joueur);
            }
            return supprime;
        }

        public void AucunJoueursPret()
        {
            JoueursPret.Clear();
        }
        public override string ToString()
        {
            string equipeStr = "";

            equipeStr += $"{this.NomEquipe} \n==========================\n";

            equipeStr += "Joueurs Prêt : \n---------------\n";
            foreach(Joueur jo in GroupeJoueursPret)
            {
                equipeStr += $"ID: {jo.ID}\n{jo.ToString()} \n";
            }
            equipeStr+=$"Joueurs prêt total : {GroupeJoueursPret.Count}\n\n";

            equipeStr += "Joueurs Pas Prêt : \n-------------------\n";
            foreach(Joueur jo in ToutLesJoueurs.Values)
            {
                if (!GroupeJoueursPret.Contains(jo))
                {
                    equipeStr += $"Joueur: {jo.ID} : {jo.ToString()} \n";
                }
            }

            equipeStr+=$"Joueurs total : {ToutLesJoueurs.Count}\n\n";

            return equipeStr;
        }
    }
}
