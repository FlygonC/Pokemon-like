using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum PlayerControlSteps { PickBattler, PickAttack, PickTarget, };

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private BattleManager Battle;
    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private PlayerControlSteps CurrentStep = PlayerControlSteps.PickBattler;

    [SerializeField]
    private Battler SelectedBattler = null;
    [SerializeField]
    private int SelectedAttack = 0;

    [SerializeField]
    private BattlerSelector AllySelectorPrefab;
    [SerializeField]
    private AttackButton MoveSelector;
    [SerializeField]
    private TargetSelector TargetSelectorPrefab;

    // Use this for initialization
    void Start ()
    {
        Refresh();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //RefreshQuick();
	    if (Input.GetButtonDown("Fire2"))
        {
            CancelAll();
        }
	}

    void BreakDown()
    {
        foreach (BattlerSelector i in FindObjectsOfType<BattlerSelector>())
        {
            Destroy(i.gameObject);
        }
        foreach (AttackButton i in FindObjectsOfType<AttackButton>())
        {
            Destroy(i.gameObject);
        }
        foreach (TargetSelector i in FindObjectsOfType<TargetSelector>())
        {
            Destroy(i.gameObject);
        }
    }
    public void Refresh()
    {
        BreakDown();

        switch (CurrentStep)
        {
            case PlayerControlSteps.PickBattler:
                
                foreach (Battler i in Battle.ActiveBattlers)
                {
                    if (i.owner == Player.Player1 && i.isTargetable /*&& i.preparingAction == false*/)
                    {
                        BuildAllySelectButton(i);
                    }
                }

                break;
            case PlayerControlSteps.PickAttack:

                if (SelectedBattler != null)
                {
                    AttackButton newButton = Instantiate(MoveSelector);
                    newButton.transform.SetParent(MainCanvas.transform);
                    newButton.transform.position = Camera.main.WorldToScreenPoint(SelectedBattler.transform.position);

                    newButton.SourceController = this;
                    newButton.PickBattler = SelectedBattler;
                }

                break;
            case PlayerControlSteps.PickTarget:

                foreach (Battler i in ApplicableTargets(SelectedBattler.creature.Attacks[SelectedAttack].Target))
                {
                    if (i.isTargetable)// need to check applicability
                    {
                        TargetSelector newButton = Instantiate(TargetSelectorPrefab);
                        newButton.transform.SetParent(MainCanvas.transform);
                        newButton.transform.position = Camera.main.WorldToScreenPoint(i.transform.position);

                        newButton.SourceController = this;
                        newButton.pick = i;
                    }
                }

                break;
            default:
                break;
        }
    }
    public void RefreshQuick()
    {
        if (CurrentStep == PlayerControlSteps.PickBattler)
        {
            BreakDown();

            foreach (Battler i in Battle.ActiveBattlers)
            {
                if (i.owner == Player.Player1 && i.isTargetable /*&& i.preparingAction == false*/)
                {
                    BuildAllySelectButton(i);
                }
            }
        }
    }

    public void CancelAll()
    {
        CurrentStep = PlayerControlSteps.PickBattler;
        SelectedBattler = null;

        Refresh();
    }

    public void PickBattler(Battler picked)
    {
        SelectedBattler = picked;
        CurrentStep = PlayerControlSteps.PickAttack;

        Refresh();
    }
    public void PickAttack(int picked)
    {
        SelectedAttack = picked;
        CurrentStep = PlayerControlSteps.PickTarget;

        Refresh();
    }
    public void PickTarget(Battler picked)
    {
        Command madeCom = new Command();
        madeCom.moveToUse = SelectedAttack;
        madeCom.targetSide = picked.owner;
        madeCom.targetPosition = picked.position;

        SelectedBattler.CommitCommand(madeCom);

        CurrentStep = PlayerControlSteps.PickBattler;

        Refresh();
    }

    public List<Battler> ApplicableTargets(Targeting _target)
    {
        List<Battler> returnList = new List<Battler>();

        switch (_target)
        {
            case Targeting.Any:
                foreach (Battler i in Battle.ActiveBattlers)
                {
                    if (i.isTargetable)
                    {
                        returnList.Add(i);
                    }
                }
                break;
                
            case Targeting.Other:
                foreach (Battler i in Battle.ActiveBattlers)
                {
                    if (i.isTargetable && i != SelectedBattler)
                    {
                        returnList.Add(i);
                    }
                }
                break;
                
            case Targeting.Ally:
                foreach (Battler i in Battle.ActiveBattlers)
                {
                    if (i.isTargetable && i.owner == SelectedBattler.owner)
                    {
                        returnList.Add(i);
                    }
                }
                break;

            case Targeting.Foe:
            case Targeting.Double:
                foreach (Battler i in Battle.ActiveBattlers)
                {
                    if (i.isTargetable && i.owner != SelectedBattler.owner)
                    {
                        returnList.Add(i);
                    }
                }
                break;

            default:
                foreach (Battler i in Battle.ActiveBattlers)
                {
                    if (i.isTargetable)
                    {
                        returnList.Add(i);
                    }
                }
                break;
        }

        return returnList;
    }

    void BuildAllySelectButton(Battler _target)
    {
        BattlerSelector newButton = Instantiate(AllySelectorPrefab);
        newButton.transform.SetParent(MainCanvas.transform);
        newButton.transform.position = Camera.main.WorldToScreenPoint(_target.transform.position + new Vector3(0, 1, 0));

        if (_target.preparingAction)
        {
            newButton.GetComponent<Image>().color = Color.yellow - new Color(0, 0, 0, 0.5f);
        }
        else
        {
            newButton.GetComponent<Image>().color = Color.blue - new Color(0, 0, 0, 0.5f); ;
        }

        newButton.SourceController = this;
        newButton.pick = _target;
    }
}
