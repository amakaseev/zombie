using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background: MonoBehaviour {

  public float scrollSpeed = -0.5f;

  private Vector2           _offset;
  private Renderer          _renderer;

  void Start () {
    _renderer = GetComponent<Renderer>();
    _offset = _renderer.material.mainTextureOffset;

    ScaleToScreen();

    Actions.OnPlay += OnPlay;
    Actions.OnPlayerDie += OnPlayerDie;

    enabled = false;
  }

  void OnPlay() {
    enabled = true;
    _offset.x = 0;
  }

  void OnPlayerDie() {
    enabled = false;
  }

  void ScaleToScreen() {
    var topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    var worldSpaceWidth = topRightCorner.x * 2;
    var worldSpaceHeight = topRightCorner.y * 2;

    var spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
    var scaleFactorX = worldSpaceWidth / spriteSize.x;
    var scaleFactorY = worldSpaceHeight / spriteSize.y;

    gameObject.transform.localScale = new Vector3(scaleFactorX, scaleFactorX, 1);
  }

  void Update() {
    var dt = Time.deltaTime;
   _offset.x -= scrollSpeed * dt;
    while (_offset.x < 0) {
      _offset.x += 1;
    }
    _renderer.material.mainTextureOffset = _offset;
  }

}
