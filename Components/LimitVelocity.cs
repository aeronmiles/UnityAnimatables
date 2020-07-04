using UnityEngine;

namespace UnityAnimatables
{
    public class LimitVelocity : Animatable
    {
        public float MaxVelocity = 1f;

        Rigidbody rb;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            Animator.I.Add(this);
        }

        private void OnDisable()
        {
            Animator.I.Remove(this);
        }

        public override void Animate()
        {
            rb.velocity = rb.velocity.magnitude > MaxVelocity ? rb.velocity.normalized * MaxVelocity : rb.velocity;
        }
    }
}