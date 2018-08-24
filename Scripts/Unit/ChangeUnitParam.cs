using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeUnitParams
{
    protected UnitParam unitParam;

    protected ParamUnit DamageParam;
    protected ParamUnit SpeedAttackParam;
    protected ParamUnit MaxHpParam;

    public ChangeUnitParams(UnitParam _unitParam)
    {
        unitParam = _unitParam;

        DamageParam = new ParamUnit(ParamsType.Damage, unitParam.Damage);
        SpeedAttackParam = new ParamUnit(ParamsType.AttackSpeed, unitParam.SpeedAttack);
        MaxHpParam = new ParamUnit(ParamsType.MaxHP, unitParam.maxHP);
    }

    public virtual float GetDistance()
    {
        return unitParam.AttackDistance;
    }
    public virtual float GetDamage()
    {
        return DamageParam.GetResultStat();
    }
    public virtual float GetAttackSpeed()
    {
        return SpeedAttackParam.GetResultStat();
    }
    public virtual float GetCoolDown()
    {
        return unitParam.CoolDown;
    }

    public virtual float GetMaxHp()
    {
        return MaxHpParam.GetResultStat();
    }

    public void ChangeParam(ParamUnit _param)
    {
        switch (_param.GetParamsType())
        {
            case ParamsType.AttackSpeed:
                ChangeParam(SpeedAttackParam, _param);
                break;
            case ParamsType.Damage:
                ChangeParam(DamageParam, _param);
                break;
            case ParamsType.MaxHP:
                ChangeParam(MaxHpParam, _param);
                break;
            default:
                break;
        }

    }

    protected void ChangeParam(ParamUnit param, ParamUnit newParam)
    {
        param.AddPercent(newParam.Percent);
        param.AddValue(newParam.ParamValue);
    }
}
