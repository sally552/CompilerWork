using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public abstract class AuraUnit :ScriptableObject
{
    [SerializeField]
    protected ParticleSystem heroAura;

    [SerializeField]
    protected ParticleSystem EnemyHit;

    [SerializeField]
    protected ParticleSystem heroActivatedAura;

    protected ParticleSystem go;

    public virtual void ActivatedAura(HeroController controller)
    {
        if (heroAura != null)
        {
            go = Instantiate(heroAura, controller.transform.position + Vector3.up * 2, Quaternion.identity);
            go.Play();
        }
    }

    protected virtual IEnumerator ActivatedTimeAura(float time = 0)
    {
  
        yield return new WaitForSeconds(time);
        if (heroActivatedAura != null)
        {
            heroActivatedAura.Stop();
            heroActivatedAura.Play();
        }
    }
}
