using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PrimaryButtonEvent : UnityEvent<bool> { }

public class AnchoredObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnableObject;

    [SerializeField]
    private Camera mainCamera;

    private PrimaryButtonEvent primaryButtonPress;
    private bool lastButtonState = false;
    private List<InputDevice> devicesWithPrimaryButton;


    private void Awake()
    {
        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
            primaryButtonPress.AddListener(OnPrimaryButtonPressedChanged);
        }

        devicesWithPrimaryButton = new List<InputDevice>();

        if (mainCamera == null)
        {
            mainCamera = GameObject.FindAnyObjectByType<Camera>();
        }
    }

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithPrimaryButton.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
        {
            devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithPrimaryButton.Contains(device))
            devicesWithPrimaryButton.Remove(device);
    }

    private void OnPrimaryButtonPressedChanged(bool isPressed)
    {
        if (isPressed)
        {
            GameObject newObj = GameObject.Instantiate(this.spawnableObject);
            newObj.transform.position = mainCamera.transform.position + mainCamera.transform.forward;
        }
    }

    void Update()
    {
        bool tempState = false;
        foreach (var device in devicesWithPrimaryButton)
        {
            bool primaryButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) // did get a value
                        && primaryButtonState // the value we got
                        || tempState; // cumulative result from other controllers
        }

        if (tempState != lastButtonState) // Button state changed since last frame
        {
            primaryButtonPress.Invoke(tempState);
            lastButtonState = tempState;
        }
    }
}
