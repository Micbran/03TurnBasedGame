using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCombatState : CombatState
{
    private bool finishSetup = false;

    public override void OnEnter()
    {
        this.finishSetup = false;
        Debug.Log("Entering LoseCombatState.");
    }

    public override void Tick()
    {
        if (!this.finishSetup)
        {
            this.finishSetup = true;
            StartCoroutine(LoseRoutine());
        }
    }

    IEnumerator LoseRoutine()
    {
        Debug.Log("Losing game. . . . .");
        yield return new WaitForSeconds(1f);

        Debug.Log("Game lost!");
        this.LoseGame();
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public override void OnExit()
    {
        this.finishSetup = false;
        Debug.Log("Exiting LoseCombatState.");
    }
}
