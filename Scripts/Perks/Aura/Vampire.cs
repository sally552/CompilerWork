using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vampire Aura", menuName = "Create/New perc/New Aura/Vampire")]
public class Vampire : AuraUnit
{
    [Space]
    [Space]
    [SerializeField]
    private float percent;

    private ParticleSystem p;

    public override void ActivatedAura(HeroController controller)
    {
        base.ActivatedAura(controller);

        if (heroActivatedAura)
        {
           p = Instantiate(heroActivatedAura, controller.transform.position + Vector3.up * 2, Quaternion.identity);
        }

        controller.OnStructed += (float x) =>
        {
            if (p)
            {
                p.Stop();
                p.Play();
            }
            controller.OnGetHealt(x / 100 * percent);
            Debug.Log("Вампиризм: восстановили " + x / 100 * percent + " хп");
        };

    }
}
