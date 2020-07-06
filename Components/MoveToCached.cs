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

        public void Animate()
        {
            x += Time.deltaTime / Period;
            float a = Rate.Evaluate(x);
            transform.position = Vector3.Lerp(transform.position, Cached.Position, a);
            transform.rotation = Quaternion.Lerp(transform.rotation, Cached.Rotation, a);
            transform.localScale = Vector3.Lerp(transform.localScale, Cached.Scale, a);
        }
    }
}
