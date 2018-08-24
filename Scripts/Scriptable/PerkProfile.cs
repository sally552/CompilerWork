using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New perc", menuName = "Create/New Perc")]
public class PerkProfile : ScriptableObject
{

    public string PerkName;                   // имя
    public string Description;                // описание

    public Sprite Image;

    public List<HeroType> HeroesActivatedPerk; // список героев, у кого может быть
    [SerializeField]
    protected List<WeightPerq> WeightInitialization; // вес
    public bool IsRepeat;                     // повторяется ли

    public PerkType PerkType;                // тип перка
    public UnitType UnitType;                // на кого действует
    //для параметров
    [SerializeField]
    private ParamSkill PerkParam;
    public List<ParamUnit> GetPerkParam()
    {
        return PerkParam.PerkParam;
    }

    //для аур
    [SerializeField]
    public AuraUnit auraUnit;

    //для активок
    public ActivePerk active;

    public IEnumerable<WeightPerq> GetWeight()
    {
        var WeightSelect = WeightInitialization.OrderBy(w => w.Wave);
        return WeightSelect;
    }
    [Serializable]
    public class WeightPerq
    {
        public int Wave;
        public int Weight;
    }
}
