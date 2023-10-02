using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jailer : MonoBehaviour, Punchable
{
    SpriteRenderer spriteRenderer;
    AudioSource aud;

    public AudioClip hitSound;
    public Sprite runningSprite;
    public Sprite prepareAttackSprite;
    public Sprite attackSprite;

    public float speed;
    public float judgingTime;
    public float waitBeforeHitTime;
    public float distance_For_Attack;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        aud = GetComponentInChildren<AudioSource>();
    }

    bool isAttacking;
    Timer aludeTimer;

    void Update()
    {
        Player player = Player.Instance;
        Vector3 distance = player.transform.position - transform.position;
        if(isAttacking)
        {
            if(distance.magnitude <= distance_For_Attack)
            {
                aud.Pause();
                transform.forward = distance;
                if (aludeTimer == null)
                {
                    spriteRenderer.sprite = prepareAttackSprite;
                    aludeTimer = new Timer(waitBeforeHitTime);
                }
                else
                    aludeTimer.UpdateTime(Time.deltaTime);

                if (aludeTimer.TimeOut)
                {
                    hitPlayer(player);
                    enabled = false;
                }
            }
            else
            {
                aud.Play();
                aludeTimer = null;
                spriteRenderer.sprite = runningSprite;
                transform.Translate(distance.normalized * Time.deltaTime * speed, Space.World);
                transform.forward = distance;
            }
        }
    }
    void hitPlayer(Player player)
    {
        aud.PlayOneShot(hitSound);
        spriteRenderer.sprite = attackSprite;
        player.BlackOut(0.2f);
        GameManager.Instance.Lose(1f);
    }

    public void AttackPlayer()
    {
        Run.After(judgingTime, () => { isAttacking = true; });
    }

    public void Punch(Vector3 position, Vector3 direction, float impulse)
    {
        throw new System.NotImplementedException();
    }
}
