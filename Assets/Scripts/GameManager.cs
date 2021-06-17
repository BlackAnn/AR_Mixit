using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//TO DO: divide game logic & color mixing logic (?)
public class GameManager : MonoBehaviour
{
    private Dictionary<string, ColorSphere> activeSpheres = new Dictionary<string, ColorSphere>();
    public ResultSphere resultSphere;
    public SphereParent sphereParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SubscribeTarget(string markerName,  ColorSphere sphere)
    {
        //Debug.Log("Target Subscribed: " + markerName);
        if (!activeSpheres.ContainsKey(markerName))
        {
            activeSpheres.Add(markerName, sphere);
        }
    }

    public void UnsubscribeTarget(string markerName)
    {
        //Debug.Log("Target Unsubscribed: " + markerName);
        if (activeSpheres.ContainsKey(markerName))
        {
            activeSpheres.Remove(markerName);
        }
    }

    //TO DO: clean up method
    //TO DO: Restrict max distance between the spheres (before mixing)
    public void MixColors()
    {
        Vector3 resultPosition = new Vector3();
        
        List<ColorNames> mixingColors = new List<ColorNames>();

        //Mix only if there are two targets detected
        if (activeSpheres.Count == 2)
        {
            foreach (KeyValuePair<string, ColorSphere> s in activeSpheres)
            {
                resultPosition += s.Value.GetPosition();             //get positions of the two spheres to calculate their midpoint
                mixingColors.Add(s.Value.colorId);                  //get colors of the two spheres to use for mixing
            }

            //get mixed Color
            int resultColorId = (int)MixedColors.Instance.GetMixedColor(mixingColors[0], mixingColors[1]);
            Color resultColor = ColorPreset.GetColorById(resultColorId);

            //calculate midpoint position and assign spheres to new parent
            resultPosition = resultPosition/2;
            sphereParent.transform.position = resultPosition;
            foreach (KeyValuePair<string, ColorSphere> s in activeSpheres)
            {
                s.Value.ActivateMixing(resultPosition);
                s.Value.transform.parent = sphereParent.transform;
            }

            //Activate Mixing Animations
            sphereParent.ActivateMixing();
            resultSphere.ShowSphere(resultColor, resultPosition);
        }
        else
        {
            Debug.Log("You need to lay down two target images in order two mix the two colors");
            //TO DO: add UI user info here
        }
    }



    
}
