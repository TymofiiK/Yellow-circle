using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public AudioClip die;
    public Transform[] waypoints;
    public Vector2 spawnLocation;
    public float speed;
    private int waypointIndex;
    public int health;
    public GameObject goal;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            collision.GetComponent<Bullet>().Die();
            TakeDamage();
        }
    }
    void Start()
    {
        transform.position = spawnLocation;
        goal = GameObject.FindGameObjectWithTag("Circle");
    }

    void Update()
    {
        if(transform.position == goal.transform.position)
        {
            SceneManager.LoadScene("L");
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        { 

            if(waypointIndex < 3)
            {
                waypointIndex++;
            }

            
        }
    }
    public void TakeDamage()
    {

        health--;
        if (health == 0)
        {
            StartCoroutine(PlaySFX());
            Destroy(gameObject);
        }
    }

    IEnumerator PlaySFX()
    {
        AudioSource.PlayClipAtPoint(die, transform.position);
        yield return new WaitForSeconds(0.3f);
    }
}
