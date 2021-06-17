using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class containing constants for the colors
public class ColorPreset 
{

    //Primary Colors
    public static ColorPreset CYAN = new ColorPreset(ColorNames.CYAN, "Cyan", new Color(0, 1, 1));
    public static ColorPreset MAGENTA = new ColorPreset(ColorNames.MAGENTA, "Magenta", new Color(1, 0, 1));
    public static ColorPreset YELLOW = new ColorPreset(ColorNames.YELLOW, "Gelb", new Color(1, 1, 0));

    //Secondary Colors
    public static ColorPreset RED = new ColorPreset(ColorNames.RED, "Rot", new Color(1, 0, 0));
    public static ColorPreset BLUE = new ColorPreset(ColorNames.BLUE, "Blau", new Color(0, 0, 1));
    public static ColorPreset GREEN = new ColorPreset(ColorNames.GREEN, "Grün", new Color(0, 1, 0));

    //Tertiary Colors
    public static ColorPreset CYAN_GREEN = new ColorPreset(ColorNames.CYAN_GREEN, "Cyan_Gruen", new Color(0, 1, 0.5f));
    public static ColorPreset CYAN_BLUE = new ColorPreset(ColorNames.CYAN_BLUE, "Cyan_Blau", new Color(0, 0.5f, 1));
    public static ColorPreset MAGENTA_RED = new ColorPreset(ColorNames.MAGENTA_RED, "Magenta_Rot", new Color(1, 0, 0.5f));
    public static ColorPreset MAGENTA_BLUE = new ColorPreset(ColorNames.MAGENTA_BLUE, "Magenta_Blau", new Color(0.5f, 0, 1));
    public static ColorPreset YELLOW_RED = new ColorPreset(ColorNames.YELLOW_RED, "Gelb_Rot", new Color(1, 0.5f, 0));
    public static ColorPreset YELLOW_GREEN = new ColorPreset(ColorNames.YELLOW_GREEN, "Gelb_Blau", new Color(0.5f, 1, 0));

    //Black (ändern?)
    public static ColorPreset BLACK = new ColorPreset(ColorNames.BLACK, "Schwarz", new Color(0, 0, 0));


    private ColorNames id;
    private string displayName;
    private Color color;

    ColorPreset(ColorNames id, string name, Color color)
    {
        this.id = id;
        this.displayName = name;
        this.color = color;
    }

    private static List<ColorPreset> GetValues()
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
