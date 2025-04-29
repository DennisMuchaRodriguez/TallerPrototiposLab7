using UnityEngine;


public class Player2 : BasePlayerController, IAimable, IMoveable, IAttackable
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Camera playerCamera; 

    private Vector2 _aimPosition;

    public Vector2 Position
    {
        get => _aimPosition;
        set => _aimPosition = value;
    }

    public void Move(Vector2 direction)
    {
   
        Vector3 movement = new Vector3(direction.x, 0f, direction.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void Attack(Vector2 mouseScreenPosition)
    {
        
        Ray ray = playerCamera.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            Instantiate(explosionPrefab, hit.point, Quaternion.identity);
        }
        else
        {
            
            Vector3 explosionPosition = ray.GetPoint(10f); 
            Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        }
    }
}