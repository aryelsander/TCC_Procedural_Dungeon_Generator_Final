using UnityEngine;

public class BoomerangEffect : MonoBehaviour
{

    private Vector3 startPosition;
   [SerializeField] private Vector2 frequency;
    [SerializeField] private Vector2 amplitude;
    private float time;
    
    public Vector2 Frequency { get => frequency; set => frequency = value; }
    public Vector2 Amplitude { get => amplitude; set => amplitude = value; }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        time += 0.01f;

        Vector3 sinoidalMove = startPosition + new Vector3(transform.up.x * Mathf.Sin(time * frequency.x) * amplitude.x, transform.up.y * Mathf.Sin(time * frequency.y) * amplitude.y, 0);
        transform.position = sinoidalMove;
    }

}
