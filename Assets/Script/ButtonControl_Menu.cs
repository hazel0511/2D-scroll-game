using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl_Menu : MonoBehaviour {
    public Canvas findcanvas;
    public static bool newgame=false;
    // Use this for initialization
    void Start () {
        findcanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadPlayScene1()
    {
        newgame = true;
        //SceneManager.LoadScene("mario01");
        SceneManager.LoadScene("mario01_v2");
        DisableSomeFunction();
    }

    public void LoadPlayScene2()
    {
        newgame = true;
        SceneManager.LoadScene("mario02");
        DisableSomeFunction();
    }

    private void DisableSomeFunction()  //避免切換場景亂按
    {
        GameObject.Find("Level1_Button").GetComponent<UnityEngine.UI.Button>().enabled = false;
        GameObject.Find("Level2_Button").GetComponent<UnityEngine.UI.Button>().enabled = false;
        GameObject.Find("Exit_Button").GetComponent<UnityEngine.UI.Button>().enabled = false;
        GameObject.Find ("Tip_Button").GetComponent<UnityEngine.UI.Button> ().enabled = false;

    }


    public void ShowTipCanvas()
    {
        findcanvas.enabled = true;
    }
    public void HideTipCanvas()
    {
        findcanvas.enabled = false;
    }

    public void ExitProgram()
    {
        Application.Quit();
    }
}
