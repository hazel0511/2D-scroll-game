using UnityEngine;
using System.Collections;
using UnityEngine.UI;//for Text
using UnityEngine.SceneManagement;
using UnityEngine.Events;  //for Invoke("Method",second)
using Fungus;

public class Character2DControl : MonoBehaviour 
{
	private Character2D character;  //呼叫Character2D.cs中的函式
	private bool jump;
	private float movingSpeed;


	public AudioClip catchSE;
    public AudioClip jumpSE;
    public AudioClip deathSE;

    public AudioClip winSE;

	public AudioSource audioPlayer;

	private int count;
    private float life;

	public Text countText;
    //public Text bonusText;
    public Image hpBar;
 

	public Canvas wincanvas;
	public Canvas losecanvas;
    public Canvas deadcanvas;

    public GameObject channel;


    public static bool iscollision = false;
    private bool switchscene=false;

    public Flowchart flowchart;

    //public Canvas Question01;
    //public Canvas Question011;

    // Use this for initialization
    void Start () 
	{
		character = GetComponent<Character2D> ();

        //紀錄生命數量
        life = PlayerPrefs.GetFloat("life", 3.0f);
        if(life>0)  
        {
            if (switchscene == true||ButtonControl_Menu.newgame)//下一關
            {
                life = 3.0f;
                updateHPBar();

                count = 0;
                GetContext();

                Vector3 move2 = new Vector3(-8.4f, -2f, 1.0f);
                GetComponent<Transform>().position = move2;
                switchscene = false;
            }
            else
            {
                /*if (ButtonControl_Menu.newgame)
                {
                    life = 3.0f;
                    updateHPBar();

                    count = 0;
                    GetContext();

                    Vector3 move2 = new Vector3(-8.4f, -2f, 1.0f);
                    GetComponent<Transform>().position = move2;
                    switchscene = false;
                }
                else   */
                {
                    updateHPBar();//更新血量條顯示

                    //紀錄之前的分數
                    count = 0;
                    //count = PlayerPrefs.GetInt("point", 0);  //分數不能累計
                    GetContext();

                    //============設定新位置
                    float xPosition = PlayerPrefs.GetFloat("xPosition", -8.4f);
                    float yPosition = PlayerPrefs.GetFloat("yPosition", -2f);
                    Vector3 move = new Vector3(xPosition, yPosition, 1.0f);
                    GetComponent<Transform>().position = move;
                }
            }

        }
        else//生命小於零代表重玩
        {
            life = 3.0f;  //重新再玩生命值點滿
            updateHPBar();

            count = 0;
            GetContext();

            Vector3 move = new Vector3(-8.4f, -2f, 1.0f);
            GetComponent<Transform>().position = move;
        }


		//countText = GameObject.Find ("Count_text");
		//bonusText = GameObject.Find ("Bonus_text");
		GetContext ();


        channel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () 
	{
        //get jump input by "jump" button set in input setting
        if (Input.GetButtonDown("Jump")||Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
            PlayJumpSE();
        }
        /*
		if (Input.GetKey(KeyCode.R)) {
			SceneManager.LoadScene ("quiz");
		}
        */
	}

	void FixedUpdate()
	{
		//get input by Axis set in input setting
		movingSpeed = Input.GetAxis("Horizontal");

		//pass parameters to character script, and then it can move  呼叫Character2D中的函式
		character.Move(movingSpeed, jump);

		//jump is reset after each time that physical engine updated
		jump = false;


        Bonus();
	}


	void OnCollisionEnter2D(UnityEngine.Collision2D collision)  ///要改成2D
	{

        //if(!iscollision){
          //  iscollision = true;
        //}
        if (iscollision == false)
        {
            if (collision.gameObject.tag == "Add_Point")
            {
                PlayCatchSE();
                print("catch");
                //count =count+1*(int)Time.fixedDeltaTime*50;  //一秒加一次   fixedDeltaTime=1/50
                count = count + 1;
                //bonusText.GetComponent<Text> ().text = "+"+ point.ToString ();
                GetContext();  //刷新記分板

                //int limit=collision.gameObject.GetComponent<Add_Limit>().limit;
                //limit-=1;
                //print(limit);
                //if(limit<=0)
                collision.gameObject.GetComponent<Add_Limit>().limit -= 1;  //得分點被撞三次後消失
                print(collision.gameObject.GetComponent<Add_Limit>().limit);
                if (collision.gameObject.GetComponent<Add_Limit>().limit <= 0)
                {
                    Destroy(collision.gameObject);
                }
                /*
                MessageReceived[] receivers = GameObject.FindObjectsOfType<Fungus.MessageReceived>();
                if(receivers!=null)
                {
                    if (collision.gameObject.name== "Question01")
                    foreach (var receiver in receivers)
                    {
                        receiver.OnSendFungusMessage("集體暴力");
                    }

                }*/
                if (collision.gameObject.name == "Question03")
                {
                    Flowchart.BroadcastFungusMessage("集體暴力");
                    //Question01.enabled = true;

                }
                if (collision.gameObject.name == "Question02")
                {
                    Flowchart.BroadcastFungusMessage("歸因謬誤");
                }
                if (collision.gameObject.name == "Question01")
                {
                    Flowchart.BroadcastFungusMessage("系統外差異");
                }

                if (collision.gameObject.name == "Question04")
                {
                    Flowchart.BroadcastFungusMessage("受虐化");
                }
                if (collision.gameObject.name == "Question05")
                {
                    Flowchart.BroadcastFungusMessage("暴力範式");
                }
                if (collision.gameObject.name == "Question06")
                {
                    Flowchart.BroadcastFungusMessage("危機產生");
                }
                if (collision.gameObject.name == "Question07")
                {
                    Flowchart.BroadcastFungusMessage("迫害文本");
                }

            }

            iscollision = true;
        }
        else
        {
            iscollision = false;
        }

        if (collision.gameObject.tag == "Enemy"||collision.gameObject.tag=="Minus_Point")
        {
            PlayDeathSE();
            life = life - Time.deltaTime * 40.0f;  //一秒扣一分
            updateHPBar();//更新血量條顯示

            if (life <= 0)
            {
                deadcanvas.enabled = false;
                losecanvas.enabled = true;
                PlayerPrefs.SetFloat("life", life); //更新血量為零
            }

            if (life >= 0)
            {
                Destroy(collision.gameObject);//先摧毀怪物以免重複扣分
                deadcanvas.enabled = true;
                PlayerPrefs.SetFloat("life", life);
                PlayerPrefs.SetFloat("xPosition", transform.position.x);
                PlayerPrefs.SetFloat("yPosition", transform.position.y);
                PlayerPrefs.SetInt("point", count);

            }

        }

        if (count >= 10)
        {

            //channel = GameObject.Find("channel");
            channel.SetActive(true);    //Don't show a channel until getting enough points
        }
        if (collision.gameObject.tag == "Channel" && count >= 0)
        {
            print("next level");
            wincanvas.enabled = true;
            if (SceneManager.GetActiveScene().name == "mario01"||SceneManager.GetActiveScene().name=="mario01_v2") //第一關進入第二關
            {
                switchscene = true;
                audioPlayer.PlayOneShot(winSE);
                Invoke("LoadNextLevel", 10.0f);
                    
            }
            else   //第二關勝利後回主選單
            {
                //switchscene = true;
                audioPlayer.PlayOneShot(winSE);
                Invoke("ReturnMenu", 10.0f);
            }

        }

	}
    /*
    void OnCollisionExit2D(Collision2D collision)
    {
        iscollision = false;

    }*/

    void Bonus()
    {
        
        string MyBoolName03 = "Question03";
        if (flowchart.GetBooleanVariable(MyBoolName03) ==true)
        {
            print("第三題答對");
            count += 5;
            flowchart.SetBooleanVariable(MyBoolName03, false);
        }

        string MyBoolName02 = "Question02";
        if (flowchart.GetBooleanVariable(MyBoolName02) == true)
        {
            print("第二題答對");
            count += 5;
            flowchart.SetBooleanVariable(MyBoolName02, false);
        }

        string MyBoolName01 = "Question01";
        if (flowchart.GetBooleanVariable(MyBoolName01) == true)
        {
            print("第一題答對");
            count += 5;
            flowchart.SetBooleanVariable(MyBoolName01, false);
        }

        string MyBoolName04 = "Question04";
        if (flowchart.GetBooleanVariable(MyBoolName04) == true)
        {
            print("第4題答對");
            count += 5;
            flowchart.SetBooleanVariable(MyBoolName04, false);
        }

        string MyBoolName05 = "Question05";
        if (flowchart.GetBooleanVariable(MyBoolName05) == true)
        {
            print("第5題答對");
            count += 5;
            flowchart.SetBooleanVariable(MyBoolName05, false);
        }

        string MyBoolName06 = "Question06";
        if (flowchart.GetBooleanVariable(MyBoolName06) == true)
        {
            print("第6題答對");
            count += 5;
            flowchart.SetBooleanVariable(MyBoolName06, false);
        }

        string MyBoolName07 = "Question07";
        if (flowchart.GetBooleanVariable(MyBoolName07) == true)
        {
            print("第7題答對");
            count += 5;
            flowchart.SetBooleanVariable(MyBoolName07, false);
        }

        countText.text = "Count: " + count.ToString();
    }



    void GetContext()
	{
		//countText.GetComponent<Text>().text="Count: " + count.ToString ();
		countText.text="Count: " + count.ToString ();


		//bonusText.GetComponent<Text> ().text = "+"+ point.ToString ();
		//countText.text= "Count_fk" + count.ToString ();
		if (count >= 25) 
		{
			//winText.text = "YOU WIN";
			//countText.text="WIN";
			print ("WIN");
			//wincanvas.enabled = true;

		}
		if (count < 0) {
			losecanvas.enabled = true;
		}
	}

    void updateHPBar()
    {
        hpBar.fillAmount = life / 3.0f;
    }


    public void PlayCatchSE()
	{
		audioPlayer.PlayOneShot(catchSE);
	}
    public void PlayJumpSE()
    {
        audioPlayer.PlayOneShot(jumpSE);
    }
    public void PlayDeathSE()
    {
        audioPlayer.PlayOneShot(deathSE);
    }


    public void LoadNextLevel()
    {
        switchscene = true;
        ButtonControl_Menu.newgame = true;
        SceneManager.LoadScene("mario02");
    }

    public void ReturnMenu()
    {
        //switchscene = true;
        print("start new game");
        SceneManager.LoadScene("startmenu");
        //ButtonControl_Menu.newgame = true;
        
    }

}
