using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMovement : MonoBehaviour
{
    [SerializeField] private WhaleController controller;
    private float restStopWatch = 0;
    private void Update()
    {
        bool isWhaleMoving = controller.GetWhale().GetIsWhaleMoving();
        if (!isWhaleMoving)
        {
            restStopWatch += Time.deltaTime;
            if(restStopWatch >= controller.GetWhale().GetWhaleRestTime())
            {
                SetRandomDestination();
            }
        }
        else
        {
            restStopWatch = 0;
        }
    }

    private void SetRandomDestination()
    {
        float radius = 50f;

        Vector2 randomCircle = Random.insideUnitCircle * radius;

        Vector3 randomPosition = transform.position + new Vector3(
            randomCircle.x,
            0,
            randomCircle.y
        );

        controller.GetWhale().Move(randomPosition);

    }
}
