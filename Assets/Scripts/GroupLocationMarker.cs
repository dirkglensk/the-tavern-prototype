using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupLocationMarker : MonoBehaviour
{
    private Transform anchor;

    public void SetAnchor(Transform newAnchor)
    {
        this.anchor = newAnchor;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchor.transform.position;
    }
}
