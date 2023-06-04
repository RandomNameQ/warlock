using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetVectorFromMouse
{
    public static Ray ray;
    

    public static Vector3 Take(float yPos)
    {
        // Cast a ray from the mouse position into the scene
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        int groundLayerMask = LayerMask.GetMask("Ground"); // Set the layer name "Ground" here

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            float x = hit.point.x;
            float y = hit.point.y;
            float z = hit.point.z;
            // 2 = player height
            var _pointToMove = new Vector3(x, yPos, z);
            return _pointToMove;
        }
        return Vector3.zero;
    }
}
