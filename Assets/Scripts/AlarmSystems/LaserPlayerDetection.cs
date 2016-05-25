using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {
    private GameObject player;
    private LastPlayerSighting lastPlayerSightning;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSightning = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void OnTriggerStay(Collider other)
    {
        if (GetComponent<MeshRenderer>())
        {
            if (other.gameObject == player)
            {
                lastPlayerSightning.position = other.transform.position;
            }
        }
    }
}
