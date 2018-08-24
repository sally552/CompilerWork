using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPerkUI : MonoBehaviour
{
    [SerializeField]
    [Header("префаб панели")]
    private GameObject panelPerk;

    [SerializeField]
    private Transform content;

    [SerializeField]
    [Header("панель, где будут перки")]
    private GameObject perkUI;

    [SerializeField]
    [Header("ссылка на базу с перками")]
    private PerkDataBase perkDataBase;

    private void Start()
    {
        CoreLoopGame.Instance.OnEndWave += OpenPAnel;
    }

    private void OpenPAnel()
    {
        perkUI.SetActive(true);
        IEnumerator<PerkProfile> newPerk = perkDataBase.GetPercs().GetEnumerator();
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        if (!newPerk.MoveNext())
        {
            ClosePanel(null);
            return;
        }
        do
        {
            var go = Instantiate(panelPerk, content);
            var perk = go.GetComponent<PerkUI>();
            perk.SetName(newPerk.Current.PerkName);
            perk.SetDescription(newPerk.Current.Description);
            perk.SetImage(newPerk.Current.Image);
            perk.perk = newPerk.Current;

            perk.OnButtonClick += ClosePanel;
        }
        while (newPerk.MoveNext());
    }

    private void ClosePanel(PerkProfile perc)
    {
        if(perc != null)
            perkDataBase.AddNowPerk(perc);
        perkUI.SetActive(false);
        CoreLoopGame.Instance.AddNewPerk();
    }
}
