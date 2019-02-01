using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

    //mode
    public int mode = -1;
    public BinaryFormatter bf;
    public FileStream file;
    private String path = "/home/miola/Documents/mar/MarProject/ghost.txt";

    //ghost
    private List<GhostState> lgs;
    private ListGhostState data;
    private int count;
    private GameObject ghost;


    public List<GameObject> cpList = new List<GameObject>();
    public GameObject car;
    public Rigidbody carRigidbody;
    public GameObject currentCp;

    public Text timeText;
    public GameObject timeManager;

    void Start () {
        timeManager = GameObject.Find("timeManager");

        mode = timeManager.GetComponent<TimeManager>().GetMode();
        switch (mode)
        {
            case 2:
                bf = new BinaryFormatter();
                if (File.Exists(path))
                {
                    file = File.OpenWrite(path);
                }
                else
                {
                    file = File.Create(path);
                }
                lgs = new List<GhostState>();
                break;
            case 3:
                ghost = GameObject.Find("ghost");
                bf = new BinaryFormatter();
                if (File.Exists(path))
                {
                    count = 0;
                    file = File.OpenRead(path);
                    data = (ListGhostState)bf.Deserialize(file);
                }
                else
                {
                    Debug.Log("No Ghost file found !!!");
                }
                break;
            case 4:
                bf = new BinaryFormatter();
                if (File.Exists(path))
                {
                    count = 0;
                    file = File.OpenRead(path);
                    data = (ListGhostState)bf.Deserialize(file);
                }
                else
                {
                    Debug.Log("saved file not found !!!");
                }
                break;
        }
        timeManager.GetComponent<TimeManager>().StartTimer();
        UpdateTimeText();

        car = GameObject.Find("car");
        carRigidbody = car.GetComponent<Rigidbody>();
        currentCp = GameObject.Find("checkpoint1");
        cpList.Add(currentCp); 
    }
	
	void Update () {
        switch (mode)
        {
            case 1:
                GMUpdate();
                break;
            case 2:
                GhostState gs = new GhostState(carRigidbody.transform.position.x, carRigidbody.transform.position.y, carRigidbody.transform.position.z, carRigidbody.transform.rotation.x, carRigidbody.transform.rotation.y, carRigidbody.transform.rotation.z);
                lgs.Add(gs);
                GMUpdate();
                break;
            case 3:
                List<GhostState> l = data.GetList();
                GhostState g = l[count];
                Vector3 ghostPosition = new Vector3(g.GetPositionX(), g.GetPositionY(), g.GetPositionZ());
                Quaternion ghostRotation = new Quaternion(g.GetRotationX(), g.GetRotationY(), g.GetRotationZ(), 1f);
                ghost.transform.position = ghostPosition;
                ghost.transform.rotation = ghostRotation;
                count++;
                GMUpdate();
                break;
            case 4:
                List<GhostState> savedl = data.GetList();
                GhostState savedg = savedl[count];
                Vector3 savedPosition = new Vector3(savedg.GetPositionX(), savedg.GetPositionY(), savedg.GetPositionZ());
                Quaternion savedRotation = new Quaternion(savedg.GetRotationX(), savedg.GetRotationY(), savedg.GetRotationZ(), 1f);
                carRigidbody.transform.position = savedPosition;
                carRigidbody.transform.rotation = savedRotation;
                count++;
                GMUpdate();
                break;
        }
    }

    public void GMUpdate()
    {
        timeManager.GetComponent<TimeManager>().UpdateTimer();
        UpdateTimeText();

        if (TestFinish())
        {
            switch (mode)
            {
                case 2:
                    ListGhostState sLGS = new ListGhostState(lgs);
                    bf.Serialize(file, sLGS);
                    file.Close();
                    break;
                case 3:
                    file.Close();
                    break;
            }
            SceneManager.LoadScene("ResultScene");
        }

        //reset au dernier cp
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetLastCp();
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

    public void ResetLastCp()
    {
        carRigidbody.isKinematic = true;
        carRigidbody.position = new Vector3(this.GetComponent<GameManager>().GetCurrentCp().transform.position.x, 2.75f, this.GetComponent<GameManager>().GetCurrentCp().transform.position.z);
        carRigidbody.rotation = Quaternion.Euler(this.GetComponent<GameManager>().GetCurrentCp().transform.rotation.x, this.GetComponent<GameManager>().GetCurrentCp().transform.rotation.y - 90f, this.GetComponent<GameManager>().GetCurrentCp().transform.rotation.z);
        carRigidbody.isKinematic = false;
    }

}

[Serializable]
class ListGhostState
{
    public List<GhostState> lgs;

    public ListGhostState(List<GhostState> l)
    {
        lgs = l;
    }

    public List<GhostState> GetList()
    {
        return lgs;
    }
}

[Serializable]
class GhostState
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public GhostState(float px, float py, float pz, float rx, float ry, float rz)
    {
        positionX = px;
        positionY = py;
        positionZ = pz;
        rotationX = rx;
        rotationY = ry;
        rotationZ = rz;
    }

    public float GetPositionX()
    {
        return positionX;
    }

    public float GetPositionY()
    {
        return positionY;
    }

    public float GetPositionZ()
    {
        return positionZ;
    }

    public float GetRotationX()
    {
        return rotationX;
    }

    public float GetRotationY()
    {
        return rotationY;
    }

    public float GetRotationZ()
    {
        return rotationZ;
    }
}