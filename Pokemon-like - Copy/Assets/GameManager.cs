using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum Player { Player1, Player2 };

public class GameManager : MonoBehaviour {

    protected GameManager() { }
    public static GameManager Game;


    public List<Creature> player1Party = new List<Creature>();
    public List<Creature> player2Party = new List<Creature>();


    // Use this for initialization
    void Awake () {
	    if (Game == null)
        {
            DontDestroyOnLoad(gameObject);
            Game = this;
            TypeMatchups.SetupTypeMatchups();
            GameData.LoadAttacks();
            GameData.LoadSpecies();
        }
        else if (Game != this)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
