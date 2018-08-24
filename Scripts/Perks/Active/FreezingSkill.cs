using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Freezing Skill", menuName = "Create/New perc/New Active Skill/Freezing")]
public class FreezingSkill : ActivePerk
{
    [SerializeField]
    private ParticleSystem EnemyFreez;

    [SerializeField]
    private float Damage;

    [SerializeField]
    private float timeFreeze;

    private AliveEnemy aliveis;

    public override  void ActivatedSkill()
    {
    }

    public override void Init(AliveEnemy _alivies)
    {
        aliveis = _alivies;
        Init();
    }

    private void Init()
    {
        CoreLoopGame.Instance.StartCoroutine(RestartSkill());
        Time = 0;
    }

}
