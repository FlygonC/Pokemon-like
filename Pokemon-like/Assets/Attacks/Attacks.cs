using UnityEngine;
using System.Collections;
using System;

public class Tackle : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Tackle"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 75; } }
    public override ElementTypes Type { get { return ElementTypes.Normal; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 90; } }
}

public class MindBlast : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Mind Blast"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 75; } }
    public override ElementTypes Type { get { return ElementTypes.Normal; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 90; } }
}

public class TailWhip : Attack, iBuffTarget
{
    // Attack
    public override string Name { get { return "Tail Whip"; } }
    public override Targeting Target { get { return Targeting.Double; } }
    public override int EnergyCost { get { return 10; } }
    public override ElementTypes Type{ get { return ElementTypes.Normal; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // Buff Target
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Defence, -1) }; } }
}

public class Splash : Attack, iDealDamage, iBuffTarget
{
    // Attack
    public override string Name { get { return "Splash"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 85; } }
    public override ElementTypes Type { get { return ElementTypes.Water; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 90; } }
    // IBuffTarget
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Speed, -1) }; } }
}

public class HeatBlast : Attack, iDealDamage, iBuffTarget
{
    // Attack
    public override string Name { get { return "Heat Blast"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 85; } }
    public override ElementTypes Type { get { return ElementTypes.Fire; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 90; } }
    // iBufftarget
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Attack, -1) }; } }
}

public class ShadowPunch : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Shadow Punch"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 75; } }
    public override ElementTypes Type { get { return ElementTypes.Ghost; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 100; } }
}

public class Icicle : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Icicle"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 75; } }
    public override ElementTypes Type { get { return ElementTypes.Ice; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 90; } }
}

public class Shock : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Shock"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 80; } }
    public override ElementTypes Type { get { return ElementTypes.Electric; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 80; } }
    public float AttackAccuracy { get { return 100; } }
}

public class Gust : Attack, iDealDamage, iBuffTarget
{
    // Attack
    public override string Name { get { return "Gust"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 85; } }
    public override ElementTypes Type { get { return ElementTypes.Wind; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 100; } }
    // iBufftarget
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Evasion, -1) }; } }
}

public class Absorb : Attack, iDealDamage, iStealHP
{
    // Attack
    public override string Name { get { return "Absorb"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 65; } }
    public override ElementTypes Type { get { return ElementTypes.Plant; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 50; } }
    public float AttackAccuracy { get { return 90; } }
    // iStealHP
    public float Ratio { get { return 0.5f; } }
}

public class AuraBlast : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Aura Blast"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 80; } }
    public override ElementTypes Type { get { return ElementTypes.Pulse; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Power; } }
    public float AttackPower { get { return 80; } }
    public float AttackAccuracy { get { return 90; } }
}

public class Sting : Attack, iDealDamage, iBuffTarget
{
    // Attack
    public override string Name { get { return "Sting"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 85; } }
    public override ElementTypes Type { get { return ElementTypes.Poison; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 75; } }
    public float AttackAccuracy { get { return 90; } }
    // iBufftarget
    public StatBuff[] BuffTarget { get { return new StatBuff[1] { new StatBuff(StatEnum.Power, -1) }; } }
}

public class RockThrow : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Rock Throw"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 75; } }
    public override ElementTypes Type { get { return ElementTypes.Pulse; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 80; } }
    public float AttackAccuracy { get { return 80; } }
}

public class StrikePunch : Attack, iDealDamage
{
    // Attack
    public override string Name { get { return "Strike Punch"; } }
    public override Targeting Target { get { return Targeting.Other; } }
    public override int EnergyCost { get { return 80; } }
    public override ElementTypes Type { get { return ElementTypes.Force; } }
    public override AttackNature Nature { get { return AttackNature.Harmful; } }
    // iDealDamage
    public AttackType AttackType { get { return AttackType.Physical; } }
    public float AttackPower { get { return 80; } }
    public float AttackAccuracy { get { return 90; } }
}