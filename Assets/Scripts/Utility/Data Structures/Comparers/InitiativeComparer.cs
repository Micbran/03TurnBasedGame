using System.Collections.Generic;

public class InitiativeComparer : IComparer<Actor>
{
    public int Compare(Actor x, Actor y)
    {
        if (x.Initiative < y.Initiative)
        {
            return 1;
        }
        if (x.Initiative > y.Initiative)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
