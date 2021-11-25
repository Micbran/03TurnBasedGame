using UnityEngine;
using UnityEngine.UI;

public class ActorHealthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarForeground;
    private int maxHealth = 0;

    public void InitializeHealthbar(int maxHealth)
    {
        this.maxHealth = maxHealth;
        transform.rotation = Quaternion.Euler(-45, -135, 0);
    }

    public void UpdateHealthbar(int newHealth)
    {
        this.healthbarForeground.fillAmount = (float)(Mathf.Max(newHealth, 0)) / this.maxHealth;
    }
}
