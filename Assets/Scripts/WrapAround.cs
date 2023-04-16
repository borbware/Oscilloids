using UnityEngine;

public class WrapAround : MonoBehaviour
{
    enum WrapAroundModeEnum {
        Rectangular,
        Circular
    }
    [SerializeField] float size;
    [SerializeField] WrapAroundModeEnum wrapAroundMode = WrapAroundModeEnum.Circular;
    void RectangularWrapAround()
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
    void CircularWrapAround()
    {
        float radius = LevelManager.instance.halfScreenWidth;
        if (transform.position.magnitude > radius + size)
            transform.position = -transform.position;
    }
    void Update()
    {
        if (wrapAroundMode == WrapAroundModeEnum.Rectangular)
            RectangularWrapAround();
        else if (wrapAroundMode == WrapAroundModeEnum.Circular)
            CircularWrapAround();

    }
}
