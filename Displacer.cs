using Unity.Mathematics;
using UnityEngine;

namespace UnityAnimatables
{
    public class Displacer : MonoBehaviour
    {
        public Texture2D HeightMap = null;
        public Texture2D NormalMap = null;
        public float DisplaceStrength = 1f;
        [Range(0f, 1f)]
        public float NormalStrength = 0.5f;
        public Vector3 Size = new Vector3(1f, 1f, 1f);
        public Vector3 Offset = new Vector3(0f, 0f, 0f);
        public bool Tile = false;

        [Header("Animation")]
        public Vector2 OffsetRate = new Vector2(0.1f, 0f);
        public float RotationRate = 1f;

        [Header("Debugging")]
        [SerializeField] bool debugNormal = true;


        Vector3[] normals;

        public void Update()
        {
            if (Displace.Displacables.Count != normals.Length) normals = new Vector3[Displace.Displacables.Count];
            
            int i = 0;
            foreach (var t in Displace.Displacables)
            {
                var p = t.RB.position;
                var o = OffsetRate * Time.time;
                int x = Mathf.FloorToInt((p.x + Offset.x + o.x) / Size.x * HeightMap.width);
                int y = Mathf.FloorToInt((p.z + Offset.z + o.y) / Size.z * HeightMap.height);
                float targetHeight = (HeightMap.GetPixel(x, y).grayscale * Size.y) + Offset.y;
                Vector3 force = new Vector3(0f, (targetHeight + t.Cached.Position.y) - p.y, 0f) * Time.deltaTime * DisplaceStrength;
                t.RB.AddForce(force);

                normals[i] = (NormalMap.GetPixel(x, y).rgb() * 2f) - Vector3.one;
                normals[i].x = normals[i].y;
                normals[i].y = math.max(2f - (NormalStrength * 2f), 0.1f);
                Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, normals[i]);
                toRotation *= Quaternion.Euler(Vector3.up * t.Cached.Rotation.eulerAngles.y);
                t.RB.MoveRotation(Quaternion.Slerp(t.transform.rotation, toRotation, RotationRate * Time.deltaTime));
                i++;
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + new Vector3(0f, Size.y * 0.5f, 0f), Size);
        }

        private void OnDrawGizmos()
        {
            if (debugNormal)
            {
                int i = 0;
                foreach (var t in Displace.Displacables)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(t.transform.position, t.transform.position + normals[i]);
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(t.transform.position, t.transform.position + (t.transform.up * 0.1f));
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(t.transform.position, t.transform.position + (t.transform.right * 0.1f));
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(t.transform.position, t.transform.position + (transform.forward * 0.1f));
                    i++;
                }
            }

        }

    }
}