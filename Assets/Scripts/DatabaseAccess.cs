

using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DatabaseAccess : MonoBehaviour
{
    // Start is called before the first frame update
    MongoClientSettings settings;
    MongoClient client;
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    public List<Player> listPlayers;

    [SerializeField]
    UIManager uiManager;

    void Start()
    {
        listPlayers = new List<Player>();

        settings = MongoClientSettings.FromConnectionString("mongodb+srv://quentin:paul@cluster0.of4wf.mongodb.net/game?retryWrites=true&w=majority");
        client = new MongoClient(settings);
        database = client.GetDatabase("game");
        collection = database.GetCollection<BsonDocument>("players");


        GetPlayersInfos();


        // Ajouter un joueur dans la DB
        //Player player = new Player();
        //player.pseudo = "golum";
        //player.classe = "sorcier";
        //player.level = "666";
        //player.special = "42000";
        //player.description = "perdu dans une caverne";
        //InsertPlayer(player);


    }

    public void externUpdatePlayer0Name()
    {
        // Mettre à jour les infos d'un joueur dans la DB    /!
        Player tempPlayer = listPlayers[0];
        tempPlayer.pseudo = "superGuerrierdeFouFurieuxOmegaCraque" + Random.Range(0, 30000).ToString();
        UpdatePlayer(tempPlayer);

        GetPlayersInfos();
        //displayPlayers();
        uiManager.UpdateUI(ref listPlayers);

    }


    public async void GetPlayersInfos()
    {
        var infos = collection.FindAsync(new BsonDocument());
        var data = await infos;
        listPlayers.Clear();

        foreach (var playerJson in data.ToList())
        {
            string temp = playerJson.ToString();
            temp = JsonAdapterToClass(temp);
            Player player = JsonUtility.FromJson<Player>(temp);
            listPlayers.Add(player);
        }

        //displayPlayers();
    }


    private string JsonAdapterToClass(string s)
    {
        s = s.Replace("ObjectId(", "").Replace(")", "");
        s = s.Replace("rage", "special").Replace("mana", "special");
        
        return s;
    }

    private void displayPlayers()
    {
        Debug.Log(listPlayers.Count);
        foreach (var p in listPlayers)
        {
            p.callTheToString();
        }
    }

    private BsonDocument createPlayerJson(Player p)
    {
        string specialType = "rage";
        if (p.classe == "sorcier")
        {
            specialType = "mana";
        }
        return new BsonDocument { { "pseudo", p.pseudo }, {"classe", p.classe }, { "level", p.level }, { specialType, p.special }, {"description", p.description } };
    }

    public void UpdatePlayer(Player new_p)
    {
        collection.FindOneAndReplace(p => p["_id"] == ObjectId.Parse(new_p._id), createPlayerJson(new_p)); 

    }

    private void InsertPlayer(Player p)
    {
        collection.InsertOne(createPlayerJson(p));
    }


    IEnumerator getData()
    {

        yield return new WaitForSeconds(2);
    }
}
