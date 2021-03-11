using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public GameObject poof;
    public int BaseHP = 10;
    public int hp;
    

    public float deathTime = 0.5f;

    void Start()
    {
        hp = BaseHP;
    }

    void Die()
    {
        Instantiate(poof,transform.position, transform.rotation);
        Destroy(gameObject);
    }


    public void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            TakeDamage();
            //Debug.Log(gameObject.name + " HIT. HP is now " + hp);
            Destroy(other.gameObject);
        }
    }

}
