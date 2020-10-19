using System;
using System.Collections.Generic;

namespace GestionEquipeFootball
{
    public class GestionEquipeConsole
    {
        private static GestionEquipeConsole instance = new GestionEquipeConsole();
        private GCommande Gcommande = new GCommande();
        private int Fenetre =0; // quel fenetre afficher
        private bool ContinueProgramme=true; // prochaine commande à executer
        Equipe equipe ;

        private enum nomCommande{
            AffichePlan = 0,
            AfficheEquipe = 1,
            Joueur = 2,
            PosJoueur =3,
            SupprimeJoueur =4,
            AjouteJoueur=5,
            ChNomEquipe=6,
            Go=7,
            NextDay=8,
            Stop=9,
            help=10,
            AjouteDate=11,
            SupprimeDate=12,
            DeplaceDate=13

        }

        private GestionEquipeConsole()
        {
            }

        static public GestionEquipeConsole GetInstance()
        {
            return instance;
        }



        public void MainGestionEquipe()
        {
            Planning.DateCourante = DateTime.Today;
            CreationCommande();
            Console.WriteLine("Utiliser une equipe pré-construite (avec des match prévu) ou en créé une nouvelle. \n\nVoulez vous creer une nouvelle equipe ?");
            if (Confirmation()) { 
                Init();  // Avec une interface pour creer son equipe au lancement
            }else InitDEV(); // initialisation déjà codée => plus rapide pour les tests

            Console.Clear();
            while (ContinueProgramme)
            {
                AppuyerPourContinuer();
                Console.Clear();
                switch (Fenetre)
                {
                    case 0:
                        help();
                        break;
                    case 1:
                        GestionPlanning();
                        break;
                    case 2:
                        Console.Write($"{Planning.DateCourante.ToLongDateString()} ");
                        if(!equipe.PlanningPratique.JourLibre(Planning.DateCourante ))  Console.WriteLine($" : {equipe.PlanningPratique.PratiqueDuJour(Planning.DateCourante ).ToString()} prevu.");
                        else Console.WriteLine(" : Rien de prevu.");
                        Console.WriteLine(equipe.ToString());
                        break;
                }
                Cmd();
            }
        }

        public void CreationCommande()
        {
            Gcommande.AjoutCommande(0,new string[]{"0","AffichePlanning","=>Affiche la fenetre du planning de l'equipe."});
            Gcommande.AjoutCommande(1,new string[]{"1","AfficheEquipe","=>Affiche tout les joueurs de l'equipe."});
            Gcommande.AjoutCommande(2,new string[]{"2","Joueur","[ID] =>Ajoute ou enleve un joueur à la liste des joueurs prêt."});
            Gcommande.AjoutCommande(3,new string[]{"3","PosJoueur","[ID] [G/AD/AG/DD/DG] =>Modifier (ajout ou suppression) la position d'un joueur sur le terrain."});
            Gcommande.AjoutCommande(4,new string[]{"4","SupprimeJoueur","[ID] =>Suppression d'un joueur de l'equipe."});
            Gcommande.AjoutCommande(5,new string[]{"5","AjouteJoueur","opt[NIVEAU] opt[VITALITE] =>Ajout d'un joueur à l equipe."});
            Gcommande.AjoutCommande(6,new string[]{"6","ChNomEquipe","CNE","[nouveauNom]=>Changer le nom de l'equipe."});
            Gcommande.AjoutCommande(7,new string[]{"7","Go","=>Lance le match ou l entrainement du jour (implique un NextDay automatique"});
            Gcommande.AjoutCommande(8,new string[]{"8","NextDay","=>Passe au lendemain"});
            Gcommande.AjoutCommande(9,new string[]{"9","Stop","=>Quitter Le programme."});
            Gcommande.AjoutCommande(10,new string[]{"10","help","h","=>Affichage de la liste des commande."});
            Gcommande.AjoutCommande(11,new string[]{"01","AjouteDate","[a] [m] [j] [M/E]=>Ajout d'une date dans le PLANNING. 'M' pour match et 'E' pour entrainement"});
            Gcommande.AjoutCommande(12,new string[]{"02","SupprimeDate","[a] [m] [j]=>Retrait d'une date dans le PLANNING."});
            Gcommande.AjoutCommande(13,new string[]{"03","DeplaceDate","[oldA] [oldM] [oldJ] [newA] [newM] [newJ]=> deplace une date avec son match/entrainement."});
        
        }
        public void InitDEV()
        {
            equipe = new Equipe(7,"les Diablotins");
            equipe.JoueurPret(equipe.ToutLesJoueurs[0]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[1]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[2]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[3]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[4]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[5]);
            equipe.JoueurPret(equipe.ToutLesJoueurs[6]);
            equipe.GroupeJoueursPret[0].AjoutPosition(PosTer.GARDIEN);
            equipe.GroupeJoueursPret[1].AjoutPosition(PosTer.ATT_D);
            equipe.GroupeJoueursPret[2].AjoutPosition(PosTer.ATT_G);
            equipe.GroupeJoueursPret[3].AjoutPosition(PosTer.DEF_D);
            equipe.GroupeJoueursPret[4].AjoutPosition(PosTer.DEF_G);
            equipe.GroupeJoueursPret[5].AjoutPosition(PosTer.DEF_D);
            equipe.GroupeJoueursPret[6].AjoutPosition(PosTer.DEF_G);
            equipe.GroupeJoueursPret[5].AjoutPosition(PosTer.GARDIEN);
            equipe.GroupeJoueursPret[6].AjoutPosition(PosTer.GARDIEN);

            equipe.PlanningPratique.ajouterDate(new Match(),Planning.DateCourante);
            equipe.PlanningPratique.ajouterDate(new Match(),Planning.DateCourante.AddDays(1));
            equipe.PlanningPratique.ajouterDate(new Match(),Planning.DateCourante.AddDays(2));
        }

        public void Init() //Creation de l equipe
        {

            Console.WriteLine("Bienvenue dans ce logiciel de gestion d'equipe.");
            int tailleEquipe;
            string nomEquipe;

            do {
                Console.WriteLine("Combien de joueur dans cette equipe ?: ");
                tailleEquipe = InputInt();
                Console.WriteLine($"Avec {tailleEquipe} joueurs, cette equipe doit bien porter un nom ?: ");
                nomEquipe = Console.ReadLine();
                equipe = new Equipe(tailleEquipe, nomEquipe);
                Console.WriteLine($"l'equipe {nomEquipe} avec {tailleEquipe} joueurs peut être créée ? ");
            }while(!Confirmation());

            Console.WriteLine($"{nomEquipe} avec ses {tailleEquipe} joueurs vient de naître, félicitation ! ");
        }


        public void GestionPlanning()
        {
            int nbMatch=0;
            int nbEntrainement=0;
            Console.WriteLine($"Aujourd'hui : {Planning.DateCourante .ToLongDateString()}\n");
            Console.WriteLine(equipe.PlanningPratique.ToString());
            foreach(IPratique pr in equipe.PlanningPratique.ACalendrier.Values)
            {
                if(pr.GetType().Equals(typeof(Match))) nbMatch++;
                if(pr.GetType().Equals(typeof(Entrainement))) nbEntrainement++;
            }
            Console.WriteLine($"\nnombre de match prevu : {nbMatch}");
            Console.WriteLine($"\nnombre d'entrainement prevu : {nbEntrainement}");
        }

        private void GestionMatchEntrainement()
        {
            if (!equipe.PlanningPratique.JourLibre(Planning.DateCourante ))
            {
                IPratique prat = equipe.PlanningPratique.PratiqueDuJour(Planning.DateCourante );
                if (equipe.PlanningPratique.PratiqueDuJour(Planning.DateCourante ).DejaJouer == 0)
                {
                    if (prat.PeuJouer(equipe))
                    {
                        prat.Joue(equipe);
                        Planning.JourSuivant();
                    }else
                    {
                        Console.WriteLine("L'equipe ne peux pas jouer. \nCes raisons en sont peut être la cause :");
                        Console.WriteLine("\tEntre 5 et 7 joueurs pour un match. (verifier les penalités des joueurs)");
                        Console.WriteLine("\tTout les joueurs doivent être assigné à au moins un poste pour un match.");
                        Console.WriteLine("\tMinimum 6 de vitalite pour un match.");
                        Console.WriteLine("\tMinimum 4 de vitalite pour un Entrainement.");
                    }
                }else Console.WriteLine($"{prat.ToString()} deja été joué.");
            }
            else Console.WriteLine("Aucun match ou entrainement prevu pour aujourd'hui.");
        }

        private void JoueurIdPos(int IDJoueur,string pos) //Methode propre à la commande Joueur [ID] [pos]
        {
            PosTer pos_ter;
            if(pos == "G" || pos == "AD"  || pos == "AG" || pos == "DD" || pos == "DG")
            {
                if(pos == "G") pos_ter = PosTer.GARDIEN;
                else if(pos == "AD") pos_ter = PosTer.ATT_D;
                else if(pos == "AG") pos_ter = PosTer.ATT_D;
                else if(pos == "DD") pos_ter = PosTer.DEF_D;
                else pos_ter = PosTer.DEF_G;
                equipe.ToutLesJoueurs[IDJoueur].AjouOuSuppPosition(pos_ter);
            }
            else throw new ArgumentException($"Cette position est inconnu");
        }

        private void help()
        {
            Console.WriteLine("=======COMMANDE_DISPONIBLE=======\n=================================\n");
            Console.WriteLine("Chaque commandes possède un numero utilisable à la place de celle ci.");
            Console.WriteLine("'h' ou 'help' permet de revenir sur ce menu. N'importe quelle commande est utilisable sur n'importe quelle fenetre.\n");
            Console.WriteLine(Gcommande.ToString());
        }

        /*
        *==============================OUTIL PRATIQUE======================================
        *==================================================================================     
        */

        private int InputInt() // attend un entier en Input
        {
            string input = Console.ReadLine();
            int intPut;
            while(!Int32.TryParse(input,out intPut)){
                Console.WriteLine("Attention, un nombre est attendu ici.");
                input = Console.ReadLine();
            }
            return intPut;
        }

        private bool testInt(String nb, out int nbo)
        {
            return Int32.TryParse(nb,out nbo);
        }

        private bool Confirmation()
        {
            string input;
            do {
                Console.WriteLine("(y/n)");
                input = Console.ReadLine();
            } while ((input != "y" && input != "n"));
            return input == "y";
        }

        private void AppuyerPourContinuer()
        {
                Console.WriteLine("Appuyer sur une touche pour continuer...");
                Console.ReadKey();
        }


        private void Cmd()
        {
            string cmd = Console.ReadLine();
            string[] cmds = cmd.Split(' ');
            if (Gcommande.CmdExist(cmds[0]))
            {
                switch (cmds.Length) //test du nombre d'argument entré
                {
                    case 1:
                        if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.AffichePlan) Fenetre = 1;
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.AfficheEquipe) Fenetre = 2;
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.help) Fenetre = 0;
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.Stop) ContinueProgramme = false;
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.AjouteJoueur) equipe.AjouterUnJoueurEquipe(new Joueur());
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.NextDay) Planning.JourSuivant();
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.Go) this.GestionMatchEntrainement();

                        
                        break;

                    case 2:
                        if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.SupprimeJoueur)
                        {
                            if (testInt(cmds[1], out int idJo))
                            {
                                if (equipe.SupprimerJoueurEquipe(equipe.ToutLesJoueurs[idJo])) { }
                                else Console.WriteLine("Joueur inconnu.");
                            }
                            else
                                Console.WriteLine("Attention Valeur entière attendue.");
                        }
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.ChNomEquipe)
                        {
                            equipe.NomEquipe = cmds[1];
                        }
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.Joueur)
                        {
                            if (testInt(cmds[1], out int idJo))
                            {
                                if (equipe.ToutLesJoueurs.ContainsKey(idJo))
                                    if (equipe.JoueurPasPret(equipe.ToutLesJoueurs[idJo])) { }
                                    else equipe.JoueurPret(equipe.ToutLesJoueurs[idJo]);
                                else Console.WriteLine("Joueur inconnu.");
                            }
                            else
                                Console.WriteLine("Attention Valeur entière attendue.");
                        }
                        break;

                    case 3:
                        if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.PosJoueur)
                        {
                            if (testInt(cmds[1], out int idJo))
                            {
                                if (equipe.ToutLesJoueurs.ContainsKey(idJo))
                                    try
                                    {
                                        JoueurIdPos(idJo, cmds[2]);
                                    }
                                    catch (ArgumentException e) { Console.WriteLine(e.Message); }
                                else Console.WriteLine("Joueur inconnu.");
                            }
                            else
                                Console.WriteLine("Attention Valeur entière attendue.");
                        }
                        else if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.AjouteJoueur)
                        {
                            if (testInt(cmds[1], out int vitalite)
                                && testInt(cmds[1], out int niveau))
                            {
                                equipe.AjouterUnJoueurEquipe(new Joueur(niveau, vitalite));
                            }
                            else
                                Console.WriteLine("Attention Valeur entière attendue.");
                        }
                        break;
                    case 4:
                        if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.SupprimeDate)
                        {
                            if (testInt(cmds[1], out int annee) &&
                                testInt(cmds[2], out int mois) &&
                                testInt(cmds[3], out int jour))
                            {
                                if (equipe.PlanningPratique.SupprimerDate(new DateTime(annee, mois, jour))) { }
                                else Console.WriteLine("Date inconnue");
                            }
                            else Console.WriteLine("Date invalide.");
                        }
                        break;
                    case 5:

                        if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.AjouteDate)
                        {
                            if (testInt(cmds[1], out int annee) &&
                                testInt(cmds[2], out int mois) &&
                                testInt(cmds[3], out int jour))
                            {
                                try
                                {
                                    IPratique pr = FactPratique.Creer(cmds[4]);
                                    DateTime date = new DateTime(annee, mois, jour);

                                    bool existe = !equipe.PlanningPratique.JourLibre(date);
                                    if (existe)
                                    {
                                        Console.WriteLine("Cette date existe deja, l'ecraser pour la nouvelle ?");
                                        existe = !Confirmation();
                                    }
                                    if (!existe) equipe.PlanningPratique.ajouterDate(pr, date);
                                }
                                catch (ArgumentException e) { Console.WriteLine(e.Message); }
                            }
                            else Console.WriteLine("Date invalide.");
                        }
                        break;
                    case 7:

                        if (Gcommande.indexDe(cmds[0]) == (uint)nomCommande.DeplaceDate)
                        {
                            Console.WriteLine("DeplaceDate : ");
                            if (testInt(cmds[1], out int aannee) &&
                                testInt(cmds[2], out int amois) &&
                                testInt(cmds[3], out int ajour) &&
                                testInt(cmds[4], out int nannee) &&
                                testInt(cmds[5], out int nmois) &&
                                testInt(cmds[6], out int njour))
                            {
                                DateTime anciennedate = new DateTime(aannee, amois, ajour);
                                DateTime nouvedate = new DateTime(nannee, nmois, njour);

                                bool existe = !equipe.PlanningPratique.JourLibre(nouvedate);

                                if (existe)
                                {
                                    Console.WriteLine("Cette date est deja prise, deplacer quand même ?");
                                    existe = !Confirmation();
                                }
                                if (!existe) equipe.PlanningPratique.DeplacerDate(anciennedate, nouvedate);
                            }
                            else Console.WriteLine("Date invalide.");
                        }
                        break;
                }
            }else Console.WriteLine("Cette commande n'existe pas.");
        }





    }

    class GCommande //permet d'avoir plusieurs nom pour une commande. Ne gere pas le nombre de parametre.
    {
        private Dictionary<uint,string[]> listeCommande= new Dictionary<uint, string[]>(); // indexCommande, [commandeNom1,commandeNomN,...,DESCRIPTION] description  obligatoire, si pas de description laisser un champ vide.

        public bool AjoutCommande(uint index, string[] listeNom)
        {
            bool indexExistant = listeCommande.ContainsKey(index);
            if (!indexExistant)
            {
                listeCommande.Add(index,listeNom);
            }
            return indexExistant;
        }

        public uint indexDe(string commande)
        {
            int indexCmd = -1;
            string[] lstCmd;

            if (CmdExist(commande))
            {
                foreach (uint lstNCmd in listeCommande.Keys)
                {
                    lstCmd = listeCommande[lstNCmd];
                    for(int i=0 ; i<lstCmd.Length-1 ; i++)
                    {
                        if (lstCmd[i] == commande) {
                            indexCmd = (int)lstNCmd;
                        }
                    }
                    if(indexCmd != -1) break;

                }
            }else throw new ArgumentException("Commande inconnue.");
            return (uint)indexCmd;
        }

        public bool CmdExist(string commande)
        {
            bool exist = false;

            foreach (string[] lstCmd in listeCommande.Values)
            {
                for(int i=0 ; i<lstCmd.Length-1 && !exist ; i++)
                {
                    exist = lstCmd[i] == commande;
                }
                if(exist) break;
            }
            return exist;
        }

        public override string ToString()
        {
            string str = "";
            string[] tmp;
            foreach (string[] i in listeCommande.Values)
            {
                tmp = i;
                for(int j = 0; j< tmp.Length ; j++)
                {
                    if(j>= tmp.Length-2)

                        str+= tmp[j]+" ";
                    else
                        str+=tmp[j]+" | ";
                }
                str+="\n";
            }
            return str;
        }

    }
}
