using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float minTime;
    public float maxTime;
    public bool move;
    private SpriteRenderer spriteRend;
    public Sprite[] variants;
    private int lastRandom = 0;
    private int currentRandom = 0;

    private void Start()
    {
        if(move)LeanTween.moveY(gameObject, 5, 0.6f).setEaseInOutQuad().setLoopPingPong(-1);
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        GameObject objectToSpawn = Instantiate(prefab, transform.position, Quaternion.identity, null);
        if (variants.Length > 0)
        {
            spriteRend = objectToSpawn.GetComponent<SpriteRenderer>(); do
            {
                currentRandom = Random.Range(0, variants.Length);
            }
            while (lastRandom == currentRandom);
            spriteRend.sprite = variants[currentRandom];
            lastRandom = currentRandom;
        }
        StartCoroutine(SpawnObject());
    }
}
