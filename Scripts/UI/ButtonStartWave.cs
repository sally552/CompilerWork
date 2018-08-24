using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStartWave : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    private Button _button;
    private string startStr = "Start game";
    private string nextWave = "Next wave";
    private string gameOver = "Game over";
    private string gameWin = "Game win";

    private void Start()
    {
        _button = GetComponent<Button>();
        _text.text = startStr;
        _button.onClick.AddListener(() =>
        {
            CoreLoopGame.Instance.StartWave();
            gameObject.SetActive(false); 
        });

        CoreLoopGame.Instance.OnAddPerk += () =>
        {
            _text.text = nextWave;
            gameObject.SetActive(true);
        };

        CoreLoopGame.Instance.OnWin += () =>
        {
            _text.text = gameWin;
        };
        CoreLoopGame.Instance.OnLose += () =>
        {
            _text.text = gameOver;
        };

        //CoreLoopGame.Instance.Restart += () =>
        //{
        //    gameObject.SetActive(true);
        //    _text.text = startStr;
        //};
    }
}
