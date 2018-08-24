using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private UnitController memberShip;
    private Transform _camera;
    private Transform _transform;

    private void Start()
    {
        memberShip = gameObject.GetComponent<UnitController>();

        if (memberShip.transform.tag != "Player")
        {
            slider.gameObject.SetActive(false);
        }

        memberShip.OnChangeHp += (v) =>
        {
            slider.gameObject.SetActive(true);
            if (v <= 0)
            {
                slider.gameObject.SetActive(false);
                return;
            }
            slider.value = v;
        };
        _transform = transform;
        _camera = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if (memberShip != null)
        {
            if (memberShip.transform.tag != "Player")
            {
                slider.transform.LookAt(_camera);
            }
        }

    }
}
