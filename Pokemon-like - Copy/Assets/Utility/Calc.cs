using UnityEngine;
using System.Collections;

static public class Calc {

    public const float defenceConstant = 200;
    
    public static int Damage3(Battler attacker, iDealDamage attackUsed, Battler target, float[] mods)
    {
        float typeEffectiveness = TypeEffectiveness((attackUsed as Attack).Type, target.creature.Species.types);
        if (typeEffectiveness == 0)
        {
            return 0;
        }
        float attackerPower = 0;// The Attacker Attack/Power times Attack/Power stat mods
        float defenderDefenceMitigation = 0;// The effective health multiplier of the Targets Defence/Resistance
        switch (attackUsed.AttackType)
        {
            case AttackType.Physical:
                attackerPower = attacker.creature.Stats.Attack * attacker.statMods.AttackMultiplier;
                defenderDefenceMitigation = defenceConstant / (defenceConstant + (target.creature.Stats.Defence * target.statMods.DefenceMultiplier));
                break;
            case AttackType.Power:
                attackerPower = attacker.creature.Stats.Power * attacker.statMods.PowerMultiplier;
                defenderDefenceMitigation = defenceConstant / (defenceConstant + (target.creature.Stats.Resistance * target.statMods.ResistanceMultiplier));
                break;
        }
        
        float attackDamage = attackerPower * (attackUsed.AttackPower / 100);// The unmodded damage of the attack

        float farAcc = 1 + ((( (attacker.statMods.Accuracy + 10) * (attackUsed.AttackAccuracy / 100)) - (target.statMods.Evasion + 10)) / 10.0f);
        //Debug.Log("farAcc = " + farAcc);
        float hitMod = Random.Range(Mathf.Min(farAcc, 1), Mathf.Max(farAcc, 1));

        float mod = 1;// Concatinate of other mods
        foreach (float i in mods)
        {
            mod *= i;
        }

        return Mathf.CeilToInt(attackDamage * defenderDefenceMitigation * hitMod * typeEffectiveness * mod);
    }

    public static float TypeEffectiveness(ElementTypes attack, ElementTypes[] defender)
    {
        float effect = 1;
        foreach (ElementTypes i in defender)
        {
            effect *= TypeMatchups.GetMatchup(attack, i);
        }
        return effect;
    }

    public static CreatureStats CalcStats(Creature creature)
    {
        CreatureStats bass = creature.Species.baseStats;
        return new CreatureStats(
            Mathf.Ceil((bass.HP)),
            bass.Energy,
            bass.Attack,
            bass.Power,
            bass.Defence,
            bass.Resistance,
            bass.Speed
            );
    }
}

public static class Util
{
    public static ElementTypes[] DefType(ElementTypes first)
    {
        return new ElementTypes[1] { first };
    }
    public static ElementTypes[] DefTyps(ElementTypes first, ElementTypes second)
    {
        return new ElementTypes[2] { first, second };
    }
}