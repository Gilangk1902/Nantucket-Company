using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private MotherShip controller;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject destinationHighlight;

    public void Update()
    {
        if (Input.GetMouseButton(0) && controller.getIsEnabled()) 
        {
            Vector3 pos = GetMouseWorldPosition();
            Move(pos);
        }
        HideDestinationHighlight();

    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.point;

        return Vector3.zero;
    }

    private void Move(Vector3 destination)
    {
        destinationHighlight.transform.position = destination;
        destinationHighlight.SetActive(true);

        //partyManagement.getPartyLeader().Move(partyManagement.getFormationSpot(1));
        controller.GetShip().Move(destination);
        
    }

    private void HideDestinationHighlight()
    {
        if (controller.GetShip().HasReachedDestination()){ destinationHighlight.SetActive(false); }
    }

}
