using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldOn = false;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    public GameObject _laserPrefab;
    [SerializeField]
    public GameObject _tripleShotPrefab;
    [SerializeField]
    private float _speed =5.0f;
    [SerializeField]
    private float _allowFire = 0.5f;
    [SerializeField]
    private float _fireRate = 1f;
    [SerializeField]
    private float speedBoostValue = 1.25f;
    [SerializeField]
    public int playerLives = 3;
    [SerializeField]
    private GameObject _explosion;
    // _ means variable is private



    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject[] engines;

    private ScreenManager _screenManager;
    private ViewportHandler _viewPortHandler;

    private int hitCount = 0;

    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Lives =" + playerLives); // display player lives at the start of the game.
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager != null)
        {
            _uiManager.UpdateLives(playerLives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _viewPortHandler = GameObject.Find("Main Camera").GetComponent<ViewportHandler>();
        if(_spawnManager != null)
        {
            _spawnManager.startSpawning();
        }


        _audioSource = GetComponent<AudioSource>();
        hitCount = 0;


        _screenManager = GameObject.Find("ScreenManager").GetComponent<ScreenManager>();
        this.transform.localScale = new Vector2((float)(_screenManager.getScreenWidth() * 0.05), (float)(_screenManager.getScreenHeight() * 0.03));

        joystick = _uiManager.joystick;

    }

// Update is called once per frame
void Update()
    {
        Movement();
        Shoot();
    }


   public void Damage()
    {

        if(isShieldOn == true) // if player has shield on
        {
            isShieldOn = false;
            _shieldGameObject.SetActive(false);
            return; // we do nothing = we dont substract life
        }

        hitCount++;
        if (hitCount == 1)
        {
            engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            engines[1].SetActive(true);
        }

        playerLives--; //substract life after every collision
        _uiManager.UpdateLives(playerLives);
        Debug.Log("Player Lives = " + playerLives); // display current number of lives
        if (playerLives < 1)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.hasGameStarted = false;
            _uiManager.showTitleScreen();
            Destroy(gameObject); //destroy player if it has 0 lives
        }
    }

    private void Shoot()
    {
            if (canTripleShot)   // When TripleShot PowerUp Enabled
            {

                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                StartCoroutine(ShootRoutine());

            }
            else if (Time.time > (_allowFire / 1.05f))
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.75f, 0f), Quaternion.identity);
                StartCoroutine(ShootRoutine());

            }

        _allowFire = Time.time + _fireRate;
            


    }

    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(2.0f);
    }

    private void Movement()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        

        if (isSpeedBoostActive == true)
        {
            if (joystick.Horizontal > 0f)
            {
                transform.Translate(Vector3.right * _speed * speedBoostValue * horizontalInput * Time.deltaTime);

            }
            else if (joystick.Horizontal < 0f)
            {


                transform.Translate(Vector3.right * _speed * speedBoostValue * horizontalInput * Time.deltaTime);
            }

            if(joystick.Vertical > 0f)
            {
                transform.Translate(Vector3.up * _speed * speedBoostValue * verticalInput * Time.deltaTime);
            } else if(joystick.Vertical < 0f)
            {
                transform.Translate(Vector3.down * _speed * speedBoostValue * verticalInput * Time.deltaTime);

            }



        }
        else
        {
            if (joystick.Horizontal > 0f)
            {
                transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);

            }
            else if (joystick.Horizontal < 0f)
            {


                transform.Translate(Vector3.left * (-_speed)   * horizontalInput * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.zero);
            }

            if (joystick.Vertical > 0f)
            {
                transform.Translate(Vector3.up * _speed  * verticalInput * Time.deltaTime);
            }
            else if (joystick.Vertical < 0f)
            {
                transform.Translate(Vector3.down * (-_speed)  * verticalInput * Time.deltaTime);

            } else
            {
                transform.Translate(Vector3.zero);
            }

        }



        if (transform.position.y > _viewPortHandler.MiddleCenter.y)
        {
            transform.position = new Vector3(transform.position.x, _viewPortHandler.MiddleCenter.y, 0);
        }
        else if (transform.position.y < _viewPortHandler.BottomCenter.y)
        {
            transform.position = new Vector2(transform.position.x, _viewPortHandler.BottomCenter.y);
        }
        else if (transform.position.x < _viewPortHandler.MiddleLeft.x)
        {
            transform.position = new Vector2(_viewPortHandler.MiddleRight.x - 0.25f, transform.position.y);
        }
        else if (transform.position.x > _viewPortHandler.MiddleRight.x)
        {
            transform.position = new Vector2(_viewPortHandler.MiddleLeft.x + 0.25f, transform.position.y);
        }

    }

    public void ShieldPowerUpOn()
    {
        isShieldOn = true;
        _shieldGameObject.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }

    public IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isShieldOn = false;
        _shieldGameObject.SetActive(false);
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleSHotPowerDownRoutine());
    }

    public IEnumerator TripleSHotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;

    }

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }



}
 