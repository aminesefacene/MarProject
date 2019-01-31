using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<GameObject> cpList = new List<GameObject>();
    public GameObject car;
    public Rigidbody carRigidbody;
    public GameObject currentCp;

    public Camera mainCamera;
    public Camera firstCamera;

    public Text timeText;
    public GameObject timeManager;

    // Use this for initialization
    void Start () {

        timeManager = GameObject.Find("timeManager");
        timeManager.GetComponent<TimeManager>().StartTimer();
        UpdateTimeText();

        car = GameObject.Find("car");
        carRigidbody = car.GetComponent<Rigidbody>();

        //camera
        mainCamera = car.GetComponent<Camera>();
        firstCamera = GameObject.Find("replayCamera1").GetComponent<Camera>();
        //mainCamera.enabled = true;
        //firstCamera.enabled = false;

        currentCp = GameObject.Find("checkpoint1");
        cpList.Add(currentCp);
    }
	
	// Update is called once per frame
	void Update () {

        timeManager.GetComponent<TimeManager>().UpdateTimer();
        UpdateTimeText();

        if (TestFinish())
        {
            SceneManager.LoadScene("ResultScene");
        }

        //reset au dernier cp
        if (Input.GetKeyDown(KeyCode.Space))
        {
            carRigidbody.isKinematic = true;
            carRigidbody.position = new Vector3(this.GetComponent<GameManager>().GetCurrentCp().transform.position.x, 2.75f, this.GetComponent<GameManager>().GetCurrentCp().transform.position.z);
            carRigidbody.rotation = Quaternion.Euler(this.GetComponent<GameManager>().GetCurrentCp().transform.rotation.x, this.GetComponent<GameManager>().GetCurrentCp().transform.rotation.y - 90f, this.GetComponent<GameManager>().GetCurrentCp().transform.rotation.z);
            carRigidbody.isKinematic = false;
        }
        //retour au menu
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void UpdateTimeText()
    {
        timeText.text = timeManager.GetComponent<TimeManager>().GetTimer().ToString();
    }

    public void SetNewCP(GameObject newCp)
    {
        currentCp = newCp;
    }

    public GameObject GetCurrentCp()
    {
        Debug.Log(currentCp);
        return currentCp;
    }

    public void AddCpToList(GameObject cp)
    {
        if (!cpList.Contains(cp))
        {
            cpList.Add(cp);
        }
    }

    public bool TestFinish()
    {
        return (cpList.Count == 4) && (cpList[cpList.Count - 1].name == "finnishLine");
    }

}
