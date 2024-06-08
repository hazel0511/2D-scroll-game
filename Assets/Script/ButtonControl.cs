using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {

	private Canvas findcanvas;
	private Canvas findcanvas2;
    private Canvas findcanvas3;





    // Use this for initialization
    void Start () {
		findcanvas = GameObject.Find ("Win_Canvas").GetComponent<Canvas> ();
		findcanvas.enabled = false;
		findcanvas2 = GameObject.Find ("Lose_Canvas").GetComponent<Canvas> ();
		findcanvas2.enabled = false;
        findcanvas3 = GameObject.Find("Dead_Canvas").GetComponent<Canvas>();
        findcanvas3.enabled = false;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadPlayScene()
	{
        //SceneManager.LoadScene ("mario01");
        SceneManager.LoadScene("startmenu");
	}
    public void LoadLevel1()
    {
        ButtonControl_Menu.newgame = false;
        //SceneManager.LoadScene("mario01");
        SceneManager.LoadScene("mario01_v2");
        //SceneManager.LoadScene("level01");
    }

    public void LoadLevel2()
    {
        ButtonControl_Menu.newgame = false;
        SceneManager.LoadScene("mario02");
    }
    public void ShowWinCanvas()
	{
		//findcanvas = GameObject.Find ("Setting_Canvas").GetComponent<Canvas> ();
		findcanvas.enabled = true;
	}

	public void HideWinCanvas()
	{
		//findcanvas = GameObject.Find ("Setting_Canvas").GetComponent<Canvas> ();
		findcanvas.enabled = false;
	}

	public void ShowLoseCanvas()
	{
		findcanvas2.enabled = true;
	}
	public void HideLoseCanvas()
	{
		findcanvas2.enabled = false;
	}

    public void ShowDeadCanvas()
    {
        findcanvas3.enabled = true;
    }
    public void HideDeadCanvas()
    {
        findcanvas3.enabled = false;
    }

    public void ExitProgram()
	{
		Application.Quit ();
	}






}
