using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCombatState : CombatState
{
    private bool finishSetup = false;

    public override void OnEnter()
    {
        this.finishSetup = false;
        Debug.Log("Entering WinCombatState.");
    }

    public override void Tick()
    {
        if (!this.finishSetup)
        {
            this.finishSetup = true;
            StartCoroutine(WinRoutine());
        }
    }

    IEnumerator WinRoutine()
    {
        Debug.Log("Winning game. . . . .");
        yield return new WaitForSeconds(1f);

        Debug.Log("Game won!");
        this.WinGame();
    }

    private void WinGame()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public override void OnExit()
    {
        this.finishSetup = false;
        Debug.Log("Exiting WinCombatState.");
    }
}
