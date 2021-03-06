﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour {

    public PickUp pickUpTarget;

    Renderer child;

    void Awake()
    {
        child = GetComponentInChildren<SpriteRenderer>();
    }
    

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collider.enabled = false;
            pickUpTarget.Get();
            child.enabled = false;
            GameObject.Destroy(this.gameObject, 1.0f);
        }
    }
}
