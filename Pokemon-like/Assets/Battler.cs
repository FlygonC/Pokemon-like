using UnityEngine;
using System.Collections;

[System.Serializable]
public class Command
{
    public int moveToUse;
    public Player targetSide;
    public int targetPosition;
    public int finalTarget
    {
        get
        {
            switch (targetSide)
            {
                case Player.Player1:
                    return targetPosition;

                case Player.Player2:
                    return targetPosition + 2;
            }
            return 0;
        }
    }
}

public class Battler : MonoBehaviour {

    public Player owner;
    public int partyIndex = -1;
    public int position = 0;

    public Command currentCommand;
    
    public int coolDown = 0;
    public bool preparingAction = false;
    public int actionTime = 0;

    public float HPRestore = 0;
    public float energyRestore = 0;

    public bool isTargetable
    {
        get
        {
            return partyIndex >= 0;
        }
    }
    public StatMods statMods = new StatMods();
    public CreatureStats FinalStats
    {
        get
        {
            CreatureStats final = new CreatureStats(
                creature.Stats.HP,
                creature.Stats.Energy,
                creature.Stats.Attack * statMods.AttackMultiplier,
                creature.Stats.Power * statMods.PowerMultiplier,
                creature.Stats.Defence * statMods.DefenceMultiplier,
                creature.Stats.Resistance * statMods.ResistanceMultiplier,
                creature.Stats.Speed * statMods.SpeedMultiplier
                );

            return final;
        }
    }

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
    public Attack commandedAttack
    {
        get
        {
            return creature.Attacks[currentCommand.moveToUse];
        }
    }
    public bool notOnCooldown
    {
        get
        {
            return coolDown <= 0;
        }
    }

    
    public void CommitCommand(Command _command)
    {
        currentCommand = _command;
        preparingAction = true;
        actionTime = Mathf.CeilToInt( Attack.baseCooldownTime * (100 / (100 + FinalStats.Speed)) );
    }
}
