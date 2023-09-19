using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Start : MonoBehaviour
{
    public GameObject Helppannel;
    // Start is called before the first frame update
    void Start()
    {
        Helppannel.SetActive(false);
    }


    public void Help()
    {
        Helppannel.SetActive(true);
    }
    public void Close()
    {
        Helppannel.SetActive(false);
    }
    public void Gamestart()
    {
        SceneManager.LoadScene("home");
    }
}
