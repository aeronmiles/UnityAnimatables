
namespace UnityAnimatables
{
    public class Displace : AnimatableComponents
    {
        private void OnEnable()
        {
            var ds = FindObjectsOfType<Displacer>();
            foreach (var d in ds) d.Add(this);
        }

        private void OnDisable()
        {            
            var ds = FindObjectsOfType<Displacer>();
            foreach (var d in ds) d.Remove(this);
        }
    }
}