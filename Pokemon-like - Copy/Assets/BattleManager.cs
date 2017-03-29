using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.UI;
using System;


//public enum TurnState { Commands, BeginTurn, Move, Action, PostAction, EndTurn };
public enum TurnState { Preturn, BeginTurn, UpdateTurn, EndTurn };
public enum TargetType { Single, Double, All, AllOther, }

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

[System.Serializable]
public class Battler
{
    public Player owner;
    public int partyIndex = -1;
    public int position = 0;

    public Command currentCommand;

    public bool movedThisTurn = false;
    //public bool fainted = false;
    //public bool faintQueued = false;
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
    public Attack commandedAttack
    {
        get
        {
            return creature.Attacks[currentCommand.moveToUse];
        }
    }

}


public class BattleManager : MonoBehaviour
{
    public abstract class BattleEvent
    {
        public BattleManager battle;
        protected bool prevented = false;
        protected string reason;
        // Enforce Constructor(s)
        protected BattleEvent(BattleManager _battle)
        {
            battle = _battle;
        }
        // Run
        public void Run()
        {
            // Run all Event Effectors before Event is run(Effectors may prevent the event from running)
            if (this is BE_TakeTurn)
            {
                foreach (BattleEffector i in battle.BattleEffectors)
                {
                    i.OnTakeTurn(this as BE_TakeTurn);
                }
            }
            if (this is BE_UseAttack)
            {
                foreach (BattleEffector i in battle.BattleEffectors)
                {
                    i.OnUseAttack(this as BE_UseAttack);
                }
            }
            if (this is BE_ReceiveAttack)
            {
                foreach (BattleEffector i in battle.BattleEffectors)
                {
                    i.OnReceiveAttack(this as BE_ReceiveAttack);
                }
            }
            if (this is BE_ReceiveDamage)
            {
                foreach (BattleEffector i in battle.BattleEffectors)
                {
                    i.OnReceiveDamage(this as BE_ReceiveDamage);
                }
            }
            if (this is BE_ReceiveBuff)
            {
                foreach (BattleEffector i in battle.BattleEffectors)
                {
                    i.OnReceiveBuff(this as BE_ReceiveBuff);
                }
            }
            // TODO: do onReceiveStatus effectors
            if (this is BE_RestoreEnergy)
            {
                foreach (BattleEffector i in battle.BattleEffectors)
                {
                    i.OnRestoreEnergy(this as BE_RestoreEnergy);
                }
            }
            if (this is BE_RestoreHP)
            {
                foreach (BattleEffector i in battle.BattleEffectors)
                {
                    i.OnRestoreHP(this as BE_RestoreHP);
                }
            }
            // Stop the event if prevented
            if (prevented)
            {
                battle.InsertEvent(new BE_Message(battle, reason));
                return;
            }
            else
            {
                RunState();
            }
        }

        protected abstract void RunState();

        public void PreventEvent(string _reason)
        {
            prevented = true;
            reason = _reason;
        }
    }

    public class BE_TakeTurn : BattleEvent
    {
        public float energyDiscount = 0;

        public BE_TakeTurn(BattleManager _battle) : base(_battle) { }
        protected override void RunState()
        {
            battle.SetTimedelay(0.0f);
            // UseAttack (if able)
            bool attackTypeMatch = false;
            foreach (ElementTypes i in battle.sourceBattler.creature.Types)
            {
                if (i == battle.attackBeingUsed.Type)
                {
                    attackTypeMatch = true;
                    Debug.Log(battle.sourceBattler.creature.Name + "'s type matches its Attack.");
                }
            }
            float requiredEnergy = battle.attackBeingUsed.EnergyCost * (1.0f - energyDiscount);
            if (attackTypeMatch)
            {
                requiredEnergy *= 0.5f;
            }
            bool enoughEnergy = false;
            if (battle.sourceBattler.creature.currentEnergy >= Mathf.Ceil(requiredEnergy))
            {
                enoughEnergy = true;
            }
            // Final check
            if (enoughEnergy)
            {
                battle.InsertEvent(new BE_UseAttack(battle, requiredEnergy));
            }
            else
            {
                battle.InsertEvent(new BE_Message(battle, "" + battle.sourceBattler.creature.Name + " dosen't have enough energy to use " + battle.attackBeingUsed.Name + "."));
            }

        }
    }
    public class BE_UseAttack : BattleEvent
    {
        private float energyUsed;

        public BE_UseAttack(BattleManager _battle, float _energyUsed) : base(_battle)
        {
            energyUsed = _energyUsed;
        }
        protected override void RunState()
        {
            //When a Battler uses an attack, the attack must be passed on to all that it targets
            battle.SetTimedelay(2.0f);
            Debug.Log(battle.sourceBattler.creature.Name + " used " + battle.attackBeingUsed.Name + "!");
            //Consume Energy
            battle.sourceBattler.creature.currentEnergy -= Mathf.CeilToInt(energyUsed);
            // Send attack to targets
            List<Battler> targets = new List<Battler>();
            // Single Target Attack
            if (battle.sourceBattler.commandedAttack.Target == Targeting.Other)
            {
                battle.RetargetCheck(battle.sourceBattler);
                targets.Add(battle.activeBattlers[battle.sourceBattler.currentCommand.finalTarget]);
            }
            foreach (Battler i in targets)
            {
                battle.InsertEvent(new BE_ReceiveAttack(battle, i));
            }
        }
    }
    public class BE_SendAttack : BattleEvent
    {
        public Battler target;
        public BE_SendAttack(BattleManager _battle, Battler _target) : base(_battle)
        {
            target = _target;
        }
        protected override void RunState()
        {
            battle.SetTimedelay(0.0f);
            // When an attack is sent to a target:
            // Check validity of attack and,
            // if valid, target recieves attack
            battle.InsertEvent(new BE_ReceiveAttack(battle, target));
        }
    }
    public class BE_ReceiveAttack : BattleEvent
    {
        public Battler target;
        public BE_ReceiveAttack(BattleManager _battle, Battler _target) : base(_battle)
        {
            target = _target;
        }
        protected override void RunState()
        {
            battle.SetTimedelay(0.0f);
            // When a battler recives attack:
            // For each effect, target recives that effect
            // ADD NEW CODE FOR EFFECTS HERE. Must insert events backwards of intended order of execution
            if (battle.attackBeingUsed is iBuffTarget)
            {
                foreach (StatBuff i in (battle.attackBeingUsed as iBuffTarget).BuffTarget)
                {
                    battle.InsertEvent(new BE_ReceiveBuff(battle, target, i));
                }
            }
            if (battle.attackBeingUsed is iDealDamage)
            {
                battle.InsertEvent(new BE_ReceiveDamage(battle, target));
            }
        }
    }
    public class BE_ReceiveDamage : BattleEvent
    {
        public Battler target;

        public List<float> finalDmgMods = new List<float>();

        public BE_ReceiveDamage(BattleManager _battle, Battler _target) : base(_battle)
        {
            target = _target;
        }
        protected override void RunState()
        {
            battle.SetTimedelay(1.0f);
            // Test effectiveness of damage
            string prefix = "";
            float effect = Calc.TypeEffectiveness(battle.attackBeingUsed.Type, target.creature.Types);
            if (effect > 1)
            {
                prefix = "It's super effective! ";
            }
            if (effect < 1)
            {
                prefix = "It's not very effective. ";
            }
            // real damage
            int damage = 0;
            damage = Calc.Damage3(battle.sourceBattler, battle.attackBeingUsed as iDealDamage, target, finalDmgMods.ToArray());
            target.creature.TakeDamage(damage);
            Debug.Log(prefix + target.creature.Name + " took " + damage + " damage!");
        }
    }
    public class BE_ReceiveBuff : BattleEvent
    {
        Battler target;
        StatBuff buff;
        public BE_ReceiveBuff(BattleManager _battle, Battler _target, StatBuff _buff) : base(_battle)
        {
            target = _target;
            buff = _buff;
        }
        protected override void RunState()
        {
            // Check if target is still available
            if (target.isTargetable)
            {
                battle.SetTimedelay(1.0f);
                switch (buff.statToChange)
                {
                    //case StatEnum.HP:
                    //break;
                    case StatEnum.Attack:
                        target.statMods.Attack += buff.buff;
                        break;
                    case StatEnum.Power:
                        target.statMods.Power += buff.buff;
                        break;
                    case StatEnum.Defence:
                        target.statMods.Defence += buff.buff;
                        break;
                    case StatEnum.Speed:
                        target.statMods.Speed += buff.buff;
                        break;
                    default:
                        break;
                }
                if (buff.buff < 0)
                {
                    Debug.Log(target.creature.Name + "'s " + buff.statToChange.ToString() + " dropped.");
                }
                else if (buff.buff > 0)
                {
                    Debug.Log(target.creature.Name + "'s " + buff.statToChange.ToString() + " increased.");
                }
            }
        }
    }
    //private class BE_ReceiveStatus : BattleEvent
    public class BE_Message : BattleEvent
    {
        string message;
        public BE_Message(BattleManager _battle, string _message) : base(_battle)
        {
            message = _message;
        }
        protected override void RunState()
        {
            battle.SetTimedelay(2.0f);
            Debug.Log(message);
        }
    }
    public class BE_RestoreEnergy : BattleEvent
    {
        private float restoreAmount;
        public float extraAmount = 0;

        public BE_RestoreEnergy(BattleManager _battle) : base(_battle) { }
        protected override void RunState()
        {
            if (battle.sourceBattler.isTargetable)
            {
                battle.SetTimedelay(1.0f);
                restoreAmount = (battle.sourceBattler.creature.Stats.Energy * 0.1f) + extraAmount;

                battle.sourceBattler.creature.currentEnergy += Mathf.CeilToInt(restoreAmount);
                Debug.Log(battle.sourceBattler.creature.Name + " restored some energy. (" + Mathf.CeilToInt(restoreAmount) + ")");
            }
        }
    }
    public class BE_RestoreHP : BattleEvent
    {
        public float restoreAmount;

        public BE_RestoreHP(BattleManager _battle, float _amount) : base(_battle)
        {
            restoreAmount = _amount;
        }
        protected override void RunState()
        {
            if (battle.sourceBattler.isTargetable)
            {
                battle.SetTimedelay(1.0f);
                battle.sourceBattler.creature.currentHP += Mathf.CeilToInt(restoreAmount);
                Debug.Log(battle.sourceBattler.creature.Name + " restored some HP. (" + Mathf.CeilToInt(restoreAmount) + ")");
            }
        }
    }



    // Input and battle progression
    private int currentTurn = 0;
    [SerializeField]
    private TurnState turnState = TurnState.Preturn;
    public TurnState TurnState
    {
        get
        {
            return turnState;
        }
    }

    public bool waitingForInput = true;
    public bool waitingForCommands = true;
    public bool waitingForSubstitute = false;
    
    // Battle Data
    [SerializeField]
    private Battler[] activeBattlers = new Battler[4];
    public Battler[] ActiveBattlers
    {
        get
        {
            return activeBattlers;
        }
    }
    private List<Creature> activeCreatures
    {
        get
        {
            List<Creature> battlers = new List<Creature>();
            foreach (Battler i in activeBattlers)
            {
                battlers.Add(i.creature);
            }
            return battlers;
        }
    }
    private List<BattleEffector> BattleEffectors
    {
        get
        {
            List<BattleEffector> found = new List<BattleEffector>();
            foreach (Battler i in activeBattlers)
            {
                // Get efectors from abilities
                found.Add(i.creature.ActiveAbility.EnforceEffector(i));
                // future: get effectors from items
            }

            return found;
        }
    }

    // Engine fields
    private Battler sourceBattler;
    public Battler SourceBattler
    {
        get
        {
            return sourceBattler;
        }
    }
    private Attack attackBeingUsed;
    public Attack AttackBeingUsed
    {
        get
        {
            return attackBeingUsed;
        }
    }
    private List<BattleEvent> eventQueue = new List<BattleEvent>();
    private BattleEvent currentState = null;

    // Other
    [SerializeField]
    private float timeDelay = 0;
    [SerializeField]
    private float currentWait = 0;

    // Use this for initialization
    void Start()
    {
        activeBattlers[0].owner = Player.Player1;
        activeBattlers[0].position = 0;
        activeBattlers[1].owner = Player.Player1;
        activeBattlers[1].position = 1;
        activeBattlers[2].owner = Player.Player2;
        activeBattlers[2].position = 0;
        activeBattlers[3].owner = Player.Player2;
        activeBattlers[3].position = 1;
    }


    void Update()
    {
        if (currentWait < timeDelay)
        {
            currentWait += Time.deltaTime;
        }
        if (currentWait >= timeDelay)
        {
            TurnUpdate();// Turn Update
            currentWait = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            currentWait = timeDelay;
        }

        foreach (Battler i in activeBattlers)
        {
            if (i.creature.currentHP > i.creature.Stats.HP)
            {
                i.creature.currentHP = (int)i.creature.Stats.HP;
            }
            if (i.creature.currentEnergy > i.creature.Stats.Energy)
            {
                i.creature.currentEnergy = (int)i.creature.Stats.Energy;
            }
        }

        /**/
        string drawing = "";
        for (int i = 0; i < activeBattlers.Length; i++)
        {
            Battler subject = activeBattlers[i];
            if (subject.partyIndex == -1)
            {
                continue;
            }
            if (subject.creature != null)
            {
                drawing += subject.creature.Write();
                drawing += "\tCommand: " + subject.commandedAttack.Name + "(" + subject.currentCommand.moveToUse + ")" + subject.commandedAttack.EnergyCost + " on " + activeBattlers[subject.currentCommand.finalTarget].creature.Name + "(" + subject.currentCommand.finalTarget + ")\n";
            }
        }

        if (FindObjectsOfType<Text>().Where(x => x.name == "DebugText").FirstOrDefault())
        {
            FindObjectsOfType<Text>().Where(x => x.name == "DebugText").FirstOrDefault().text = drawing;
        }
        /**/
    }


    void TurnUpdate()
    {

        switch (turnState)
        {
            case TurnState.Preturn:
                break;

            case TurnState.BeginTurn:

                currentTurn++;
                Debug.Log("Turn " + currentTurn + ".");

                foreach (Battler i in activeBattlers)
                {
                    // Reset flags for battlers
                    i.movedThisTurn = false;
                }

                turnState = TurnState.UpdateTurn;
                break;

            case TurnState.UpdateTurn:
                // Get new State
                if (eventQueue.Count > 0 && currentState == null)
                {
                    currentState = PopTopEvent();
                }
                // Use State
                if (currentState != null)
                {
                    currentState.Run();
                    //Debug.Log(" => Ran Event " + currentState.ToString());
                    // Clear State
                    currentState = null;
                }
                // If no more States
                if (eventQueue.Count == 0 && currentState == null)
                {
                    // Check for next Attacker
                    Battler nextAttacker = GetNextMove();
                    if (nextAttacker != null)
                    {
                        // add next attackers Actions/Events to the Queue
                        // Add Attack!
                        RetargetCheck(nextAttacker);
                        //  eventQueue.Add(new BE_BattlerUseAttack(nextAttacker, nextAttacker.commandedAttack, Actives[nextAttacker.currentCommand.finalTarget]));
                        BattlerTakeTurn(nextAttacker, nextAttacker.commandedAttack);
                        // Set Flags
                        nextAttacker.movedThisTurn = true;
                        // Continue turn
                        turnState = TurnState.UpdateTurn;
                    }
                    else
                    {
                        // If no more Attackers, end the turn
                        turnState = TurnState.EndTurn;
                    }
                }
                
                //QueueDisplay();
                break;

            case TurnState.EndTurn:

                /*foreach (Battler i in Actives)
                {
                    i.currentCommand = null;
                }*/
                timeDelay = 0;
                turnState = TurnState.Preturn;

                break;
        }

    }


    Battler GetNextMove()
    {
        foreach (Battler i in activeBattlers)
        {
            if (i.movedThisTurn == false && i.isTargetable)
            {
                return i;
            }
        }
        return null;
    }

    void BattlerTakeTurn(Battler _source, Attack _attack)
    {
        sourceBattler = _source;
        attackBeingUsed = _attack;
        
        AddEvent(new BE_TakeTurn(this));

        AddEvent(new BE_RestoreEnergy(this));

        //QueueDisplay();
    }


    public void AddEvent(BattleEvent _event)
    {
        eventQueue.Add(_event);
    }
    public void InsertEvent(BattleEvent _event)
    {
        eventQueue.Insert(0,_event);
    }
    BattleEvent PopTopEvent()
    {
        BattleEvent next = null;
        if (eventQueue.Count > 0)
        {
            next = eventQueue[0];
            eventQueue.RemoveAt(0);
        }
        return next;
    }

    void RetargetCheck(Battler battler)
    {
        if (activeBattlers[battler.currentCommand.finalTarget].isTargetable == false)
        {
            if (battler.currentCommand.targetSide != battler.owner)
            {
                switch (battler.currentCommand.targetPosition)
                {
                    case 0:
                        battler.currentCommand.targetPosition = 1;
                        break;
                    case 1:
                        battler.currentCommand.targetPosition = 0;
                        break;
                }
                Debug.Log("(" + battler.creature.Name + "'s target was changed)");
            }
        }
    }

    public void StartTurn()
    {
        if (turnState == TurnState.Preturn)
        {
            turnState = TurnState.BeginTurn;
        }
    }

    public void SetTimedelay(float amount)
    {
        timeDelay = amount;
    }

    void QueueDisplay()
    {
        string output = "";
        foreach (BattleEvent i in eventQueue)
        {
            output += i.ToString();
            output += " , ";
        }
        Debug.Log(output);
    }
}
