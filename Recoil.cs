using Unity.Mathematics;
using UnityEngine;

namespace UnityAnimatables
{
    [RequireComponent(typeof(CachedTransforms))]
    public class Recoil : Animatable
    {
        public float Strength = 1f;
        public float Radius = 1f;
        public AnimationCurve StrengthAtDistance = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        Rigidbody rb;
        CachedTransforms cached;
        private void OnEnable()
        {
            Animator.I.Add(this);
            rb = GetComponent<Rigidbody>();
            cached = GetComponent<CachedTransforms>();
            cached.Cache();
        }

        private void OnDisable()
        {
            Animator.I.Remove(this);
        }

        public override void Animate()
        {
            Vector3 pos = transform.position;
            Quaternion rot = rb.rotation;
            float s = StrengthAtDistance.Evaluate(math.distance(cached.Position, pos) / Radius);
            Vector3 f = math.normalize((cached.Position - pos)) * s * Time.deltaTime * Strength;
            rb.AddForce(f);

            quaternion r = Quaternion.Slerp(rb.rotation, cached.Rotation, s * Time.deltaTime * Strength);
            rb.MoveRotation(r);
        }
    }
}
