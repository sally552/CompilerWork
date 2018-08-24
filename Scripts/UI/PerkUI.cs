using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PerkUI : MonoBehaviour
{
    public Action<PerkProfile> OnButtonClick;
    public PerkProfile perk { get; set; }

    [SerializeField]
    private Text namePerk;

    [SerializeField]
    private Text description;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Image image;

    public void SetName(string name)
    {
        namePerk.text = name;
    }

    public void SetDescription(string _description)
    {
        description.text = _description;
    }

    public void SetImage(Sprite _image)
    {
            image.sprite = _image;
    }

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            switch (perk.PerkType)
            {
                case PerkType.Aura:
                case PerkType.Passive:
                    OnButtonClick.Invoke(perk);
                    break;
                case PerkType.Effect:
                    OnButtonClick.Invoke(perk);
                    break;
                case PerkType.Object:
                    break;
                default: break;
            }
        });
    }
}
