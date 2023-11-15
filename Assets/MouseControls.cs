using Oculus.Interaction;
using UnityEngine;
using UnityEngine.XR;

public class MouseControls : MonoBehaviour
{
    void Update()
    {
        if (!XRSettings.isDeviceActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name.Contains("CardSlot")
                    && Input.GetMouseButtonDown(0))
                {
                    CardSlot cardSlot = hit.collider.gameObject.GetComponent<CardSlot>();
                    cardSlot.ShowMonster();
                }

                if (hit.collider.gameObject.name.Contains("Button")
                    && Input.GetMouseButtonDown(0))
                {
                    InteractableUnityEventWrapper button = hit.collider.gameObject.GetComponentInParent< InteractableUnityEventWrapper >();
                    button.WhenUnselect.Invoke();
                }
            }
        }
    }
}