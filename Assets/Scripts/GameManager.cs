using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject menu;

    public float difficulty = .25f;    

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Cursor());
        }
    }

    public void LoadScene(float difficulty)
    {
        difficulty = difficulty;
        SceneManager.LoadScene("GameScene");
    }
    
    IEnumerator Cursor()
    {
        menu.SetActive(!menu.activeInHierarchy);
        if (!menu.activeInHierarchy)
            GameObject.Find("RigidBodyFPSController").GetComponent<RigidbodyFirstPersonController>().LockCursor();
        else
            GameObject.Find("RigidBodyFPSController").GetComponent<RigidbodyFirstPersonController>().UnLockCursor();

        yield return new WaitForEndOfFrame();

        GameObject.Find("RigidBodyFPSController").GetComponent<RigidbodyFirstPersonController>().enabled = !menu.activeInHierarchy;
    }    
}
