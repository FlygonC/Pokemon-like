using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour {

    public int findBattler;
    public Battler subject
    {
        get
        {
            return FindObjectOfType<BattleManager>().ActiveBattlers[findBattler];
        }
    }

    public float fillAmount = 0;
    public float barMove;

    [SerializeField]
    private Sprite greenbar;
    [SerializeField]
    private Sprite yellowBar;
    [SerializeField]
    private Sprite redBar;

    private Image hpBar;

	// Use this for initialization
	void Start ()
    {
        hpBar = GetComponentsInChildren<Image>()[1];
        barMove = 1;
        fillAmount = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (subject.isTargetable)
        {
            barMove = subject.creature.currentHP / subject.creature.Stats.HP;
            if (fillAmount > barMove)
            {
                fillAmount -= Mathf.Min(0.01f, fillAmount - barMove);
            }
            if (fillAmount < barMove)
            {
                fillAmount += Mathf.Min(0.01f, barMove - fillAmount);
                //fillAmount = barMove;
            }

            if (fillAmount > 1)
            {
                fillAmount = 1;
            }

            // Color change
            hpBar.fillAmount = fillAmount;
            if (fillAmount > 0.5f)
            {
                hpBar.sprite = greenbar;
            }
            else
            {
                if (fillAmount <= 0.5f)
                {
                    hpBar.sprite = yellowBar;
                }
                if (fillAmount <= 0.25f)
                {
                    hpBar.sprite = redBar;
                }
            }
        }
	}
}
