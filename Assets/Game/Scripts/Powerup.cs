using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private int powerupID; //0=tripleshot, 1=speedboost, 2=shields

    [SerializeField]
    private AudioClip _clip;


    private ScreenManager _screenManager;



    private void Start()
    {
        _screenManager = GameObject.Find("ScreenManager").GetComponent<ScreenManager>();
        this.transform.localScale = new Vector2((float)(_screenManager.getScreenWidth() * 0.05), (float)(_screenManager.getScreenHeight() * 0.03));

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
        //access the player
        Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                //enable tripleshot
                if(powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }else if(powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                    //enable speed boost here

                }
                else if(powerupID == 2)
                {
                    player.ShieldPowerUpOn();
                    //enable shields
                }




            }

        
        //destroy ourselves
        Destroy(this.gameObject);

        }
       
    }



}
