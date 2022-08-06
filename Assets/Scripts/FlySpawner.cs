using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    public GameObject fly;

    private void Start()
    {
        LeanTween.moveY(gameObject, 5, 0.6f).setEaseInOutQuad().setLoopPingPong(-1);
        StartCoroutine(SpawnFly());
    }

    private IEnumerator SpawnFly()
    {
        yield return new WaitForSeconds(Random.Range(1f, 7f));
        Instantiate(fly, transform.position, Quaternion.identity, null);
        StartCoroutine(SpawnFly());
    }
}
