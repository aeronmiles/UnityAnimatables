using Unity.Mathematics;
using UnityEngine;

namespace UnityAnimatables
{
    [RequireComponent(typeof(Cached))]
    public class Recoil : Animatable, IAnimate
    {
        public float Strength = 1f;
        public float Radius = 1f;
        public AnimationCurve StrengthAtDistance = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private void OnEnable()
        {
            AnimController.I.Add(this);
            Cached.Cache();
        }

        private void OnDisable()
        {
            AnimController.I.Remove(this);
        }

        public void Animate()
        {
            Vector3 pos = transform.position;
            Quaternion rot = RB.rotation;
            float s = StrengthAtDistance.Evaluate(math.distance(Cached.Position, pos) / Radius);
            Vector3 f = math.normalize((Cached.Position - pos)) * s * Time.deltaTime * Strength;
            RB.AddForce(f);

            quaternion r = Quaternion.Slerp(RB.rotation, Cached.Rotation, s * Time.deltaTime * Strength);
            RB.MoveRotation(r);
        }
    }
}
