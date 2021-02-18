using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;

    private ScreenManager _screenManager;
    private ViewportHandler _viewportHandler;

    private float spawnTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _screenManager = GameObject.Find("ScreenManager").GetComponent<ScreenManager>();
        _viewportHandler = GameObject.Find("Main Camera").GetComponent<ViewportHandler>();
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }


    public void startSpawning()
    {
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator enemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {

            Instantiate(enemyShipPrefab, new Vector3(Random.Range(_viewportHandler.TopLeft.x + 0.25f, _viewportHandler.TopRight.x - 0.25f), _viewportHandler.TopCenter.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime / 2f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(_viewportHandler.TopLeft.x + 0.25f, _viewportHandler.TopRight.x - 0.25f), _viewportHandler.TopCenter.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime * 3f);
        }
    }

}
