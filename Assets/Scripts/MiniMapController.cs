using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniMapController : MonoBehaviour {

    public Image _enemyDotPrefab;
    public Image _enemyUpPrefab;
    public Image _enemyDownPrefab;

    private GameObject _player;
    private GameObject[] _enemies;
    private Image[] _enemyImages;
    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
	
	// Update is called once per frame
	void Update () {
        if(_player != null) { 
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            ClearImages();
            DrawMiniMap();
        }
    }
    void ClearImages()
    {
        foreach(GameObject enemyImage in GameObject.FindGameObjectsWithTag("MiniMapEnemy"))
        {
            Destroy(enemyImage);
        }
    }
    void DrawMiniMap()
    {
        float xDistance;
        float yDistance;
        float zDistance;
        string yDirection;
        Image enemyImage;
        foreach (GameObject enemy in _enemies)
        {
            xDistance = _player.transform.position.x - enemy.transform.position.x;
            yDistance = _player.transform.position.y - enemy.transform.position.y;
            zDistance = _player.transform.position.z - enemy.transform.position.z;
            if(Math.Abs(yDistance) < 2)
            {
                enemyImage = Instantiate(
                    _enemyDotPrefab,
                    transform.position,
                    transform.rotation);
            }
            else if (_player.transform.position.y > enemy.transform.position.y)
            {
                enemyImage = Instantiate(
                  _enemyDownPrefab,
                  transform.position,
                  transform.rotation);
            }
            else
            {
                enemyImage = Instantiate(
                  _enemyUpPrefab,
                  transform.position,
                  transform.rotation);
            }
            enemyImage.transform.SetParent(gameObject.transform);
        }
    }
}
