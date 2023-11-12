using Oculus.Interaction;
using UnityEngine;
using UnityEngine.XR;

public class MouseControls : MonoBehaviour
{
    void Update()
    {
        if (!XRSettings.isDeviceActive)
        {
            // Check if the left mouse button is clicked
            Debug.Log("dsfdsgds");
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any collider
            if (Physics.Raycast(ray, out hit))
            {
                // Log the name of the object hit
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

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

                // You can perform other actions with the hit object here
                // For example, you can access hit.collider.gameObject to get the GameObject that was hit
            }
        }
    }
}