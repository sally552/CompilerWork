using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ParamUnit
{
    [SerializeField]
    private ParamsType ParamType;
    [SerializeField]
    private float percent = 1;
    [SerializeField]
    private float valueParam = 0;

    private float result;

    public float ParamValue { get { return valueParam; } private set { } }
    public float Percent { get { return percent; } private set { } }

    public ParamUnit(float stat)
    {
        Percent = 1;
        ParamValue = 0;
        result = stat;
    }

    public ParamUnit(ParamsType _paramType, float stat) : this(stat)
    {
        ParamType = _paramType;
    }

    public float GetResultStat()
    {
        return result;
    }

    public ParamsType GetParamsType()
    {
        return ParamType;
    }

    public void AddPercent(float percent)
    {
        result *= percent;
    }

    public void AddValue(float value)
    {
        result += value;
    }
}
