using UnityEngine;

namespace UnityAnimatables
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cached : MonoBehaviour
    {
        [Header("Transforms")]
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        [Header("Rigidbody")]
        public Vector3 Velocity;
        public Vector3 AngularVelocity;


        Rigidbody _rb;
        Rigidbody rb
        {
            get
            {
                if (_rb == null) _rb = GetComponent<Rigidbody>();
                return _rb;
            }
        }

        private void OnEnable()
        {
            Cache();
        }

        public void Cache()
        {
            Position = transform.position;
            Rotation = transform.rotation;
            Scale = transform.localScale;
            Velocity = rb.velocity;
            AngularVelocity = rb.angularVelocity;
        }
    }
}