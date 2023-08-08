using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    public TMP_Text Best;
    public TMP_Text Second;
    public TMP_Text Third;
    public void SceneChange()
    {
        SceneManager.LoadScene("New Scene");
    }

    public void Score()
    {
        Best.text = PlayerPrefs.GetFloat("BestScore").ToString();
        Second.text = PlayerPrefs.GetFloat("SecondScore").ToString();
        Third.text = PlayerPrefs.GetFloat("ThirdScore").ToString();
    }
}
