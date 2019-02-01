using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostRace : MonoBehaviour
{

    public GameObject timeManager;

    void Start()
    {
        timeManager = GameObject.Find("timeManager");
    }

    public void PlayGame()
    {
        timeManager.GetComponent<TimeManager>().SetMode(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
