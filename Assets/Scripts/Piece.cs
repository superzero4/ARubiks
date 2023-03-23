using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    float speed = 3;
    bool isFalling = true;

    public float Speed { get => speed; set => speed = value; }
    public void PreparePiece()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }
    private void Update()
    {
        if (!isFalling)
            return;

        //Move the piece
        transform.position = transform.position - new Vector3(0, 1, 0) * speed * Time.deltaTime;

        if (transform.position.y <= -5)
            Destroy(gameObject);
    }
    //Attach the piece to the square it fallen on
    public void SetOnFace(Transform newPos)
    {
        isFalling = false;
        gameObject.transform.parent = newPos;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localEulerAngles = Vector3.zero;
        gameObject.transform.localScale = new Vector3(1.34f, 6.67f, 1.34f);
    }

    //Return isFalling
    public bool GetIsFalling()
    {
        return isFalling;
    }
}
