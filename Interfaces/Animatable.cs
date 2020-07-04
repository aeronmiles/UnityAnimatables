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

        public static Vector3 AverageRotation(this HashSet<Animatable> animatables)
        {
            float x = 0f, y = 0f, z = 0f, w = 0f;
            foreach (var a in animatables)
            {
                var q = a.transform.rotation;
                x += q.x; y += q.y; z += q.z; w += q.w;
            }
            float k = 1.0f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
            return new Quaternion(x * k, y * k, z * k, w * k).eulerAngles;
        }

        public static Vector3 AverageScale(this HashSet<Animatable> animatables)
        {
            int l = animatables.Count;
            Vector3 avg = float3.zero;
            foreach (var a in animatables)
            {
                avg += a.transform.localScale;
            }
            return avg / l;
        }
    }
}
