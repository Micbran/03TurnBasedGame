using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveToPoint : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float distanceTheta = 1e-10f;

    private float movementLimit;
    public float MovementLimit => this.movementLimit;
    private float currentMovement;
    public float CurrentMovement => this.currentMovement;
    private bool isMoving;
    public bool IsMoving => this.isMoving;

    private Transform baseTransform;
    private Rigidbody rb;
    private Vector3 destination;

    private void Awake()
    {
        this.movementLimit = 0;
        this.currentMovement = 0;
        this.isMoving = false;

        this.baseTransform = this.transform;
        this.rb = this.GetComponent<Rigidbody>();
        this.destination = this.baseTransform.position;
    }

    public void StartNewMoveAction(float newMovementLimit)
    {
        this.currentMovement = 0;
        this.movementLimit = newMovementLimit;
        this.destination = this.baseTransform.position;
        this.isMoving = false;
    }

    public void SetNewDestination(Vector3 newDestination)
    {
        if (this.HasReachedMovementLimit()) return;
        this.destination = newDestination;
        this.isMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Actor>() || collision.gameObject.GetComponent<InvisibleWall>())
        {
            this.destination = this.baseTransform.position;
            this.isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (this.HasReachedDestination() || this.HasReachedMovementLimit())
        {
            this.destination = this.baseTransform.position;
            this.isMoving = false;
            return;
        }
        this.isMoving = true;
        Vector3 newPosition = this.baseTransform.position + this.CalculatePerFrameDestination();
        this.currentMovement += Vector3.Distance(this.baseTransform.position, newPosition);
        this.rb.MovePosition(newPosition);
    }

    private Vector3 CalculatePerFrameDestination()
    {
        return Vector3.Normalize(this.destination - this.baseTransform.position) * this.moveSpeed * Time.deltaTime;
    }

    private bool HasReachedDestination()
    {
        return Vector3.Distance(this.destination, this.baseTransform.position) < this.distanceTheta;
    }

    private bool HasReachedMovementLimit()
    {
        return this.currentMovement >= this.movementLimit;
    }
}
