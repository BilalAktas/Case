using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviorHelper<CameraFollow>
{
    [SerializeField]
    private float distance;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 finalOffset;

    [SerializeField]
    private Transform target;

    private void LateUpdate()
    {
        transform.position = target.position + offset + (-transform.forward * distance);
    }    

    public IEnumerator ChangeOffset()
    {
        float t=0f;
        while (t<1)
        {
            offset = Vector3.Lerp(offset, finalOffset, t);
            distance = Mathf.Lerp(distance, 13, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0, 0), t);

            t += Time.deltaTime * 2f;

            yield return new WaitForEndOfFrame();
        }

    }
}
