using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AliveEnemy
{
    private List<EnemyController> aliveEnemyes = new List<EnemyController>();

    public bool IsAliveEnemy()
    {
        return aliveEnemyes.Count == 0;
    }

    public void Clear()
    {
        aliveEnemyes = aliveEnemyes.Where(item => item != null).ToList();
    }

    public void AddEnemy(EnemyController enemy)
    {
        aliveEnemyes.Add(enemy);
    }
}
