using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(WorldSpaceHealthBar))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class EnemyController : UnitController
{
    public Action OnAttack;
    public Action OnRun;


    public PathEnemyes _path { get; set; }
    //////////////////////
    [SerializeField]
    private Rigidbody model;
    //////////////////////


    private NavMeshAgent _agent;
    private IEnumerator<NavMeshPath> _pathMesh;
    private enum EnemyState
    {
        RUN,
        ATTACK,
        DEATH
    };
    private EnemyState CurrentState = EnemyState.RUN;
    private HeroController _hero;
    private ChangeEnemyParams _paramsReceive;

    private List<ParamUnit> percEnemy = new List<ParamUnit>();


    protected override void Start()
    {
        base.Start();
        gameObject.tag = "Enemy";
        _agent = GetComponent<NavMeshAgent>();
        _paramsReceive = new ChangeEnemyParams((EnemyProfile)_unitParam);
        SetParam();
        _agent.speed = ((EnemyProfile)_unitParam).speed;
        _pathMesh = _path.GetPathMesh().GetEnumerator();
         _agent.SetDestination(_path.GetSpawnLine());
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            _hero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>();
        }
        SetKinematic(true);
        StartCoroutine(StateRun());
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (Vector3.Distance(transform.position, _hero.transform.position) <= _paramsReceive.GetDistance())
            {
                CurrentState = EnemyState.ATTACK;
            }
        }
        else _hero = null;
        coolDown += Time.deltaTime;
    }
    protected override void Damageble()
    {
        _hero.GetAttack(_paramsReceive.GetDamage(), false);
    }

    private IEnumerator StateRun()
    {
        //настройка состояния
        CurrentState = EnemyState.RUN;
        _agent.isStopped = false;

        //само состояние
        while (CurrentState == EnemyState.RUN)
        {
            if (!_agent.hasPath)
            {
                if (_pathMesh.MoveNext())
                {
                    _agent.SetPath(_pathMesh.Current);
                }
                else //дошли до конца
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                OnRun.Invoke();
            }
            yield return null;
        }

        if (CurrentState == EnemyState.ATTACK)
            StartCoroutine(StateAttack());
    }
    private IEnumerator StateAttack()
    {

        //настройка состояния
        CurrentState = EnemyState.ATTACK;
        _agent.isStopped = true;
      coolDown = _paramsReceive.GetCoolDown();

        //само состояние
        while (CurrentState == EnemyState.ATTACK)
        {
            if (coolDown >= _paramsReceive.GetCoolDown())
            {
                coolDown = 0;
                OnAttack.Invoke();
            }
            if (!_hero)
                StartCoroutine(StateRun());
            if(_hero)
                transform.LookAt(_hero.transform);
            yield return null;

        }
    }
    private IEnumerator StateDeath()
    {
        CurrentState = EnemyState.DEATH;
        _agent.enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        //SetKinematic(false);
        while (CurrentState == EnemyState.DEATH)
        {
            yield return new WaitForSeconds(0.01f);
            SetKinematic(false);
            //transform.rotation = UnityEngine.Random.rotation;
            Destroy(gameObject,5f);
            Destroy(this);
        }
    }

    private void SetKinematic(bool IsActive)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        Vector3 dir = Vector3.zero;
        if (!IsActive)
        {
            dir = -transform.right;
            if (_path.DeathDirection == DeathDirection.Right && _hero)
                dir = transform.right;
          //  dir = transform.TransformDirection(dir);
        }
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = IsActive;
            rb.AddForce((dir*3+ Vector3.up*5).normalized * 1300, ForceMode.Force);

        }
    }
    private void SetParam()
    {
        foreach (var i in percEnemy)
        {
            if (percEnemy.Count > 0)
            {
                _paramsReceive.ChangeParam(i);
            }
        }
    }
    protected override void GetHit(float dmg)
    {
        base.GetHit(dmg);
        if(_hero)
        _hero.Struck(dmg);
    }
    protected override void Death()
    {
        base.Death();
        gameObject.tag = "Zombie";
        StartCoroutine(StateDeath());
    }

    public override float GetAttackSpeed()
    {
        return _paramsReceive.GetAttackSpeed();
    }

    public override void SetParamSettings(ParamUnit param)
    {
        percEnemy.Add(param);
    }
}
