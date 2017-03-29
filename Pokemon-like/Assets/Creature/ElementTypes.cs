using UnityEngine;
using System.Collections;

public enum ElementTypes : int
{
    Normal = 0,
    Water,
    Fire,
    Wind,
    Plant,
    Pulse,
    Poison,
    Rock,
    Electric,
    Ice,
    Force,
    Ghost,
}

public static class TypeMatchups
{
    const float Immune = 0;
    const float Normal = 1;
    const float SuperEffective = 2;
    const float Weak = 0.5f;

    static float[,] MatchupMatrix = new float[(int)ElementTypes.Ghost + 1, (int)ElementTypes.Ghost + 1];
    static void SetMatchup(ElementTypes attacker, ElementTypes defender, float effectivness)
    {
        MatchupMatrix[(int)attacker, (int)defender] = effectivness;
    }
    public static float GetMatchup(ElementTypes attacker, ElementTypes defender)
    {
        return MatchupMatrix[(int)attacker, (int)defender];
    }

    public static void SetupTypeMatchups()
    {
        Debug.Log("Loading Type Matchup Matrix...");
        for (int i = 0; i <= (int)ElementTypes.Ghost; i++)
        {
            for (int j = 0; j <= (int)ElementTypes.Ghost; j++)
            {
                MatchupMatrix[i, j] = 1;
            }
        }
        // TODO
        // NORMAL
        SetMatchup(ElementTypes.Normal, ElementTypes.Rock, Weak);
        SetMatchup(ElementTypes.Normal, ElementTypes.Force, Weak);
        SetMatchup(ElementTypes.Normal, ElementTypes.Ghost, Weak);
        //Beat by Poison
        //Beat by Ghost
        // WATER
        SetMatchup(ElementTypes.Water, ElementTypes.Fire, SuperEffective);
        SetMatchup(ElementTypes.Water, ElementTypes.Rock, SuperEffective);
        SetMatchup(ElementTypes.Water, ElementTypes.Ghost, SuperEffective);
        SetMatchup(ElementTypes.Water, ElementTypes.Water, Weak);
        SetMatchup(ElementTypes.Water, ElementTypes.Plant, Weak);
        SetMatchup(ElementTypes.Water, ElementTypes.Pulse, Weak);
        //Beat by Wind
        //Beat by Plant
        //Beat by Electric
        //Strong to Fire
        //Strong to Force
        // FIRE
        SetMatchup(ElementTypes.Fire, ElementTypes.Plant, SuperEffective);
        SetMatchup(ElementTypes.Fire, ElementTypes.Ice, SuperEffective);
        SetMatchup(ElementTypes.Fire, ElementTypes.Ghost, SuperEffective);
        SetMatchup(ElementTypes.Fire, ElementTypes.Fire, Weak);
        SetMatchup(ElementTypes.Fire, ElementTypes.Water, Weak);
        SetMatchup(ElementTypes.Fire, ElementTypes.Pulse, Weak);
        SetMatchup(ElementTypes.Fire, ElementTypes.Rock, Weak);
        //Beat by Water
        //Beat by Rock
        //Strong to Ice
        // WIND
        SetMatchup(ElementTypes.Wind, ElementTypes.Water, SuperEffective);
        SetMatchup(ElementTypes.Wind, ElementTypes.Fire, SuperEffective);
        SetMatchup(ElementTypes.Wind, ElementTypes.Plant, SuperEffective);
        SetMatchup(ElementTypes.Wind, ElementTypes.Pulse, Weak);
        SetMatchup(ElementTypes.Wind, ElementTypes.Rock, Weak);
        // PLANT
        SetMatchup(ElementTypes.Plant, ElementTypes.Water, SuperEffective);
        SetMatchup(ElementTypes.Plant, ElementTypes.Rock, SuperEffective);
        SetMatchup(ElementTypes.Plant, ElementTypes.Plant, Weak);
        SetMatchup(ElementTypes.Plant, ElementTypes.Pulse, Weak);
        // PULSE
        SetMatchup(ElementTypes.Pulse, ElementTypes.Ghost, SuperEffective);
        //Strong to Water, Fire, Wind, Plant
        // POISON
        SetMatchup(ElementTypes.Poison, ElementTypes.Normal, SuperEffective);
        SetMatchup(ElementTypes.Poison, ElementTypes.Plant, SuperEffective);
        SetMatchup(ElementTypes.Poison, ElementTypes.Force, SuperEffective);
        SetMatchup(ElementTypes.Poison, ElementTypes.Poison, Weak);
        SetMatchup(ElementTypes.Poison, ElementTypes.Pulse, Weak);
        SetMatchup(ElementTypes.Poison, ElementTypes.Rock, Weak);
        // ROCK
        SetMatchup(ElementTypes.Rock, ElementTypes.Fire, SuperEffective);
        SetMatchup(ElementTypes.Rock, ElementTypes.Poison, SuperEffective);
        SetMatchup(ElementTypes.Rock, ElementTypes.Force, Weak);
        SetMatchup(ElementTypes.Rock, ElementTypes.Ghost, Weak);
        // ELECTRIC
        SetMatchup(ElementTypes.Electric, ElementTypes.Water, SuperEffective);
        SetMatchup(ElementTypes.Electric, ElementTypes.Wind, SuperEffective);
        SetMatchup(ElementTypes.Electric, ElementTypes.Electric, Weak);
        SetMatchup(ElementTypes.Electric, ElementTypes.Plant, Weak);
        SetMatchup(ElementTypes.Electric, ElementTypes.Rock, Weak);
        // ICE
        SetMatchup(ElementTypes.Ice, ElementTypes.Pulse, SuperEffective);
        SetMatchup(ElementTypes.Ice, ElementTypes.Poison, SuperEffective);
        SetMatchup(ElementTypes.Ice, ElementTypes.Electric, SuperEffective);
        SetMatchup(ElementTypes.Ice, ElementTypes.Ghost, SuperEffective);
        SetMatchup(ElementTypes.Ice, ElementTypes.Ice, Weak);
        SetMatchup(ElementTypes.Ice, ElementTypes.Fire, Weak);
        SetMatchup(ElementTypes.Ice, ElementTypes.Rock, Weak);
        // FORCE
        SetMatchup(ElementTypes.Force, ElementTypes.Rock, SuperEffective);
        SetMatchup(ElementTypes.Force, ElementTypes.Ice, SuperEffective);
        SetMatchup(ElementTypes.Force, ElementTypes.Water, Weak);
        SetMatchup(ElementTypes.Force, ElementTypes.Ghost, Weak);
        // GHOST
        SetMatchup(ElementTypes.Ghost, ElementTypes.Normal, SuperEffective);
        SetMatchup(ElementTypes.Ghost, ElementTypes.Pulse, SuperEffective);
        SetMatchup(ElementTypes.Ghost, ElementTypes.Force, SuperEffective);
        SetMatchup(ElementTypes.Ghost, ElementTypes.Electric, SuperEffective);
        SetMatchup(ElementTypes.Ghost, ElementTypes.Fire, Weak);
        SetMatchup(ElementTypes.Ghost, ElementTypes.Rock, Weak);
        SetMatchup(ElementTypes.Ghost, ElementTypes.Ice, Weak);
    }
}
