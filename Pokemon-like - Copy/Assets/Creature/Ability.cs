using UnityEngine;
using System.Collections;

public abstract class Ability
{
    /*protected Battler owner;
    protected Ability(Battler _owner)
    {
        owner = _owner;
    }*/
    public abstract BattleEffector EnforceEffector(Battler _source);
}

public class Determined : Ability
{
    public class Effect : BattleEffector
    {
        public Effect(Battler _source) : base (_source) { }

        public override void OnReceiveDamage(BattleManager.BE_ReceiveDamage _event)
        {
            if (_event.target == source)
            {
                float reduction = _event.target.creature.currentHP / _event.target.creature.Stats.HP;
                reduction = 0.5f + (0.5f * reduction);
                _event.finalDmgMods.Add(reduction);
                Debug.Log(_event.target.creature.Name + "'s Determined reduced damage to x" + reduction);
            }
        }
    }

    public override BattleEffector EnforceEffector(Battler _source)
    {
        return new Effect(_source);
    }
}

public class FrostArmor : Ability
{
    public class Effect : BattleEffector
    {
        public Effect(Battler _source) : base(_source) { }

        public override void OnReceiveDamage(BattleManager.BE_ReceiveDamage _event)
        {
            if (_event.battle.AttackBeingUsed is iDealDamage)
            {
                if (_event.target == source && (_event.battle.AttackBeingUsed as iDealDamage).AttackType == AttackType.Physical)
                {
                    _event.finalDmgMods.Add(0.75f);
                    Debug.Log(_event.target.creature.Name + "'s Frost Armor reduces physical damage!");
                }
            }
        }
    }

    public override BattleEffector EnforceEffector(Battler _source)
    {
        return new Effect(_source);
    }
}

public class PureAura : Ability
{
    public class Effect : BattleEffector
    {
        public Effect(Battler _source) : base(_source) { }

        public override void OnUseAttack(BattleManager.BE_UseAttack _event)
        {
            if (_event.battle.AttackBeingUsed.Type == ElementTypes.Ghost)
            {
                _event.PreventEvent(_event.battle.SourceBattler.creature.Name + " can't use " + _event.battle.AttackBeingUsed.Name + " because of " + source.creature.Name + "'s Pure Aura!");
            }
        }
    }

    public override BattleEffector EnforceEffector(Battler _source)
    {
        return new Effect(_source);
    }
}

public class Adrenaline : Ability
{
    public class Effect : BattleEffector
    {
        public Effect(Battler _source) : base(_source) { }

        public override void OnRestoreEnergy(BattleManager.BE_RestoreEnergy _event)
        {
            if (_event.battle.SourceBattler == source)
            {
                _event.extraAmount = _event.battle.SourceBattler.creature.Stats.Energy * 0.1f;
                Debug.Log(_event.battle.SourceBattler.creature.Name + " restored extra energy due to its Adrenaline.");
            }
        }
    }

    public override BattleEffector EnforceEffector(Battler _source)
    {
        return new Effect(_source);
    }
}

public class FastHealer : Ability
{
    public class Effect : BattleEffector
    {
        public Effect(Battler _source) : base(_source) { }

        public override void OnTakeTurn(BattleManager.BE_TakeTurn _event)
        {
            if (_event.battle.SourceBattler == source)
            {
                _event.battle.AddEvent(new BattleManager.BE_RestoreHP(_event.battle, 10.0f));
                Debug.Log(_event.battle.SourceBattler.creature.Name + "'s FastHealer!");
            }
        }
    }

    public override BattleEffector EnforceEffector(Battler _source)
    {
        return new Effect(_source);
    }
}

public class DreadAura : Ability
{
    public class Effect : BattleEffector
    {
        public Effect(Battler _source) : base(_source) { }

        public override void OnRestoreHP(BattleManager.BE_RestoreHP _event)
        {
            if (_event.battle.SourceBattler.owner != source.owner)
            {
                _event.PreventEvent(_event.battle.SourceBattler.creature.Name + " can't restore HP because of " + source.creature.Name + "'s Dread Aura!");
            }
        }
    }

    public override BattleEffector EnforceEffector(Battler _source)
    {
        return new Effect(_source);
    }
}

public class Battery : Ability
{
    public class Effect : BattleEffector
    {
        public Effect(Battler _source) : base(_source) { }

        public override void OnTakeTurn(BattleManager.BE_TakeTurn _event)
        {
            if (_event.battle.SourceBattler == source)
            {
                _event.energyDiscount += 0.25f;
                Debug.Log(source.creature.Name + "'s Battery reduces energy cost of attacks.");
            }
        }
    }

    public override BattleEffector EnforceEffector(Battler _source)
    {
        return new Effect(_source);
    }
}