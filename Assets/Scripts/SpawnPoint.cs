
using UnityEngine;
using Helper.Utilities;

public class SpawnPoint : MonoBehaviour
{

    public Side side;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("SpawnPoint") || collision.CompareTag("Room"))
        {
            Destroy(gameObject);
        }
    }

}