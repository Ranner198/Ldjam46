using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlantLogic : MonoBehaviour
{
    public float age;
    public float water;
    public float soil;    
    public float health;
    public string status;

    public bool bugs, planted, potted, watered, finished;

    public bool active;

    public Animator anim;

    public MeshRenderer mesh;

    public Slider AgeSlider, WaterSlider, SoilSlider, HealthSlider;

    public GameObject plantStatus, stepsToComplete, bugsObject, warningIcon, plant;

    public TextMeshProUGUI finishedText;

    public void Watered()
    {
        if (!watered)
        {
            watered = true;
            Planted();
        }
        else
            water += 30;
    }
    public void Soiled()
    {
        if (!potted)
        {
            potted = true;
            Planted();
        }
        else
            soil += 15;
    }
    public void Sprayed()
    {
        if (bugs)
        {
            bugs = false;
            bugsObject.SetActive(false);
            warningIcon.SetActive(false);
            health += 5;
        }
        else
            health -= 5;
    }

    public void PlantedSeeds()
    {
        planted = true;
        Planted();
    }

    public void Planted()
    {        
        if (planted && potted && watered && !active)
        {            
            plantStatus.SetActive(true);
            age = 0;
            water = 75f;
            soil = 75f;
            health = 75f;
            InvokeRepeating("UpdateColor", 1, 1);
            InvokeRepeating("SpawnBugs", 15, 15);
            active = true;
            stepsToComplete.SetActive(false);
            StartCoroutine(Wait3Seconds());
            plant.SetActive(true);
        }
    }

    void SpawnBugs()
    {
        int val = Random.Range(0, 100);

        if (val > 70)
        {
            bugsObject.SetActive(true);
            warningIcon.SetActive(true);
            bugs = true;
        }
    }

    IEnumerator Wait3Seconds()
    {
        yield return new WaitForSeconds(3);
        Status.instance.StatusUpdate("Well? I guess I should just sit around and wait.");
    }

    IEnumerator Wait4Seconds()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("Load Next Scene");
    }

    public void Update()
    {
        if (active)
        {
            if (health <= 0)
            {
                // Gamemanager lose
                anim.StopPlayback();
                Status.instance.StatusUpdate("Oh no! its dead!");
                finishedText.text = "You've failed to keep the plant alive :(";
                active = false;
            }

            if (bugs)
                health -= Time.deltaTime * 2 * GameManager.instance.difficulty;

            // Subtract values
            water -= Time.deltaTime * GameManager.instance.difficulty;
            soil -= Time.deltaTime * GameManager.instance.difficulty;

            if (water > 75 && water < 100 && soil > 75 && soil < 100)
                health += Time.deltaTime * GameManager.instance.difficulty;
            else if (water < 50 || soil < 50)
                health -= Time.deltaTime * GameManager.instance.difficulty;
            else if (water > 100 || soil > 100)
                health -= Time.deltaTime * GameManager.instance.difficulty;

            age = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length * (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * anim.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;
            print(age);
            print(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlantIdle" && !finished)
            {
                finished = true;
                active = false;
                anim.StopPlayback();
                Status.instance.StatusUpdate("You did it! Thanks for playing!");
                finishedText.text = "You did it, you're a real botanist!";
                StartCoroutine(Wait4Seconds());
            }
        }        
        AgeSlider.value = age;
        WaterSlider.value = water;
        SoilSlider.value = soil;
        HealthSlider.value = health;
    }

    // Update Color of plant based off health value
    public void UpdateColor()
    {
        mesh.material.color = new Color(health/100, health/100, health/100, 1);
    }
}
