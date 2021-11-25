using UnityEngine;

public class BlinkObject : MonoBehaviour
{
    [SerializeField] private Color OriginalColor;
    [SerializeField] private Color BlinkColor;

    private MeshRenderer mesh;
    private float timer = 0f;
    private float blinkTimer = 1f;
    private bool isSwapped = false;

    private void Awake()
    {
        this.mesh = this.GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        this.timer = this.blinkTimer;
        this.mesh.material.color = this.OriginalColor;
        this.isSwapped = false;
    }

    private void Update()
    {
        this.timer -= Time.deltaTime;
        if (this.timer <= 0) this.SwapColor();
    }

    private void SwapColor()
    {
        this.timer = this.blinkTimer;
        this.isSwapped = !this.isSwapped;
        this.mesh.material.color = this.isSwapped ? this.BlinkColor : this.OriginalColor;
    }
}
