using UnityEngine;
using System.Collections;
using System;

public class Tackle : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Tackle"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 50; } }
    public override ElementTypes Type { get { return ElementTypes.Normal; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 50; } }
    public float AttackAccuracy { get { return 90; } }
}

public class MindBlast : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Mind Blast"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 50; } }
    public override ElementTypes Type { get { return ElementTypes.Normal; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 50; } }
    public float AttackAccuracy { get { return 90; } }
}

public class TailWhip : Attack, iBuffTarget
{
    // Attack
    public override string Name { get { return "Tail Whip"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 10; } }
    public override ElementTypes Type{ get { return ElementTypes.Normal; } }
    // Buff Target
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Defence, -1) }; } }
}

public class Splash : Attack, iDealDamage, iBuffTarget
{
    // Attack
    public override string Name { get { return "Splash"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 60; } }
    public override ElementTypes Type { get { return ElementTypes.Water; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 50; } }
    public float AttackAccuracy { get { return 90; } }
    // IBuffTarget
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Speed, -1) }; } }
}

public class HeatBlast : Attack, iDealDamage, iBuffTarget
{
    // Attack
    public override string Name { get { return "Heat Blast"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 60; } }
    public override ElementTypes Type { get { return ElementTypes.Fire; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 50; } }
    public float AttackAccuracy { get { return 90; } }
    // iBufftarget
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Attack, -1) }; } }
}

public class ShadowPunch : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Shadow Punch"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 50; } }
    public override ElementTypes Type { get { return ElementTypes.Ghost; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 50; } }
    public float AttackAccuracy { get { return 100; } }
}

public class Icicle : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Icicle"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 50; } }
    public override ElementTypes Type { get { return ElementTypes.Ice; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 50; } }
    public float AttackAccuracy { get { return 90; } }
}

public class Shock : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Shock"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 65; } }
    public override ElementTypes Type { get { return ElementTypes.Electric; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 65; } }
    public float AttackAccuracy { get { return 100; } }
}