using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car: RoadObject {

  public Sprite[] sprites;
  public float maxSpeed;

  float speed;

  void Start() {
    var spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

    speed = Random.Range(maxSpeed * 0.5f, maxSpeed);
  }

  public override void OnTriggerEnter2D(Collider2D other) {
    base.OnTriggerEnter2D(other);

    if (other.CompareTag("Player") || other.CompareTag("Wall")) {
      speed = 0;
    }
  }

  void Update() {
    var position = transform.position;
    position.x -= speed * Time.deltaTime;
    transform.position = position;
  }

}
