using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    float movementSpeed = 1;
    public System.Action<Point> onReachedPosition;
    float faultPosition = 1f;
    Point targetPoint = null;
    Rigidbody characterRigidbody;

    private void Awake() => characterRigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        if (targetPoint != null)
        {
            Vector3 targetPosition = new Vector3(targetPoint.PointPosition.x, 1, targetPoint.PointPosition.z);
            Vector3 direction = (targetPoint.PointPosition - transform.position).normalized; direction = new Vector3(direction.x, 0, direction.z);
            characterRigidbody.velocity = (direction * movementSpeed * 0.1f);

            if (Vector3.Distance(transform.position ,targetPoint.PointPosition) < faultPosition)
            {
                characterRigidbody.velocity = Vector3.zero;
                onReachedPosition?.Invoke(targetPoint);
            }
        }
    }

    /// <summary> Установить точку для передвижения. </summary>
    public void SetMovementPosition(Point point) => targetPoint = point;
}
