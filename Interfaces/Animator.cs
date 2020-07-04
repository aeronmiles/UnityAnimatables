using System.Collections.Generic;

namespace UnityAnimatables
{
    public class Animator : Singleton<Animator>
    {
        public HashSet<Animatable> Animatables = new HashSet<Animatable>();

        private void Update()
        {
            int l = Animatables.Count;
            if (l == 0) return;
            
            foreach (var a in Animatables) if (a.gameObject.activeSelf) a.Animate();
        }

        internal void Add<T>(T animatable) where T : Animatable
        {
            Animatables.Add(animatable);
        }

        internal void Remove<T>(T animatable) where T : Animatable
        {
            Animatables.Remove(animatable);
            Animatables.TrimExcess();
        }
    }

}