
namespace UnityAnimatables
{
    public class LimitVelocity : Animatable, IAnimate
    {
        public float MaxVelocity = 1f;

        private void OnEnable()
        {
            AnimController.I.Add(this);
        }

        private void OnDisable()
        {
            AnimController.I.Remove(this);
        }
        
        public void Animate()
        {
            RB.velocity = RB.velocity.magnitude > MaxVelocity ? RB.velocity.normalized * MaxVelocity : RB.velocity;
        }
    }
}