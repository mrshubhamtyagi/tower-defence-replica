using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtentionMehods
{
    public static Color HexToColor(this Color color, string colorString)
    {
        if (ColorUtility.TryParseHtmlString(colorString, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Could not convert color.");
            return color;
        }
    }
}
