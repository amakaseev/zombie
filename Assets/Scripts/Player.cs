using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

  public bool alive = true;
  public Vector2 moveSpeed = new Vector2(5, 5);
  public Action<string> onDie;
  Vector2 moveDirection = Vector3.zero;
  PlayerInput _playerInput;

  void Start() {
    _playerInput = GetComponent<PlayerInput>();
  }

  public void OnMove(InputValue input) {
    moveDirection = input.Get<Vector2>();
  }

  public void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Enemy")) {
      alive = false;
      _playerInput.enabled = false;
      Actions.OnPlayerDie();
    }
  }

  void Update() {
    if (moveDirection != Vector2.zero) {
      var position = transform.position;
      position.x += moveDirection.x * moveSpeed.x * Time.deltaTime;
      position.y += moveDirection.y * moveSpeed.y * Time.deltaTime;
      if (position.x > 6) position.x = 6;
      if (position.x < -7) position.x = -7;
      if (position.y > 2.15f) position.y = 2.15f;
      if (position.y < -2.15f) position.y = -2.15f;
      transform.position = position;
    }
  }

}
