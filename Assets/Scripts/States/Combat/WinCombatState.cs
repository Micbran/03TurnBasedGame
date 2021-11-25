using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCombatState : CombatState
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
            this.WinGame();
        }
    }

    private void WinGame()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public override void OnExit()
    {
        this.finishSetup = false;
    }
}
