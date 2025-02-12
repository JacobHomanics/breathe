using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public PlayerMotor controller;

    public float damping = 0.5f;

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    void Update()
    {
        var localized = controller.NormalizedInputMoveDirection;

        if (localized.x > 0)
        {
            anim.SetFloat("X", 1f, damping, Time.deltaTime);
        }
        else if (localized.x < 0)
        {
            anim.SetFloat("X", -1f, damping, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("X", 0f, damping, Time.deltaTime);
        }

        if (localized.z > 0)
        {
            anim.SetFloat("Z", 1f, damping, Time.deltaTime);
        }
        else if (localized.z < 0)
        {
            anim.SetFloat("Z", -1f, damping, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Z", 0f, damping, Time.deltaTime);
        }
    }
}