using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputController input;

    [SerializeField] private float zoomSensitivity = 0.5f;
    [SerializeField] private float moveSensitivity = 0.5f;

    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float minZoom = 20f;

    private Transform baseTransform;

    private void Awake()
    {
        this.baseTransform = transform;
    }

    private void OnEnable()
    {
        this.input.CameraUp += OnCameraUp;
        this.input.CameraDown += OnCameraDown;
        this.input.CameraLeft += OnCameraLeft;
        this.input.CameraRight += OnCameraRight;

        this.input.CameraZoom += OnCameraZoom;
    }

    private void OnDisable()
    {
        this.input.CameraUp -= OnCameraUp;
        this.input.CameraDown -= OnCameraDown;
        this.input.CameraLeft -= OnCameraLeft;
        this.input.CameraRight -= OnCameraRight;

        this.input.CameraZoom -= OnCameraZoom;
    }

    private void OnCameraUp() // +x +z
    {
        Vector3 movement = (Vector3.forward + Vector3.right) * this.moveSensitivity * Time.deltaTime;
        this.baseTransform.position += movement; 
    }

    private void OnCameraDown() // -x -z
    {
        Vector3 movement = (Vector3.back + Vector3.left) * this.moveSensitivity * Time.deltaTime;
        this.baseTransform.position += movement;
    }

    private void OnCameraLeft() // -x +z
    {
        Vector3 movement = (Vector3.forward + Vector3.left) * this.moveSensitivity * Time.deltaTime;
        this.baseTransform.position += movement;
    }

    private void OnCameraRight() // +x -z
    {
        Vector3 movement = (Vector3.back + Vector3.right) * this.moveSensitivity * Time.deltaTime;
        this.baseTransform.position += movement;
    }

    private void OnCameraZoom(float scrollDeltaY)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + scrollDeltaY * -1 * this.zoomSensitivity, this.maxZoom, this.minZoom);
    }
}
