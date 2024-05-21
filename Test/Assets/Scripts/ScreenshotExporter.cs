using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotExporter : MonoBehaviour
{
    #region Array or not?
    /*
     * Opted agains an array of camera positions to have better readability of which camera position is where.
     * When using more than the four given positions and/or targets I would recommend switching to an array setup and then
     * looping through the angles and targets, allowing for any number of screenshot-angles.
    */
    #endregion

    [SerializeField] Camera _screenshotCamera; // Assign your camera in the Inspector
    [SerializeField] string _exportFilePath = "PersistentDataPath"; // Folder path to save screenshots

    [Space(10)]
    [SerializeField] string _northScreenshotName = "Spielplatz Screenshot (Nord nach Süd)";
    [SerializeField] Transform _camNorth;
    [SerializeField] Transform _camNorthTarget;

    [Space(10)]
    [SerializeField] string _eastScreenshotName = "Spielplatz Screenshot (Ost nach West)";
    [SerializeField] Transform _camEast;
    [SerializeField] Transform _camEastTarget;

    [Space(10)]
    [SerializeField] string _southScreenshotName = "Spielplatz Screenshot (Süd nach Nord)";
    [SerializeField] Transform _camSouth;
    [SerializeField] Transform _camSouthTarget;

    [Space(10)]
    [SerializeField] string _westScreenshotName = "Spielplatz Screenshot (West nach Ost)";
    [SerializeField] Transform _camWest;
    [SerializeField] Transform _camWestTarget;

    string _fileFormat = ".png";
    string _exportFolderName = "Screenshots";

    /// <summary>
    /// Capture screenshots from all specified camera positions.
    /// </summary>
    public void ExportScreenshots()
    {
        StartCoroutine(CaptureScreenshotsWithDelay());
    }

    private IEnumerator CaptureScreenshotsWithDelay()
    {
        for (int i = 0; i < 4; i++)
        {
            CaptureScreenshot(i);
            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        }
    }

    void CaptureScreenshot(int index)
    {
        string screenshotName = "-";

        string exportFolderPath = GetExportFilePath();
        if (!Directory.Exists(exportFolderPath))
        {
            Directory.CreateDirectory(exportFolderPath);
        }

        string compiledFileName;

        // set camera position and target:
        switch (index)
        {
            case 0:
                _screenshotCamera.transform.position = _camNorth.position;
                _screenshotCamera.transform.LookAt(_camNorthTarget);
                screenshotName = _northScreenshotName + _fileFormat;

                compiledFileName = GetExportFilePath() + "/" + screenshotName;
                Debug.Log(compiledFileName.ToString());
                ScreenCapture.CaptureScreenshot(compiledFileName);

                Debug.Log("screenshot taken:" + index + " with name " + compiledFileName);
                Debug.Log("camera now at position:" + _screenshotCamera.transform.position);
                return;

            //break;

            case 1:
                _screenshotCamera.transform.position = _camEast.position;
                _screenshotCamera.transform.LookAt(_camEastTarget);
                screenshotName = _eastScreenshotName + _fileFormat;

                compiledFileName = GetExportFilePath() + "/" + screenshotName;
                Debug.Log(compiledFileName.ToString());
                ScreenCapture.CaptureScreenshot(compiledFileName);
                Debug.Log("screenshot taken:" + index + " with name " + compiledFileName);
                Debug.Log("camera now at position:" + _screenshotCamera.transform.position);
                return;

            //break;

            case 2:
                _screenshotCamera.transform.position = _camSouth.position;
                _screenshotCamera.transform.LookAt(_camSouthTarget);
                screenshotName = _southScreenshotName + _fileFormat;

                compiledFileName = GetExportFilePath() + "/" + screenshotName;
                Debug.Log(compiledFileName.ToString());
                ScreenCapture.CaptureScreenshot(compiledFileName);
                Debug.Log("screenshot taken:" + index + " with name " + compiledFileName);
                Debug.Log("camera now at position:" + _screenshotCamera.transform.position);
                return;

            //break;

            case 3:
                _screenshotCamera.transform.position = _camWest.position;
                _screenshotCamera.transform.LookAt(_camWestTarget);
                screenshotName = _westScreenshotName + _fileFormat;

                compiledFileName = GetExportFilePath() + "/" + screenshotName;
                Debug.Log(compiledFileName.ToString());
                ScreenCapture.CaptureScreenshot(compiledFileName);
                Debug.Log("screenshot taken:" + index + " with name " + compiledFileName);
                Debug.Log("camera now at position:" + _screenshotCamera.transform.position);
                return;

            //break;

            default:
                Debug.LogWarning("Higher number of camera angles set thatn are actually assigned.");
                return;
        }

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