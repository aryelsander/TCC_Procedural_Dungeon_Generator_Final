using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float attack;
    private string enemyTag;
    private new Rigidbody2D rigidbody2D;
    private bool destroyOnCollision;
    public Rigidbody2D Rigidbody2D { get => rigidbody2D; }
    public string EnemyTag { get => enemyTag; set => enemyTag = value; }
    public float Attack { get => attack; set => attack = value; }
    public bool DestroyOnCollision { get => destroyOnCollision; set => destroyOnCollision = value; }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            collision.GetComponent<BaseUnityController>().OnTakeDamage(attack);
            if(destroyOnCollision)
                Destroy(gameObject);
        }
    }



}