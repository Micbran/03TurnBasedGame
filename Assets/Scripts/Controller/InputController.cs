using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedUp = delegate { };
    public event Action PressedDown = delegate { };
    public event Action CameraLeft = delegate { };
    public event Action CameraRight = delegate { };
    public event Action CameraUp = delegate { };
    public event Action CameraDown = delegate { };
    public event Action<float> CameraZoom = delegate { };
    public event Action<Ray> ClickedLocation = delegate { };

    private void Update()
    {
        this.DetectConfirm();
        this.DetectCancel();
        this.DetectUp();
        this.DetectDown();
        this.DetectCameraUp();
        this.DetectCameraDown();
        this.DetectCameraLeft();
        this.DetectCameraRight();
        this.DetectCameraZoom();
        this.DetectClickedLocation();
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            this.PressedConfirm?.Invoke();
        }
    }

    private void DetectCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            this.PressedCancel?.Invoke();
        }
    }

    private void DetectUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            this.PressedUp?.Invoke();
        }
    }

    private void DetectDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            this.PressedDown?.Invoke();
        }
    }

    private void DetectCameraUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.CameraUp?.Invoke();
        }
    }

    private void DetectCameraDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            this.CameraDown?.Invoke();
        }
    }

    private void DetectCameraLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.CameraLeft?.Invoke();
        }
    }

    private void DetectCameraRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.CameraRight?.Invoke();
        }
    }

    private void DetectCameraZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            this.CameraZoom?.Invoke(Input.mouseScrollDelta.y);
        }
    }

    private void DetectClickedLocation()
    {
        // Plane or using collider of ground, but for now, generic raycast
        // But best for now to just pass a ray and allow scripts to decide what they want to do
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            this.ClickedLocation?.Invoke(ray);
        }
    }
}
