using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedUp = delegate { };
    public event Action PressedDown = delegate { };
    public event Action<Vector2> ClickedLocation = delegate { };

    private void Update()
    {
        DetectConfirm();
        DetectCancel();
        DetectUp();
        DetectDown();
        DetectClickedLocation();
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            PressedConfirm?.Invoke();
        }
    }

    private void DetectCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            PressedCancel?.Invoke();
        }
    }

    private void DetectUp()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            PressedUp?.Invoke();
        }
    }

    private void DetectDown()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            PressedDown?.Invoke();
        }
    }

    private void DetectClickedLocation()
    {

    }
}
