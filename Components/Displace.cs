
namespace UnityAnimatables
{
    public class Displace : Animatable
    {
        private void OnEnable()
        {
            Animator.I.Add(this);
        }

        private void OnDisable()
        {
            Animator.I.Remove(this);
        }
    }
}