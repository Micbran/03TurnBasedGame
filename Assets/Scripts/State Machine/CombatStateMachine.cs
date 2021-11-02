using UnityEngine;

public class CombatStateMachine : StateMachine
{
    [SerializeField] InputController input;
    public InputController Input => this.input;

    [SerializeField] TurnController turn;
    public TurnController Turn => this.turn;

    [SerializeField] LogController log;
    public LogController Log => this.log;

    private void Start()
    {
        this.ChangeState<SetupCombatGameState>();
    }
}
