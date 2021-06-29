using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class containing constants for the colors
public class ColorPreset 
{

    //Primary Colors
    public static ColorPreset CYAN_BLUE = new ColorPreset(ColorNames.CYAN_BLUE, "Cyanblau", new Color(42/255f, 113/255f, 176/255f), false);
    public static ColorPreset MAGENTA_RED = new ColorPreset(ColorNames.MAGENTA_RED, "Magentarot", new Color(227/255f, 35/255f, 34/255f), false);
    public static ColorPreset YELLOW = new ColorPreset(ColorNames.YELLOW, "Gelb", new Color(244/255f, 229/255f, 0), false);

    //Secondary Colors
    public static ColorPreset ORANGE = new ColorPreset(ColorNames.ORANGE, "Orange", new Color(241/255f, 142/255f, 28/255f), true);
    public static ColorPreset VIOLETT = new ColorPreset(ColorNames.VIOLETT, "Violett", new Color(109/255f, 57/255f, 139/255f), true);
    public static ColorPreset GREEN = new ColorPreset(ColorNames.GREEN, "Gruen", new Color(0, 142/255f, 91/255f), true);

    //Tertiary Colors
    public static ColorPreset LIGHT_GREEN = new ColorPreset(ColorNames.LIGHT_GREEN, "Gellgruen", new Color(140/255f, 187/255f, 38/255f), true);
    public static ColorPreset DARK_YELLOW = new ColorPreset(ColorNames.DARK_YELLOW, "Dunkelgelb", new Color(253/255f, 198/255f, 11/255f), true);
    public static ColorPreset ORANGE_RED = new ColorPreset(ColorNames.ORANGE_RED, "Orangerot", new Color(234/255f, 98/255f, 31/255f), true);
    public static ColorPreset PURPLE_RED = new ColorPreset(ColorNames.PURPLE_RED, "Purpurrot", new Color(196/255f, 3/255f, 125/255f), true);
    public static ColorPreset BLUE_VIOLETT = new ColorPreset(ColorNames.BLUE_VIOLETT, "Blauviolett", new Color(68/255f, 78/255f, 153/255f), true);
    public static ColorPreset BLUE_GREEN = new ColorPreset(ColorNames.BLUE_GREEN, "Blaugruen", new Color(6/255f, 150/255f, 187/255f), true);

    //Black (ändern?)
    public static ColorPreset BLACK = new ColorPreset(ColorNames.BLACK, "Dunkelgrau", new Color(0.1f, 0.1f, 0.1f), false);


    private ColorNames id;
    private string displayName;
    private Color color;
    private bool mixable;

    ColorPreset(ColorNames id, string name, Color color, bool mixable)
    {
        this.id = id;
        this.displayName = name;
        this.color = color;
        this.mixable = mixable;
    }

    public ColorNames GetID() {
        return id;
    }

    public bool GetMixable() {
        return mixable;
    }

    public Color GetColor() {
        return color;
    }

    //ColorPreset.GetValues()[random]  --> random Farbwert erhalten
    //ColorPreset.GetValues()[(int)ColorNames.CYAN]  --> Werte fuer Cyan
    public static List<ColorPreset> GetValues()
    {
        List<ColorPreset> list = new List<ColorPreset>();

        list.Insert((int)CYAN_BLUE.id, CYAN_BLUE);
        list.Insert((int)MAGENTA_RED.id, MAGENTA_RED);
        list.Insert((int)YELLOW.id, YELLOW);
        list.Insert((int)ORANGE.id, ORANGE);
        list.Insert((int)VIOLETT.id, VIOLETT);
        list.Insert((int)GREEN.id, GREEN);
        list.Insert((int)LIGHT_GREEN.id, LIGHT_GREEN);
        list.Insert((int)DARK_YELLOW.id, DARK_YELLOW);
        list.Insert((int)ORANGE_RED.id, ORANGE_RED);
        list.Insert((int)PURPLE_RED.id, PURPLE_RED);
        list.Insert((int)BLUE_VIOLETT.id, BLUE_VIOLETT);
        list.Insert((int)BLUE_GREEN.id, BLUE_GREEN);
        list.Insert((int)BLACK.id, BLACK);

        return list;
    }

    //zB: id = (int)ColorNames.CYAN   ---> gibt Wert fuer Cyan zurueck
    public static Color GetColorById(int id)
    {
        //TO DO: add check if id exists
        return GetValues()[id].color;
    }

    public static string GetDisplayNameById(int id)
    {
        //TO DO: add check if id exists
        return GetValues()[id].displayName;
    }


}
