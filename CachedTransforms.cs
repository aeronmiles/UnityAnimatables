using UnityEngine;

namespace UnityAnimatables
{
    public class CachedTransforms : MonoBehaviour
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        private void OnEnable()
        {
            Position = transform.position;
            Rotation = transform.rotation;
            Scale = transform.localScale;
        }

        public void Cache()
        {
            OnEnable();
        }
    }
}