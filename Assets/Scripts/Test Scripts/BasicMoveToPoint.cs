using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicMoveToPoint : MonoBehaviour
{
    [SerializeField] InputController input;

    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private float theta = 1e-15f;

    private Transform baseTransform;
    private Rigidbody rb;
    private Vector3 destination;

    private void Awake()
    {
        this.baseTransform = transform;
        this.destination = transform.position;
        this.rb = this.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.ClickedLocation += MouseClick;
    }

    private void OnDisable()
    {
        input.ClickedLocation -= MouseClick;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.destination, this.baseTransform.position) > this.theta)
        {
            this.rb.MovePosition(this.baseTransform.position + this.CalculatePerFrameDestination());
        }
    }

    private Vector3 CalculatePerFrameDestination()
    {
        return Vector3.Normalize(this.destination - this.transform.position) * this.moveSpeed * Time.deltaTime;

    }

    private void MouseClick(Ray ray)
    {
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000f))
        {
            this.destination = hitData.point;
        }
    }
}
