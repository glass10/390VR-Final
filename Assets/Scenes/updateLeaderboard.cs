using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class updateLeaderboard : MonoBehaviour
{
    public SortedList pacman = new SortedList();
    public SortedList galaga = new SortedList();
    // Start is called before the first frame update
    void Start()
    {
        //Delete Additional Player
        GameObject.Destroy(GameObject.Find("Player-Game"));

        //Load Leaderboards
        load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void load()
    {
        // Load Map 1 Leaderboard
        if (File.Exists(Application.persistentDataPath + "/pacman.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/pacman.dat");
            SortedList newPacman = (SortedList)bf.Deserialize(fs);
            fs.Close();

            pacman = newPacman;

        }

        // Load Map 2 Leaderboard
        if (File.Exists(Application.persistentDataPath + "/galaga.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/galaga.dat");
            SortedList newGalaga = (SortedList)bf.Deserialize(fs);
            fs.Close();

            galaga = newGalaga;

        }

        // Update actual leaderboards
        GameObject textObject1 = GameObject.Find("PacmanLeaderboard");
        GameObject textObject2 = GameObject.Find("GalagaLeaderboard");

        // Pacman Leaderboard
        int pacmanSize = pacman.Count;
        Debug.Log("pacman Size: " + pacmanSize);
        if (pacmanSize > 5)
        {
            pacmanSize = 5;
        }
        string pacmanLeaderboard = "";
        for (int i = pacmanSize; i >= 1; i--)
        {
            pacmanLeaderboard += (pacmanSize-i) + " - " + pacman.GetByIndex(i-1) + " - " + pacman.GetKey(i-1).ToString() + "\n"; 
        }
        textObject1.GetComponent<TextMesh>().text = pacmanLeaderboard;

        // Galaga Leaderboard
        int galagaSize = galaga.Count;
        Debug.Log("galaga Size: " + galagaSize);
        if (galagaSize > 5)
        {
            galagaSize = 5;
        }
        string galagaLeaderboard = "";
        for (int i = galagaSize; i >= 1; i--)
        {
            galagaLeaderboard += (galagaSize - i + 1) + " - " + galaga.GetByIndex(i-1) + " - " + galaga.GetKey(i-1).ToString() + "\n";
        }
        textObject2.GetComponent<TextMesh>().text = galagaLeaderboard;
    }
}
