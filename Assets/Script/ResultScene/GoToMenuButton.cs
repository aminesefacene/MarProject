﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuButton : MonoBehaviour
{

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
