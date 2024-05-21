using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityGLTF;
using GLTF;
using GLTF.Schema;

/// <summary>
/// Allows exporting a designated GO, and it's children to be exported as GLB. Make sure this script is attached to an Object in the active scene.
/// </summary>
public class GLBExporter : MonoBehaviour
{
    [Header("GLB Export Setup:")]

    [Tooltip("Default: persistentDataPath. You may set up a desired file path to export to instead.")]
    [SerializeField] string _exportFilePath = "PersistentDataPath";

    [Tooltip("This is the name of the exported GLB in your assigned save-files directory.")]
    [SerializeField] string _exportFileName = "3D Spielplatz Modell";

    [Tooltip("This ID can be assigned to clearly mark to which device this model belongs.")]
    public int deviceID = 0;

    [Tooltip("Set the parent of the geometry you want to export as 3D composition to GLB here. Drag and Drop from hierarchy")]
    [SerializeField] Transform[] _compositionToBeExported; // needs to be array for the GLTF exporter:

    [Tooltip("Report when an export was completed.")]
    [SerializeField] bool _reportExportComplete = true;


    string _exportFileFormat = ".glb";
    string _exportFolderName = "3D Komposition";

    /// <summary>
    /// Call this function to export the desired 3D composition to GLB.
    /// </summary>
    public void ExportComposition()
    {
        // ensure that a composition is assigned:
        if (_compositionToBeExported == null || _compositionToBeExported.Length == 0)
        {
            Debug.LogError("No 3D-composition assigned to be exported!", this);
            return;
        }

        // get the full file path for the export:
        string filePath = GetExportFilePath();

        // export the composition to GLB:
        ExportGLB(filePath, _compositionToBeExported);

        // if desired report success and location of file:
        if (_reportExportComplete)
        {
            Debug.Log("GLB-export complete. file path: " + filePath + "/" + _exportFolderName);
        }
    }

    /// <summary>
    /// Save the export to desired location, if none assigned, use persistentDataPath as default.
    /// </summary>
    /// <returns></returns>
    private string GetExportFilePath()
    {
        if(_exportFilePath == "PersistentDataPath") // no custom path assigned:
        {
            return Application.persistentDataPath + "/" + _exportFolderName;
        }else // custom path assigned - use this instead:
        {
            return _exportFilePath + "/" + _exportFolderName;
        }
    }

    /// <summary>
    /// Using the designated path and name the assigned composition will get exported to GLB now. 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="compositionToBeExported"></param>
    private void ExportGLB(string filePath, Transform[] compositionToBeExported)
    {
        // create a new exporter instance:
        var options = new ExportContext();
        var exporter = new GLTFSceneExporter(compositionToBeExported, options);

        // export the composition to GLB:
        exporter.SaveGLB(filePath, _exportFileName + deviceID.ToString() + _exportFileFormat);
    }
}