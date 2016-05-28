using UnityEngine;
using System.Collections;

public class AnimatorSetup {
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTime = 0.6f;

    private Animator anim;
    private HashIds hash;

    public AnimatorSetup(Animator animator, HashIds hash)
    {
        anim = animator;
        this.hash = hash;
    }

    public void Setup(float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;
        anim.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}
