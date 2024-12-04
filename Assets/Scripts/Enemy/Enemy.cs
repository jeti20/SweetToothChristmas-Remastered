using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{//make enemy follow "Player" with constant speed
    [SerializeField] private float _speed;
    private Rigidbody _enemyRb;
    private GameObject _player;

    //Growth
    private Vector3 scaleChange;

    private void Awake()
    {
        scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
    }

    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        EnemyMovement();
        Growth();
        DestroyEnemy();
    }

    private void EnemyMovement()
    {
        Vector3 _lookDirection = (_player.transform.position - transform.position).normalized; //czym dalej jest przecwinik do gracza tym wi�ksza r�znica wi�c i spped jest wi�kszy, dlatego uzywamy normalized kt�re m�wi nam �e b�dzie mia�o taki sam speed
        _enemyRb.AddForce(_lookDirection * _speed);
    }

    private void Growth()
    {
        if (transform.localScale.x < 20)
        {
            transform.localScale += scaleChange * Time.deltaTime;
        }
    }

    private void DestroyEnemy()
    {
        // Niszczy przeciwnika, je�li jego pozycja jest poza okre�lonymi granicami
        if (transform.position.y < -5 || transform.position.z > 19 || transform.position.z < -40 || transform.position.x < -20 || transform.position.x > 25)
        {
            Destroy(gameObject);
        }

        Growth();
    }


}
