using GracesGames.SimpleFileBrowser.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSongsManager : MonoBehaviour
{
    FileBrowser fileBrowser;
    void Awake()
    {
        fileBrowser = GetComponent<FileBrowser>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    //// Open a file browser to save and load files
    //private void OpenFileBrowser(FileBrowserMode fileBrowserMode)
    //{
    //    // Create the file browser and name it
    //    GameObject fileBrowserObject = Instantiate(FileBrowserPrefab, transform);
    //    fileBrowserObject.name = "FileBrowser";
    //    // Set the mode to save or load
    //    FileBrowser fileBrowserScript = fileBrowserObject.GetComponent<FileBrowser>();
    //    fileBrowserScript.SetupFileBrowser(PortraitMode ? ViewMode.Portrait : ViewMode.Landscape);
    //    if (fileBrowserMode == FileBrowserMode.Save)
    //    {
    //        fileBrowserScript.SaveFilePanel("DemoText", FileExtensions);
    //        // Subscribe to OnFileSelect event (call SaveFileUsingPath using path) 
    //        fileBrowserScript.OnFileSelect += SaveFileUsingPath;
    //    }
    //    else
    //    {
    //        fileBrowserScript.OpenFilePanel(FileExtensions);
    //        // Subscribe to OnFileSelect event (call LoadFileUsingPath using path) 
    //        fileBrowserScript.OnFileSelect += LoadFileUsingPath;
    //    }
    //}
}
