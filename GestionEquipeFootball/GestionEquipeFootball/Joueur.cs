using System;
namespace GestionEquipeFootball
{
    public class Joueur
    {
        private static int NbJoueur = 0; 
        public readonly int ID;

        private Penalite penalite;
        private int vitalite ;
        private int niveau ;
        private PosTer[] position = { 0, 0, 0 } ;
        /*============================Get and Set============================*/
        public Penalite Gpenalite
        {
            get { return penalite ; }
            set { penalite = value;}
        }
        public int Vitalite
        {
            get { return vitalite; }
            set
            {
                this.vitalite = value;
                if (this.vitalite < 1) this.vitalite = 1;
                else if (this.vitalite > 10) this.vitalite = 10;
            }    
        }

        public int Niveau
        {
            get { return niveau; }
            set {
                this.niveau = value;
                if (this.niveau < 1) this.niveau = 1;
                else if (this.niveau > 7) this.niveau = 7;
            }
        }
        /*=====CONSTRUCTEURS======*/

        public Joueur()
        {
            this.vitalite = 1;
            this.niveau = 1;
            this.ID = NbJoueur;
            NbJoueur++;
            penalite = new Penalite();
        }

        public Joueur(int niv,int vita)
        {
            if (niv < 1) niv = 1;
            if (niv > 7) niv = 7;
            if (vita < 1) vita = 1;
            if (vita > 10) vita = 10;
            this.vitalite = vita;
            this.niveau = niv;
            this.ID = NbJoueur;
            NbJoueur++;
            this.penalite = new Penalite();
        }

        public Joueur(int niv,int vita,Penalite penalite)
        {
            if (niv < 1) niv = 1;
            if (niv > 7) niv = 7;
            if (vita < 1) vita = 1;
            if (vita > 10) vita = 10;
            this.vitalite = vita;
            this.niveau = niv;
            this.ID = NbJoueur;
            NbJoueur++;
            this.penalite = penalite;
        }

        /*=============TRAITEMENT DES POSITIONS SUR LE TERRAIN===================*/
        /*
         * Systeme de position:
         *      les positions pour un joueur sont stocké dans un tableau de taille 3.
         *      un joueur ne peu pas avoir deux fois le même poste, une verification à chaque ajout.
         *      à chaque ajout ou suppression, un tri est effectué. Les postes sont alors rangé dans l ordre listé dans PosTer.cs du plud grand au plus petit
         *      un tri est effectué pour :
         *          -Placer tout les '0' à la fin du tableau (utile à la methode TotalPosition()
         *      une valeur de 0 dans le tableau signifie qu'il n occupe pas de poste à cet indice.
         *      
         */

        public bool AjoutPosition(PosTer pos)
        {
            int nbPoste = TotalPosition();
            bool AjoutReussi = nbPoste < this.position.Length && !EstUn(pos);

            if (AjoutReussi)  this.position[nbPoste] = pos;
            TriPoste();
            return AjoutReussi;
        }

        public bool SuppressionPosition(PosTer pos)
        {
            int indiceDePos = IndiceDe(pos);
            bool supprime = indiceDePos < this.position.Length;
            if (supprime)
            {
                this.position[indiceDePos] = 0;
            }
            TriPoste();
            return supprime;
        }

        public void AjouOuSuppPosition(PosTer pos)
        {
            if (this.EstUn(pos))
                SuppressionPosition(pos);
            else 
                AjoutPosition(pos);
        }

        public void ToutSupprimer()
        {
            for (int i = 0; i < this.position.Length; i++) this.position[i] = 0;
        }

        public bool EstUn(PosTer posTerrain) 
        { 
            return this.IndiceDe(posTerrain) < this.position.Length; 
        }

        public int TotalPosition() 
        {
            int i = 0;
            while (this.position[i] != 0 && i < this.position.Length) i++;
            return i;
        }

        private int IndiceDe(PosTer posTerrain) // retourne la taille du tableau si la position n'est pas trouvée
        {
            int i = 0;
            while (i < position.Length && position[i] != posTerrain) i++;
            return i;
        }

        private void TriPoste()
        {
            for(int i = this.position.Length-1; i>=1; i--)
            {
                for(int j = 0; j<=i-1; j++)
                {
                    if(this.position[j+1] > this.position[j])
                    {
                        PosTer pos= this.position[j + 1];
                        this.position[j + 1] = this.position[j];
                        this.position[j] = pos;
                    }
                }
            }
        }


        /*====================RESTE======================*/

        public void accident(int valeur)
        {
            if (valeur >= 1 && valeur <= 6)
            {
                Vitalite -= valeur;
                Niveau -= valeur;
            }
        }

        public override string ToString()
        {
            string posTerrain = "";
            for(int i = 0; i<position.Length; i++)
            {
                posTerrain += $"{i+1})";
                switch (position[i])
                {
                    case (PosTer)1: posTerrain += "Gardien. "; break;
                    case (PosTer)2: posTerrain += "Defenseur_Gauche. "; break;
                    case (PosTer)3: posTerrain += "Defenseur_Droite. "; break;
                    case (PosTer)4: posTerrain += "Attaquant_Gauche. "; break;
                    case (PosTer)5: posTerrain += "Attaquant_Droite. "; break;
                    default: posTerrain += "Vide. "; break;
                }
            }
            string StatJoueur =
                $"Joueur en :  "+ posTerrain + " ||| " +
                $"Vitalité : {this.vitalite} --- " +
                $"Niveau : {this.niveau} --- " +
                $"Efficacité : {this.niveau * this.vitalite} ";
            if(!penalite.EstPurgee()) StatJoueur += $"----{penalite.ToString()} ----\n";
            else StatJoueur +="\n";

            return StatJoueur;
        }
    }
}
