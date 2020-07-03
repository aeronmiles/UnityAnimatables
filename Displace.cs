using UnityEngine;
using Unity.Mathematics;

namespace UnityAnimatables
{
    [RequireComponent(typeof(Cached))]
    public class Displace : Animatable
    {
        #region Inspector
        public Texture2D HeightMap = null;
        public Texture2D NormalMap = null;
        public float DisplaceStrength = 1f;
        [Range(0f, 1f)]
        public float NormalStrength = 0.5f;
        public Vector3 Size = new Vector3(1f, 1f, 1f);
        public Vector3 Offset = new Vector3(0f, 0f, 0f);

        [Header("Animation")]
        public Vector2 OffsetRate = new Vector2(0.1f, 0f);
        public float RotationRate = 1f;

        [Header("Debugging")]
        [SerializeField] bool debugNormal = true;
        #endregion

        Rigidbody rb;
        Vector3 normal;
        Vector3 force;
        Cached cached;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            Animator.I.Add(this);
            cached = GetComponent<Cached>();
            cached.Cache();
        }

        private void OnDisable()
        {
            Animator.I.Remove(this);
        }

        public override void Animate()
        {
            var p = transform.position;
            var o = OffsetRate * Time.time;
            int x = Mathf.FloorToInt((p.x + Offset.x + o.x) / Size.x * HeightMap.width);
            int y = Mathf.FloorToInt((p.z + Offset.z + o.y) / Size.z * HeightMap.height);

            float targetHeight = (HeightMap.GetPixel(x, y).grayscale * Size.y) + Offset.y;
            force = new Vector3(0f, (targetHeight + cached.Position.y) - p.y, 0f) * Time.deltaTime * DisplaceStrength;
            rb.AddForce(force);

            normal = (NormalMap.GetPixel(x, y).rgb() * 2f) - Vector3.one;
            normal.x = normal.y;
            normal.y = math.max(2f - (NormalStrength * 2f), 0.1f);
            Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, normal);
            toRotation *= Quaternion.Euler(Vector3.up * cached.Rotation.eulerAngles.y);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, RotationRate * Time.deltaTime));
        }

        private void OnDrawGizmos()
        {
            if (debugNormal)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transform.position, transform.position + normal);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + (transform.up * 0.1f));
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + (transform.right * 0.1f));
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 0.1f));
            }

        }

    }
}