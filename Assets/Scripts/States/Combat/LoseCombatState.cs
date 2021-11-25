using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCombatState : CombatState
{
    private bool finishSetup = false;

    public override void OnEnter()
    {
        this.finishSetup = false;
    }

    public override void Tick()
    {
        if (!this.finishSetup)
        {
            this.finishSetup = true;
            this.LoseGame();
        }
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public override void OnExit()
    {
        this.finishSetup = false;
    }
}
