using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

  public float scrollSpeed = -0.5f;
  public GameObject enemyPrefab;
  public float spawnTime = 1;
  public float world2Texture = 16;

  private Vector2           _offset;
  private float             _timeToSpawn;
  private List<GameObject>  _enemies = new List<GameObject>();
  private Renderer          _renderer;

  void Start () {
    Application.targetFrameRate = 60;
    _renderer = GetComponent<Renderer>();
    _offset = _renderer.material.mainTextureOffset;

    ScaleToScreen();

    Actions.OnPlayerDie += OnPlayerDie;
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

  void SpawnEnemy() {
    var enemy = Instantiate(enemyPrefab);
    enemy.transform.position = new Vector3(10, Random.Range(-2f, 2f), 0);
    _enemies.Add(enemy);
  }

  void OnPlayerDie() {
    enabled = false;
  }

  void Update() {
    var dt = Time.deltaTime;
    _timeToSpawn += dt;
    if (_timeToSpawn >= spawnTime) {
      _timeToSpawn -= spawnTime;
      SpawnEnemy();
    }

    int enemyCount = _enemies.Count;
    for (int i = enemyCount - 1; i >=0; --i) {
      var enemy = _enemies[i];
      var position = enemy.transform.position;
      position.x += world2Texture * scrollSpeed * dt;
      enemy.transform.position = position;
      if (enemy.transform.position.x < -10) {
        Destroy(enemy);
        _enemies.Remove(enemy);
      }
    }


    _offset.x -= scrollSpeed * dt;
    while (_offset.x < 0) {
      _offset.x += 1;
    }
    _renderer.material.mainTextureOffset = _offset;
  }

}
