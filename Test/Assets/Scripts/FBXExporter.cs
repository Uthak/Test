using Autodesk.Fbx;
using UnityEngine;
using UnityEditor;

public class FBXExporter : MonoBehaviour
{
    [SerializeField] string exportFileName = "ExportedScene.fbx";
    [SerializeField] Transform compositionGeometryParent; // Reference to the parent object containing all scene geometry

    public void ExporterButton()
    {
       // FbxExporter.;
    }

    public void ExportScene()
    {
        string filePath = Application.persistentDataPath + "/" + exportFileName;

        using (FbxManager fbxManager = FbxManager.Create())
        {
            // Configure IO settings.
            fbxManager.SetIOSettings(FbxIOSettings.Create(fbxManager, Globals.IOSROOT));

            // Export the scene
            using (FbxExporter exporter = FbxExporter.Create(fbxManager, "myExporter"))
            {
                // Initialize the exporter.
                bool status = exporter.Initialize(filePath, -1, fbxManager.GetIOSettings());

                if (!status)
                {
                    Debug.LogError("Failed to initialize FBX exporter.");
                    return;
                }

                // Create a new scene to export
                FbxScene scene = FbxScene.Create(fbxManager, "myScene");
                //FbxScene scene = this.FbxScene;

                // Export the scene geometry parent and its children
                //ExportGameObject(sceneGeometryParent.gameObject, scene);
                //ExportComposition(compositionGeometryParent.gameObject, scene);

                // Export the scene to the file.
                exporter.Export(scene);
            }
        }

        Debug.Log("Exported scene to: " + filePath);
    }


    // this works, but export is empty:
    private void ExportComposition(string fileName)
    {
        using (FbxManager fbxManager = FbxManager.Create())
        {
            // configure IO settings.
            fbxManager.SetIOSettings(FbxIOSettings.Create(fbxManager, Globals.IOSROOT));

            // Export the scene
            using (FbxExporter exporter = FbxExporter.Create(fbxManager, "myExporter"))
            {

                // Initialize the exporter.
                bool status = exporter.Initialize(fileName, -1, fbxManager.GetIOSettings());

                // Create a new scene to export
                FbxScene scene = FbxScene.Create(fbxManager, "myScene");

                // Export the scene to the file.
                exporter.Export(scene);
            }
        }
    }

    /* // Testing:
    public void ExportScene()
    {
        string filePath = Application.persistentDataPath + "/" + exportFileName;

        using (FbxManager fbxManager = FbxManager.Create())
        {
            // Configure IO settings.
            fbxManager.SetIOSettings(FbxIOSettings.Create(fbxManager, Globals.IOSROOT));

            // Export the scene
            using (FbxExporter exporter = FbxExporter.Create(fbxManager, "myExporter"))
            {
                // Initialize the exporter.
                bool status = exporter.Initialize(filePath, -1, fbxManager.GetIOSettings());

                if (!status)
                {
                    Debug.LogError("Failed to initialize FBX exporter.");
                    return;
                }

                // Create a new scene to export
                FbxScene scene = FbxScene.Create(fbxManager, "myScene");

                // Export the scene geometry parent and its children
                //ExportGameObject(sceneGeometryParent.gameObject, scene);
                ExportComposition(sceneGeometryParent.gameObject, scene);

                // Export the scene to the file.
                exporter.Export(scene);
            }
        }

        Debug.Log("Exported scene to: " + filePath);
    }


    // this works, but export is empty:
    private void ExportComposition(string fileName)
    {
        using (FbxManager fbxManager = FbxManager.Create())
        {
            // configure IO settings.
            fbxManager.SetIOSettings(FbxIOSettings.Create(fbxManager, Globals.IOSROOT));

            // Export the scene
            using (FbxExporter exporter = FbxExporter.Create(fbxManager, "myExporter"))
            {

                // Initialize the exporter.
                bool status = exporter.Initialize(fileName, -1, fbxManager.GetIOSettings());

                // Create a new scene to export
                FbxScene scene = FbxScene.Create(fbxManager, "myScene");

                // Export the scene to the file.
                exporter.Export(scene);
            }
        }
    }*/

    /* // THis works, but exports are empty!
    public class FBXExporter : MonoBehaviour
    {
        public string exportFileName = "ExportedScene.fbx";
        public Transform sceneGeometryParent; // Reference to the parent object containing all scene geometry

        public void ExportScene()
        {
            string filePath = Application.persistentDataPath + "/" + exportFileName;

            using (FbxManager fbxManager = FbxManager.Create())
            {
                // Configure IO settings.
                fbxManager.SetIOSettings(FbxIOSettings.Create(fbxManager, Globals.IOSROOT));

                // Export the scene
                using (FbxExporter exporter = FbxExporter.Create(fbxManager, "myExporter"))
                {
                    // Initialize the exporter.
                    bool status = exporter.Initialize(filePath, -1, fbxManager.GetIOSettings());

                    if (!status)
                    {
                        Debug.LogError("Failed to initialize FBX exporter.");
                        return;
                    }

                    // Create a new scene to export
                    FbxScene scene = FbxScene.Create(fbxManager, "myScene");

                    // Export the scene geometry parent and its children
                    ExportGameObject(sceneGeometryParent.gameObject, scene);

                    // Export the scene to the file.
                    exporter.Export(scene);
                }
            }

            Debug.Log("Exported scene to: " + filePath);
        }


        // this works, but export is empty:
        private void ExportGameObject(GameObject go, FbxScene scene)
        {
            // Create an FbxNode for the GameObject
            FbxNode node = FbxNode.Create(scene, go.name);

            // Get the translation, rotation, and scale from the GameObject's transform
            Vector3 position = go.transform.position;
            Quaternion rotation = go.transform.rotation;
            Vector3 scale = go.transform.localScale;

            // Convert the rotation to a quaternion
            FbxQuaternion fbxRotation = new FbxQuaternion(rotation.x, rotation.y, rotation.z, rotation.w);

            // Create transformation matrix using constructor
            FbxMatrix matrix = new FbxMatrix(new FbxVector4(position.x, position.y, position.z), fbxRotation, new FbxVector4(scale.x, scale.y, scale.z));

            // Set the transformation matrix for the node
            node.LclTranslation.Set(new FbxDouble3(position.x, position.y, position.z));
            node.LclRotation.Set(new FbxDouble3(rotation.x, rotation.y, rotation.z));
            node.LclScaling.Set(new FbxDouble3(scale.x, scale.y, scale.z));

            // Add node to the scene
            scene.GetRootNode().AddChild(node);

            // Add components, materials, etc. to the node as needed
            // (You may need to iterate through components and add appropriate FBX elements)

            // Recursively export children
            foreach (Transform child in go.transform)
            {
                ExportGameObject(child.gameObject, scene);
            }
        }*/

    /*
    private void ExportGameObject(GameObject go, FbxScene scene)
    {
        // Create an FbxNode for the GameObject
        FbxNode node = FbxNode.Create(scene, go.name);

        // Get the translation, rotation, and scale from the GameObject's transform
        Vector3 position = go.transform.position;
        Quaternion rotation = go.transform.rotation;
        Vector3 scale = go.transform.localScale;

        // Convert the rotation to a quaternion
        FbxQuaternion fbxRotation = new FbxQuaternion(rotation.x, rotation.y, rotation.z, rotation.w);

        // Set transform using the correct types
        FbxMatrix matrix = new FbxMatrix();
        matrix.SetTRS(new FbxVector4(position.x, position.y, position.z), fbxRotation, new FbxVector4(scale.x, scale.y, scale.z));

        // Set the transformation matrix for the node
        node.LclTranslation.Set(new FbxDouble3(position.x, position.y, position.z));
        node.LclRotation.Set(new FbxDouble3(rotation.x, rotation.y, rotation.z));
        node.LclScaling.Set(new FbxDouble3(scale.x, scale.y, scale.z));

        // Add node to the scene
        scene.GetRootNode().AddChild(node);

        // Add components, materials, etc. to the node as needed
        // (You may need to iterate through components and add appropriate FBX elements)

        // Recursively export children
        foreach (Transform child in go.transform)
        {
            ExportGameObject(child.gameObject, scene);
        }
    }*/

    /*
    private void ExportGameObject(GameObject go, FbxScene scene)
    {
        // Create an FbxNode for the GameObject
        FbxNode node = FbxNode.Create(scene, go.name);

        // Get the translation, rotation, and scale from the GameObject's transform
        Vector3 position = go.transform.position;
        Quaternion rotation = go.transform.rotation;
        Vector3 scale = go.transform.localScale;

        // Convert the rotation to a quaternion
        FbxQuaternion fbxRotation = new FbxQuaternion(rotation.x, rotation.y, rotation.z, rotation.w);

        // Set transform using the correct types
        FbxMatrix matrix = new FbxMatrix();
        matrix.Set(new FbxVector4(position.x, position.y, position.z)); // Set translation
        matrix.SetR(fbxRotation); // Set rotation
        matrix.SetS(new FbxVector4(scale.x, scale.y, scale.z)); // Set scale

        // Set the transform of the node
        node.LclTranslation.Set(new FbxDouble3(position.x, position.y, position.z));
        node.LclRotation.Set(new FbxDouble3(rotation.x, rotation.y, rotation.z));
        node.LclScaling.Set(new FbxDouble3(scale.x, scale.y, scale.z));

        // Add node to the scene
        scene.GetRootNode().AddChild(node);

        // Add components, materials, etc. to the node as needed
        // (You may need to iterate through components and add appropriate FBX elements)

        // Recursively export children
        foreach (Transform child in go.transform)
        {
            ExportGameObject(child.gameObject, scene);
        }
    }*/

    /*
    private void ExportGameObject(GameObject go, FbxScene scene)
    {
        // Create an FbxNode for the GameObject
        FbxNode node = FbxNode.Create(scene, go.name);

        // Set transform
        FbxMatrix matrix = new FbxMatrix();
        matrix.SetTRS(go.transform.position, go.transform.rotation, go.transform.localScale);
        node.EvaluateGlobalTransform();

        // Add node to the scene
        scene.GetRootNode().AddChild(node);

        // Add components, materials, etc. to the node as needed
        // (You may need to iterate through components and add appropriate FBX elements)

        // Recursively export children
        foreach (Transform child in go.transform)
        {
            ExportGameObject(child.gameObject, scene);
        }
    }*/
}


/* // This version was attempting to export each obj seperately
public class FBXExporter : MonoBehaviour
{
    public string exportFileName = "ExportedScene.fbx";

    public void ExportScene()
    {
        string filePath = Application.persistentDataPath + "/" + exportFileName;

        using (FbxManager fbxManager = FbxManager.Create())
        {
            // Configure IO settings.
            fbxManager.SetIOSettings(FbxIOSettings.Create(fbxManager, Globals.IOSROOT));

            // Export the scene
            using (FbxExporter exporter = FbxExporter.Create(fbxManager, "myExporter"))
            {
                // Initialize the exporter.
                bool status = exporter.Initialize(filePath, -1, fbxManager.GetIOSettings());

                if (!status)
                {
                    Debug.LogError("Failed to initialize FBX exporter.");
                    return;
                }

                // Create a new scene to export
                FbxScene scene = FbxScene.Create(fbxManager, "myScene");

                // Add GameObjects to the scene (for example, the current scene's root GameObject)
                GameObject[] gameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                foreach (GameObject go in gameObjects)
                {
                    ExportGameObject(go, scene);
                }

                // Export the scene to the file.
                exporter.Export(scene);
            }
        }

        Debug.Log("Exported scene to: " + filePath);
    }

    private void ExportGameObject(GameObject go, FbxScene scene)
    {
        // Create an FbxNode for the GameObject
        FbxNode node = FbxNode.Create(scene, go.name);

        // Set transform
        FbxMatrix matrix = new FbxMatrix();
        matrix.SetTRS(go.transform.position, go.transform.rotation, go.transform.localScale);
        node.EvaluateGlobalTransform();

        // Add node to the scene
        scene.GetRootNode().AddChild(node);

        // Add components, materials, etc. to the node as needed
        // (You may need to iterate through components and add appropriate FBX elements)
    }
}*/






/* // base version from docutmentationb
protected void ExportScene(string fileName)
{
    using (FbxManager fbxManager = FbxManager.Create())
    {
        // configure IO settings.
        fbxManager.SetIOSettings(FbxIOSettings.Create(fbxManager, Globals.IOSROOT));

        // Export the scene
        using (FbxExporter exporter = FbxExporter.Create(fbxManager, "myExporter"))
        {

            // Initialize the exporter.
            bool status = exporter.Initialize(fileName, -1, fbxManager.GetIOSettings());

            // Create a new scene to export
            FbxScene scene = FbxScene.Create(fbxManager, "myScene");

            // Export the scene to the file.
            exporter.Export(scene);
        }
    }
}*/



/* // first attempts
public class FBXExporter : MonoBehaviour
{
    #region References:
    // for GLTF export:
    // https://docs.unity3d.com/Packages/com.unity.cloud.gltfast@5.2/manual/ExportRuntime.html

    // for FBX export in runtime:
    // https://docs.unity3d.com/Packages/com.unity.formats.fbx@2.0/manual/devguide.html
    // note: test on Android as documentation says Win/MacOS only

https://docs.unity3d.com/Packages/com.autodesk.fbx@4.0/api/Autodesk.Fbx.FbxMatrix.html
    #endregion

    public static string exportFolder = "Assets/ExportedModels";
    public static string modelName = "ExportedModel";

    public static void ExportToFBX()
    {
        // Create export folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(exportFolder))
        {
            AssetDatabase.CreateFolder("Assets", "ExportedModels");
        }

        // Set export path
        string filePath = exportFolder + "/" + modelName + ".fbx";

        // Export scene to FBX
        bool success = UnityEditor.FbxExporter.ExportGameObjects(filePath, Selection.gameObjects, false);

        if (success)
        {
            Debug.Log("Exported FBX successfully: " + filePath);
        }
        else
        {
            Debug.LogError("Failed to export FBX");
        }
    }
}*/
