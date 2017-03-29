using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Species
{
    public readonly string name;
    public readonly CreatureStats baseStats;
    public readonly ElementTypes[] types;
    public readonly Ability[] possibleAbilities;
    // Moves can learn

    public Species(string _name, ElementTypes[] _types, CreatureStats _stats, Ability[] _abilities)
    {
        name = _name;
        types = _types;
        baseStats = _stats;
        possibleAbilities = _abilities;
    }
}
