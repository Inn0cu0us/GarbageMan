﻿using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

    public float speed = 6f;
    public float range = 100f;

    Vector2 movement;
    Vector2 aim;
    LineRenderer gunLine;
    //	Animator anim;
    Rigidbody2D playerRigidbody;
    public LayerMask layerToIgnore;

    public GameObject bullet;
    public Transform bulletSpawn;
    public float fireRate;

    private float nextFire;

    private float atan2;

    void Awake()
    {
        //		anim = GetComponent<Animator> ();
        playerRigidbody = GetComponent<Rigidbody2D>();
        gunLine = GetComponent<LineRenderer>();
        gunLine.sortingLayerName = "OnGround";
    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float aimH = Input.GetAxisRaw("Horizontal2");
        float aimV = Input.GetAxisRaw("Vertical2");

        Move(h, v);
        Aiming(aimH, aimV);
        //		Animating (h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, v);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + movement);
        playerRigidbody.angularVelocity = 0;
    }

    void Aiming(float h, float v)
    {
        if (h == 0 && v == 0)
        {
            gunLine.enabled = false;
        }
        else
        {
            //			Debug.Log("h: " + h + "  v: " + v);
            aim.Set(h, v);
            gunLine.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(playerRigidbody.position, aim, Mathf.Infinity, ~layerToIgnore);
            gunLine.SetPosition(0, playerRigidbody.position);
            if (hit.collider == null)
            {
                //				Debug.Log("NoHit!");
                gunLine.SetPosition(1, playerRigidbody.position + aim.normalized * range);
            }
            else
            {
                //				Debug.Log ("Hit!" + hit.collider.name);
                gunLine.SetPosition(1, playerRigidbody.position + aim.normalized * hit.distance);
            }

            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                atan2 = Mathf.Atan2(aim.y, aim.x);
                Instantiate(bullet, bulletSpawn.position, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg - 90f));
            }
        }
    }

    //	void Animating(float h, float v)
    //	{
    //		bool walking = h != 0f || v != 0f;
    //		anim.SetBool ("IsWalking", walking);
    //	}
}
