using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

//класс для обработки ошибок ошибок
public class ErrorHandler
{

    public static void SetError(ErrorType _errorStr, string _erorrValue)
    {
        if (errorType == ErrorType.None)
        {
            errorType = _errorStr;
            errorCh = _erorrValue;
        }
    }


    private static ErrorType errorType;
    private static string errorCh;

    public static string GetError()
    {
        if (errorType != ErrorType.None)
            return "Oшибка: " + GetErrorType(errorType) + errorCh;
        return null;
    }

    public static void ResetError()
    {
        errorType = ErrorType.None;
    }

    private static string GetErrorType(ErrorType errorType)
    {
        var da = (DescriptionAttribute[])(errorType.GetType().GetField(errorType.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return da.Length > 0 ? da[0].Description : errorType.ToString();
    }

    public enum ErrorType
    {
        [Description("Ошибок нет")]
        None = 0,
        [Description("Деление на ноль, выражение: ")]
        DivizionZero = 1,
        [Description("Не хватает скобок, номер знака: ")]
        NotHaveparentheses = 2,
        [Description("Пустая строка")]
        EmptyLine = 3,
        [Description("Проблема со знаками. обратите внимание на знак: ")]
        ProblemSigns = 4,

    }
}


