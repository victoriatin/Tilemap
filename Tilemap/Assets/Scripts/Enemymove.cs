using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove : MonoBehaviour
{

   public Vector2 position1;
   public Vector2 position2;
  public float speed;

   
    // Update is called once per frame
    void Update()
    {
          float time = Mathf.PingPong(Time.time * speed, 1);
          transform.position = Vector2.Lerp(position1, position2, time);
    }
}

