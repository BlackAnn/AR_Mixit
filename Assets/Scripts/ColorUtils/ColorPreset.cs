using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class containing constants for the colors
public class ColorPreset
{

    //Primary Colors
    public static ColorPreset CYAN = new ColorPreset(ColorNames.CYAN, "Cyan", new Color(0, 1, 1), false);
    public static ColorPreset MAGENTA = new ColorPreset(ColorNames.MAGENTA, "Magenta", new Color(1, 0, 1), false);
    public static ColorPreset YELLOW = new ColorPreset(ColorNames.YELLOW, "Gelb", new Color(1, 1, 0), false);

    //Secondary Colors
    public static ColorPreset RED = new ColorPreset(ColorNames.RED, "Rot", new Color(1, 0, 0), true);
    public static ColorPreset BLUE = new ColorPreset(ColorNames.BLUE, "Blau", new Color(0, 0, 1), true);
    public static ColorPreset GREEN = new ColorPreset(ColorNames.GREEN, "Grün", new Color(0, 1, 0), true);

    //Tertiary Colors
    public static ColorPreset CYAN_GREEN = new ColorPreset(ColorNames.CYAN_GREEN, "Cyan_Gruen", new Color(0, 1, 0.5f), true);
    public static ColorPreset CYAN_BLUE = new ColorPreset(ColorNames.CYAN_BLUE, "Cyan_Blau", new Color(0, 0.5f, 1), true);
    public static ColorPreset MAGENTA_RED = new ColorPreset(ColorNames.MAGENTA_RED, "Magenta_Rot", new Color(1, 0, 0.5f), true);
    public static ColorPreset MAGENTA_BLUE = new ColorPreset(ColorNames.MAGENTA_BLUE, "Magenta_Blau", new Color(0.5f, 0, 1), true);
    public static ColorPreset YELLOW_RED = new ColorPreset(ColorNames.YELLOW_RED, "Gelb_Rot", new Color(1, 0.5f, 0), true);
    public static ColorPreset YELLOW_GREEN = new ColorPreset(ColorNames.YELLOW_GREEN, "Gelb_Blau", new Color(0.5f, 1, 0), true);

    //Black (ändern?)
    public static ColorPreset BLACK = new ColorPreset(ColorNames.BLACK, "Schwarz", new Color(0, 0, 0), false);


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

        list.Insert((int)CYAN.id,CYAN);
        list.Insert((int)MAGENTA.id, MAGENTA);
        list.Insert((int)YELLOW.id, YELLOW);
        list.Insert((int)RED.id, RED);
        list.Insert((int)BLUE.id, BLUE);
        list.Insert((int)GREEN.id, GREEN);
        list.Insert((int)CYAN_GREEN.id, CYAN_GREEN);
        list.Insert((int)CYAN_BLUE.id, CYAN_BLUE);
        list.Insert((int)MAGENTA_RED.id, MAGENTA_RED);
        list.Insert((int)MAGENTA_BLUE.id, MAGENTA_BLUE);
        list.Insert((int)YELLOW_RED.id, YELLOW_RED);
        list.Insert((int)YELLOW_GREEN.id, YELLOW_GREEN);
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
