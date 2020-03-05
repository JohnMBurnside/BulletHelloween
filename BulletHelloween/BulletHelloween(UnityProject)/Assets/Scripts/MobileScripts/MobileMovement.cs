using UnityEngine;
public class MobileMovement : MonoBehaviour
{
    public GameObject prefab;
    public float speed = 5;
    float moveX = 0;
    float moveY = 0;
    public float shootDelay = .5f;
    float timer = 0;
    public float bulletLifetime = 10;
    public float bulletSpeed = 5;
    void Update()
    {
        timer += Time.deltaTime;
        Vector2 moveDir = new Vector2(moveX, moveY);
        GetComponent<Rigidbody2D>().velocity = moveDir * speed;
        if (Input.touchCount > 0 && timer > shootDelay)
            Shoot();
    }
    public void Shoot()
    {
        timer = 0;
        GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        Vector2 shootDir = new Vector2(touchPos.x - transform.position.x, touchPos.y - transform.position.y);
        shootDir.Normalize();
        bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }
    public void MoveRight() { moveX = 1; }
    public void MoveLeft() { moveX = -1; }
    public void MoveUp() { moveY = 1; }
    public void MoveDown() { moveY = -1; }
    public void StopMoveX() { moveX = 0; }
    public void StopMoveY() { moveY = 0; }
}
