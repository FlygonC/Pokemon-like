using UnityEngine;
using System.Collections;

public enum BattleState { IdleTime, DoingActions, }

public class BattleManager2 : MonoBehaviour {

    [SerializeField]
    private Battler2[] activeBattlers;
    public Battler2[] ActiveBattlers
    {
        get
        {
            return activeBattlers;
        }
    }

    [SerializeField]
    private BattleState battleState = BattleState.IdleTime;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (battleState)
        {
            case BattleState.IdleTime:

                foreach (Battler2 i in activeBattlers)
                {
                    i.UpdateInTime(Time.deltaTime);
                }

                break;
            case BattleState.DoingActions:
                break;
            default:
                break;
        }
    }
}
