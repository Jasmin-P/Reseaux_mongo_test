

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
    List<Player> listPlayers;

    void Start()
    {
        listPlayers = new List<Player>();

        settings = MongoClientSettings.FromConnectionString("mongodb+srv://quentin:paul@cluster0.of4wf.mongodb.net/game?retryWrites=true&w=majority");
        client = new MongoClient(settings);
        database = client.GetDatabase("game");
        collection = database.GetCollection<BsonDocument>("players");

        /*BsonDocument newItem = new BsonDocument { { "pseudo", "Player 1" } };
        
        collection.InsertOne(newItem);
        */

        //Debug.Log("item added");

        Player player = new Player();

        //string jsonString = "{\"playerId\":\"8484239823\",\"pseudo\":\"Powai\",\"type\":\"super voleur\",\"level\":\"62\",\"description\":\"c'est un voleur, mais il est super gentil.\"}";
        //player = JsonUtility.FromJson<Player>(jsonString);

        player.pseudo = "golum";
        player.classe = "sorcier";
        player.level = "666";
        player.special = "42000";
        player.description = "perdu dans une caverne";

        InsertPlayer(player);

        GetPlayersInfos();


        

        //Player playerInstance = new Player();
        //playerInstance.playerId = "8484239823";
        //playerInstance.pseudo = "Powai";
        //playerInstance.type = "Random Nick";
        //playerInstance.level = "10";
        //playerInstance.description = "Random Nick";

        //Convert to JSON
        //string playerToJson = JsonUtility.ToJson(playerInstance);
        //Debug.Log(playerToJson);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public async void GetPlayersInfos()
    {
        var infos = collection.FindAsync(new BsonDocument());
        var data = await infos;

        //Debug.Log(data.ToList()[0].ToString());
        /*
        string test = data.ToList()[0].ToString();
        test = JsonAdapterToClass(test);


        Player player = JsonUtility.FromJson<Player>(test);
        player.callTheToString();
        */
        
        foreach (var playerJson in data.ToList())
        {
            string temp = playerJson.ToString();
            temp = JsonAdapterToClass(temp);
            Player player = JsonUtility.FromJson<Player>(temp);
            listPlayers.Add(player);
            //player.callTheToString();
        }

        displayPlayers();

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

    private void InsertPlayer(Player p)
    {
        collection.InsertOne(createPlayerJson(p));
    }


    IEnumerator getData()
    {

        yield return new WaitForSeconds(2);
    }
}
