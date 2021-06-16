using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPreset 
{

  
    public static ColorPreset CYAN = new ColorPreset(ColorNames.CYAN, "Cyan", Resources.Load<Material>("Materials/cyanMaterial"));
    public static ColorPreset MAGENTA = new ColorPreset(ColorNames.MAGENTA, "Magenta", Resources.Load<Material>("Materials/magentaMaterial"));
    public static ColorPreset YELLOW = new ColorPreset(ColorNames.YELLOW, "Gelb", Resources.Load<Material>("Materials/yellowMaterial"));

    public static ColorPreset RED = new ColorPreset(ColorNames.RED, "Rot", Resources.Load<Material>("Materials/redMaterial"));
    public static ColorPreset BLUE = new ColorPreset(ColorNames.BLUE, "Blau", Resources.Load<Material>("Materials/blueMaterial"));
    public static ColorPreset GREEN = new ColorPreset(ColorNames.GREEN, "Grün", Resources.Load<Material>("Materials/greenMaterial"));

    private ColorNames id;
    private string displayName;
    //private Color color;
    private Material material;

    ColorPreset(ColorNames id, string name, Material material)
    {
        this.id = id;
        this.displayName = name;
        //this.color = color;
        this.material = material;
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

        return list;
    }

    public static Material GetMaterialById(int id)
    {
        //TO DO: add check if id exists
        return GetValues()[id].material;
    }

    public static string GetDisplayNameById(int id)
    {
        //TO DO: add check if id exists
        return GetValues()[id].displayName;
    }


}
