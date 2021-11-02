using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }
}
