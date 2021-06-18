using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//@Anna, hier sind ein paar Methoden/Variablen, die wichtig für dich sein könnten
//MixColors() aktiviert das Farbmischen, wenn zwei Spheren angezeigt werden
//Reset() setzt alles in den Initialzustand zurück (zB nachdem gemischt wurde)
//das boolean "displaySpheres" sagt aus ob die Spheren angezeigt werden sollen, wenn ein Image Target getrackt wird (während dem Mischen oder während einer anderen Nutzer-Interaktion sollen z.B. keine neuen Spheren angezeigt werden). 
public class GameManager : MonoBehaviour
{
    private Dictionary<ImageTarget, ColorSphere> activeSpheres = new Dictionary<ImageTarget, ColorSphere>();
    public ResultSphere resultSphere;
    public ParentObject parentObject;

    private bool displaySpheres;

    // Start is called before the first frame update
    void Start()
    {
        displaySpheres = true;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SubscribeTarget(ImageTarget marker)
    {
        
        if (!activeSpheres.ContainsKey(marker))
        {
            //only instanciate sphere if spheres are currently displaying
            if (displaySpheres)
            {
                Debug.Log("Subscribe Target: " + marker.colorId);

                activeSpheres.Add(marker, marker.InstanciateSphere());
                Debug.Log("Dict Length: " + activeSpheres.Count);

            }
            else
            {
                activeSpheres.Add(marker, null);
                Debug.Log("Subscribe Target (null): " + marker.colorId);
                Debug.Log("Dict Length: " + activeSpheres.Count);

            }
        }
        

    }

 

    public void UnsubscribeTarget(ImageTarget marker)
    {
        if (activeSpheres.ContainsKey(marker))
        {
            Debug.Log("Unsubscribe Target: " + marker.colorId);

            //destroy sphere before unsubscribing the marker
            if (activeSpheres[marker] != null)
            {
                Destroy(activeSpheres[marker].gameObject);
            }
            activeSpheres.Remove(marker);
            Debug.Log("Dict Length: " + activeSpheres.Count);

        }
    }

    
    //TO DO: Restrict max distance between the spheres (before mixing)
    public void MixColors()
    {
   
        //Mix only if there are two targets detected
        if (activeSpheres.Count == 2)
        {
            //once Mixing starts, no new spheres will be displayed
            displaySpheres = false;

            List<ColorSphere> mixingSpheres = new List<ColorSphere>(activeSpheres.Values);

            Vector3 resultPosition = CalculateMidpointPosition(mixingSpheres[0], mixingSpheres[1]);
            parentObject.SetPosition(resultPosition);

            Color resultColor = GenerateMixedColor(mixingSpheres[0], mixingSpheres[1]);

            //activate Mixing Animation
            foreach (ColorSphere sphere in mixingSpheres)
            {
                sphere.ActivateMixing(resultPosition, parentObject.gameObject);
            }
            parentObject.ActivateMixing();
            resultSphere.ShowSphere(resultColor, resultPosition);
        }
        else
        {
            Debug.Log("You need to lay down two target images in order two mix the two colors");
            //TO DO: add UI user info here
        }
    }

   
    private Vector3 CalculateMidpointPosition(ColorSphere sphere1, ColorSphere sphere2)
    {
        return (sphere1.GetPosition() + sphere2.GetPosition()) / 2;
    }

    private Color GenerateMixedColor(ColorSphere sphere1, ColorSphere sphere2)
    {
        int colorIndex = (int)MixedColors.Instance.GetMixedColor(sphere1.GetColorId(), sphere2.GetColorId());
        return ColorPreset.GetColorById(colorIndex);
    }


    //resets the gamelogic to its initial state (resultSphere disappears and new spheres will be displayed if image target is detected)
    public void Reset()
    {
        displaySpheres = true;

        resultSphere.Reset();

        //generate spheres, if there are already detected image targets
        List<ImageTarget> keys = new List<ImageTarget>(activeSpheres.Keys);
        foreach (ImageTarget key in keys)
        {
            if(activeSpheres[key] == null)
            {
                activeSpheres[key] = key.InstanciateSphere();
            }
        }
    }

}
