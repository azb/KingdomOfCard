using System.Collections;
using UnityEngine;

public class ParentToObject : MonoBehaviour
{
    [SerializeField] private Transform newParent;
    [SerializeField] private string name;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer(1f);
    }

    void StartTimer(float waitTime)
    {
        if (gameObject.activeSelf)
        {
            IEnumerator timer = Timer(waitTime);
            StartCoroutine(timer);
        }
        else
        {
            Debug.LogError("Game Object is not active" + gameObject.name, gameObject);
        }
    }

    IEnumerator Timer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //Do something
        if (gameObject.activeSelf)
        {
            ZeroPosition();
        }
        else
        {
            StartTimer(1f);
        }
    }

    void ZeroPosition()
    {
        if (newParent != null)
        {
            transform.parent = newParent;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            if (name != "")
            {
                GameObject parentGO = GameObject.Find(name);

                if (parentGO == null)
                {
                    return;
                }

                if (!parentGO.activeSelf)
                {
                    return;
                }

                if (!gameObject.activeSelf)
                {
                    return;
                }

                transform.parent = parentGO.transform;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogError("Name is empty " + gameObject.name, gameObject);
            }
        }
    }
}
