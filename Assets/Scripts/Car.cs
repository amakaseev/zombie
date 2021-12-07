using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car: RoadObject {

  public Sprite[] sprites;

  void Start() {
    var spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
  }

  void Update() {

  }

}
