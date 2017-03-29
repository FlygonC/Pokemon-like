using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Creature
{
    // Defining fields
    [SerializeField]
    private SpeciesEnum speciesID;
    [SerializeField]
    private string nickname;
    [SerializeField]
    private List<AttackEnum> attacksIDs = new List<AttackEnum>();
    [SerializeField]
    private int abilityIndex;
    // Data Proprties
    public Species Species
    {
        get
        {
            return GameData.speciesIndex[speciesID];
        }
    }
    public CreatureStats Stats
    {
        get { return Calc.CalcStats(this); }
    }
    public ElementTypes[] Types
    {
        get
        {
            return Species.types;
        }
    }
    public List<Attack> Attacks
    {
        get
        {
            List<Attack> ret = new List<Attack>();
            foreach (AttackEnum i in attacksIDs)
            {
                ret.Add(GameData.attacksIndex[i]);
            }
            return ret;
        }
    }
    public Ability ActiveAbility
    {
        get
        {
            return Species.possibleAbilities[Mathf.Min(abilityIndex, Species.possibleAbilities.Length - 1)];
        }
    }
    public string Name
    {
        get
        {
            if (nickname.Length == 0)
            {
                return Species.name;
            }
            return nickname;
        }
    }

    // Active fields
    public int currentHP;
    public int currentEnergy;
    // public HeldItem

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Clamp(currentHP - amount, 0, (int)Stats.HP);
    }

    public string Write()
    {
        string ret = Name;
        ret += ": HP: " + currentHP + " /" + Stats.HP + " EN: " + currentEnergy + "/" + Stats.Energy + "  " + Stats.Attack + "/" + Stats.Power + "/" + Stats.Defence + "/" + Stats.Resistance + "/" + Stats.Speed + "\n";
        return ret;
    }
}
