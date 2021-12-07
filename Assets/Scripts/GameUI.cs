using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI: MonoBehaviour
{

    public GameObject mainMenu;
    public AudioClip  clickSFX;
    public Text scoresText;
    public Text recordText;
    int scores;
    int record;

    void Awake() {
      mainMenu.SetActive(true);
      record = PlayerPrefs.GetInt("Record", 0);
      recordText.text = "Record: " + record.ToString();

      Actions.OnPlayerDie += OnPlayerDie;
      Actions.OnZombieDie += OnZombieDie;
    }

    public void Play() {
      mainMenu.SetActive(false);

      scores = 0;
      scoresText.text = "0";

      Actions.OnPlay();
      AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position);
    }

    void OnPlayerDie() {
      mainMenu.SetActive(true);
      recordText.text = "Record: " + record.ToString();
    }

    void OnZombieDie() {
      scores++;
      scoresText.text = scores.ToString();

      if (scores > record) {
        record = scores;
        PlayerPrefs.SetInt("Record", record);
        PlayerPrefs.Save();
      }
    }

}
