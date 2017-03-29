using UnityEngine;
using System.Collections;

public class Battler2 : MonoBehaviour {

    public Player owner;
    public int partyIndex = -1;
    public int position = 0;

    //public Command currentCommand;
    public float coolDown = 0;
    public bool preparingAction = false;
    public float actionTime = 0;
    
    public bool isTargetable
    {
        get
        {
            return partyIndex >= 0;
        }
    }
    public StatMods statMods = new StatMods();

    // Convenience Properties
    public Creature creature
    {
        get
        {
            if (partyIndex > -1)
            {
                if (owner == Player.Player1)
                {
                    return GameManager.Game.player1Party[partyIndex];
                }
                else if (owner == Player.Player2)
                {
                    return GameManager.Game.player2Party[partyIndex];
                }
            }

            return null;
        }
    }
    /*public Attack commandedAttack
    {
        get
        {
            return creature.Attacks[currentCommand.moveToUse];
        }
    }*/

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void UpdateInTime(float deltatime)
    {
        if (coolDown > 0)
        {
            coolDown -= deltatime;
        }
        if (actionTime > 0)
        {
            actionTime -= deltatime;
        }
        if (actionTime <= 0)
        {
            preparingAction = false;
        }
    }
}
