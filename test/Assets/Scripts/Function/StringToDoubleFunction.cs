using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class StrtodFunction : ParserFunction
{
    protected override double Evaluate(string data, ref int from)
    {
        double num;
        if (!Double.TryParse(Item, out num))
        {
            ErrorHandler.SetError(ErrorHandler.ErrorType.ProblemSigns, from.ToString());
        }
        return num;
    }
    public string Item { private get; set; }
}
