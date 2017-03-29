using UnityEngine;
using System.Collections;

public class TargetSelector : MonoBehaviour {

    [SerializeField]
    RectTransform thisTrans;

    public PlayerController SourceController;

    public Battler pick;

    public bool MouseOver
    {
        get
        {
            if (Input.mousePosition.x >= thisTrans.position.x - (thisTrans.rect.width / 2) &&
                Input.mousePosition.x <= thisTrans.position.x + (thisTrans.rect.width / 2) &&
                Input.mousePosition.y >= thisTrans.position.y - (thisTrans.rect.height / 2) &&
                Input.mousePosition.y <= thisTrans.position.y + (thisTrans.rect.height / 2))
            {
                return true;
            }
            return false;
        }
    }

    void Start()
    {
        thisTrans = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (MouseOver && Input.GetButtonDown("Fire1"))
        {
            SourceController.PickTarget(pick);
        }
    }
}
