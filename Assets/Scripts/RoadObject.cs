using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadObject: MonoBehaviour {

  public AudioClip collideSFX;
  public Sprite collideSprite;

  public void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      if (collideSprite != null) {
        GetComponent<SpriteRenderer>().sprite = collideSprite;
      }
      AudioSource.PlayClipAtPoint(collideSFX, Camera.main.transform.position);
    }
  }

}
