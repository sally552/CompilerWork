using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing skill", menuName = "Create/New perc/New Active Skill/Healing")]
public class HealingSkill : ActivePerk
{
    [SerializeField]
    private float health;

    [SerializeField]
    private ParticleSystem HeroParticle;
    private ParticleSystem part;

    private HeroController hero;

    public override void ActivatedSkill()
    {
        if (Time >= timeRestartSkill)
        {
            if (part != null)
            {
                part.Stop();
                part.Play();
            }
            Time = 0;
            hero.GetHealt(health);
        }
    }

    private void Init()
    {
        hero.StartCoroutine(RestartSkill());
        if (HeroParticle != null)
        {
            part = Instantiate(HeroParticle, hero.transform.position + Vector3.up*2, Quaternion.identity);
        }
        Time = 0;

    }

    public override void Init(HeroController _hero)
    {
        hero = _hero;
        Init();
    }

}
