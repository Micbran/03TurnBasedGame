using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour
{
    [SerializeField] private TextFade errorText = null;
    [SerializeField] public CombatStateMachine stateMachine = null;
    [SerializeField] private PlayerUIManager playerUI = null;
    [Space(10)]
    [SerializeField] private AudioClip ErrorSoundEff;


    private void Awake()
    {

    }

    private void OnEnable()
    {
        this.stateMachine.NewTurnStarted += OnNewTurnStarted;
        this.stateMachine.ActorUIUpdate += UpdateActorDisplay;
        this.playerUI.OnActionsUpdated += OnUIActionsUpdated;
    }

    private void OnDisable()
    {
        this.stateMachine.NewTurnStarted -= OnNewTurnStarted;
        this.stateMachine.ActorUIUpdate -= UpdateActorDisplay;
        this.playerUI.OnActionsUpdated -= OnUIActionsUpdated;
    }

    public void DisplayErrorMessage(string message, float duration = 3f)
    {
        AudioHelper.PlayClip2D(this.ErrorSoundEff, 1f);
        this.errorText.gameObject.SetActive(true);
        this.errorText.DisplayText(message, duration);
    }

    private void OnNewTurnStarted()
    {
        this.playerUI.OnNewTurnBegan(this.stateMachine.CurrentActor);
    }

    private void OnUIActionsUpdated(List<ActionButton> actionButtons)
    {
        this.stateMachine.OnUIActionsUpdated(actionButtons);
    }

    private void UpdateActorDisplay(Actor currentActor)
    {
        this.playerUI.UpdateActorDisplay(currentActor);
    }
}
