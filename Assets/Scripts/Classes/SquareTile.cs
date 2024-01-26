using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTile : MonoBehaviour
{
    [SerializeField] private bool GO;
    [SerializeField] private bool goJail;
    [SerializeField] private bool travel;
    [SerializeField] private bool superChance;

    public void SquareTileFunction()
    {
        if (GO)
        {
            UIManager.Instance.EndAction();
        }
        else if (goJail)
        {
            MovingManager.Instance.MoveToJail();
            GameManager.Instance.SetIsInJail(true);
            UIManager.Instance.EndAction();
        }
        else if (travel)
        {
            UIManager.Instance.TogglePanelOn("travel");
        }
        
        else if (superChance)
        {
            UIManager.Instance.TogglePanelOn("superchance");
        }

    }
}
