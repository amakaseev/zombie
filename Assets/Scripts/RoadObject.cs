using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadObject: MonoBehaviour {

  public AudioClip collideSFX;
  public Sprite collideSprite;

  public virtual void OnTriggerEnter2D(Collider2D other) {
    if (collideSprite != null) {
      GetComponent<SpriteRenderer>().sprite = collideSprite;
    }
    AudioSource.PlayClipAtPoint(collideSFX, Camera.main.transform.position);
  }

}
