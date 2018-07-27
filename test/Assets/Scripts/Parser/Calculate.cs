using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;


public class Calculate 
{

    public const char START_ARG = '(';
    public const char END_ARG = ')';
    public const char END_LINE = '\n';

    private static Calculate instance;
    public static Calculate Instance
    {
        get
        {
            if (instance == null)
                instance = new Calculate();
            return instance;
        }
        private set { }
    }

    // основной процесс
    public double loadAndCalculate(string data, ref int from, char to = END_LINE)
    {
        if (from >= data.Length || data[from] == to)
        {
            Debug.Log("Loaded invalid data: " + data);
        }

        List<Cell> listToMerge = new List<Cell>(16);
        StringBuilder item = new StringBuilder();

        do
        { // первая часть
            char ch = data[from++];
            if (StillCollecting(item.ToString(), ch, to))
            { // если символ относится к предыдущему операнду
                item.Append(ch);
                if (from < data.Length && data[from] != to)
                {
                    continue;
                }
            }

            //закончили получение следующей части, рекурсивно вызывать части далее
            ParserFunction func = new ParserFunction(data, ref from, item.ToString(), ch);
            double value = func.GetValue(data, ref from);

            char action = ValidAction(ch) ? ch
                                          : UpdateAction(data, ref from, ch, to);

            listToMerge.Add(new Cell(value, action));
            //item.Clear(); // для 4.0
            item.Length = 0;

        } while (from < data.Length && data[from] != to);

        if (from < data.Length && (data[from] == END_ARG || data[from] == to))
        { // рекурсивно перемещать один символ вперед
            from++;
        }

        Cell baseCell = listToMerge[0];
        int index = 1;

        return Merge(baseCell, ref index, listToMerge);
    }

    private int GetPriority(char action)
    {
        switch (action)
        {
            case '-':
            case '+': return 2;
            case '*':
            case '/': return 3;
            case '^': return 4;
            default: return 0;
        }
    }

    private bool StillCollecting(string item, char ch, char to)
    {
        // остановиться, если получили ) или ,
        char stopCollecting = (to == END_ARG || to == END_LINE) ?
                               END_ARG : to;
        return (item.Length == 0 && (ch == '-' || ch == END_ARG)) ||
              !(ValidAction(ch) || ch == START_ARG || ch == stopCollecting);
    }

    private bool ValidAction(char ch)
    {
        return ch == '*' || ch == '/' || ch == '+' || ch == '-' || ch == '^';
    }

    private char UpdateAction(string item, ref int from, char ch, char to)
    {
        if (from >= item.Length || item[from] == END_ARG || item[from] == to)
        {
            return END_ARG;
        }

        int index = from;
        char res = ch;
        while (!ValidAction(res) && index < item.Length)
        { // искать сивол до тех пор, пока не найдется действие
            res = item[index++];
        }

        from = ValidAction(res) ? index
                                : index > from ? index - 1
                                               : from;
        return res;
    }

    // вызывается снаружи + рекурсия внутри, возвращает результат слияния частей
    private double Merge(Cell current, ref int index, List<Cell> listToMerge, bool mergeOneOnly = false)
    {
        while (index < listToMerge.Count)
        {
            Cell next = listToMerge[index++];

            while (!СanMergeCells(current, next))
            {
                //ищем ячейки для слияния, если нельзя слить сейчас - переходим к селдующей.
                Merge(next, ref index, listToMerge, true /* mergeOneOnly */);
            }
            MergeCells(current, next);
            if (mergeOneOnly)
            {
                return current.Value;
            }
        }

        return current.Value;
    }

    private void MergeCells(Cell leftCell, Cell rightCell)
    {
        switch (leftCell.Action)
        {
            case '^':
                leftCell.Value = Math.Pow(leftCell.Value, rightCell.Value);
                break;
            case '*':
                leftCell.Value *= rightCell.Value;
                break;
            case '/':
                if (rightCell.Value == 0)
                {
                    Debug.Log("Деление на ноль");
                    ErrorHandler.SetError(ErrorHandler.ErrorType.DivizionZero, 
                       leftCell.Value.ToString() + leftCell.Action.ToString() + rightCell.Value.ToString());
                }
                leftCell.Value /= rightCell.Value;
                break;
            case '+':
                leftCell.Value += rightCell.Value;
                break;
            case '-':
                leftCell.Value -= rightCell.Value;
                break;
        }
        leftCell.Action = rightCell.Action;
    }

    private bool СanMergeCells(Cell leftCell, Cell rightCell)
    {
        return GetPriority(leftCell.Action) >= GetPriority(rightCell.Action);
    }
}
