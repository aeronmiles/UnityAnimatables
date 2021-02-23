using UnityEngine;


namespace UnityAnimatables
{
    public class MoveToCached : Animatable, IAnimate
    {
        [SerializeField] AnimationCurve Rate = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public float Period = 2f;

        float x;

        private void OnEnable()
        {
            AnimController.I.Add(this);
            x = 0f;
        }

        private void OnDisable()
        {
            AnimController.I.Remove(this);
        }

        Vector3 pos;
        Quaternion rot;
        Vector3 scale;
        public void Animate()
        {
            if (x == 0f)
            {
                pos = transform.position;
                rot = transform.rotation;
                scale = transform.localScale;
            }
            x += Time.deltaTime / Period;
            float a = Rate.Evaluate(x);
            transform.position = Vector3.Lerp(pos, Cached.Position, a);
            transform.rotation = Quaternion.Lerp(rot, Cached.Rotation, a);
            transform.localScale = Vector3.Lerp(scale, Cached.Scale, a);
        }
    }
}
