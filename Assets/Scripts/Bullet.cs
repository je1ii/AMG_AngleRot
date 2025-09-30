using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float speed = 6f;
    public float deathRange = 0.5f;

    private Transform player;
    private Vector2 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        this.transform.position += (Vector3)direction * speed * Time.deltaTime;
        if (player != null)
        {
            var distance = Mathf.Sqrt(Mathf.Pow(player.position.x - this.transform.position.x, 2) + Mathf.Pow(player.position.y - this.transform.position.y, 2));
            if (distance < deathRange)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }
}
