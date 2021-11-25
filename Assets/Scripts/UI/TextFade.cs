using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    [SerializeField] private Text textFieldToFade;
    [SerializeField] private Color baseColor;
    
    private float timer = 0f;
    private float timerStart = 0f;

    public void DisplayText(string message, float duration)
    {
        this.timer = duration;
        this.timerStart = duration;
        this.textFieldToFade.text = message;
    }

    private void Update()
    {
        this.timer -= Time.deltaTime;
        if (this.timer <= 0) this.DeactivateText();
        this.textFieldToFade.color = new Color(this.baseColor.r, this.baseColor.g, this.baseColor.b, this.timer / this.timerStart);
    }

    public void DeactivateText()
    {
        this.textFieldToFade.text = "";
        this.textFieldToFade.color = new Color(this.baseColor.r, this.baseColor.g, this.baseColor.b, 1f);
        this.gameObject.SetActive(false);
    }
}
