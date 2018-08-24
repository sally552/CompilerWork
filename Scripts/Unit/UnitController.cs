using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class UnitController : MonoBehaviour
{
    public Action OnDeath;
    public Action<float> OnGetHit;
    public Action<float> OnChangeHp;
    public Action<float> OnGetHealt;
    public Action<float> OnStructed;

    public Action OnGetHitAnim;

    [SerializeField]
    protected UnitParam _unitParam;

    protected float _hp;
    protected float Hp
    {
        get
        {
            if (_hp >= GetMaxHp())
                Hp = GetMaxHp();
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    protected float coolDown;

    protected abstract void Damageble();

    public abstract float GetAttackSpeed();

    public void GetAttack(float dmg, bool isStun = false)
    {
        OnGetHit.Invoke(dmg);
        if (isStun)
        {
            OnGetHitAnim.Invoke();
        }
    }

    protected virtual void Start()
    {
        OnGetHit += GetHit;
        OnDeath += Death;
        OnGetHealt += GetHealt;
        Hp = _unitParam.maxHP;

    }

    protected virtual void GetHit(float dmg)
    {
        Hp -= dmg;
        OnChangeHp.Invoke(Hp / GetMaxHp());
        if (Hp <= 0)
        {
            OnDeath.Invoke();
        }
    }

    public virtual void GetHealt(float _health)
    {
        Hp += _health;
        OnChangeHp.Invoke(Hp / GetMaxHp());
    }

    protected virtual float GetMaxHp()
    {
        return _unitParam.maxHP;
    }

    protected virtual void Death()
    {
        Debug.Log(gameObject.name + "  умер");
    }

    public abstract void SetParamSettings(ParamUnit param);

    public virtual void Struck(float dmg)
    {
        Debug.Log("Удар юнита:" + gameObject.name + " достиг цели");
        if (OnStructed != null)
            OnStructed.Invoke(dmg);
    }

}
