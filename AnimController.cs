using System.Collections.Generic;

namespace UnityAnimatables
{
    public class AnimController : Singleton<AnimController>
    {
        public TypeHashSet<Animatable> Animatables = new TypeHashSet<Animatable>();

        public HashSet<Animatable> Get<T>() where T : Animatable => Animatables.Get<T>();
        public HashSet<Animatable> GetAll() => Animatables.GetAll();
        public void Add<T>(T item) where T : Animatable => Animatables.Add(item);
        public void Remove<T>(T item) where T : Animatable => Animatables.Remove(item);

        private void Update()
        {
            foreach (var k in Animatables.Set.Keys)
            {
                if (typeof(IAnimate).IsAssignableFrom(k))
                {
                    foreach (var a in Animatables.Set[k])
                    {
                        if (a.Enabled) ((IAnimate)a).Animate();
                    }
                }
            }
        }
    }
}
