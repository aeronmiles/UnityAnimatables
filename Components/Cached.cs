using UnityEngine;

namespace UnityAnimatables
{
    [ExecuteInEditMode]
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
        Rigidbody RB
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
            Velocity = RB.velocity;
            AngularVelocity = RB.angularVelocity;
        }
    }
}