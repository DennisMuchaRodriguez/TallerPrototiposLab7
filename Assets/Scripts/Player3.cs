using UnityEngine;
using System.Collections;
public class Player3 : BasePlayerController, IAimable, IMoveable, IAttackable
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private Camera playerCamera; 

    private Vector2 _aimPosition;
    private bool isDashing = false;
    private Vector3 dashTarget;

    public Vector2 Position
    {
        get => _aimPosition;
        set => _aimPosition = value;
    }

    public void Move(Vector2 direction)
    {
        if (isDashing) return;

       
        if (direction != Vector2.zero)
        {
            Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Attack(Vector2 mouseScreenPosition)
    {
        if (isDashing) return;

    
        Ray ray = playerCamera.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            dashTarget = hit.point;
            StartCoroutine(Dash());
        }
        else
        {
            
            dashTarget = ray.GetPoint(100f);
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

      
        Vector3 direction = (dashTarget - transform.position).normalized;
        direction.y = 0f; 
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation; 
        }

        // 2. Mueve al personaje en línea recta
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }

        isDashing = false;
    }
}