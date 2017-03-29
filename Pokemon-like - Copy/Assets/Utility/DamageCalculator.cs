using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DamageCalculator : MonoBehaviour {

    [System.Serializable]
    public struct Battler
    {
        public float attackstrength;
        public float defence;
    }
    [System.Serializable]
    public struct Attack
    {
        public float power;
    }

    public Battler attacker;
    public Attack attack;
    public Battler defender;

    public float damageDealt;

    public float other;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // current
        //damageDealt = (attacker.attackstrength * (attack.power / 100)) - (defender.defence * 0.5f);

        //damageDealt = (attacker.attackstrength / (defender.defence)) * attack.power;

        damageDealt = (attacker.attackstrength * (attack.power / 100)) * (Calc.defenceConstant / (Calc.defenceConstant + defender.defence));

        other = (100 / (100 + attacker.defence));
	}
}
