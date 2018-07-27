using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParserFunction
{
    private ParserFunction m_impl;

    public ParserFunction()
    {
        m_impl = this;
    }

    public ParserFunction(string data, ref int from, string item, char ch)
    {
        if (item.Length == 0 && ch == Calculate.START_ARG)
        {
            // функции нет, выражение в круглых скобках
            m_impl = s_idFunction;
            return;
        }

        if (m_functions.TryGetValue(item, out m_impl))
        {
            // известная функция
            return;
        }

        // функции нет, возможно число
        s_strtodFunction.Item = item;
        m_impl = s_strtodFunction;
    }

    public void AddFunction(string name, ParserFunction function)
    {
        m_functions[name] = function;
    }

    public double GetValue(string data, ref int from)
    {
        return m_impl.Evaluate(data, ref from);
    }

    protected virtual double Evaluate(string data, ref int from)
    {
        return 0;
    }


    private static Dictionary<string, ParserFunction> m_functions = new Dictionary<string, ParserFunction>();

    private static  StrtodFunction s_strtodFunction = new StrtodFunction();
    private static  IdentityFunction s_idFunction = new IdentityFunction();
}
