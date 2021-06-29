using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing constants for colors used in the app. Each color preset has a display name, a color value and a boolean indicating if the color is mixable or not.
/// </summary>
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
    public static ColorPreset LIGHT_GREEN = new ColorPreset(ColorNames.LIGHT_GREEN, "Hellgruen", new Color(140/255f, 187/255f, 38/255f), true);
    public static ColorPreset DARK_YELLOW = new ColorPreset(ColorNames.DARK_YELLOW, "Dunkelgelb", new Color(253/255f, 198/255f, 11/255f), true);
    public static ColorPreset ORANGE_RED = new ColorPreset(ColorNames.ORANGE_RED, "Orangerot", new Color(234/255f, 98/255f, 31/255f), true);
    public static ColorPreset PURPLE_RED = new ColorPreset(ColorNames.PURPLE_RED, "Purpurrot", new Color(196/255f, 3/255f, 125/255f), true);
    public static ColorPreset BLUE_VIOLETT = new ColorPreset(ColorNames.BLUE_VIOLETT, "Blauviolett", new Color(68/255f, 78/255f, 153/255f), true);
    public static ColorPreset BLUE_GREEN = new ColorPreset(ColorNames.BLUE_GREEN, "Blaugruen", new Color(6/255f, 150/255f, 187/255f), true);

    //Black (ändern?)
    public static ColorPreset BLACK = new ColorPreset(ColorNames.BLACK, "Dunkelgrau", new Color(0.3f, 0.3f, 0.3f), false);


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

    /// <summary>
    /// returns the display name of the color
    /// </summary>
    /// <returns>display name as string</returns>
    public string GetDisplayName() {
        return displayName;
    }

    /// <summary>
    /// returns the enum matching the color
    /// </summary>
    /// <returns>ColorNames enum</returns>
    public ColorNames GetID() {
        return id;
    }

    /// <summary>
    /// returns if color is mixable or not
    /// </summary>
    /// <returns>true, if color is mixable</returns>
    public bool GetMixable() {
        return mixable;
    }

    /// <summary>
    /// returns color value of the Color preset
    /// </summary>
    /// <returns>Color value</returns>
    public Color GetColor() {
        return color;
    }

    /// <summary>
    /// returns a list containint all the color presets
    /// </summary>
    /// <returns>List with all the Color Presets</returns>
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

    /// <summary>
    /// returns a color value based on its index
    /// </summary>
    /// <param name="id">index of the color value in the color preset list. The id is matching with the ColorNames enum</param>
    /// <returns>Color matching the id</returns>
    public static Color GetColorById(int id)
    {
        return GetValues()[id].color;
    }

    /// <summary>
    /// returns a color display name based on its index
    /// </summary>
    /// <param name="id">index of the color value in the color preset list. The id is matching with the ColorNames enum</param>
    /// <returns>display name matching the id</returns>
    public static string GetDisplayNameById(int id)
    {
        return GetValues()[id].displayName;
    }

    /// <summary>
    /// returns display name based on a color Value
    /// </summary>
    /// <param name="color">color value, for which the display name will be returned</param>
    /// <returns>matching display name, null if no display name is found</returns>
    public static string GetDisplayNameByColor(Color color)
    {
            List<ColorPreset> colorList = ColorPreset.GetValues();
            foreach (ColorPreset c in colorList)
            {
                if (color.Equals(c.GetColor()))
                {
                    return c.GetDisplayName();
                }
            }
            return null;
    }


}
