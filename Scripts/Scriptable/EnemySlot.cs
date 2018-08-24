using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemySlot
{
    public GameObject prefab;
    public float timeSpawn = 1f;
    [SerializeField]
    private string nameSpawn;


    private PathEnemyes _spwan;
    public PathEnemyes Spawn
    {
        get
        {
            if (_spwan == null)
            {
                _spwan = GameObject.Find(nameSpawn).GetComponent<PathEnemyes>();
            }
            return _spwan;
        }
        private set { }
    }
}
