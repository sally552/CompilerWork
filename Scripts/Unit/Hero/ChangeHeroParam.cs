using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ChangeHeroParams : ChangeUnitParams
{
    private SwipeAttackSettings swipeAttack;
    private SwipeEnum swipeType;
    private List<TargetAttack> Coll;

    public ParticleSystem part { get; private set; }

    public bool IsStun { get; private set; }

    private HeroProfile heroParam;

    public ChangeHeroParams(HeroProfile _heroProfile) : base(_heroProfile)
    {
        heroParam = _heroProfile;
    }


    public IEnumerable<TargetAttack> GetEnemy(Vector3 playerPosition)
    {
        Coll = new List<TargetAttack>();
        foreach (SwipeAttackSettings.PathSetting f in swipeAttack.settings)
        {
            unitParam.AttackDistance = f.attackDistance;
            Collider[] hit = Physics.OverlapSphere(playerPosition, GetDistance());
            foreach (Collider col in hit)
            {
                if (col.tag == "Enemy" && col.GetComponent<EnemyController>()._path.GetPathName() == f.pathName)
                {
                    Debug.Log("Hero hit damage: " + GetDamage() * f.damage);
                    Coll.Add(new TargetAttack(col, GetDamage()*f.damage));
                }
            }
        }
        return Coll;
    }

    public void SetSwipeActive(SwipeEnum _swipeType)
    {
        part = null;
        swipeType = _swipeType;
        swipeAttack = heroParam.swapeSetting.First(x => x.swipeType == swipeType);
        unitParam.CoolDown = swipeAttack.coolDown;
        unitParam.SpeedAttack = swipeAttack.speedAttack;
    //    unitParam.Damage = heroParam.Damage;
        if (swipeAttack.particleAttack != null)
        {
            part = swipeAttack.particleAttack;
        }
        IsStun = swipeAttack.isStun;
    }

    public override float GetAttackSpeed()
    {
        Debug.Log("Hero has attack speed: " + SpeedAttackParam.GetResultStat());
        return base.GetAttackSpeed();
    }
}

public class TargetAttack
{
    public Collider _col { get; private set; }
    public float _dmg { get; private set; }

    public TargetAttack(Collider _col, float _dmg)
    {
        this._col = _col;
        this._dmg = _dmg;
    }
}


