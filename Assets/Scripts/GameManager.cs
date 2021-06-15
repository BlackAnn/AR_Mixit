using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, ColorSphere> activeSpheres = new Dictionary<string, ColorSphere>();

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
        sphere.PositionToString();
        Debug.Log("Target Subscribed: " + markerName);
        if (!activeSpheres.ContainsKey(markerName))
        {
            activeSpheres.Add(markerName, sphere);
        }
    }

    public void UnsubscribeTarget(string markerName)
    {
        Debug.Log("Target Unsubscribed: " + markerName);
        activeSpheres.Remove(markerName);
    }

    public void MixColors()
    {
        foreach (KeyValuePair<string, ColorSphere> s in activeSpheres)
        {
            s.Value.ActivateMixing();
        }
        //Show ResultSphere (-> Make Script for ResultSphere)
    }
}
