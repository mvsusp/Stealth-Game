using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    private Animator anim;
    private HashIds hash;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        anim.SetLayerWeight(1, 100f);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");

        MovementManagement(h, v, sneak);
    }

    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);

        AudioManagement(shout);
    }

    void MovementManagement(float horizontal, float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (horizontal != 0 || vertical != 0)
        {
            Rotating(horizontal, vertical);
            anim.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }
    }

    void Rotating(float hotizontal, float vertical)
    {
        var targetDirection = new Vector3(hotizontal, 0, vertical);
        var targetRotation = Quaternion.LookRotation(targetDirection);
        var rb = GetComponent<Rigidbody>();
        var newRotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        rb.MoveRotation(newRotation);
    }

    void AudioManagement(bool shout)
    {
        var audioSource = GetComponent<AudioSource>();

        if ( anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}
