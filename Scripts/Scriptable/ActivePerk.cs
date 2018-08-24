using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ActivePerk : ScriptableObject, IActivePerc
{
    [SerializeField]
    protected float timeRestartSkill;

    public ActiveType ActiveType;

    protected float time;
    protected float Time
    {
        get
        {
            if (time > timeRestartSkill)
                return timeRestartSkill;
            return time;
        }
        set
        {
            time = value;
        }
    }

    
    public Sprite image;

    public virtual void ActivatedSkill()
    {
        throw new NotImplementedException();
    }

    protected virtual IEnumerator RestartSkill()
    {
        while (true)
        {
            yield return null;
            Time += UnityEngine.Time.deltaTime;
        }
    }

    public virtual float PercentTime()
    {
        return Time / timeRestartSkill;
    }

    public virtual void Init(HeroController hero)
    {
        throw new NotImplementedException();
    }

    public virtual void Init(AliveEnemy alives)
    {
        throw new NotImplementedException();
    }

}

public enum ActiveType
{
    Hero,
    Enemy
}

