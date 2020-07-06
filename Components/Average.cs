using UnityEngine;

namespace UnityAnimatables
{
    [RequireComponent(typeof(Cached))]
    public class Average : Animatable, IAnimate
    {
        public float Rate = 1f;

        public XYZBool Position;
        public XYZBool Rotation;
        public XYZBool Scale;
        
        private void OnEnable()
        {
            AnimController.I.Add(this);
        }

        private void OnDisable()
        {
            AnimController.I.Remove(this);
        }
        
        public void Animate()
        {
            // if (Position.Any)
            // {
            //     var p = AnimController.I.Animatables.AveragePosition();
            //     p.x = Position.X ? p.x : transform.position.x;
            //     p.y = Position.Y ? p.y : transform.position.y;
            //     p.z = Position.Z ? p.z : transform.position.z;

            //     transform.position = Vector3.Slerp(transform.position, p, Time.deltaTime * Rate);
            // }

            // if (Rotation.Any)
            // {
            //     var r = AnimController.I.Animatables.AverageRotation();
            //     r.x = Rotation.X ? r.x : transform.rotation.eulerAngles.x;
            //     r.y = Rotation.Y ? r.y : transform.rotation.eulerAngles.y;
            //     r.z = Rotation.Z ? r.z : transform.rotation.eulerAngles.z;

            //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(r), Time.deltaTime * Rate);
            // }

            // if (Scale.Any)
            // {
            //     var s = AnimController.I.Animatables.AverageScale();
            //     s.x = Scale.X ? s.x : transform.localScale.x;
            //     s.y = Scale.Y ? s.y : transform.localScale.y;
            //     s.z = Scale.Z ? s.z : transform.localScale.z;

            //     transform.localScale = Vector3.Slerp(transform.localScale, s, Time.deltaTime * Rate);
            // }
        }
    }
}

