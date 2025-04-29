using UnityEngine;


public class Player1 : BasePlayerController, IAimable, IMoveable, IAttackable
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private Vector2 _aimPosition;

    public Vector2 Position
    {
        get => _aimPosition;
        set => _aimPosition = value;
    }

    public void Move(Vector2 direction)
    {
      
        Vector3 movement = new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void Attack(Vector2 position)
    {
       
        if (bulletPrefab && firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector2 direction = (position - (Vector2)transform.position).normalized;
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * 10f;
        }
    }
}
