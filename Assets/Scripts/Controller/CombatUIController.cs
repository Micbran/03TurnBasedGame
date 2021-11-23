using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour
{
    [SerializeField] private Text textUI = null;
    [SerializeField] public CombatStateMachine stateMachine = null;
    [SerializeField] private PlayerUIManager playerUI = null;


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

    private void Update()
    {
        switch(stateMachine.CurrentState)
        {
            case PlayerCharacterTurnCombatState pct:
                textUI.text = "Player turn...!";
                textUI.color = Color.green;
                break;
            case EnemyCharacterTurnCombatState ect:
                textUI.text = "Enemy turn...!";
                textUI.color = Color.red;
                break;
            case PickNextActorCombatState pna:
                textUI.text = "Pick actor state...!";
                textUI.color = Color.blue;
                break;
            case SetupCombatGameState scg:
                textUI.text = "Setup state...!";
                textUI.color = Color.cyan;
                break;
            case LoseCombatState lcs:
                textUI.text = "Lose state...!";
                textUI.color = Color.black;
                break;
            case WinCombatState wcs:
                textUI.text = "Win state...!";
                textUI.color = Color.yellow;
                break;
            default:
                textUI.text = "Unrecognized state.";
                textUI.color = Color.cyan;
                break;
        }
    }
}
