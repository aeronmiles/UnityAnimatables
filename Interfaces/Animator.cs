using System;
using System.Collections.Generic;

namespace UnityAnimatables
{
    public class Animator : Singleton<Animator>
    {
        public Dictionary<Type, HashSet<Animatable>> Animatables
        {
            get;
            private set;
        } = new Dictionary<Type, HashSet<Animatable>>();

        private HashSet<Animatable> all = new HashSet<Animatable>();

        private void Update()
        {
            foreach (var k in Animatables.Keys)
            {
                if (typeof(IAnimate).IsAssignableFrom(k))
                {
                    foreach (var a in Animatables[k])
                    {
                        if (a.gameObject.activeSelf) (a as IAnimate).Animate();
                    }
                }
            }
        }

        public void Add<T>(T animatable) where T : Animatable
        {
            if (!Animatables.ContainsKey(typeof(T)))
                Animatables.Add(typeof(T), new HashSet<Animatable>());

            Animatables[typeof(T)].Add(animatable);
            all.Add(animatable);
        }

        public void Remove<T>(T animatable) where T : Animatable
        {
            if (!Animatables.ContainsKey(typeof(T))) return;

            Animatables[typeof(T)].Remove(animatable);
            Animatables[typeof(T)].TrimExcess();
            all.Remove(animatable);
            all.TrimExcess();
        }

        public HashSet<Animatable> GetAll<T>() where T : Animatable
        {
            if (typeof(T) == typeof(Animatable))
            {
                return all;
            }

            foreach (var k in Animatables.Keys)
            {
                if (k == typeof(T)) return Animatables[k];
            }

            return null;
        }
    }
}
