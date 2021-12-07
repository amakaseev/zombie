using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUI: MonoBehaviour {

    public GameObject mainMenu;
    public GameObject gameMenu;
    public AudioClip  clickSFX;
    public Text scoresText;
    public Text recordText;

    PlayerInput _playerInput;
    int _scores;
    int _record;

    void Start() {
      mainMenu.SetActive(true);
      _record = PlayerPrefs.GetInt("Record", 0);
      recordText.text = "Record: " + _record.ToString();

      _playerInput = GetComponent<PlayerInput>();
      _playerInput.enabled = false;

      Actions.OnPlayerDie += OnPlayerDie;
      Actions.OnZombieDie += OnZombieDie;
    }

    public void Play() {
      mainMenu.SetActive(false);
      _playerInput.enabled = true;

      _scores = 0;
      scoresText.text = "0";

      Actions.OnPlay();
      AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position);
    }

    public void OnGameMenu() {
      if (gameMenu.activeSelf) {
        gameMenu.SetActive(false);
        Time.timeScale = 1f;
      } else {
        gameMenu.SetActive(true);
        Time.timeScale = 0f;
      }
    }

    public void Quit() {
      Application.Quit();
    }

    void OnPlayerDie() {
      _playerInput.enabled = false;

      mainMenu.SetActive(true);
      recordText.text = "Record: " + _record.ToString();
    }

    void OnZombieDie() {
      _scores++;
      scoresText.text = _scores.ToString();

      if (_scores > _record) {
        _record = _scores;
        PlayerPrefs.SetInt("Record", _record);
        PlayerPrefs.Save();
      }
    }

}
