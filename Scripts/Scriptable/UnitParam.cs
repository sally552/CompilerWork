using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


abstract public class UnitParam : ScriptableObject
{
    [Header("unit param")]
    public int maxHP;


    [Space]
    [Header("attack param")]
    public float AttackDistance;
    public float Damage;
    public float CoolDown;
    public float SpeedAttack;

}