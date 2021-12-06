using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

  public float scrollSpeed = -0.5f;
  public GameObject[] objectsPrefab;
  public float spawnTime = 1;

  private Vector2           _offset;
  private float             _timeToSpawn;
  private List<GameObject>  _roadObjects = new List<GameObject>();
  private Renderer          _renderer;

  void Start () {
    Application.targetFrameRate = 60;
    _renderer = GetComponent<Renderer>();
    _offset = _renderer.material.mainTextureOffset;

    ScaleToScreen();

    Actions.OnPlay += OnPlay;
    Actions.OnPlayerDie += OnPlayerDie;

    enabled = false;
  }

  void ScaleToScreen() {
    var topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    var worldSpaceWidth = topRightCorner.x * 2;
    var worldSpaceHeight = topRightCorner.y * 2;

    var spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
    var scaleFactorX = worldSpaceWidth / spriteSize.x;
    var scaleFactorY = worldSpaceHeight / spriteSize.y;

    gameObject.transform.localScale = new Vector3(scaleFactorX * 0.5f, gameObject.transform.localScale.y, 1);
  }

  void SpawnRoadObject() {
    var roadObject = Instantiate(objectsPrefab[(int)Random.Range(0, objectsPrefab.Length)]);
    roadObject.transform.position = new Vector3(15, Random.Range(-2f, 2f), 0);
    _roadObjects.Add(roadObject);
  }

  void OnPlay() {
    enabled = true;
    _offset.x = 0;
    int objectsCount = _roadObjects.Count;
    for (int i = objectsCount - 1; i >=0; --i) {
      Destroy(_roadObjects[i]);
    }
    _roadObjects.Clear();
  }

  void OnPlayerDie() {
    enabled = false;
  }

  void Update() {
    var dt = Time.deltaTime;
    _timeToSpawn += dt;
    if (_timeToSpawn >= spawnTime) {
      _timeToSpawn -= spawnTime;
      SpawnRoadObject();
    }

    float world2Texture = gameObject.transform.localScale.x * 10.2f;
    int roadObjectCount = _roadObjects.Count;
    for (int i = roadObjectCount - 1; i >=0; --i) {
      var roadObject = _roadObjects[i];
      var position = roadObject.transform.position;
      position.x += world2Texture * scrollSpeed * dt;
      roadObject.transform.position = position;
      if (roadObject.transform.position.x < -15) {
        Destroy(roadObject);
        _roadObjects.Remove(roadObject);
      }
    }


    _offset.x -= scrollSpeed * dt;
    while (_offset.x < 0) {
      _offset.x += 1;
    }
    _renderer.material.mainTextureOffset = _offset;
  }

}
