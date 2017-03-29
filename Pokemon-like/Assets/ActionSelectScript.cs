using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionSelectScript : MonoBehaviour {

    public int findBattler;
    public Battler subject
    {
        get
        {
            return FindObjectOfType<BattleManager>().ActiveBattlers[findBattler];
        }
    }

    private BattleManager battle;

    [SerializeField]
    private Button[] AttackButtons;
    [SerializeField]
    private Button[] TargetButtons;
    [SerializeField]
    private Text DescribeText;

    // Use this for initialization
    void Start () {
        battle = FindObjectOfType<BattleManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < 4; i++)
        {
            if (i < subject.creature.Attacks.Count)
            {
                AttackButtons[i].interactable = true;
                AttackButtons[i].GetComponentInChildren<Text>().text = subject.creature.Attacks[i].Name;
            }
            else
            {
                AttackButtons[i].interactable = false;
                AttackButtons[i].GetComponentInChildren<Text>().text = "";
            }

            if (battle.ActiveBattlers[i].isTargetable)
            {
                TargetButtons[i].GetComponentInChildren<Text>().text = battle.ActiveBattlers[i].creature.Name;
                if (subject.commandedAttack.Target == Targeting.Other && subject == battle.ActiveBattlers[i])
                {
                    TargetButtons[i].interactable = false;
                    //TargetButtons[i].GetComponentInChildren<Text>().text = battle.ActiveBattlers[i].creature.Name;
                }
                else
                {
                    TargetButtons[i].interactable = true;
                }
            }
            else
            {
                TargetButtons[i].interactable = false;
                TargetButtons[i].GetComponentInChildren<Text>().text = "";
            }
        }

        DescribeText.text = subject.creature.Name + ", use " + subject.commandedAttack.Name + " on " + battle.ActiveBattlers[subject.currentCommand.finalTarget].creature.Name + ".";
	}

    public void SetAttack(int _att)
    {
        subject.currentCommand.moveToUse = _att;
    }
    public void SetTargetPosition(int _att)
    {
        subject.currentCommand.targetPosition = _att;
    }
    public void SetTargetSide(int _att)
    {
        subject.currentCommand.targetSide = (Player)_att;
    }
}
