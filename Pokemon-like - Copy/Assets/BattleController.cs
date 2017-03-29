using UnityEngine;
using System.Collections;

using System.Linq;

public class BattleController : MonoBehaviour {

    public BattleManager battle;

	// Use this for initialization
	void Start ()
    {
        battle = FindObjectOfType<BattleManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        if (battle.TurnState == TurnState.Preturn)
        {
            foreach (Battler i in battle.ActiveBattlers.Where(x => x.owner == Player.Player2))
            {
                i.currentCommand.moveToUse = Random.Range(0, i.creature.Attacks.Count);
                i.currentCommand.targetSide = Player.Player1;
                i.currentCommand.targetPosition = Random.Range(0, 2);
            }
        }
	}

    public void ConfirmTurn()
    {
        battle.StartTurn();
    }
}
