
using System.Collections.Generic;

namespace UnityAnimatables
{
    public class Displace : AnimatableComponents
    {
        public static HashSet<Displace> Displacables = new HashSet<Displace>();
        private void OnEnable()
        {
            Displacables.Add(this);
        }

        private void OnDisable()
        {            
            Displacables.Remove(this);
        }
    }
}