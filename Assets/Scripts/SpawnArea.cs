using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
   public Vector2 size;


   private void OnDrawGizmos()
   {
    Gizmos.color = Color.red;

    float left = transform.position.x  - size.x / 2;
    float right = transform.position.x  - size.x / 2;
    float top = transform.position.y  - size.x / 2;
    float buttom = transform.position.y - size.x / 2;

    Gizmos.DrawLine(new Vector3(left, top), new Vector3(right, top));
    Gizmos.DrawLine(new Vector3(left, buttom), new Vector3(right, buttom));
   
    Gizmos.DrawLine(new Vector3(left, top), new Vector3(left, buttom));
    Gizmos.DrawLine(new Vector3(right, top), new Vector3(right, buttom));

   }
}
