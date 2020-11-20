
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette
{
    public static readonly Dictionary<ObjType, Color> Colors = new Dictionary<ObjType, Color>()
    {
        {ObjType.Number, new Color(0.9f, 0.9f, 1)},
        {ObjType.Vector, new Color(0.9f, 1, 1)}
    };
}
