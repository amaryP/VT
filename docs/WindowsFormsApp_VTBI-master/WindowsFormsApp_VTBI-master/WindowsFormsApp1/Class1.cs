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
		public string nom_championnat { get;  }
		public string feuillet { get; }
		public string id_championnat { get; }
		public string saison { get; }
		public string pays { get; }
		//variable initialisé en dur dans la classe pou

		public string ID_MATCH_col { get; }

		public  ColonnesClasseurChampionnat()
        {
			//
        }
	  public ColonnesClasseurChampionnat(string feuillet_championnat, string _saison)
	     {
		Console.WriteLine("Nouvelle instance CelluleSheet créée.");
			//string SheetServiceId,
			//on va commencer par aller recuperer l'ensemble des infos...donc pour le moment on est pas en full dynamique ? il faut table de reference quelquepart...autant que ca soit ici...
			//meme pas besoin d'aller chercher l'info dans une feuille pour le moment, il faut forcement l'avoir quelquepart...donc bon.suffisant ici (on va essayer d'utiliser le meme modele pour
			//l'ensemble des championnats, pas de raisons que cela soit different.
			idclasseur = "1tSHo6EAigkKmiJLBkUxVH7DoQKYOUJefWSSEQNXjVpg";// 'PASP_TEST';
			nom_championnat = feuillet_championnat;
			feuillet = feuillet_championnat;
			id_championnat="61";
			saison = _saison;
			pays = "France";
			ID_MATCH_col = "A";
			Console.WriteLine("fait");
	 }
	//  public Response fixture { get; set; }

	
}
