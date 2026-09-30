using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using System;

public static class BuildXRScene
{
    [MenuItem("Tools/Build EC_XR_SanchezDeyvi Scene")]
    public static void BuildScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "EC_XR_SanchezDeyvi";

        CreateMaterials();
        CreateFloor();
        CreateWalls();
        CreateLighting();
        CreateObjects();
        CreateXRRig();
        CreateTeleportationArea();
        CreateRayInteraction();

        string path = "Assets/Scenes/EC_XR_SanchezDeyvi.unity";
        EditorSceneManager.SaveScene(scene, path);
        Debug.Log("Scene EC_XR_SanchezDeyvi created successfully at: " + path);
    }

    static void CreateMaterials()
    {
        System.IO.Directory.CreateDirectory("Assets/Materials");

        var redMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        redMat.color = new Color(0.9f, 0.2f, 0.2f);
        redMat.name = "RedMat";
        AssetDatabase.CreateAsset(redMat, "Assets/Materials/RedMat.mat");

        var blueMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        blueMat.color = new Color(0.2f, 0.4f, 0.9f);
        blueMat.name = "BlueMat";
        AssetDatabase.CreateAsset(blueMat, "Assets/Materials/BlueMat.mat");

        var greenMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        greenMat.color = new Color(0.2f, 0.8f, 0.3f);
        greenMat.name = "GreenMat";
        AssetDatabase.CreateAsset(greenMat, "Assets/Materials/GreenMat.mat");

        var yellowMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        yellowMat.color = new Color(0.95f, 0.85f, 0.1f);
        yellowMat.name = "YellowMat";
        AssetDatabase.CreateAsset(yellowMat, "Assets/Materials/YellowMat.mat");

        var purpleMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        purpleMat.color = new Color(0.6f, 0.2f, 0.8f);
        purpleMat.name = "PurpleMat";
        AssetDatabase.CreateAsset(purpleMat, "Assets/Materials/PurpleMat.mat");

        var floorMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        floorMat.color = new Color(0.35f, 0.35f, 0.4f);
        floorMat.name = "FloorMat";
        AssetDatabase.CreateAsset(floorMat, "Assets/Materials/FloorMat.mat");

        var wallMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        wallMat.color = new Color(0.7f, 0.7f, 0.75f);
        wallMat.name = "WallMat";
        AssetDatabase.CreateAsset(wallMat, "Assets/Materials/WallMat.mat");
    }

    static void CreateFloor()
    {
        var floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.name = "Floor";
        floor.transform.position = Vector3.zero;
        floor.transform.localScale = new Vector3(2, 1, 2);
        floor.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/FloorMat.mat");
    }

    static void CreateWalls()
    {
        var wallMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/WallMat.mat");

        var northWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        northWall.name = "Wall_North";
        northWall.transform.position = new Vector3(0, 1.5f, 5);
        northWall.transform.localScale = new Vector3(10, 3, 0.2f);
        northWall.GetComponent<Renderer>().sharedMaterial = wallMat;

        var southWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        southWall.name = "Wall_South";
        southWall.transform.position = new Vector3(0, 1.5f, -5);
        southWall.transform.localScale = new Vector3(10, 3, 0.2f);
        southWall.GetComponent<Renderer>().sharedMaterial = wallMat;

        var eastWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        eastWall.name = "Wall_East";
        eastWall.transform.position = new Vector3(5, 1.5f, 0);
        eastWall.transform.localScale = new Vector3(0.2f, 3, 10);
        eastWall.GetComponent<Renderer>().sharedMaterial = wallMat;

        var westWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        westWall.name = "Wall_West";
        westWall.transform.position = new Vector3(-5, 1.5f, 0);
        westWall.transform.localScale = new Vector3(0.2f, 3, 10);
        westWall.GetComponent<Renderer>().sharedMaterial = wallMat;
    }

    static void CreateLighting()
    {
        var lightGO = new GameObject("Directional Light");
        var light = lightGO.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        lightGO.transform.rotation = Quaternion.Euler(50, -30, 0);

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.4f, 0.4f, 0.45f);
    }

    static void CreateObjects()
    {
        // Object 1: Red Cube (grabbable)
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Grabbable_Cube";
        cube.transform.position = new Vector3(-1.5f, 0.5f, 1.5f);
        cube.transform.localScale = Vector3.one * 0.3f;
        cube.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/RedMat.mat");
        cube.AddComponent<Rigidbody>();
        cube.AddComponent<XRGrabInteractable>();

        // Object 2: Blue Sphere (grabbable)
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "Grabbable_Sphere";
        sphere.transform.position = new Vector3(1.5f, 0.5f, 1.5f);
        sphere.transform.localScale = Vector3.one * 0.3f;
        sphere.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/BlueMat.mat");
        sphere.AddComponent<Rigidbody>();
        sphere.AddComponent<XRGrabInteractable>();

        // Object 3: Green Cylinder
        var cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.name = "Cylinder";
        cylinder.transform.position = new Vector3(0, 0.5f, -1.5f);
        cylinder.transform.localScale = new Vector3(0.3f, 0.5f, 0.3f);
        cylinder.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/GreenMat.mat");
        cylinder.AddComponent<Rigidbody>();

        // Object 4: Yellow Capsule
        var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.name = "Capsule";
        capsule.transform.position = new Vector3(-2, 0.5f, -1.5f);
        capsule.transform.localScale = Vector3.one * 0.25f;
        capsule.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/YellowMat.mat");
        capsule.AddComponent<Rigidbody>();

        // Object 5: Purple Key (grabbable)
        var key = GameObject.CreatePrimitive(PrimitiveType.Cube);
        key.name = "Key";
        key.transform.position = new Vector3(2, 0.5f, -1.5f);
        key.transform.localScale = new Vector3(0.15f, 0.4f, 0.15f);
        key.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/PurpleMat.mat");
        key.AddComponent<Rigidbody>();
        key.AddComponent<XRGrabInteractable>();

        // Object 6: Table (static)
        var table = GameObject.CreatePrimitive(PrimitiveType.Cube);
        table.name = "Table";
        table.transform.position = new Vector3(0, 0.4f, 0);
        table.transform.localScale = new Vector3(2, 0.1f, 1);
        var tableMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        tableMat.color = new Color(0.5f, 0.35f, 0.2f);
        table.GetComponent<Renderer>().sharedMaterial = tableMat;

        // Table legs
        for (int i = 0; i < 4; i++)
        {
            var leg = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            leg.name = $"TableLeg_{i}";
            float x = (i % 2 == 0) ? -0.8f : 0.8f;
            float z = (i < 2) ? -0.4f : 0.4f;
            leg.transform.position = new Vector3(x, 0.15f, z);
            leg.transform.localScale = new Vector3(0.05f, 0.3f, 0.05f);
            leg.GetComponent<Renderer>().sharedMaterial = tableMat;
        }
    }

    static void CreateXRRig()
    {
        // XR Origin
        var xrOriginGO = new GameObject("XROrigin");
        var xrOrigin = xrOriginGO.AddComponent<XROrigin>();
        xrOrigin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Floor;

        // Camera
        var cameraGO = new GameObject("Main Camera");
        cameraGO.tag = "MainCamera";
        cameraGO.transform.SetParent(xrOriginGO.transform);
        cameraGO.transform.localPosition = Vector3.zero;
        var camera = cameraGO.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.1f, 0.1f, 0.15f);
        xrOrigin.Camera = camera;

        // TrackedPoseDriver - REQUIRED for XR camera tracking
        var tpdType = Type.GetType("UnityEngine.InputSystem.XR.TrackedPoseDriver, Unity.InputSystem");
        if (tpdType != null)
        {
            var tpd = cameraGO.AddComponent(tpdType) as MonoBehaviour;
            if (tpd != null)
            {
                var trackingTypeField = tpdType.GetField("trackingType");
                if (trackingTypeField != null)
                {
                    var trackingTypeEnum = trackingTypeField.FieldType;
                    var rotationAndPosition = Enum.Parse(trackingTypeEnum, "RotationAndPosition");
                    trackingTypeField.SetValue(tpd, rotationAndPosition);
                }
                var updateTypeField = tpdType.GetField("updateType");
                if (updateTypeField != null)
                {
                    var updateTypeEnum = updateTypeField.FieldType;
                    var updateAndBeforeRender = Enum.Parse(updateTypeEnum, "UpdateAndBeforeRender");
                    updateTypeField.SetValue(tpd, updateAndBeforeRender);
                }
            }
        }

        // Left Controller
        var leftControllerGO = new GameObject("Left Controller");
        leftControllerGO.transform.SetParent(xrOriginGO.transform);
        leftControllerGO.transform.localPosition = new Vector3(-0.3f, 1.2f, 0);
        leftControllerGO.AddComponent<XRDirectInteractor>();

        // Right Controller
        var rightControllerGO = new GameObject("Right Controller");
        rightControllerGO.transform.SetParent(xrOriginGO.transform);
        rightControllerGO.transform.localPosition = new Vector3(0.3f, 1.2f, 0);
        var rightRay = rightControllerGO.AddComponent<XRRayInteractor>();
        rightRay.lineType = XRRayInteractor.LineType.StraightLine;
        rightRay.maxRaycastDistance = 10f;

        // Locomotion - components will resolve references at runtime
        var locomotionGO = new GameObject("Locomotion System");
        locomotionGO.transform.SetParent(xrOriginGO.transform);
        locomotionGO.AddComponent<LocomotionMediator>();
        locomotionGO.AddComponent<TeleportationProvider>();
        var snapTurn = locomotionGO.AddComponent<SnapTurnProvider>();
        snapTurn.turnAmount = 45f;
    }

    static void CreateTeleportationArea()
    {
        var teleportArea = GameObject.CreatePrimitive(PrimitiveType.Plane);
        teleportArea.name = "TeleportationArea";
        teleportArea.transform.position = new Vector3(0, 0.01f, 3);
        teleportArea.transform.localScale = new Vector3(1.5f, 1, 1.5f);
        var teleportMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        teleportMat.color = new Color(0.2f, 0.6f, 1f, 0.5f);
        teleportMat.SetFloat("_Surface", 1);
        teleportMat.SetFloat("_Blend", 0);
        teleportArea.GetComponent<Renderer>().sharedMaterial = teleportMat;
        teleportArea.AddComponent<TeleportationArea>();
    }

    static void CreateRayInteraction()
    {
        // Create a target object for ray interaction (light switch)
        var switchGO = new GameObject("LightSwitch");
        switchGO.transform.position = new Vector3(3, 1.5f, 4.8f);
        var switchCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        switchCube.name = "SwitchButton";
        switchCube.transform.SetParent(switchGO.transform);
        switchCube.transform.localScale = Vector3.one * 0.2f;
        var switchMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        switchMat.color = Color.white;
        switchCube.GetComponent<Renderer>().sharedMaterial = switchMat;

        // Add XR Simple Interactable for the switch
        var switchInteractable = switchGO.AddComponent<XRSimpleInteractable>();
        var rayInteraction = switchGO.AddComponent<RayInteraction>();
        rayInteraction.switchMaterial = switchMat;
    }
}
