using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Status : MonoBehaviour
{
    public TextMeshProUGUI status;
    public static Status instance;
    private void Awake()
    {
        instance = this;
    }

    public void StatusUpdate(string text)
    {
        StartCoroutine(Wait(text));
    }

    IEnumerator Wait(string text)
    {
        status.CrossFadeAlpha(1, 1f, true);
        status.text = text;
        yield return new WaitForSeconds(2);
        status.CrossFadeAlpha(0, 2, true);
    }
}
