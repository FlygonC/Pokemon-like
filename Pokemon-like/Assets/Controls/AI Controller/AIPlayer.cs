using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIPlayer : MonoBehaviour {

    //public Player sideControlled = Player.Player2;
    [SerializeField]
    private BattleManager battle;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (Battler i in battle.ActiveBattlers.Where(x => x.owner == Player.Player2))
        {
            if (i.preparingAction == false)
            {
                MakeRandomAction(i);
            }
        }
	}

    void MakeRandomAction(Battler actor)
    {
        int selectedAttack = Random.Range(0, actor.creature.Attacks.Count);

        Player sideTargeted = Player.Player1;

        switch (actor.creature.Attacks[selectedAttack].Nature)
        {
            case AttackNature.Helpful:
                sideTargeted = Player.Player2;
                break;
            case AttackNature.Harmful:
                sideTargeted = Player.Player1;
                break;
        }

        Command setCom = new Command();
        setCom.moveToUse = selectedAttack;
        setCom.targetSide = sideTargeted;
        setCom.targetPosition = Random.Range(0, 2);

        actor.CommitCommand(setCom);
    }
}
