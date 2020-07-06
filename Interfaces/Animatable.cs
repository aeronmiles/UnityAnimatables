using UnityEngine;

namespace UnityAnimatables
{
    public interface IAnimate
    {
        void Animate();
    }

    [ExecuteInEditMode]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Cached))]

    public abstract class Animatable : MonoBehaviour
    {
        public bool Enabled = true;
        Cached cached = null;
        public Cached Cached
        {
            get
            {
                if (cached == null) cached = GetComponent<Cached>();
                return cached;
            }
        }

        Rigidbody _rb;
        public Rigidbody RB
        {
            get
            {
                if (_rb == null) _rb = GetComponent<Rigidbody>();
                return _rb;
            }
        }
    }
}
