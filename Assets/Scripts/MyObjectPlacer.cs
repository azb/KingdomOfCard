using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyObjectPlacer : MonoBehaviour
{

    OVRSceneAnchor[] allAnchors;

    private List<GameObject> sceneModelDeskVolumes = new List<GameObject>();
    private List<GameObject> sceneModelWallPlanes = new List<GameObject>();
    private List<GameObject> sceneModelWindowPlanes = new List<GameObject>();
    private List<GameObject> sceneModelFloorPlanes = new List<GameObject>();


    
    public GameObject basicChessPrefab;
    private bool chessBoardPlaced = false;
    public GameObject catPrefab;
    private bool catPlaced = false;
    public GameObject submarinePrefab;
    private bool submarinePlaced = false;

    OVRSceneManager sceneManager;
    OVRManager vrManager;

    public void Start()
    {
        sceneManager = FindObjectOfType<OVRSceneManager>();
        if(sceneManager != null)
            sceneManager.SceneModelLoadedSuccessfully += SceneLoadedSuccessfully;
        vrManager = FindObjectOfType<OVRManager>();
    }

    /*
    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("PRIMARY INDEX PRESSED");
            ExecuteChessPlacement();
        }
        else if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log("SECONDARY HAND TRIGGER PRESSED");
            ExecuteCatScene();
        }
    }*/

    public void SceneLoadedSuccessfully()
    {
        allAnchors = FindObjectsOfType<OVRSceneAnchor>();

        if(allAnchors.Length > 0 && allAnchors != null)
        {
            for (int i = 0; i < allAnchors.Length; i++)
            {
                MyObjectClassifier objectClassifier = allAnchors[i].gameObject.GetComponent<MyObjectClassifier>();

                if (objectClassifier != null)
                {
                    switch (objectClassifier.objectType)
                    {
                        case ObjectType.Desk:
                            Debug.Log("Found one object of type " + ObjectType.Desk.ToString());
                            sceneModelDeskVolumes.Add(allAnchors[i].gameObject);
                            break;
                        case ObjectType.Floor:
                            Debug.Log("Found one object of type " + ObjectType.Floor.ToString());
                            sceneModelFloorPlanes.Add(allAnchors[i].gameObject);
                            break;
                        case ObjectType.Wall:
                            Debug.Log("Found one object of type " + ObjectType.Wall.ToString());
                            sceneModelWallPlanes.Add(allAnchors[i].gameObject);
                            break;
                        case ObjectType.Window:
                            Debug.Log("Found one object of type " + ObjectType.Window.ToString());
                            sceneModelWindowPlanes.Add(allAnchors[i].gameObject);
                            break;
                    }
                }
            }
        }
    }

    public void EnablePassThrough()
    {
        for (int i = 0; i < allAnchors.Length; i++)
        {
            MeshRenderer renderer = allAnchors[i].GetComponent<MeshRenderer>();
            if (renderer)
                renderer.enabled = false;
        }

        vrManager.isInsightPassthroughEnabled = true;
    }


    public void ExecuteChessPlacement()
    {
        if (sceneModelDeskVolumes.Count > 0)
        {
            EnablePassThrough();
            GameObject chessObject = GameObject.Instantiate(basicChessPrefab);
            chessObject.transform.position = sceneModelDeskVolumes[0].transform.localPosition; 
        }
    }

    public void ExecuteCatScene()
    {
        if (sceneModelDeskVolumes.Count > 0)
        {
            EnablePassThrough();
            GameObject catObject = GameObject.Instantiate(catPrefab);
            catObject.transform.position = sceneModelDeskVolumes[0].transform.localPosition;
        }
    }

    public void ExecuteSubmarineScene()
    {
        if (sceneModelWindowPlanes.Count > 0)
        {
            EnablePassThrough();
            GameObject submarineObject = GameObject.Instantiate(submarinePrefab);
            submarineObject.transform.position = sceneModelWindowPlanes[0].transform.position - sceneModelWindowPlanes[0].transform.forward;
        }
    }
}
