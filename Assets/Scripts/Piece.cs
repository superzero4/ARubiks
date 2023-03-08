using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] float speed = 5;
    bool isFalling = true;

    private void Update()
    {
        if (!isFalling)
            return;

        transform.position = transform.position - new Vector3(0, 1, 0) * speed * Time.deltaTime;

        if(transform.position.y <= -5)
            Destroy(gameObject);
    }

    public void SetOnFace(Transform newPos)
    {
        isFalling = false;
        gameObject.transform.parent = newPos;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localEulerAngles = Vector3.zero;
        gameObject.transform.localScale = new Vector3(1.34f, 6.67f, 1.34f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Piece>() && isFalling)
        {
            Destroy(gameObject);
        }
    }

    public bool GetIsFalling()
    {
        return isFalling;
    }
}
