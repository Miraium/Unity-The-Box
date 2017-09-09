using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	// 定数定義：壁方向
	public const int WALL_FLONT = 1;
	public const int WALL_RIGHT = 2;
	public const int WALL_BACK = 3;
	public const int WALL_LEFT = 4;

	// 定数定義：ボタンカラー
	public const int COLOR_GREEN = 0;
	public const int COLOR_RED = 1;
	public const int COLOR_BLUE = 2;
	public const int COLOR_WHITE = 3;

	// public変数はインスペクターで設定ができるようになる
	public GameObject myPanelWalls;	// 壁全体
	private int wallNo;

	public GameObject buttonHammer;
	public GameObject imageHammerIcon;

	public GameObject buttonMessage;
	public GameObject buttonMessageText;

	public GameObject[] buttonLamp = new GameObject[3];
	public Sprite[] buttonPicture = new Sprite[4];
	public Sprite hammerPicture;

	private bool doesHaveHammer;	// ハンマーを持っているか
	private int[] buttonColor = new int[3];	// 金庫のボタン

	public GameObject buttonKey;
	public GameObject imageKeyIcon;
	public GameObject buttonPig;
	public Sprite keyPicture;
	private bool doesHaveKey;


	// Use this for initialization
	void Start () {
		// スタート時は前を向く
		wallNo = WALL_FLONT;
		// トンカチは持っていない
		doesHaveHammer = false;

		buttonColor[0] = COLOR_GREEN;
		buttonColor[1] = COLOR_RED;
		buttonColor[2] = COLOR_BLUE;

		// 鍵は持っていない
		doesHaveKey = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushButtonRight(){
		wallNo++;
		if (wallNo > WALL_LEFT)
		{
			wallNo = WALL_FLONT;
		}
		DisplayWall();
		ClearButtons();
	}

	public void PushButtonLeft(){
		wallNo--;
		if (wallNo < WALL_FLONT)
		{
			wallNo = WALL_LEFT;
		}
		DisplayWall();
		ClearButtons();
	}

	void DisplayWall(){
		switch (wallNo)
		{
			case WALL_FLONT:
				myPanelWalls.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
				break;
			case WALL_RIGHT:
				myPanelWalls.transform.localPosition = new Vector3(-1000.0f,0.0f,0.0f);
				break;
			case WALL_BACK:
				myPanelWalls.transform.localPosition = new Vector3(-2000.0f,0.0f,0.0f);
				break;
			case WALL_LEFT:
				myPanelWalls.transform.localPosition = new Vector3(-3000.0f,0.0f,0.0f);
				break;
		}
	}

	void ClearButtons(){
		buttonHammer.SetActive(false);
		buttonKey.SetActive(false);
		buttonMessage.SetActive(false);
	}

	public void PushButtonMemo(){
		DisplayMessage("エッフェル塔と書いてある。");
	}

	public void PushButtonMessage(){
		buttonMessage.SetActive(false);	//メッセージを消す
	}

	void DisplayMessage(string mes){
		buttonMessage.SetActive(true);
		buttonMessageText.GetComponent<Text>().text = mes;
	}

	public void PushButtonLamp1(){
		Debug.Log("PushButtonLamp1()");
		ChangeButtonColor(0);
	}

	public void PushButtonLamp2(){
		ChangeButtonColor(1);
	}

	public void PushButtonLamp3(){
		ChangeButtonColor(2);
	}

	void ChangeButtonColor(int buttonNo){
		// ボタンbuttonNoの色を1つ進める
		buttonColor[buttonNo]++;
		// 1つ進めた後に、該当のものがない場合は戻す。
		if (buttonColor[buttonNo] > COLOR_WHITE)
		{
			buttonColor[buttonNo] = COLOR_GREEN;
		}
		// 色(の番号)が確定したので、その色(番号)に該当するspriteをbuttonLampに代入する。
		// (spriteは事前に定義してある)
		buttonLamp[buttonNo].GetComponent<Image>().sprite = buttonPicture[buttonColor[buttonNo]];

		// ボタンの色順をチェック
		if (buttonColor[0] == COLOR_BLUE && 
		buttonColor[1] == COLOR_WHITE &&
		buttonColor[2] == COLOR_RED)
		{
			// もしまだトンカチを持っていなければ
			if (doesHaveHammer == false)
			{
				DisplayMessage("金庫の中にトンカチが入っていた。");
				buttonHammer.SetActive(true);	// トンカチの絵を表示
				imageHammerIcon.GetComponent<Image>().sprite = hammerPicture;

				doesHaveHammer = true;
			}
			
		}
	}

	public void PushButtonHammer(){
		buttonHammer.SetActive(false);
	}
	

	public void PushButtonPig(){
		// トンカチを持っていなければ割れない
		if (doesHaveHammer == false)
		{
			// トンカチを持っていない
			DisplayMessage("素手では割れない。");
		}else{
			// トンカチを持っている
			DisplayMessage("貯金箱が割れて中から鍵が出てきた。");
			buttonPig.SetActive(false);
			buttonKey.SetActive(true);
			imageKeyIcon.GetComponent<Image>().sprite = keyPicture;

			doesHaveKey = true;
		}

	}

	public void PushButtonKey(){
		buttonKey.SetActive(false);
	}

	public void PushButtonBox(){
		if (doesHaveKey == false)
		{
			// 鍵を持っていない
			DisplayMessage("鍵かかっている");
		}else{
			// 鍵を持っている
			SceneManager.LoadScene("ClearScene");
		}
	}
}
