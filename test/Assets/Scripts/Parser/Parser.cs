using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Parser
{

    public string Process(string data)
    {
        // убираем пробелы и выделяем скобки
        string expression = Preprocess(data);
        
        int from = 0;

        return Calculate.Instance.loadAndCalculate(data, ref from, Calculate.END_LINE).ToString();
    }

    private string Preprocess(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            Debug.Log("пустая строка");
            ErrorHandler.SetError(ErrorHandler.ErrorType.EmptyLine, 0.ToString());
        }

        int parenthesesCount = 0;
        int parentheses = 0;
        int value = 0;
        StringBuilder result = new StringBuilder(data.Length);

        for (int i = 0; i < data.Length; i++)
        {
            char ch = data[i];
            switch (ch)
            {
                case ' ':
                case '\t':
                case '\n': continue;
                case Calculate.END_ARG:
                    parenthesesCount--;
                    break;
                case Calculate.START_ARG:
                    parenthesesCount++;
                    break;
            }
            result.Append(ch);
            if (parenthesesCount != value)
            {
                if (parenthesesCount == 0 || (parenthesesCount == 1 && value == 0))
                    parentheses = i+1;
                if (parenthesesCount == -1 && value == 0)
                {
                    parentheses = i+1;
                }
                value = parenthesesCount;
            }
               
        }

        if (parenthesesCount != 0)
        {
            Debug.Log("Не хватает скобок");
            ErrorHandler.SetError(ErrorHandler.ErrorType.NotHaveparentheses, parentheses.ToString());
        }

        return result.ToString();
    }
}
