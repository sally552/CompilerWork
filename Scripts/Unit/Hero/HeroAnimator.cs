using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{

    public ParticleSystem part;
    public float timePlayParticle = 2f;

    private Animator anim;
    private HeroController _heroController;

    private float animSpeed = 1f;
    private AnimatorStateInfo stateInfo;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _heroController = GetComponent<HeroController>();

        _heroController.OnDeath += Death;
        _heroController.OnAttackLeft += AttackLeft;
        _heroController.OnAttackRight += AttackRight;
        _heroController.OnAttackUp += AttackUp;
        _heroController.OnAttackDawn += AttackDawn;
        _heroController.OnGetHit += KnockBack;
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackLeft") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("AttackRight") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("AttackUp") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("AttackDawn"))
        {
            anim.speed = _heroController.GetAttackSpeed();
        }
        else anim.speed = animSpeed;
    }

    private void Death()
    {
        anim.Play("Death");
    }

    private void AttackLeft()
    {
        anim.Play("AttackLeft");
    }

    private void AttackRight()
    {
        anim.Play("AttackRight");
    }
    private void AttackUp()
    {
        anim.Play("AttackUp");
    }

    private void AttackDawn()
    {
        anim.Play("AttackDawn");
    }

    private void KnockBack(float hp)
    {
        anim.Play("KnockBack");
        StopCoroutine(StopParticle());
        StartCoroutine(StopParticle());
    }

    private IEnumerator StopParticle()
    {
        if (part)
        {
            part.Play();
            yield return new WaitForSeconds(timePlayParticle);
            part.Stop();
        }
    }
}
