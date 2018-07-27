using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Text text;

    //по хорошему это надо убрать кудато.
    private Parser prs = new Parser();
    private ParserFunction prsFunction = new ParserFunction();


    private void Start()
    {
        //список всех нужных дополнительных операций, констант и прочего.
        prsFunction.AddFunction("Pow", new PowFunction());

        button.onClick.AddListener(() => 
            {
                ErrorHandler.ResetError();
                var result = calculate(inputField.text);

                if (ErrorHandler.GetError() != null)
                    result = ErrorHandler.GetError();
                text.text = result; 
            });
    }

    private string calculate(string expr)
    {
        return  prs.Process(expr);
    }

    //это для Unit-test'ов Hot-полиморфизм
    private void calculateTest(string expr, double expected)
    {
        string result = prs.Process(expr);

        string outcome = result == expected.ToString() ? "OK" : "NOK " + expected.ToString();
        Debug.Log("test: expr: "+ expr+ "  ==  " + result + " -> " + outcome);
    }
}
