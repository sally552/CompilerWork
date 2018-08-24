using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyParams : ChangeUnitParams
{
    protected EnemyProfile enemyProfile;
    public ChangeEnemyParams(EnemyProfile _enemyProfile) :base(_enemyProfile)
    {
        enemyProfile = _enemyProfile;
    }

    public override float GetDamage()
    {
        Debug.Log("mob hit damage: " + DamageParam.GetResultStat());
        return base.GetDamage();
    }
    public override float GetMaxHp()
    {
        Debug.Log("mob has maxHp: " + MaxHpParam.GetResultStat());
        return base.GetMaxHp();
    }
    public override float GetAttackSpeed()
    {
        Debug.Log("mob has attack speed: " + SpeedAttackParam.GetResultStat());
        return base.GetAttackSpeed();
    }
}
