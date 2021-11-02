using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour
{
    [SerializeField] private Text logText;
    [SerializeField] private ScrollRect logScrollRect;
    [SerializeField] private Scrollbar logScroll;

    [SerializeField] private int MAX_LOG_MESSAGES = 100;

    private List<Result> resultMessages = new List<Result>();

    private void Awake()
    {
        // this.logText.text = "The game has started.";
    }

    private void UpdateLogText()
    {
        string sumString = "";
        foreach (Result res in resultMessages)
        {
            sumString += res.ToResultString() + "\n";
        }
        this.logText.text = sumString.Trim();
        Canvas.ForceUpdateCanvases();
        this.logScrollRect.verticalNormalizedPosition = 0;
    }

    public void AddNewResult(Result res)
    {
        this.resultMessages.Add(res);
        if (this.resultMessages.Count > MAX_LOG_MESSAGES)
        {
            this.resultMessages.RemoveAt(0);
        }
        this.UpdateLogText();
    }
}
