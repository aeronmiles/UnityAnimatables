using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Animatable : MonoBehaviour
{
    public abstract void Animate();
}

public static class AnimatableExt
{
    public static Vector3 AveragePosition(this List<Animatable> animatables)
    {
        int l = animatables.Count;
        Vector3 avg = float3.zero;
        for (int i = 0; i < l; i++)
        {
            avg += animatables[i].transform.position;
        }
        return avg / l;
    }
}
