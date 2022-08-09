using UnityEngine;

public class Fly : Mob
{
    void Start()
    {
        LeanTween.moveY(gameObject, Random.Range(0.5f, 1.5f), 0.8f).setEaseInOutQuad().setLoopPingPong(-1);
    }
}
