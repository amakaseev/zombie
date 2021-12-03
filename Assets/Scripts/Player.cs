using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
  public Vector2 moveSpeed = new Vector2(5, 5);
  Vector2 moveDirection = Vector3.zero;

  public void OnMove(InputValue input) {
    moveDirection = input.Get<Vector2>();
    Debug.Log(moveDirection);
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
