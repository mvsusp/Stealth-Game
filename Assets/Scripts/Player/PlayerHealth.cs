using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public float health = 100f;
    public float resetAfterDeathTime = 5f;
    public AudioClip deathClip;

    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIds hash;
    private SceneFadeInOut sceneFadeInOut;
    private LastPlayerSighting lastPlayerSighting;
    private float timer;
    private bool playerDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void Update()
    {
        if (health <= 0)
        {
            if (!playerDead)
            {
                PlayerDying();
            }
            PlayerDead();
            LevelReset();
        }
    }

    void PlayerDying()
    {
        playerDead = true;
        anim.SetBool(hash.deadBool, true);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }

    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState)
        {
            anim.SetBool(hash.deadBool, false);
        }

        anim.SetFloat(hash.speedFloat, 0);
        playerMovement.enabled = false;
        lastPlayerSighting.position = lastPlayerSighting.resetPosition;
        GetComponent<AudioSource>().Stop();
    }

    void LevelReset()
    {
        timer += Time.deltaTime;

        if (timer >= resetAfterDeathTime)
        {
            sceneFadeInOut.EndScene();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
