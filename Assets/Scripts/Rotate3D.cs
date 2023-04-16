using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3D : MonoBehaviour
{
    Transform mesh;
    Quaternion oldRotation;
    [SerializeField] float zRotateAngle;
    void Start()
    {
        mesh = transform.Find("Mesh");
        oldRotation = Quaternion.identity;
    }

    void Update()
    {
        mesh.transform.Rotate(0, 0, Time.deltaTime * zRotateAngle);
        oldRotation = transform.rotation;
    }
}
