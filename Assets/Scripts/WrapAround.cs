using UnityEngine;

public class WrapAround : MonoBehaviour
{

    [SerializeField] float size;
    void Update()
    {
        float halfScreenHeight = LevelManager.instance.halfScreenHeight;
        float halfScreenWidth = LevelManager.instance.halfScreenWidth;

        Vector3 xVec = Vector3.right * (halfScreenWidth + size) * 2; // Vector3.right = new Vector3(1,0,0)
        Vector3 yVec = Vector3.up * (halfScreenHeight + size) * 2; // Vector3.up = new Vector3(0,1,0)

        if (transform.position.x > (halfScreenWidth + size))
            transform.position -= xVec;
        if (transform.position.x < -(halfScreenWidth + size))
            transform.position += xVec;

        if (transform.position.y > (halfScreenHeight + size))
            transform.position -= yVec;
        if (transform.position.y < -(halfScreenHeight + size))
            transform.position += yVec;
    }
}
