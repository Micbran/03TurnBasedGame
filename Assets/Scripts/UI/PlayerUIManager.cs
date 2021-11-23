using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public event Action<List<ActionButton>> OnActionsUpdated = delegate { };
    [SerializeField] private ActionPointDisplayManager apDisplay = null;
    [SerializeField] private ActionDisplayManager actionDisplay = null;
    [SerializeField] private PlayerButtonManager playerButtons = null;
    [SerializeField] private ActorDisplayManager actorDisplay = null;

    public void OnNewTurnBegan(Actor currentActor)
    {
        this.apDisplay.UpdateAPValues(currentActor.ActionPoints);
        this.actionDisplay.UpdateActions(currentActor);
        this.playerButtons.UpdateButtons(currentActor);
        this.actorDisplay.UpdateActorDisplay(currentActor);
        this.ActionsUpdated();
    }

    public void UpdateActorDisplay(Actor currentActor)
    {
        this.UpdateActionPoints(currentActor);
    }

    private void UpdateActionPoints(Actor currentActor)
    {
        this.apDisplay.UpdateAPValues(currentActor.ActionPoints);
    }

    public void ActionsUpdated()
    {
        this.OnActionsUpdated?.Invoke(this.actionDisplay.ActionButtons);
    }
}
