using UnityEngine;

public class LookAt : Animatable
{
    [SerializeField] Vector3 direction = new Vector3(0f, 1f, 0f);
    [SerializeField] Vector3 axis = new Vector3(0f, 1f, 0f);
    [SerializeField] float torque = 1f;

    Rigidbody rb;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        Animator.I.Add(this);
    }

    private void OnDisable()
    {
        Animator.I.Remove(this);
    }

    public override void Animate()
    {
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 eulerAngleVelocity = new Vector3(0, 0, angle);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * torque);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
