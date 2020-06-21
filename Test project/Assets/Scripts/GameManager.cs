using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point
{
    public Vector3 PointPosition { get { return pointPosition; } }
    Vector3 pointPosition;

    public Point(Vector3 position) => pointPosition = position;
}

public class GameManager : MonoBehaviour
{
    public CharacterController characterController;
    public GameUI gameUI;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] List<Point> positionQueue = new List<Point>();

    private void Awake()
    {
        gameUI.SetGameManager(this);
        characterController.onReachedPosition += DeletePoint;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 20, groundLayerMask))
            {
                if(raycastHit.point.y == 0.5f)
                {
                    Point newPoint = new Point(raycastHit.point);
                    AddNewPositionOnQueue(newPoint);
                }
            }
        }
    }

    public void SetVelocity(float value) => characterController.MovementSpeed = value;

    void DeletePoint(Point point)
    {
        positionQueue.Remove(point);

        if (positionQueue.Count > 0)
            characterController.SetMovementPosition(positionQueue[0]);
        else if (positionQueue.Count == 0)
            characterController.SetMovementPosition(null);
    }

    void AddNewPositionOnQueue(Point point)
    {
        positionQueue.Add(point);//Добавляю позицию в очередь

        if (positionQueue.Count == 1)
            characterController.SetMovementPosition(positionQueue[0]);
    }
}
