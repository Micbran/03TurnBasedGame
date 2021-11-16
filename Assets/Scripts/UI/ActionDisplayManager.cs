using System.Collections.Generic;
using UnityEngine;

public class ActionDisplayManager : MonoBehaviour
{
    [SerializeField] private List<ActionButton> actionButtons = new List<ActionButton>();

    public void UpdateActions(Actor currentActor)
    {
        foreach (ActionButton ab in this.actionButtons)
        {
            ab.ResetButton();
        }

        for (int i = 0; i < currentActor.Actions.Count; i++)
        {
            this.actionButtons[i].UpdateAbility(currentActor.Actions[i]);
        }
    }
}
