using UnityEngine;
using System.Collections;

public enum StatEnum
{
    HP = 0,
    Energy,
    Attack,
    Power,
    Defence,
    Resistance,
    Speed,
}

[System.Serializable]
public class CreatureStats
{    
    public float[] stats = new float[7];

    public CreatureStats(float _hp, float _energy, float _attack, float _power, float _defence, float _resistance, float _speed)
    {
        stats[0] = _hp;
        stats[1] = _energy;
        stats[2] = _attack;
        stats[3] = _power;
        stats[4] = _defence;
        stats[5] = _resistance;
        stats[6] = _speed;
    }

    public float HP
    {
        get { return stats[(int)StatEnum.HP]; }
        set { stats[(int)StatEnum.HP] = value; }
    }
    public float Energy
    {
        get { return stats[(int)StatEnum.Energy]; }
        set { stats[(int)StatEnum.Energy] = value; }
    }
    public float Attack
    {
        get { return stats[(int)StatEnum.Attack]; }
        set { stats[(int)StatEnum.Attack] = value; }
    }
    public float Power
    {
        get { return stats[(int)StatEnum.Power]; }
        set { stats[(int)StatEnum.Power] = value; }
    }
    public float Defence
    {
        get { return stats[(int)StatEnum.Defence]; }
        set { stats[(int)StatEnum.Defence] = value; }
    }
    public float Resistance
    {
        get { return stats[(int)StatEnum.Resistance]; }
        set { stats[(int)StatEnum.Resistance] = value; }
    }
    public float Speed
    {
        get { return stats[(int)StatEnum.Speed]; }
        set { stats[(int)StatEnum.Speed] = value; }
    }
}

[System.Serializable]
public class StatMods
{
    [Range(-9, 9)]
    [SerializeField]
    private int attack = 0;
    [Range(-9, 9)]
    [SerializeField]
    private int power = 0;
    [Range(-9, 9)]
    [SerializeField]
    private int defence = 0;
    [Range(-9, 9)]
    [SerializeField]
    private int resistance = 0;
    [Range(-9, 9)]
    [SerializeField]
    private int speed = 0;

    [Range(-9, 9)]
    [SerializeField]
    private int accuaracy = 0;
    [Range(-9, 9)]
    [SerializeField]
    private int evasion = 0;

    public float AttackMultiplier
    {
        get
        {
            return 1 + (attack / 10.0f);
        }
    }
    public float PowerMultiplier
    {
        get
        {
            return 1 + (power / 10.0f);
        }
    }
    public float DefenceMultiplier
    {
        get
        {
            return 1 + (defence / 10.0f);
        }
    }
    public float ResistanceMultiplier
    {
        get
        {
            return 1 + (resistance / 10.0f);
        }
    }
    public float SpeedMultiplier
    {
        get
        {
            return 1 + (speed / 10.0f);
        }
    }

    public int Attack
    {
        get { return attack; }
        set
        {
            attack = Mathf.Clamp(value, -9, 9);
        }
    }
    public int Power
    {
        get { return power; }
        set
        {
            power = Mathf.Clamp(value, -9, 9);
        }
    }
    public int Defence
    {
        get { return defence; }
        set
        {
            defence = Mathf.Clamp(value, -9, 9);
        }
    }
    public int Resistance
    {
        get { return resistance; }
        set
        {
            resistance = Mathf.Clamp(value, -9, 9);
        }
    }
    public int Speed
    {
        get { return speed; }
        set
        {
            speed = Mathf.Clamp(value, -9, 9);
        }
    }

    public int Accuracy
    {
        get { return accuaracy; }
        set
        {
            accuaracy = Mathf.Clamp(value, -9, 9);
        }
    }
    public int Evasion
    {
        get { return evasion; }
        set
        {
            evasion = Mathf.Clamp(value, -9, 9);
        }
    }
}