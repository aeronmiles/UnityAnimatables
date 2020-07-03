using UnityEngine;

namespace UnityAnimatables
{
    [RequireComponent(typeof(Cached))]
    public class Average : Animatable
    {
        public float Rate = 1f;

        public XYZBool Position;

        Cached cache;
        private void OnEnable()
        {
            Animator.I.Add(this);
            cache = GetComponent<Cached>();
        }

        private void OnDisable()
        {
            Animator.I.Remove(this);
        }

        public override void Animate()
        {
            if (Position.Any)
            {
                var p = Animator.I.Animatables.AveragePosition();
                p.x = Position.X ? p.x : 0f;
                p.y = Position.Y ? p.y : 0f;
                p.z = Position.Z ? p.z : 0f;

                p += cache.Position;
                transform.position = Vector3.Slerp(transform.position, p, Time.deltaTime * Rate);
            }
        }
    }
}

