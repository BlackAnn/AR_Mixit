using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton class containing a dictionary with all the mixed colors
public sealed class MixedColors 
{

    private static MixedColors instance = null;
    private static Dictionary<Tuple<ColorNames, ColorNames>, ColorNames> mixedColorsDict;

    private MixedColors() {
        mixedColorsDict = new Dictionary<Tuple<ColorNames, ColorNames>, ColorNames>();
        //Mixing Primary-Primary
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.CYAN_BLUE, ColorNames.YELLOW), ColorNames.GREEN);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.CYAN_BLUE, ColorNames.MAGENTA_RED), ColorNames.VIOLETT);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.YELLOW, ColorNames.MAGENTA_RED), ColorNames.ORANGE);

        //Mixing Primary-Secondary
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.CYAN_BLUE, ColorNames.GREEN), ColorNames.BLUE_GREEN);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.CYAN_BLUE, ColorNames.VIOLETT), ColorNames.BLUE_VIOLETT);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.CYAN_BLUE, ColorNames.ORANGE), ColorNames.BLACK);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.MAGENTA_RED, ColorNames.GREEN), ColorNames.BLACK);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.MAGENTA_RED, ColorNames.VIOLETT), ColorNames.PURPLE_RED);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.MAGENTA_RED, ColorNames.ORANGE), ColorNames.ORANGE_RED);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.YELLOW, ColorNames.GREEN), ColorNames.LIGHT_GREEN);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.YELLOW, ColorNames.VIOLETT), ColorNames.BLACK);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.YELLOW, ColorNames.ORANGE), ColorNames.DARK_YELLOW);

        //Mixing Secondary-Secondary  -> TO DO: Kontrollieren
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.ORANGE, ColorNames.VIOLETT), ColorNames.BLACK);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.ORANGE, ColorNames.GREEN), ColorNames.BLACK);
        mixedColorsDict.Add(new Tuple<ColorNames, ColorNames>(ColorNames.VIOLETT, ColorNames.GREEN), ColorNames.BLACK);

    }



    public static MixedColors Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MixedColors();
            }
            return instance;
        }
    }

    //Aufrauf Bsp: MixedColors.Instance.GetMixedColor(color1, color2)
    //returns the result of mixing color1 and color2
    public ColorNames GetMixedColor(ColorNames color1, ColorNames color2)
    {
        Tuple <ColorNames, ColorNames> combi1 = new Tuple <ColorNames, ColorNames>(color1, color2);
        Tuple<ColorNames, ColorNames> combi2 = new Tuple<ColorNames, ColorNames>(color2, color1);

        if (mixedColorsDict.ContainsKey(combi1))
        {
            return mixedColorsDict[combi1];
        } else if (mixedColorsDict.ContainsKey(combi2))
        {
            return mixedColorsDict[combi2];
        }
        else
        {
            throw new ArgumentException("The dictionary does not contain the given color combination");
        }
        
    }

}
