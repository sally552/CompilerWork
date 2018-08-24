using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Profile/Enemy", fileName = "Enemy")]
public class EnemyProfile : UnitParam
{
    [Space]
    [Header("Speed Move")]
    public float speed;   
}
