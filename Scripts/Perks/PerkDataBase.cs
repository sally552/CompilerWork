using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PerkDataBase : MonoBehaviour
{
    [SerializeField]
    private List<PerkProfile> allPerk;

    private HeroController heroController;
    private EnemyFactory enemyFactory;
    [SerializeField]
    private List<ButtonActivatedPerk> activePerk;


    private void Start()
    {
        enemyFactory = FindObjectOfType<EnemyFactory>();
        heroController = FindObjectOfType<HeroController>();
    }

    public void AddNowPerk(PerkProfile perk)
    {
        ActivatedPerk(perk);
        if (perk.IsRepeat == false)
            allPerk.Remove(perk);
    }

    public IEnumerable<PerkProfile> GetPercs()
    {
        System.Random rnd = new System.Random();

        return allPerk.
            Where(perk => perk.HeroesActivatedPerk.
            Any(x => (x == heroController.Herotype) || (x == HeroType.All))).
            OrderBy(i => rnd.Next()).
            Take(3);
    }

    private void ActivatedPerk(PerkProfile perk)
    {
        Debug.Log("Perc Activated: " + perk.PerkName);
        switch (perk.PerkType)
        {
            case PerkType.Passive:
                ActivatedPassive(perk);
                break;
            case PerkType.Effect:
                ActivatedEffect(perk);
                break;
            case PerkType.Object:
                break;
            case PerkType.Aura:
                ActivatedAura(perk);
                break;
            default: break;
        }
    }

    private void ActivatedPassive(PerkProfile perk)
    {
        switch (perk.UnitType)
        {
            case UnitType.Enemy:
                foreach (var i in perk.GetPerkParam())
                    enemyFactory.AddNewPercEnemy(i);
                break;
            case UnitType.Hero:
                foreach (var i in perk.GetPerkParam())
                    heroController.SetParamSettings(i);
                break;
            case UnitType.All:
                foreach (var i in perk.GetPerkParam())
                {
                    heroController.SetParamSettings(i);
                    enemyFactory.AddNewPercEnemy(i);
                }
                break;
            default: break;
        }
    }

    private void ActivatedAura(PerkProfile perk)
    {
        heroController.AddAuraPerc(perk.auraUnit);
    }

    private void ActivatedEffect(PerkProfile perk)
    {
        foreach (var i in activePerk)
        {
            if (i.activePerk == null)
            {   

                if (perk.active.ActiveType == ActiveType.Enemy)
                    perk.active.Init(enemyFactory.AliveEnemy);
                if (perk.active.ActiveType == ActiveType.Hero)
                    perk.active.Init(heroController);
                i.InitPerc(perk.active);
                break;
            }
        }
    }

}
