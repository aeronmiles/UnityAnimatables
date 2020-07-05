using UnityEngine;
using Unity.Mathematics;

namespace UnityAnimatables
{
    public class Attractor : Animatable, IAnimate
    {
        public float Strength = 1f;
        public float Radius = 1f;
        [SerializeField] int maxCount = 20;
        [SerializeField] AnimationCurve StrengthAtDistance = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        Collider[] localObjs = null;

        private void OnEnable()
        {
            Animator.I.Add(this);
        }

        private void OnDisable()
        {
            Animator.I.Remove(this);
        }
        
        public void Animate()
        {
            for (int i = 0; i < maxCount; i++)
            {
                localObjs[i] = null;
            }
            Physics.OverlapSphereNonAlloc(transform.position, Radius, localObjs);
            float3 p = transform.position;
            for (int i = 0; i < maxCount; i++)
            {
                if (localObjs[i] == null) break;

                float3 op = localObjs[i].transform.position;
                float d = math.distance(op, p) / Radius;
                float3 f = (p - op) * Strength * StrengthAtDistance.Evaluate(d) * Time.deltaTime;
                localObjs[i].GetComponent<Rigidbody>().AddForce(f);
            }
        }

        private void OnValidate()
        {
            Radius = math.max(Radius, 0.000000000000000001f);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}
