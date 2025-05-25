using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAccesClasseur
{
	class ColonnesClasseurChampionnat
	{
		//proprietés
		//public string myss;
		public string get { get; set; }

		public string idclasseur { get; }
		public string nom_championnat { get; }
		public string feuillet { get; }
		public string id_championnat { get; }
		public string saison { get; }
		public string pays { get; }
						   //variable initialisé en dur dans la classe pou

		public string ID_MATCH_col { get; }
		public string DATE_HEURE_MATCH_col { get; }
		public string DAY_ROUND_col { get; }
		public string DAY_ROUND_INT_col { get; }
		public string STATUS_LONG_col { get; }
		public string STATUTS_SHORT_col { get; }
		public string ID_TEAM_HOME_col { get; }
		public string NAME_TEAM_HOME_col { get; }
		public string SCORE_HOME_col { get; }
		public string SCORE_MITEMPS_HOME_col { get; }
		public string SCORE_FINALE_HOME_col { get; }
		public string ID_TEAM_AWAY_col { get; }
		public string NAME_TEAM_AWAY_col { get; }
		public string SCORE_AWAY_col { get; }
		public string SCORE_MITEMPS_AWAY_col { get; }
		public string SCORE_FINALE_AWAY_col { get; }
		public string TIMEZONE_col { get; }
		public string VICTOIRE_DOMICILE_col { get; }
		public string VICTOIRE_EXTERIEURE_col { get; }
		public string MATCH_NUL_col { get; }
		public string MATCH_ZERO_ZERO_col { get; }
		public string DATE_MAJ_col { get; }



		public ColonnesClasseurChampionnat()
		{
			//
		}
		public ColonnesClasseurChampionnat(string feuillet_championnat, string _saison)
		{
			/*
			string idclasseur;// { get; }
			string nom_championnat;// { get; }
			string feuillet;// { get; }
			string id_championnat;// { get; }
			string saison;// { get; }
			string pays;// { get; }
		//variable initialisé en dur dans la classe pou
		
		 string ID_MATCH_col;// { get; }
			*/

		Console.WriteLine("Nouvelle instance CelluleSheet créée.");
			//string SheetServiceId,
			//on va commencer par aller recuperer l'ensemble des infos...donc pour le moment on est pas en full dynamique ? il faut table de reference quelquepart...autant que ca soit ici...
			//meme pas besoin d'aller chercher l'info dans une feuille pour le moment, il faut forcement l'avoir quelquepart...donc bon.suffisant ici (on va essayer d'utiliser le meme modele pour
			//l'ensemble des championnats, pas de raisons que cela soit different.
			idclasseur = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
			nom_championnat = feuillet_championnat;
			feuillet = feuillet_championnat;
			//a mettre en place la recherche en dynamique. on placera les libelles a rechercher dans un ensemble ["",""] à mixer avec la fonction de recherche deja developpé.

			id_championnat = "61";
			saison = _saison;
			pays = "France";
			ID_MATCH_col = "A";
			DATE_HEURE_MATCH_col = "B";
			DAY_ROUND_col = "C";
			DAY_ROUND_INT_col = "D";
			STATUS_LONG_col = "E";
			STATUTS_SHORT_col = "F";
			ID_TEAM_HOME_col = "G";
			NAME_TEAM_HOME_col = "H";
			SCORE_HOME_col = "I";
			SCORE_MITEMPS_HOME_col = "J";
			SCORE_FINALE_HOME_col = "K";
			ID_TEAM_AWAY_col = "L";
			NAME_TEAM_AWAY_col = "M";
			SCORE_AWAY_col = "N";
			SCORE_MITEMPS_AWAY_col = "O";
			SCORE_FINALE_AWAY_col = "P";
			TIMEZONE_col = "Q";
			VICTOIRE_DOMICILE_col = "R";
			VICTOIRE_EXTERIEURE_col = "S";
			MATCH_NUL_col = "T";
			MATCH_ZERO_ZERO_col = "U";
			DATE_MAJ_col = "V";

			Console.WriteLine("fait");
		}
		//  public Response fixture { get; set; }

	}
}
