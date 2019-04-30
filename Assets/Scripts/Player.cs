using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
public class Player : MonoBehaviour
{
    public float maxDragDistance = 2f;


    private SpringJoint2D spring;
    private Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        //If the rigidbody is disabled
        if(rigid.isKinematic)
        {
            Vector2 playerPos = rigid.position;
            Vector2 hookPos = spring.connectedBody.position;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (mousePos - hookPos).normalized;
            // Get distance between player and hook
            float distance = Vector2.Distance(mousePos, hookPos);
            // If distance is greater than max drag distance
            if (distance > maxDragDistance)
            {
                // cap the position
                rigid.position = hookPos + direction * maxDragDistance;
            }
            else
            {
                // set position to mouse position
                rigid.position = mousePos;
            }

        }
    }
    void OnMouseDown()
    {
        //Enable kinematic
        rigid.isKinematic = true;
    }
    void OnMouseUp()
    {
        //Disable kinematic
        rigid.isKinematic = false;
    }
}
