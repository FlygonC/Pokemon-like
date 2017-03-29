using UnityEngine;
using System.Collections;

public class PowerLevelReader : MonoBehaviour {

    public SpeciesEnum subject;
    public float powerLevel = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CreatureStats data = GameData.speciesIndex[subject].baseStats;

        powerLevel = data.HP + data.Energy + Mathf.Max(data.Attack, data.Power) + data.Defence + data.Resistance + data.Speed;
	}
}
