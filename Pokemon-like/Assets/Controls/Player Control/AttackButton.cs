using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {
    
    public Battler PickBattler;

    [SerializeField]
    private Button[] Buttons = new Button[4];

    public PlayerController SourceController;

    // Update is called once per frame
    void Update () {

        for (int i = 0; i < 4; i++)
        {
            if (i < PickBattler.creature.Attacks.Count)
            {
                Buttons[i].interactable = true;
                Buttons[i].GetComponentInChildren<Text>().text = PickBattler.creature.Attacks[i].Name;
            }
            else
            {
                Buttons[i].interactable = false;
                Buttons[i].GetComponentInChildren<Text>().text = "";
            }
        }
    }

    public void Select(int pick)
    {
        SourceController.PickAttack(pick);
    }
}
