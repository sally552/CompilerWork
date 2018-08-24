using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class EnemyFactory : MonoBehaviour
{
    public AliveEnemy AliveEnemy  { get; private set; } 

    [SerializeField]
    private List<Wave> _waves;

    private IEnumerator<Wave> _wave;
    private IEnumerator<EnemySlot> _slot;

    private bool IsLastWave = true;
    private bool IsEndWave = true;

    private List<ParamUnit> percEnemy;

    private void Awake()
    {

        _wave = GetWave().GetEnumerator();

        CoreLoopGame.Instance.OnEndWave += CompleteWave;
        CoreLoopGame.Instance.OnStartWave += StartWaves;
        CoreLoopGame.Instance.OnRestart += RestartWaves;

        AliveEnemy = new AliveEnemy();
        percEnemy = new List<ParamUnit>();

        _wave.MoveNext();
    }

    private void StartWaves()
    {
        _slot = _wave.Current.GetSlot().GetEnumerator();
        StartCoroutine(InstatieteEnemy());

        IsLastWave = !_wave.MoveNext();
        IsEndWave = true;
    }

    private void Update()
    {
        if (!IsEndWave && AliveEnemy.IsAliveEnemy())
        {
            CoreLoopGame.Instance.EndWave();
            IsEndWave = true;
            CompleteWave();
        }
        AliveEnemy.Clear();
    }

    private IEnumerator InstatieteEnemy()
    {
        float time = 0;
        while (_slot.MoveNext())
        {
            yield return new WaitForSeconds(_slot.Current.timeSpawn-time);
            time = _slot.Current.timeSpawn;
            GameObject go = GameObject.Instantiate(_slot.Current.prefab, _slot.Current.Spawn.GetSpawnLine(), Quaternion.identity);
            var controller = go.GetComponent<EnemyController>();
            controller._path = _slot.Current.Spawn;
            AliveEnemy.AddEnemy(controller);
            ///////////////////////////////////////////////////////////////////////////////
            foreach (var i in percEnemy)
            {
                if (percEnemy.Count > 0)
                    controller.SetParamSettings(i);
            }
        }
        IsEndWave = false;
        if (IsLastWave)
        {
            //последний моб последней волны вышел.
            CoreLoopGame.Instance.GameCompletedWin();
            CoreLoopGame.Instance.OnRestart();
        }

    }
    private IEnumerable<Wave> GetWave()
    {
        return _waves;
    }
    private void CompleteWave()
    {
        StopCoroutine(InstatieteEnemy());
    }

    public void RestartWaves()
    {
        _wave.Reset();
        _wave.MoveNext();
    }

    public string WaveName()
    {
        if (_wave.Current.name == null)
            return "нет волн";
        return _wave.Current.nameWave;
    }

    public void AddNewPercEnemy(ParamUnit param)
    {
        percEnemy.Add(param);
    }

    public void RemovePerc()
    {
        percEnemy.Clear();
    }
}