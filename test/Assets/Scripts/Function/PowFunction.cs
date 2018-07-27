using UnityEngine;
using System;

class PowFunction : ParserFunction
{
    // для примера функция возведения в степень.
    protected override double Evaluate(string data, ref int from)
    {
        double arg1 = Calculate.Instance.loadAndCalculate(data, ref from, ',');
        double arg2 = Calculate.Instance.loadAndCalculate(data, ref from, Calculate.END_ARG);

        return Math.Pow(arg1, arg2);
    }
}