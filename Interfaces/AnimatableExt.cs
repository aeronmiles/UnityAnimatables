
using System.Collections.Generic;
using UnityEngine;

namespace UnityAnimatables
{
    public static class AnimatableExt
    {
        public static Vector3 AveragePosition(this HashSet<Animatable> animatables)
        {
            int l = animatables.Count;
            Vector3 avg = Vector3.zero;
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
            Vector3 avg = Vector3.zero;
            foreach (var a in animatables)
            {
                avg += a.transform.localScale;
            }
            return avg / l;
        }

        public static void Cache(this HashSet<Animatable> animatables)
        {
            foreach (var a in animatables) a.Cached.Cache();
        }

        public static void Enable(this HashSet<Animatable> animatables, bool enable)
        {
            foreach (var a in animatables) a.Enabled = enable;
        }
    }
}
