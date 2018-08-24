using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chain Lightning Aura", menuName = "Create/New perc/New Aura/Chain_Lightning")]
public class Chain_Lightning : AuraUnit
{
    [Space]
    [Space]
    [SerializeField]
    private float restartTimeAura;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float damageLight;
    [SerializeField]
    private bool IsKnok;

    private ParticleSystem p;

    private Vector3 heroPosition;

    public override void ActivatedAura(HeroController controller)
    {
        base.ActivatedAura(controller);
        heroPosition = controller.transform.position;

        if (heroActivatedAura != null)
        {
            p = Instantiate(heroActivatedAura, controller.transform.position + Vector3.up * 2, Quaternion.identity);
        }
        controller.StartCoroutine(ActivatedTimeAura(restartTimeAura));
    }

    //IEnumerator LightCoroutine(Vector3 position, ParticleSystem part)
    protected override IEnumerator ActivatedTimeAura(float time = 0)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Debug.Log(heroActivatedAura);
            if (heroActivatedAura != null)
            {
                p.Stop();
                p.Play();
            }

            Collider[] hit = Physics.OverlapSphere(heroPosition, distance);
            foreach (Collider col in hit)
            {
                if (col.tag == "Enemy")
                {

                    Debug.Log(col.name + " получил леща полнией");
                    if (EnemyHit != null)
                    {
                        Debug.Log("!");
                        var enemy = Instantiate(EnemyHit, col.transform.position + Vector3.up * 2, Quaternion.identity);
                        enemy.Play();
                    }
                    col.GetComponent<EnemyController>().GetAttack(damageLight, IsKnok);
                }
            }
        }
    }
}
