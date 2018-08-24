using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(WorldSpaceHealthBar))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(HeroAnimator))]
[RequireComponent(typeof(Swipe))]
public class HeroController : UnitController
{
    [SerializeField]
    private HeroType _heroType;
    public HeroType Herotype { get { return _heroType; } private set { } }

    public Action OnAttackLeft;
    public Action OnAttackRight;
    public Action OnAttackUp;
    public Action OnAttackDawn;


    private Swipe _swipe;
    private ChangeHeroParams _paramsReceive;

    private List<AuraUnit> auraHero = new List<AuraUnit>();

    protected override void Start()
    {
        base.Start();
        gameObject.tag = "Player";
        _paramsReceive = new ChangeHeroParams((HeroProfile)_unitParam);
        _swipe = GetComponent<Swipe>();
        Debug.Log("Hero has maxHp: " + _paramsReceive.GetMaxHp());
    }

    private void Update()
    {
        coolDown += Time.deltaTime;
        if (coolDown >= _paramsReceive.GetCoolDown())
        {
            if (_swipe.SwipeLeft)
            {
                OnAttackLeft.Invoke();
                coolDown = 0;
                _paramsReceive.SetSwipeActive(SwipeEnum.Left);
            }
            if (_swipe.SwipeRight)
            {
                OnAttackRight.Invoke();
                coolDown = 0;
                _paramsReceive.SetSwipeActive(SwipeEnum.Right);
            }
            if (_swipe.SwipeUp)
            {
                OnAttackUp.Invoke();
                coolDown = 0;
                _paramsReceive.SetSwipeActive(SwipeEnum.Up);
            }
            if (_swipe.SwipeDawn)
            {
                OnAttackDawn.Invoke();
                coolDown = 0;
                _paramsReceive.SetSwipeActive(SwipeEnum.Dawn);
            }
        }
      //  GetHealt(10);
    }

    protected override float GetMaxHp()
    {
        return _paramsReceive.GetMaxHp();
    }

    //событие нанесения урона
    protected override void Damageble()
    {

        IEnumerator<TargetAttack> enemy=
         _paramsReceive.GetEnemy(transform.position).GetEnumerator();

        while (enemy.MoveNext())
        {
            enemy.Current._col.GetComponent<EnemyController>().GetAttack(
                enemy.Current._dmg, _paramsReceive.IsStun);
            if (_paramsReceive.part != null)
            {
                var part = Instantiate(_paramsReceive.part,
                    enemy.Current._col.transform.position, Quaternion.identity);
                part.Play();
            }
        }

    }

    protected override void GetHit(float dmg)
    {
        base.GetHit(dmg);
    }

    protected override void Death()
    {
        base.Death();
        gameObject.tag = "WP";
        StartCoroutine(LoadScene());
    }
    //////////////////////////////////////
    //////////////////////////////////////
    ///// временный костыль для тз////////
    //////////////////////////////////////
    //////////////////////////////////////
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      //  Destroy(this);

    }

    public override float GetAttackSpeed()
    {
        return _paramsReceive.GetAttackSpeed();
    }

    public override void SetParamSettings(ParamUnit _param)
    {
        _paramsReceive.ChangeParam(_param);
        Debug.Log("Hero has maxHp: " + _paramsReceive.GetMaxHp());
    }

    public void AddAuraPerc(AuraUnit aura)
    {
        auraHero.Add(aura);
        auraHero[auraHero.Count - 1].ActivatedAura(this);
    }
}