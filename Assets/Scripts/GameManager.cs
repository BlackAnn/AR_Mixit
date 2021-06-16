using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //TestAusgabe
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //activeSpheres.ForEach(s => Debug.Log("SpherePosition: " + s.GetPosition().x + ", " + s.GetPosition().y + ", " + s.GetPosition().z));
            Debug.Log("List size = " + activeSpheres.Count);
            foreach (KeyValuePair<string, ColorSphere> s in activeSpheres)
            {
                Debug.Log("Key = " + s.Key + "Sphere Position = " +  s.Value.GetPosition());
            }

        }
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
        activeSpheres.Remove(markerName);
    }

    public void MixColors()
    {
        Vector3 resultPosition = new Vector3();

        //Mix only if there are two targets detected
        if (activeSpheres.Count == 2)
        {
            foreach (KeyValuePair<string, ColorSphere> s in activeSpheres)
            {
                //Debug.Log(s.Key + " get mixed");
                resultPosition += s.Value.GetPosition();
            }
            resultPosition = resultPosition/2;
            sphereParent.transform.position = resultPosition;
            foreach (KeyValuePair<string, ColorSphere> s in activeSpheres)
            {
                s.Value.ActivateMixing(resultPosition);
                s.Value.transform.parent = sphereParent.transform;
            }
            sphereParent.ActivateMixing();
            resultSphere.ShowSphere(resultPosition);
        }
        else
        {
            Debug.Log("You need to lay down two target images in order two mix the two colors");
            //TO DO: add UI user info here
        }
    }
}
