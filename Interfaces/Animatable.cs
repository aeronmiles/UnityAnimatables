using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace UnityAnimatables
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Cached))]
    public abstract class AnimatableComponents : MonoBehaviour
    {
        Cached cached = null;
        public Cached Cached
        {
            get
            {
                if (cached == null) cached = GetComponent<Cached>();
                return cached;
            }
        }

        Rigidbody _rb;
        public Rigidbody RB
        {
            get
            {
                if (_rb == null) _rb = GetComponent<Rigidbody>();
                return _rb;
            }
        }
    }

    public abstract class Animatable : AnimatableComponents
    {
        public abstract void Animate();
    }

    public static class AnimatableExt
    {
        public static Vector3 AveragePosition(this HashSet<Animatable> animatables)
        {
            int l = animatables.Count;
            Vector3 avg = float3.zero;
            foreach (var a in animatables)
            {
                avg += a.transform.position;
            }
            return avg / l;
        }
    }
}
