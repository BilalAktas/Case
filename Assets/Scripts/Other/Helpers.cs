using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static IEnumerator MoveToStopPos(Vector3 stopPos, Transform obj)
    {
        float t = 0f;
        while (t < 1)
        {
            obj.position = Vector3.Lerp(obj.position, new Vector3(obj.position.x, obj.position.y, stopPos.z), t);
            t += Time.deltaTime * 2f;

            yield return new WaitForEndOfFrame();
        }

        // yield return new WaitForSeconds(.5f);
        
    }

    public static IEnumerator BreakOBJ(GameObject obj, int childNo, int break_childNo)
    {
        obj.transform.GetChild(childNo).gameObject.SetActive(false);
        obj.transform.GetChild(break_childNo).gameObject.SetActive(true);
        obj.GetComponent<BoxCollider>().isTrigger = true;
        Transform parent = obj.transform.GetChild(break_childNo);
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(.002f);
        }
        yield return new WaitForSeconds(.5f);

        obj.SetActive(false);
    }

    public static IEnumerator OpenRBWithDelay(Transform obj, float delay)
    {
        for (int i = 0; i < obj.childCount; i++)
        {
            obj.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;

            yield return new WaitForSeconds(delay);
        }
    }
}
