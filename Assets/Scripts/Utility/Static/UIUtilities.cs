using UnityEngine;

public static class UIUtilities
{
    public static Vector3 ToMetersWorldspace(float rectSize, float meters)
    {
        float scaleFactor = meters / rectSize * 2;
        return new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
