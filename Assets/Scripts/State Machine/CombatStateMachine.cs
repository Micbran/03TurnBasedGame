using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : StateMachine
{
    public event Action NewTurnStarted = delegate { };
    public event Action<List<ActionButton>> UIActionsUpdated = delegate { };
    public event Action<Actor> ActorUIUpdate = delegate { };

    public LayerMask ActorLayer;
    public LayerMask WorldUILayer;
    public LayerMask CollisionLayer;

    [SerializeField] private InputController input;
    public InputController Input => this.input;

    [SerializeField] private TurnController turn;
    public TurnController Turn => this.turn;

    [SerializeField] private LogController log;
    public LogController Log => this.log;

    [SerializeField] private CombatUIController ui;
    public CombatUIController UI => this.ui;

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

    private List<Actor> allActors = new List<Actor>();

    private void Start()
    {
        this.ChangeState<SetupCombatGameState>();
        this.allActors = new List<Actor>(FindObjectsOfType<Actor>());
        foreach (Actor a in this.allActors)
        {
            a.ActorDied += OnActorDeath;
        }
    }

    private void OnDisable()
    {
        foreach (Actor a in this.allActors)
        {
            a.ActorDied -= OnActorDeath;
        }
    }

    private void OnActorDeath(string name)
    {
        this.Log.AddNewResult(new DeathResult() { actorName = name });
        this.Turn.RemoveDead();
        this.CheckWinLoseCondition();
    }

    public bool CheckWinLoseCondition()
    {
        List<Actor> allLocalActors = new List<Actor>(FindObjectsOfType<Actor>());
        if (allLocalActors.FindAll(a => a is PlayerCharacter && !a.isDead).Count == 0)
        {
            this.ChangeState<LoseCombatState>();
            return true;
        }
        else if (allLocalActors.FindAll(a => a is EnemyCharacter && !a.isDead).Count == 0)
        {
            this.ChangeState<WinCombatState>();
            return true;
        }
        return false;
    }

    public void TriggerNewTurn()
    {
        this.NewTurnStarted?.Invoke();
        this.CheckWinLoseCondition();
    }

    public void TriggerActorUIUpdate()
    {
        this.ActorUIUpdate?.Invoke(this.CurrentActor);
    }

    public void OnUIActionsUpdated(List<ActionButton> actionButtons)
    {
        this.UIActionsUpdated?.Invoke(actionButtons);
    }

}
