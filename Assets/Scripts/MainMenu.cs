using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Locations;
    public GameObject currentLocation;
    public new GameObject camera;

    private void Awake()
    {
        camera.transform.position = Locations[0].transform.position;
        camera.transform.rotation = Locations[0].transform.rotation;
    }

    private void Start()
    {        
        UpdateLocation(1);
    }

    IEnumerator Transport()
    {
        float val = 0;
        while (val <= 1)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, currentLocation.transform.position, val);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, currentLocation.transform.rotation, val);
            val += .01f * Time.deltaTime;
            yield return new WaitForSeconds(.01f);
        }
        print("done");
    }

    public void UpdateLocation(int index)
    {
        StopAllCoroutines();
        currentLocation = Locations[index];
        StartCoroutine(Transport());
    }
}
