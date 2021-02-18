using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 5.0f;
    [SerializeField]
    private GameObject _enemyExplosion;

    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _audioClip;

    private ScreenManager _screenManager;


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();


        _screenManager = GameObject.Find("ScreenManager").GetComponent<ScreenManager>();
        this.transform.localScale = new Vector2((float)(_screenManager.getScreenWidth() * 0.05), (float)(_screenManager.getScreenHeight() * 0.03));




    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if(transform.position.y < -6.0f)
        {
            transform.position = new Vector3(Random.Range(-9.75f, 9.75f), 3.5f, transform.position.z);
            transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
            Destroy(this.gameObject); //destroy enemy
        }
        else if(other.tag == "Laser")  
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
