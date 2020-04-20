using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public LayerMask lm;
    public GameObject Camera;

    public GameObject previousObject;
    public GameObject hand, ItemInHand;    

    void Update()
    {
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, 2, lm))
        {            
            if (hit.transform.GetComponent<Item>() != null)
            {
                if (previousObject != null && previousObject != hit.transform.gameObject)
                {
                    previousObject.GetComponent<Outline>().enabled = false;
                }                    

                hit.transform.GetComponent<Outline>().enabled = true;
                previousObject = hit.transform.gameObject;

                if (Input.GetMouseButtonDown(0))
                {
                    string name = hit.transform.gameObject.name;
                    if (name == "HandSpade" || name == "WateringCan" || name == "PlantBag" || name == "SprayBottle")
                    {
                        if (ItemInHand != null && ItemInHand.name == name)
                        {
                            // Put Back
                            ItemInHand.GetComponent<Item>().PutBack();
                            ItemInHand = null;
                        }
                        else if (ItemInHand != null)
                        {
                            Status.instance.StatusUpdate("Hmmm, maybe I should put my tool back first before grabbing something else.");
                            /*
                            if (!hit.transform.gameObject.GetComponent<Item>().Use())
                            {
                                // Please put your item back warning
                                Debug.Log("Put your tool back first");                                
                            }
                            */
                        }
                        else
                        {
                            ItemInHand = hit.transform.gameObject;
                            hit.transform.gameObject.GetComponent<Item>().Pickup(hand.transform);
                        }
                    }
                    else if (ItemInHand != null)
                    {
                        var items = hit.transform.gameObject.GetComponents<Item>();
                        bool used = false;
                        if (items != null)
                        {
                            for (int i = 0; i < items.Length; i++)
                            {
                                if (items[i].Use(ItemInHand.gameObject.name))
                                {
                                    used = ItemInHand.GetComponent<Item>().Use();
                                    break;
                                }
                            }
                        }
                        // Please put your item back warning
                        if (!used)
                        {
                            Debug.Log("Put your tool back first");
                            Status.instance.StatusUpdate("Hmmm, maybe I should put my tool back first before grabbing something else.");
                        }
                    }
                    else if (ItemInHand == null)
                    {
                        var items = hit.transform.gameObject.GetComponents<Item>();
                        if (items != null)
                        {
                            for (int i = 0; i < items.Length; i++)
                            {
                                items[i].Use("Null");
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (previousObject != null)
            {
                previousObject.GetComponent<Outline>().enabled = false;
                previousObject = null;
            }
        }
    }
}
