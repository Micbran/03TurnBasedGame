using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public int Initiative => this.initiative;
    protected int initiative;

    public string Name => this.actorName;
    [SerializeField] protected string actorName;

    public override string ToString()
    {
        return $"{this.Name} - {this.Initiative}";
    }
}
