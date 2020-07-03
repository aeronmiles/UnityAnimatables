using System.Collections.Generic;
using UnityEngine;

namespace UnityAnimatables
{
    public class Animator : Singleton<Animator>
    {
        public List<Animatable> Animatables = new List<Animatable>();

        private void Update()
        {
            int l = Animatables.Count;
            if (l == 0) return;

            for (int i = 0; i < l; i++)
            {
                if (Animatables[i].gameObject.activeSelf) Animatables[i].Animate();
            }
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