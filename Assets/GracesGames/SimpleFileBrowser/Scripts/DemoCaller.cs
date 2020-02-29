using System;
using UnityEngine;
using UnityEngine.UI;

// Include these namespaces to use BinaryFormatter
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GracesGames.SimpleFileBrowser.Scripts
{
    // Demo class to illustrate the usage of the FileBrowser script
    // Able to save and load files containing serialized data (e.g. text)
    public class DemoCaller : MonoBehaviour
    {
        public ToasterScript toaster = new ToasterScript();
        string[] temporaryFilesPath = new string[4];
        string[] stringArray = new string[4];

        // Use the file browser prefab
        private string homeFolder;

        public GameObject FileBrowserPrefab;

        // Define a file extension
        public string[] FileExtensions;

        // Input field to get text to save
        private GameObject _textToSaveInputField;

        // Label to display loaded text
        private GameObject _loadedText;

        // Variable to save intermediate input result
        private string _textToSave;

        public bool PortraitMode;

        public Button saveButton;

        public int selectedImage;
        private void Awake()
        {
            homeFolder = Application.dataPath + "/Resources/Liedjes";
        }

        // Find the input field, label objects and add a onValueChanged listener to the input field
        private void Start()
        {

            _loadedText = GameObject.Find("LoadedText");

            GameObject uiCanvas = GameObject.Find("Canvas");
            if (uiCanvas == null)
            {
                Debug.LogError("Make sure there is a canvas GameObject present in the Hierarcy (Create UI/Canvas)");
            }
        }

        private void Update()
        {
            if ((!temporaryFilesPath[0].Equals("") && !temporaryFilesPath[1].Equals("") && !temporaryFilesPath[2].Equals("") && !temporaryFilesPath[3].Equals("")) && saveButton.interactable == false)
            {
                saveButton.interactable = true;
            }
        }
        // Updates the text to save with the new input (current text in input field)
        public void UpdateTextToSave(string text)
        {
            _textToSave = text;
        }
        public void AddMusic()
        {
            selectedImage = 0;
            OpenFileBrowser();
        }

        public void AddImage1()
        {
            selectedImage = 1;
            OpenFileBrowser();
        }
        public void AddImage2()
        {
            selectedImage = 2;
            OpenFileBrowser();
        }
        public void AddImage3()
        {
            selectedImage = 3;
            OpenFileBrowser();
        }

        // Open the file browser using boolean parameter so it can be called in GUI
        //public void OpenFileBrowser(bool saving)
        //{
        //    OpenFileBrowser(saving ? FileBrowserMode.Save : FileBrowserMode.Load);
        //}

        // Open a file browser to save and load files
        private void OpenFileBrowser()
        {
            // Create the file browser and name it
            GameObject fileBrowserObject = Instantiate(FileBrowserPrefab, transform);
            fileBrowserObject.name = "FileBrowser" + selectedImage;
            // Set the mode to save or load
            FileBrowser fileBrowserScript = fileBrowserObject.GetComponent<FileBrowser>();
            fileBrowserScript.SetupFileBrowser(PortraitMode ? ViewMode.Portrait : ViewMode.Landscape);
            fileBrowserScript.OpenFilePanel(FileExtensions);
            // Subscribe to OnFileSelect event (call LoadFileUsingPath using path) 
            fileBrowserScript.OnFileSelect += LoadFileUsingPath;

        }

        // Saves a file with the textToSave using a path
        public void SaveFiles()
        {
            // Make sure path and _textToSave is not null or empty
            if (!temporaryFilesPath[0].Equals("") || !temporaryFilesPath[1].Equals("") || !temporaryFilesPath[2].Equals("") || !temporaryFilesPath[3].Equals(""))
            {
                Guid random = Guid.NewGuid();
                string folder = homeFolder + "/" + random.ToString();
                System.IO.Directory.CreateDirectory(folder);
                for (int i = 0; i < temporaryFilesPath.Length; i++)
                {

                    char[] del = { '/', '\\' };
                    string[] file = temporaryFilesPath[i].Split(del);
                    if (i == 1)
                    {
                        File.Copy(temporaryFilesPath[i], folder + "/menuicon" + file[file.Length - 1]);
                    }
                    else
                    {
                        File.Copy(temporaryFilesPath[i], folder + '/' + selectedImage + file[file.Length - 1]);
                    }
                }
                //GEDAAN MET SAVEN CLEAR LIST
                stringArray = new string[4];
                temporaryFilesPath = new string[4];
                _loadedText.GetComponent<Text>().text = "Muziek: " + stringArray[0] + " \nMenuItem: " + stringArray[1] + " \nAfbeelding 1: " + stringArray[2] + " \nAfbeelding 2: " + stringArray[3];
                toaster.showToast("Nieuw lied is toegevoegd!", 2);
                saveButton.interactable = false;
            }
            else
            {
                Debug.Log("Invalid path or empty file given");
            }
        }

        // Loads a file using a path
        private void LoadFileUsingPath(string path)
        {
            if (path.Length != 0)
            {


                temporaryFilesPath[selectedImage] = path;


                // Convert the file from a byte array into a string
                //string fileData = bFormatter.Deserialize(file) as string;
                // We're done working with the file so we can close it

                // Set the LoadedText with the value of the file=



                char[] del = { '/', '\\' };
                string[] splitFile = temporaryFilesPath[selectedImage].Split(del);
                stringArray[selectedImage] = splitFile[splitFile.Length - 1];




                _loadedText.GetComponent<Text>().text = "Muziek: " + stringArray[0] + " \nMenuItem: " + stringArray[1] + " \nAfbeelding 1: " + stringArray[2] + " \nAfbeelding 2: " + stringArray[3];
                
            }
            else
            {
                Debug.Log("Invalid path given");
            }
        }


    }
}