using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Create/Waves/Wave", fileName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField]
    private List<EnemySlot> enemyLine;
    public string nameWave = "тестовая волна";



    public IEnumerable<EnemySlot> GetSlot()
    {
        var sortedTime = from u in enemyLine
                     orderby u.timeSpawn
                     select u;
        return sortedTime;
    }
}


