using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private GameObject gameMaster;

    void Start()
    {
        gameMaster = GameObject.Find("gameMaster");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "car")
        {
            gameMaster.GetComponent<GameManager>().SetNewCP(this.gameObject);
            gameMaster.GetComponent<GameManager>().AddCpToList(this.gameObject);
        }
    }
}
