using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public GameObject _player;

    private bool _isGameOver = false;
    private GameObject _gameplayCanvas;
    private GameObject _endGameCanvas;
    private Button _btnReplay;
	// Use this for initialization
	void Start () {
        _gameplayCanvas = this.gameObject.transform.Find("Gameplay").gameObject;
        _endGameCanvas = this.gameObject.transform.Find("EndGame").gameObject;
        _btnReplay = _endGameCanvas.transform.Find("btnReplay").GetComponent<Button>();
        _btnReplay.onClick.AddListener(Replay);
    }
	
	// Update is called once per frame
	void Update () {
		if(_player == null && _isGameOver == false)
        {
            _isGameOver = true;
            _gameplayCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
        }
	}
    void Replay()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
