using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject _explosionPrefab;
    public GameObject _player;
    public float _rotationSpeed;
    public float _movementSpeed;
    public float _shotCooldown = .5f;
    public GameObject _shotPrefab;
    public Transform _shotSpawn;
    public float _shotSpeed = 6f;

    private float resetTimer;

    // Use this for initialization
    void Start () {
        resetTimer = Time.time + 3;
        _player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
	
	// Update is called once per frame
	void Update () {
        if(_player != null) {
            CheckForDistance();
            RotateTowardsPlayer();
            MoveForward();
            if (Time.time > resetTimer)
            {
                resetTimer = Time.time + _shotCooldown;
                Fire();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var explosion = (GameObject)Instantiate(
            _explosionPrefab,
            transform.position,
            transform.rotation);
        Destroy(this.gameObject);
        Destroy(collision.gameObject);
    }

    void CheckForDistance()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        if (Math.Abs(distance) > 100)
        {
            Destroy(this.gameObject);
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 targetDir = _player.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = _rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime;
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var shot = (GameObject)Instantiate(
            _shotPrefab,
            _shotSpawn.position,
            _shotSpawn.rotation);

        // Add velocity to the bullet
        shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * _shotSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(shot, 4.0f);
    }
}
