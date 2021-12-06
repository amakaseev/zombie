using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    void Awake() {
      gameObject.SetActive(true);

      Actions.OnPlayerDie += OnPlayerDie;
    }

    public void Play() {
      gameObject.SetActive(false);
      Actions.OnPlay();
    }

    void OnPlayerDie() {
      gameObject.SetActive(true);
    }

}
