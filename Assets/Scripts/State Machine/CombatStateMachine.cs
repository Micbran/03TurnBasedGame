using System;
using UnityEngine;

public class CombatStateMachine : StateMachine
{
    public event Action NewTurnStarted = delegate { };

    [SerializeField] private InputController input;
    public InputController Input => this.input;

    [SerializeField] private TurnController turn;
    public TurnController Turn => this.turn;

    [SerializeField] private LogController log;
    public LogController Log => this.log;

    private Actor currentActor;
    public Actor CurrentActor
    {
        get
        {
            return this.currentActor;
        }
        set
        {
            this.currentActor = value;
        }
    }

    private void Start()
    {
        this.ChangeState<SetupCombatGameState>();
    }

    public void TriggerNewTurn()
    {
        this.NewTurnStarted?.Invoke();
    }
}
