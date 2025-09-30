using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class Turret : MonoBehaviour
{
    public float rotSpeed = 4.5f;
    public float fireRange = 4f;
    public float fireInterval = 2f;

    public Transform target;
    public GameObject bulletPrefab;

    private Coroutine fireCoroutine;

    void Update()
    {
        if(target == null) return;

        var direction = target.position - transform.position;
        direction.Normalize();
        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle -= 90f;

        var currentAngle = transform.eulerAngles.z;
        var newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotSpeed * Time.deltaTime);
        this.transform.rotation = Quaternion.Euler(0,0, newAngle);
        Debug.Log(targetAngle);


        var distance = Mathf.Sqrt(Mathf.Pow(target.position.x - this.transform.position.x, 2) + Mathf.Pow(target.position.y - this.transform.position.y, 2));

        var vectorDist = Vector3.Distance(target.position, this.transform.position);
        Debug.Log($"Distance {distance:F2}, Vector {vectorDist:F2}");

        if (distance < fireRange)
        {
            if (fireCoroutine == null) fireCoroutine = StartCoroutine(FireRoutine());
        }   
        else
        {
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
        }
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            FireBullet();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    public void FireBullet()
    {
        Vector2 dir = target.position - transform.position;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}
