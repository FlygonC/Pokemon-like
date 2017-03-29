using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpeciesEnum : int
{
    Formaea = 0,
    Aquamaea,
    Scorchmaea,
    Auramaea,
    Shockmaea,
    Frostmaea,
    Grimaea,
}

public enum AttackEnum : int
{
    Tackle = 0,
    MindBlast,
    TailWhip,
    Splash,
    HeatBlast,
    ShadowPunch,
    Icicle,
    Shock,
}

static public class GameData {

    static public Dictionary<SpeciesEnum, Species> speciesIndex = new Dictionary<SpeciesEnum, Species>();

    static public Dictionary<AttackEnum, Attack> attacksIndex = new Dictionary<AttackEnum, Attack>();


    static public void LoadSpecies()
    {
        speciesIndex.Clear();
        Debug.Log("Loading Species Dictionary...");
        speciesIndex.Add(SpeciesEnum.Formaea,       new Species("Formaea",      Util.DefType(ElementTypes.Normal),      new CreatureStats(100, 100, 100, 100, 100, 100, 100),   new Ability[1] { new Determined() }));
        speciesIndex.Add(SpeciesEnum.Aquamaea,      new Species("Aquamaea",     Util.DefType(ElementTypes.Water),       new CreatureStats(130, 100, 60, 110, 80, 90, 90),       new Ability[1] { new FastHealer() }));
        speciesIndex.Add(SpeciesEnum.Scorchmaea,    new Species("Scorchmaea",   Util.DefType(ElementTypes.Fire),        new CreatureStats(90, 120, 105, 110, 80, 80, 115),      new Ability[1] { new Adrenaline() }));
        speciesIndex.Add(SpeciesEnum.Auramaea,      new Species("Auramaea",     Util.DefType(ElementTypes.Pulse),       new CreatureStats(110, 100, 100, 100, 90, 105, 100),    new Ability[2] { new Determined(), new PureAura() }));
        speciesIndex.Add(SpeciesEnum.Shockmaea,     new Species("Shockmaea",    Util.DefType(ElementTypes.Electric),    new CreatureStats(85, 120, 100, 100, 85, 85, 125),      new Ability[1] { new Battery() }));
        speciesIndex.Add(SpeciesEnum.Frostmaea,     new Species("Frostmaea",    Util.DefType(ElementTypes.Ice),         new CreatureStats(100, 100, 115, 115, 100, 95, 90),     new Ability[1] { new FrostArmor() }));
        speciesIndex.Add(SpeciesEnum.Grimaea,       new Species("Grimaea",      Util.DefType(ElementTypes.Ghost),       new CreatureStats(100, 100, 120, 120, 80, 80, 100),     new Ability[1] { new DreadAura() }));
    }

    static public void LoadAttacks()
    {
        attacksIndex.Clear();
        Debug.Log("Loading Attacks Dictionary...");
        attacksIndex.Add(AttackEnum.Tackle,     new Tackle());
        attacksIndex.Add(AttackEnum.MindBlast,  new MindBlast());
        attacksIndex.Add(AttackEnum.TailWhip,   new TailWhip());
        attacksIndex.Add(AttackEnum.Splash,     new Splash());
        attacksIndex.Add(AttackEnum.HeatBlast,  new HeatBlast());
        attacksIndex.Add(AttackEnum.ShadowPunch, new ShadowPunch());
        attacksIndex.Add(AttackEnum.Icicle,     new Icicle());
        attacksIndex.Add(AttackEnum.Shock,      new Shock());
    }
}
