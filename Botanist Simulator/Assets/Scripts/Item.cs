using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public string ItemName;    
    public GameObject placeHolder;

    private Vector3 putBackLocation;
    private Transform ItemParent;
    private Quaternion startRotation;

    public string ItemNeeded;
    public string hint;

    public AudioClip pickupSound, useSound, putbackSound;
    public new AudioSource audio;

    [System.Serializable]
    public class InteractionEvent : UnityEvent { }

    public InteractionEvent onInteraction = new InteractionEvent();

    public void Interact()
    {
        onInteraction.Invoke();        
    }

    public void Start()
    {
        ItemParent = transform.parent;
        startRotation = transform.localRotation;
        putBackLocation = transform.localPosition;
    }

    public bool Use(string ItemInHand)
    {

        if (ItemNeeded == ItemInHand)
        {
            print(true);
            Debug.Log(ItemName + " was used!");
            StartCoroutine(WaitAFrame());
            return true;
        }
        else
        {
            Status.instance.StatusUpdate(hint);
            return false;
        }
    }

    IEnumerator WaitAFrame()
    {
        yield return new WaitForSeconds(.3f);
        Interact();
    }

    public bool Use()
    {        
        gameObject.name = ItemName;
        if (useSound != null && audio != null)
            audio.PlayOneShot(useSound);
        Interact();        
        return true;
    }

    public void Destroy()
    {
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    public void Pickup(Transform newParent)
    {
        if (placeHolder != null)
            placeHolder.SetActive(true);
        transform.parent = newParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(20, 180, 0);
        if (pickupSound != null && audio != null)
            audio.PlayOneShot(pickupSound);
    }
    public void PutBack()
    {
        placeHolder.SetActive(false);
        gameObject.transform.parent = ItemParent;
        transform.localPosition = putBackLocation;
        transform.localRotation = startRotation;
        if (putbackSound != null && audio != null)
            audio.PlayOneShot(putbackSound);
    }
}
