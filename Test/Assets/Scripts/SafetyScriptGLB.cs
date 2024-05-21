using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityGLTF;

public class SafetyScriptGLB : MonoBehaviour
{
    [SerializeField] string exportFileName = "ExportedComposition.glb";
    [SerializeField] Transform[] compositions; // Reference to the parent objects containing the 3D compositions

    //[System.Obsolete]
    public void ExportComposition()
    {
        if (compositions == null || compositions.Length == 0)
        {
            Debug.LogWarning("Compositions not assigned.");
            return;
        }

        // Get the full file path for the export
        string filePath = GetExportFilePath();

        // Ensure that the composition is active before exporting
        //composition.SetActive(true);

        // Export the composition to GLB
        ExportGLB(filePath, compositions);

        // Restore the original state of the composition
        //composition.SetActive(false);

        Debug.Log("Exported composition to: " + filePath);
        /*
        foreach (GameObject composition in compositions)
        {
            if (composition == null)
            {
                Debug.LogWarning("One of the compositions is not assigned.");
                continue;
            }

            // Get the full file path for the export
            string filePath = GetExportFilePath();

            // Ensure that the composition is active before exporting
            //composition.SetActive(true);

            // Export the composition to GLB
            ExportGLB(filePath, composition);

            // Restore the original state of the composition
            composition.SetActive(false);

            Debug.Log("Exported composition to: " + filePath);
        }*/
    }

    private string GetExportFilePath()
    {
        // Combine the export file name with the persistent data path
        return Path.Combine(Application.persistentDataPath, exportFileName);
    }

    private void ExportGLB(string filePath, Transform[] compositions)
    {
        // Create a new exporter instance
        var options = new ExportContext();
        var exporter = new GLTFSceneExporter(compositions, options);

        // Export the composition to GLB
        //exporter.SaveGLTFandBin(filePath, null); // Provide null for the extrasFileName parameter
        exporter.SaveGLTFandBin(filePath, "tst", true); // Provide null for the extrasFileName parameter
    }

    /*
    private void ExportGLB(string filePath, Transform[] compositionToBeExported)
    {
        // Create a new exporter instance
        var options = new ExportContext();
        var exporter = new GLTFSceneExporter(compositionToBeExported, options);

        // Export the composition to GLB
        //exporter.SaveGLTFandBin(filePath, null); // Provide null for the extrasFileName parameter

        // this works!!!! exporter.SaveGLTFandBin(filePath, "tst", true); // Provide null for the extrasFileName parameter

        // Export the scene to GLB format
        exporter.SaveGLB(filePath, _exportFileName);
        /*{
            Debug.Log("GLB Exported Successfully: " + filePath);
        });

    // Export the composition to GLB
    // GLTFSceneExporter.Export(filePath, compositionToBeExported, options);

}
*/
}

/* v3
using System.IO;
using UnityEngine;
using UnityGLTF;

public class GLBExporter : MonoBehaviour
{
    [SerializeField] string exportFileName = "ExportedComposition.glb";
    [SerializeField] GameObject compositionParent; // Reference to the parent object containing the 3D composition

    public void ExportComposition()
    {
        string filePath = Path.Combine(Application.persistentDataPath, exportFileName);

        if (compositionParent == null)
        {
            Debug.LogWarning("Composition parent not assigned.");
            return;
        }

        // Ensure that the compositionParent is active before exporting
        compositionParent.SetActive(true);

        // Export the composition to GLB
        ExportGLB(filePath, compositionParent);

        // Restore the original state of the compositionParent
        compositionParent.SetActive(false);

        Debug.Log("Exported composition to: " + filePath);
    }

    private void ExportGLB(string filePath, GameObject composition)
    {
        // Create a new exporter instance
        var options = new ExportOptions();
        var exporter = new GLTFSceneExporter(new Transform[] { composition.transform }, RetrieveTexturePath);

        // Export the composition to GLB
        exporter.SaveGLTFandBin(filePath, null); // Provide null for the extrasFileName parameter
    }

    // RetrieveTexturePath method used as a placeholder delegate
    private string RetrieveTexturePath(Texture2D texture)
    {
        // Implement your logic to retrieve texture paths here
        return "";
    }
}*/

//v 2
/*using System.IO;
using UnityEngine;
using UnityGLTF;

public class GLBExporter : MonoBehaviour
{
    [SerializeField] string exportFileName = "ExportedComposition.glb";
    [SerializeField] Transform compositionParent; // Reference to the parent object containing the 3D composition

    public void ExportComposition()
    {
        string filePath = Path.Combine(Application.persistentDataPath, exportFileName);

        if (compositionParent == null)
        {
            Debug.LogWarning("Composition parent not assigned.");
            return;
        }

        // Ensure that the compositionParent is active before exporting
        compositionParent.gameObject.SetActive(true);

        // Export the composition to GLB
        ExportGLB(filePath, compositionParent.gameObject);

        // Restore the original state of the compositionParent
        compositionParent.gameObject.SetActive(false);

        Debug.Log("Exported composition to: " + filePath);
    }

    private void ExportGLB(string filePath, GameObject composition)
    {
        // Create a new exporter instance
        GLTFSceneExporter exporter = new GLTFSceneExporter();

        // Export the composition to GLB
        exporter.SaveGLTFandBin(filePath, null); // Provide null for the extrasFileName parameter
    }
}*/


// v1
/*using System.IO;
using UnityEngine;
using UnityEditor;

public class GLBExporter : MonoBehaviour
{
    [SerializeField] string exportFileName = "ExportedComposition.glb";
    [SerializeField] Transform compositionParent; // Reference to the parent object containing the 3D composition

    public void ExportComposition()
    {
        string filePath = Path.Combine(Application.persistentDataPath, exportFileName);

        if (compositionParent == null)
        {
            Debug.LogWarning("Composition parent not assigned.");
            return;
        }

        // Ensure that the compositionParent is active before exporting
        compositionParent.gameObject.SetActive(true);

        // Export the composition to GLB
        ExportGLB(filePath, compositionParent.gameObject);

        // Restore the original state of the compositionParent
        compositionParent.gameObject.SetActive(false);

        Debug.Log("Exported composition to: " + filePath);
    }

    private void ExportGLB(string filePath, GameObject composition)
    {
        // Export the composition to GLTF
        GLTFUtility.ExportGLTF(composition, filePath, GLTFUtility.GLTFVersion.GLTF_2_0, true);

        // Convert the GLTF file to GLB
        GLTFUtility.ConvertGLTFToGLB(filePath, true);
    }
}*/