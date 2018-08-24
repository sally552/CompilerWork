using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonActivatedPerk : MonoBehaviour
{
    public ActivePerk activePerk { get; private set; }

    [SerializeField]
    private Image image;

    private Button skill;

    private void Start()
    {
        skill = GetComponent<Button>();

        skill.onClick.AddListener(() => 
        {
            if(activePerk != null)
            activePerk.ActivatedSkill();
        });
    }

    private void Update()
    {
        if (activePerk != null)
        {
            image.fillAmount = activePerk.PercentTime();
        }
    }

    public void InitPerc(ActivePerk perk)
    {
        activePerk = perk;
        skill.onClick.RemoveAllListeners();
        skill.onClick.AddListener(() =>
        {
            if (activePerk != null)
                activePerk.ActivatedSkill();
            Debug.Log(activePerk.name);
        });

        image.sprite = perk.image;
    }
}
