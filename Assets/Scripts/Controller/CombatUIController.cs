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
    }

    private void OnDisable()
    {
        this.stateMachine.NewTurnStarted -= OnNewTurnStarted;
    }

    private void OnNewTurnStarted()
    {
        this.playerUI.OnNewTurnBegan(this.stateMachine.CurrentActor);
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
