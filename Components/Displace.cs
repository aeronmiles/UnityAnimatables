
namespace UnityAnimatables
{
    public class Displace : Animatable
    {
        private void OnEnable()
        {
            AnimController.I.Add(this);
        }

        private void OnDisable()
        {
            AnimController.I.Remove(this);
        }
    }
}