using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (this.transform != null) Move();
    }

    private void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        this.transform.Translate(hInput * speed * Time.deltaTime, 0, 0);
        this.transform.Translate(0, vInput * speed * Time.deltaTime, 0);
    }
}
