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
        float xyDistance;
        float mapRadius;
        GameObject playerMarker = transform.Find("PlayerMarker").gameObject;
        Image enemyImage;
        foreach (GameObject enemy in _enemies)
        {
            xDistance = _player.transform.position.x - enemy.transform.position.x;
            yDistance = _player.transform.position.y - enemy.transform.position.y;
            zDistance = _player.transform.position.z - enemy.transform.position.z;
            xyDistance = Vector2.Distance(new Vector2(_player.transform.position.x, _player.transform.position.z), new Vector2(enemy.transform.position.x, enemy.transform.position.z));

            if(Math.Abs(xyDistance) < 100) { 
                if (Math.Abs(yDistance) < 2)
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
                      new Vector3(transform.position.x + xDistance, transform.position.y, transform.position.z + zDistance),
                      transform.rotation);
                }
                enemyImage.transform.SetParent(gameObject.transform);
                enemyImage.transform.position = playerMarker.transform.position;
                mapRadius = ((RectTransform)playerMarker.transform).rect.height - 25;

                var relativePosition = enemy.transform.InverseTransformDirection(_player.transform.forward);

                enemyImage.transform.position = new Vector3((int)(playerMarker.transform.position.x + (relativePosition.x * xyDistance)), (int)(playerMarker.transform.position.y + (relativePosition.z * - xyDistance)));

            }

        }
    }

    private double RadianToDegree(double angle)
    {
        return angle * (180.0 / Math.PI);
    }
}
