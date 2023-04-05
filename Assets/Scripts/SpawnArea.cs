using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
   public Vector2 size;
   private float left;
   private float right;
   private float top;
   private float buttom;

   public void spawnBall(transform ballTransform)
   { 
      float x = Random.Range(left, right);
      float y = Random.Range(buttom, top);

      ballTransform.position = new Vector3(x, y);
   }

   private void OnDrawGizmos()
   {
    Gizmos.color = Color.red;

   left = transform.position.x  - size.x / 2;
   right = transform.position.x  - size.x / 2;
   top = transform.position.y  - size.x / 2;
   buttom = transform.position.y - size.x / 2;

    Gizmos.DrawLine(new Vector3(left, top), new Vector3(right, top));
    Gizmos.DrawLine(new Vector3(left, buttom), new Vector3(right, buttom));
   
    Gizmos.DrawLine(new Vector3(left, top), new Vector3(left, buttom));
    Gizmos.DrawLine(new Vector3(right, top), new Vector3(right, buttom));

   }
}
