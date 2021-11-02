using System.Collections.Generic;

public class InitiativeQueue
{
    private List<Actor> actorQueue;
    private InitiativeComparer initComp;

    public int Count
    {
        get { return this.actorQueue.Count; }
    }

    public InitiativeQueue()
    {
        this.actorQueue = new List<Actor>();
        this.initComp = new InitiativeComparer();
    }

    public void Enqueue(Actor newActor)
    {
        this.actorQueue.Add(newActor);
        this.Readjust();
    }

    public Actor Dequeue()
    {
        if (this.Count == 0)
        {
            return null;
        }

        Actor saveActor = this.actorQueue[0];
        this.actorQueue.RemoveAt(0);

        return saveActor;
    }

    public void Clear()
    {
        this.actorQueue.Clear();
    }

    private void Readjust()
    {
        this.actorQueue.Sort(this.initComp);
    }

    public override string ToString()
    {
        return string.Join(", ", this.actorQueue);
    }
}
