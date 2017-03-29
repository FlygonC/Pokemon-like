using UnityEngine;
using System.Collections;

public enum AttackType { Physical, Power };
public enum Targeting { Any, Self, Other, All, SelfOrAlly, Ally, Foe, AllFoes}

public struct StatBuff
{
    public StatEnum statToChange;
    public int buff;
    public StatBuff(StatEnum _toChange, int _buff)
    {
        statToChange = _toChange;
        buff = _buff;
    }
}

public abstract class Attack
{
    public abstract string Name { get; }
    public abstract Targeting Target { get; }
    public abstract int EnergyCost { get; }
    public abstract ElementTypes Type { get; }
}

public interface iDealDamage
{
    AttackType AttackType { get; }
    float AttackPower { get; }
    float AttackAccuracy { get; }
}

public interface iBuffTarget
{
    StatBuff[] BuffTarget { get; }
}