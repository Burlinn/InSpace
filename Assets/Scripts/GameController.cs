using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] _enemiePrefabs;
    public GameObject _player;
    public float _spawnTime;
    public float _spawnCooldown;
    public GameObject _endGameCamera;

    private Vector3 _playerPosition;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Time.time > _spawnTime && _player != null)
        {
            _spawnTime = Time.time + _spawnCooldown;
            SpawnEnemy();
        }
        if (_player == null && _endGameCamera.activeInHierarchy == false)
        {
            _endGameCamera.SetActive(true);
            _endGameCamera.transform.position = new Vector3(_playerPosition.x + 6, _playerPosition.y + 30, _playerPosition.z);
            _endGameCamera.transform.Rotate(90, 0, 0);
        }
        else if (_player != null)
        {
            _playerPosition = _player.transform.position;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-20,20), Random.Range(-20, 20), Random.Range(-20, 20));
        int index;
        GameObject enemySpawn;
        if(spawnPoint.x < 5 && spawnPoint.x > -5)
        {
            if(spawnPoint.x > 0)
            {
                spawnPoint.x = 5;
            }
            if (spawnPoint.x < 0)
            {
                spawnPoint.x = -5;
            }
        }
        spawnPoint.x = _player.transform.position.x + spawnPoint.x;
        if (spawnPoint.y < 5 && spawnPoint.y > -5)
        {
            if (spawnPoint.y > 0)
            {
                spawnPoint.y = 5;
            }
            if (spawnPoint.y < 0)
            {
                spawnPoint.y = -5;
            }
        }
        spawnPoint.y = _player.transform.position.y + spawnPoint.y;
        if (spawnPoint.z < 5 && spawnPoint.z > -5)
        {
            if (spawnPoint.z > 0)
            {
                spawnPoint.z = 5;
            }
            if (spawnPoint.z < 0)
            {
                spawnPoint.z = -5;
            }
        }
        spawnPoint.z = _player.transform.position.z + spawnPoint.z;
        index = Random.Range(0, _enemiePrefabs.Length);
        enemySpawn = _enemiePrefabs[index];
        var enemy = (GameObject)Instantiate(
           enemySpawn,
           spawnPoint,
           new Quaternion());
    }

  

}
