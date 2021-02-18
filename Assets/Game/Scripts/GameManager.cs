using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    private UIManager _uiManager;
    private ViewportHandler _vP;

    //if gameOver = true
    //if spacekey press
    //spawn player -> gameOver = false -> hide title screen

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _vP = GameObject.Find("Main Camera").GetComponent<ViewportHandler>();
    }


        public void gameStart()
    {
        if (gameOver == true)
        {
            Instantiate(player, new Vector3(0f, _vP.BottomCenter.y + 1, 0f), Quaternion.identity);
            gameOver = false;
            _uiManager.hasGameStarted = true;
            _uiManager.hideTitleScreen();
        }
    }





}
