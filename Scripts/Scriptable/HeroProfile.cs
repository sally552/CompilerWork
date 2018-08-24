using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "Create/Profile/Hero", fileName = "Hero")]
public class HeroProfile : UnitParam
{


    [Space]
    [Header("attack param")]
    public List<SwipeAttackSettings> swapeSetting;

    [Space]
    [Header("Hero Type")]
    public HeroType heroType;

}
[Serializable]
public class SwipeAttackSettings
{
    public SwipeEnum swipeType;
    public float coolDown;
    public float speedAttack;

    public bool isStun;

    public List<PathSetting> settings;

    public ParticleSystem particleAttack;

    [Serializable]
    public class PathSetting
    {
        public string pathName;
        public float damage;
        public float attackDistance;
    }
}