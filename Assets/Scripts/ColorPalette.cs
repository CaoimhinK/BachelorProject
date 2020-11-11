
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette
{
    public static readonly Dictionary<TypeEnum, Color> Colors = new Dictionary<TypeEnum, Color>()
    {
        {TypeEnum.Number, new Color(0.9f, 0.9f, 1)},
        {TypeEnum.Vector, new Color(0.9f, 1, 1)}
    };
}
