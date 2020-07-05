namespace UnityAnimatables
{
    public class LimitVelocity : Animatable, IAnimate
    {
        public float MaxVelocity = 1f;

        private void OnEnable()
        {
            Animator.I.Add(this);
        }

        private void OnDisable()
        {
            Animator.I.Remove(this);
        }
        
        public void Animate()
        {
            RB.velocity = RB.velocity.magnitude > MaxVelocity ? RB.velocity.normalized * MaxVelocity : RB.velocity;
        }
    }
}