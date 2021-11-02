using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }
}
