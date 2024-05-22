using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotExporter : MonoBehaviour
{
    [Header("Screenshot Exporter Setup:")]
    [SerializeField] Camera _camera; // Assign your camera in the Inspector
    [SerializeField] string _exportFilePath = "PersistentDataPath"; // Folder path to save screenshots
    [SerializeField] GameObject[] _arrayOfUnits; // add all units to have pictures taken here:

    [Space(10)]
    //[SerializeField] string _unitName = "[Unit Name]_Screenshot_RawIcon";
    [SerializeField] Transform _camPosition;
    //[SerializeField] Transform _targetUnit;

    string _nameExtension = "_Unit_Screenshot";
    string _fileFormat = ".png";
    string _exportFolderName = "Unit Screenshots";


    /// <summary>
    /// Loop through the array of units and take screenshots.
    /// </summary>
    public void TakeScreenshots()
    {
        StartCoroutine(CaptureScreenshotsWithDelay());
    }

    private IEnumerator CaptureScreenshotsWithDelay()
    {
        for (int i = 0; i < _arrayOfUnits.Length; i++) // double check if the last unit was pictured
        {
            GameObject _currentUnit = _arrayOfUnits[i];
            _currentUnit.SetActive(true);

            TakePicture(_currentUnit, i);

            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
            _currentUnit.SetActive(false);

        }
    }

    /// <summary>
    /// Take a picture of the active unit.
    /// </summary>
    void TakePicture(GameObject _currentUnit, int _unitIndex)
    {
        // create screenshot folder if it doesn't exist yet:
        string exportFolderPath = GetExportFilePath();
        if (!Directory.Exists(exportFolderPath))
        {
            Directory.CreateDirectory(exportFolderPath);
        }

        if(_camPosition != null)
        {
            _camera.transform.position = _camPosition.position; // using set pos so i can move the camera later:
        }

        // focus on core of unit:
        _camera.transform.LookAt(_currentUnit.transform);

        string _compiledFileName =  _currentUnit.GetComponent<UnitManager>().unitName + _nameExtension + _fileFormat;

        _compiledFileName = GetExportFilePath() + "/" + _compiledFileName;
        Debug.Log(_compiledFileName.ToString());
        ScreenCapture.CaptureScreenshot(_compiledFileName);

        Debug.Log("screenshot taken of unit: " + _currentUnit.name + ", index: " + _unitIndex + 
            ". File-name: " + _compiledFileName, _currentUnit);
    }

    /// <summary>
    /// Save the export to desired location, if none assigned, use persistentDataPath as default.
    /// </summary>
    /// <returns></returns>
    private string GetExportFilePath()
    {
        if (_exportFilePath == "PersistentDataPath") // no custom path assigned:
        {
            return Application.persistentDataPath + "/" + _exportFolderName;
        }
        else // custom path assigned - use this instead:
        {
            return _exportFilePath + "/" + _exportFolderName;
        }
    }
}