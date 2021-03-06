using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CombatUIController))]
public class ButtonCommands : MonoBehaviour
{
    private CombatUIController combatUI = null;

    [SerializeField] private Button endTurnButton;
    [SerializeField] private Button centerOnCharacterButton;
    [SerializeField] private CameraController cameraTarget;

    private void Awake()
    {
        this.combatUI = this.GetComponent<CombatUIController>();    
    }

    private void OnEnable()
    {
        this.endTurnButton.onClick.AddListener(this.EndTurn);
        this.centerOnCharacterButton.onClick.AddListener(this.CenterOnCharacter);
    }

    private void OnDisable()
    {
        this.endTurnButton.onClick.RemoveListener(this.EndTurn);
        this.centerOnCharacterButton.onClick.RemoveListener(this.CenterOnCharacter);
    }

    public void EndTurn()
    {
        if (this.combatUI.stateMachine.CurrentState is PlayerCharacterTurnCombatState)
        {
            PlayerCharacterTurnCombatState currState = this.combatUI.stateMachine.CurrentState as PlayerCharacterTurnCombatState;
            currState.TransitionToNextTurn();
        }
    }

    public void CenterOnCharacter()
    {
        Vector3 actorPos = this.combatUI.stateMachine.CurrentActor.transform.position;
        this.cameraTarget.ShiftToPosition(actorPos);
    }
}
