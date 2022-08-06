using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float minTime;
    public float maxTime;

    private void Start()
    {
        LeanTween.moveY(gameObject, 5, 0.6f).setEaseInOutQuad().setLoopPingPong(-1);
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        Instantiate(prefab, transform.position, Quaternion.identity, null);
        StartCoroutine(SpawnObject());
    }
}
