using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell
{
    public Cell(double _value, char _action)
    {
        Value = _value;
        Action = _action;
    }

    public double Value { get; set; }
    public char Action { get; set; }
}
