using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class IdentityFunction : ParserFunction
{
    protected override double Evaluate(string data, ref int from)
    {
        return Calculate.Instance.LoadAndCalculate(data, ref from, Calculate.END_ARG);
    }
}
