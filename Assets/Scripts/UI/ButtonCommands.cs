using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CombatUIController))]
public class ButtonCommands : MonoBehaviour
{
    private CombatUIController combatUI = null;

    [SerializeField] private Button endTurnButton;
    [SerializeField] private Button winButton;
    [SerializeField] private Button loseButton;

    private void Awake()
    {
        this.combatUI = this.GetComponent<CombatUIController>();    
    }

    private void OnEnable()
    {
        this.endTurnButton.onClick.AddListener(this.EndTurn);
        this.winButton.onClick.AddListener(this.TransitionToWin);
        this.loseButton.onClick.AddListener(this.TransitionToLose);
    }

    private void OnDisable()
    {
        this.endTurnButton.onClick.RemoveListener(this.EndTurn);
        this.winButton.onClick.RemoveListener(this.TransitionToWin);
        this.loseButton.onClick.RemoveListener(this.TransitionToLose);
    }

    private void Update()
    {
        bool isPlayerTurn = this.combatUI.stateMachine.CurrentState is PlayerCharacterTurnCombatState;
        this.endTurnButton.interactable = isPlayerTurn;
        this.winButton.interactable = isPlayerTurn;
        this.loseButton.interactable = isPlayerTurn;
    }

    public void TransitionToWin()
    {
        this.combatUI.stateMachine.ChangeState<WinCombatState>();
    }

    public void TransitionToLose()
    {
        this.combatUI.stateMachine.ChangeState<LoseCombatState>();
    }

    public void EndTurn()
    {
        this.combatUI.stateMachine.ChangeState<PickNextActorCombatState>();
    }
}
