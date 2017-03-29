using UnityEngine;
using System.Collections;

public abstract class BattleEffector
{
    public Battler source;
    /*public BattleEffector()
    {
    }*/
    public BattleEffector(Battler _source)
    {
        source = _source;
    }

    public virtual void OnIdle(BattleManager _battle) { }

    public virtual void OnTakeAction(BattleManager.BE_TakeAction _event) { }
    public virtual void OnUseAttack(BattleManager.BE_UseAttack _event) { }
    //public virtual void OnSendAttack(BattleManager.BE_SendAttack _event) { }
    public virtual void OnReceiveAttack(BattleManager.BE_ReceiveAttack _event) { }
    public virtual void OnReceiveDamage(BattleManager.BE_ReceiveDamage _event) { }
    public virtual void OnReceiveBuff(BattleManager.BE_ReceiveBuff _event) { }
    //public virtual void OnReceiveStatus(BattleManager.BE_ReceiveStatus _event) { }

    public virtual void OnRestoreEnergy(BattleManager.BE_RestoreEnergy _event) { }
    public virtual void OnRestoreHP(BattleManager.BE_RestoreHP _event) { }
}
