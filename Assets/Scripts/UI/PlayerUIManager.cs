using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private ActionPointDisplayManager apDisplay = null;
    [SerializeField] private ActionDisplayManager actionDisplay = null;
    [SerializeField] private PlayerButtonManager playerButtons = null;

    public void OnNewTurnBegan(Actor currentActor)
    {
        this.apDisplay.UpdateAPValues(currentActor.ActionPoints);
        this.actionDisplay.UpdateActions(currentActor);
        this.playerButtons.UpdateButtons(currentActor);
    }

    public void UpdateActionPoints(Actor currentActor)
    {
        this.apDisplay.UpdateAPValues(currentActor.ActionPoints);
    }
}
