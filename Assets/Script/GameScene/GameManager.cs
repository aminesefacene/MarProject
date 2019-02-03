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
    private List<GhostState> l;

    //cameras
    private Camera mainCamera;
    private Camera replayCamera1;
    private Camera replayCamera2;
    private Camera replayCamera3;
    private Camera replayCamera4;
    private Camera replayCamera5;
    private Camera replayCamera6;
    private Camera replayCamera7;

    //cameras CP
    public GameObject currentCpCamera;

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
                    l = data.GetList();
                }
                else
                {
                    Debug.Log("No Ghost file found !!!");
                }
                break;
            case 4:
                mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
                replayCamera1 = GameObject.Find("Camera1").GetComponent<Camera>();
                replayCamera2 = GameObject.Find("Camera2").GetComponent<Camera>();
                replayCamera3 = GameObject.Find("Camera3").GetComponent<Camera>();
                replayCamera4 = GameObject.Find("Camera4").GetComponent<Camera>();
                replayCamera5 = GameObject.Find("Camera5").GetComponent<Camera>();
                replayCamera6 = GameObject.Find("Camera6").GetComponent<Camera>();
                replayCamera7 = GameObject.Find("Camera7").GetComponent<Camera>();

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
        currentCpCamera = GameObject.Find("checkpointCamera1");
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
                GhostState g;
                g = l[count];
                Vector3 ghostPosition = new Vector3(g.GetPositionX(), g.GetPositionY(), g.GetPositionZ());
                Quaternion ghostRotation = new Quaternion(g.GetRotationX(), g.GetRotationY(), g.GetRotationZ(), 1f);
                ghost.transform.position = ghostPosition;
                ghost.transform.rotation = ghostRotation;
                //on vérifie si le ghost à terminer ça course
                if (count < l.Count-1)
                {
                    count++;
                }
                GMUpdate();
                break;
            case 4:

                changeCameraReplay();

                List<GhostState> savedl = data.GetList();
                GhostState savedg = savedl[count];
                Vector3 savedPosition = new Vector3(savedg.GetPositionX(), savedg.GetPositionY(), savedg.GetPositionZ());
                Quaternion savedRotation = new Quaternion(savedg.GetRotationX(), savedg.GetRotationY(), savedg.GetRotationZ(), 1f);
                carRigidbody.transform.position = savedPosition;
                carRigidbody.transform.rotation = savedRotation;

                //on vérifie si la voiture à terminer ça course
                if (count < savedl.Count - 1)
                {
                    count++;
                }
                GMUpdate();
                break;
        }
    }

    public void changeCameraReplay()
    {
        switch (currentCpCamera.name) {
            case "checkpointCamera1":
                mainCamera.enabled = false;
                replayCamera1.enabled = true;
                replayCamera2.enabled = false;
                replayCamera3.enabled = false;
                replayCamera4.enabled = false;
                replayCamera5.enabled = false;
                replayCamera6.enabled = false;
                replayCamera7.enabled = false;

                replayCamera1.transform.LookAt(car.transform);
                break;
            case "checkpointCamera2":
                mainCamera.enabled = false;
                replayCamera1.enabled = false;
                replayCamera2.enabled = true;
                replayCamera3.enabled = false;
                replayCamera4.enabled = false;
                replayCamera5.enabled = false;
                replayCamera6.enabled = false;
                replayCamera7.enabled = false;

                replayCamera2.transform.LookAt(car.transform);
                break;
            case "checkpointCamera3":
                mainCamera.enabled = false;
                replayCamera1.enabled = false;
                replayCamera2.enabled = false;
                replayCamera3.enabled = true;
                replayCamera4.enabled = false;
                replayCamera5.enabled = false;
                replayCamera6.enabled = false;
                replayCamera7.enabled = false;

                replayCamera3.transform.LookAt(car.transform);
                break;
            case "checkpointCamera4":
                mainCamera.enabled = false;
                replayCamera1.enabled = false;
                replayCamera2.enabled = false;
                replayCamera3.enabled = false;
                replayCamera4.enabled = true;
                replayCamera5.enabled = false;
                replayCamera6.enabled = false;
                replayCamera7.enabled = false;

                replayCamera4.transform.LookAt(car.transform);
                break;
            case "checkpointCamera5":
                mainCamera.enabled = false;
                replayCamera1.enabled = false;
                replayCamera2.enabled = false;
                replayCamera3.enabled = false;
                replayCamera4.enabled = false;
                replayCamera5.enabled = true;
                replayCamera6.enabled = false;
                replayCamera7.enabled = false;

                replayCamera5.transform.LookAt(car.transform);
                break;
            case "checkpointCamera6":
                mainCamera.enabled = false;
                replayCamera1.enabled = false;
                replayCamera2.enabled = false;
                replayCamera3.enabled = false;
                replayCamera4.enabled = false;
                replayCamera5.enabled = false;
                replayCamera6.enabled = true;
                replayCamera7.enabled = false;

                replayCamera6.transform.LookAt(car.transform);
                break;
            case "checkpointCamera7":
                mainCamera.enabled = false;
                replayCamera1.enabled = false;
                replayCamera2.enabled = false;
                replayCamera3.enabled = false;
                replayCamera4.enabled = false;
                replayCamera5.enabled = false;
                replayCamera6.enabled = false;
                replayCamera7.enabled = true;

                replayCamera7.transform.LookAt(car.transform);
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
                case 4:
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

    public void SetNewCPCamera(GameObject newCpCamera)
    {
        currentCpCamera = newCpCamera;
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
        return (cpList.Count == 4) && (cpList[cpList.Count - 1].name == "finishLine");
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