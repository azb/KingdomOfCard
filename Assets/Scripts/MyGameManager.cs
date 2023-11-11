using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{

    OVRSceneAnchor[] allAnchors;

    List<GameObject> sceneModelDeskVolumes = new List<GameObject>();
    List<GameObject> sceneModelWallPlanes = new List<GameObject>();
    List<GameObject> sceneModelWindowPlanes = new List<GameObject>();
    List<GameObject> sceneModelFloorPlanes = new List<GameObject>();


    public GameObject basicChessPrefab;
    private GameObject basicChessObject;
    public GameObject catPrefab;
    private GameObject catObject;
    public GameObject newYorkPrefab;
    private GameObject newYorkObject;
    public GameObject submarinePrefab;
    private GameObject submarineObject;

    bool submarineInstantiated = false;

    OVRSceneManager sceneManager;
    OVRManager vrManager;

    void Start()
    {
        sceneManager = FindObjectOfType<OVRSceneManager>();
        if (sceneManager != null)
            sceneManager.SceneModelLoadedSuccessfully += SceneLoadedSuccessfully;
        vrManager = FindObjectOfType<OVRManager>();
    }

    void Update()
    {

        //// index button clicked
        //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        //{
        //    ExecuteChessPlacement();
        //}
        //// grib button clicked
        //else if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        //{
        //    ExecuteCatScene();
        //}
        //else if (OVRInput.GetDown(OVRInput.Button.One))
        //{
        //    ExecuteSubmarineMRScene();
        //}
        //else if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    ExecuteSubmarineVRScene();
        //}
    }

    public void SceneLoadedSuccessfully()
    {

        allAnchors = FindObjectsOfType<OVRSceneAnchor>();

        if (allAnchors.Length > 0 && allAnchors != null)
        {
            for (int i = 0; i < allAnchors.Length; i++)
            {
                MyObjectClassifier objectClassifier = allAnchors[i].gameObject.GetComponent<MyObjectClassifier>();

                if (objectClassifier != null)
                {
                    switch (objectClassifier.objectType)
                    {
                        case ObjectType.Desk:
                            sceneModelDeskVolumes.Add(allAnchors[i].gameObject);
                            break;
                        case ObjectType.Floor:
                            sceneModelFloorPlanes.Add(allAnchors[i].gameObject);
                            break;
                        case ObjectType.Wall:
                            sceneModelWallPlanes.Add(allAnchors[i].gameObject);
                            break;
                        case ObjectType.Window:
                            sceneModelWindowPlanes.Add(allAnchors[i].gameObject);
                            break;
                    }
                }
            }
        }
    }

    public void ExecuteChessPlacement()
    {
        if (sceneModelDeskVolumes.Count > 0)
        {
            basicChessObject = GameObject.Instantiate(basicChessPrefab);
            basicChessObject.transform.position = sceneModelDeskVolumes[0].transform.localPosition;
        }

        vrManager.isInsightPassthroughEnabled = true;
    }

    public void ExecuteCatScene()
    {
        if (sceneModelDeskVolumes.Count > 0)
        {
            catObject = GameObject.Instantiate(catPrefab);
            Vector3 catPos = new Vector3(sceneModelDeskVolumes[0].transform.localPosition.x,
                                      sceneModelDeskVolumes[0].transform.localPosition.y + 4,
                                      sceneModelDeskVolumes[0].transform.localPosition.z);

            catObject.transform.position = catPos;
        }

        vrManager.isInsightPassthroughEnabled = true;
    }
    
    public void ExecuteNewYorkView()
    {
        if (newYorkObject != null)
        {
            return;
        }

        if (sceneModelWindowPlanes.Count > 0)
        {
            newYorkObject = GameObject.Instantiate(newYorkPrefab);

            newYorkObject.transform.position = sceneModelWindowPlanes[0].transform.position;
            newYorkObject.transform.rotation = sceneModelWindowPlanes[0].transform.rotation;
        }

        vrManager.isInsightPassthroughEnabled = true;

        Destroy(submarineObject);
        submarineObject = null;
        submarineInstantiated = false;
    }

    private void InstantiateSubmarine()
    {
        submarineObject = GameObject.Instantiate(submarinePrefab);
        submarineInstantiated = true;

        submarineObject.transform.position = sceneModelWindowPlanes[0].transform.position;
        submarineObject.transform.rotation = sceneModelWindowPlanes[0].transform.rotation;

        Destroy(newYorkObject);
        newYorkObject = null;
    }

    public void ExecuteSubmarineMRScene()
    {
        if (sceneModelWindowPlanes.Count > 0)
        {
            if(!submarineInstantiated)
            {
                InstantiateSubmarine();
            }
            vrManager.isInsightPassthroughEnabled = true;

            ToggleStencilMode toggle = submarineObject.GetComponent<ToggleStencilMode>();
            if (toggle != null)
            {
                toggle.stencilMode = true;
            }
        }
    }

    public void ExecuteSubmarineVRScene()
    {
        if (!submarineInstantiated)
        {
            InstantiateSubmarine();
        }

        Destroy(basicChessObject);
        Destroy(catObject);

        ToggleStencilMode toggle = submarineObject.GetComponent<ToggleStencilMode>();
        if (toggle != null)
        {
            toggle.stencilMode = false;
        }

        vrManager.isInsightPassthroughEnabled = false;
        foreach (var anchor in allAnchors)
        {
            Crossfade crossfade = anchor.GetComponent<Crossfade>();
            if (crossfade != null)
            {
                crossfade.TriggerCrossfade();
            }
        }
    }

    public void ExecuteMySpace()
    {
        Destroy(newYorkObject);
        newYorkObject = null;
        Destroy(submarineObject);
        submarineObject = null;
        submarineInstantiated = false;

        vrManager.isInsightPassthroughEnabled = true;

        ToggleStencilMode toggle = submarineObject.GetComponent<ToggleStencilMode>();
        if (toggle != null)
        {
            toggle.stencilMode = true;
        }
    }
}
