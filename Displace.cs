using UnityEngine;
using Unity.Mathematics;

namespace UnityAnimatables
{
    [RequireComponent(typeof(Cached))]
    public class Displace : Animatable
    {
        public float Strength = 1f;
        public Texture2D HeightMap = null;
        public Texture2D NormalMap = null;
        [Range(0f, 1f)]
        public float NormalStrength = 0.5f;
        public Vector3 Size = new Vector3(2f, 1f, 2f);
        public Vector3 Offset = new Vector3(0f, 0f, 0f);
        public Vector2 OffsetRate = new Vector2(0.1f, 0f);
        public float RotationRate = 1f;

        bool debug = false;

        Rigidbody rb;
        Vector3 normal;
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
            int z = Mathf.FloorToInt((p.z + Offset.z + o.y) / Size.z * HeightMap.height);
            float targetHeight = Offset.y + (HeightMap.GetPixel(x, z).grayscale * Size.y);
            normal = (NormalMap.GetPixel(x, z).rgb() * 2f) - Vector3.one;
            normal.x = normal.y;
            normal.y = math.max(1f - NormalStrength, 0.0001f);
            Vector3 f = new Vector3(0f, targetHeight - p.y, 0f) * Time.deltaTime * Strength;
            rb.AddForce(f);

            Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, normal);
            toRotation *= Quaternion.Euler(Vector3.up * cached.Rotation.eulerAngles.y);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, RotationRate * Time.deltaTime));
        }

        private void OnDrawGizmosSelected()
        {
            if (!debug) return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + normal);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + (transform.up * 0.1f));
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, transform.position + (transform.right * 0.1f));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 0.1f));
        }

    }
}