using UnityEngine;
using System;
using System.Collections;

public class ExplosiveScript : MonoBehaviour
{
  public float Gravity = 1.0f;
  public Throwable explosive;
  public GameObject explosionPrefab;
  public SpriteRenderer spriteRenderer;
  public FishSpawner fishSpawner;
  public SeaEnvironment seaEnvironment;
  public float environmentDamage = 20f;
  public void ThrowExplosive()
  {
    Vector3 start = transform.position;

    float archHeight = UnityEngine.Random.Range(0.1f, 0.8f) * Gravity;
    float startY = start.y;

    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3 target = new Vector2(mousePos.x, mousePos.y);

    float currentDistance = Vector2.Distance(start, target);

    if (currentDistance > explosive.throwDistance)
    {
      Vector3 dir = (target - start).normalized;
      target = start + (dir * explosive.throwDistance);
    }

    StartCoroutine(MoveExplosive(start, target, 1f, archHeight));
  }

  IEnumerator MoveExplosive(Vector3 start, Vector3 target, float duration, float arcHeight)
  {
    float t = 0f;
    while (t < duration)
    {
      float castDistance = Mathf.Lerp(start.x, target.x, t);
      float newY = Mathf.Lerp(start.y, target.y, t) + Mathf.Sin(t * Mathf.PI) * arcHeight;
      transform.position = new Vector3(castDistance, newY);

      t += Time.deltaTime / duration;
      yield return null;
    }
    //trigger explosion
    StartExplosion();
    fishSpawner.SpawnSchoolOfFish(target, explosive.blastRadius);
    //reduce environment health
    seaEnvironment.decreaseFishPopulation(environmentDamage);
    Destroy(gameObject);
  }

  void StartExplosion()
  {
    GameObject explosion = Instantiate(explosionPrefab);
    explosion.transform.position = transform.position;

    Destroy(explosion, 2f);
  }
}
