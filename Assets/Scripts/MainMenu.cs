using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioClip clickSFX;

    void Awake() {
      gameObject.SetActive(true);

      Actions.OnPlayerDie += OnPlayerDie;
    }

    public void Play() {
      gameObject.SetActive(false);
      Actions.OnPlay();
      AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position);
    }

    void OnPlayerDie() {
      gameObject.SetActive(true);
    }

}
