using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem part;
    public float timePlayParticle = 2f;

    private Animator anim;
    private EnemyController _mobController;

    private float animStartSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _mobController = GetComponent<EnemyController>();

        _mobController.OnRun += Run;
        _mobController.OnAttack += Attack;
        _mobController.OnGetHitAnim += KnockBack;
        animStartSpeed = 1f;
    }

    private void Run()
    {
        anim.Play("Run");
    }

    private void Attack()
    {
        anim.Play("Attack");
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.speed = _mobController.GetAttackSpeed();
        }
        else anim.speed = animStartSpeed;
    }

    private void KnockBack()
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
