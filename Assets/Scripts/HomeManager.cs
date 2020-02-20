using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public List<Song> songs;

    public GameObject imageLeft;
    public GameObject imageRight;

    public AudioSource audioSource;

    public int indexInList = 0; //Left image standard

    public GameObject buttonLeft;
    public GameObject buttonRight;

    public GameObject buttonReturnToList;

    private bool playGame = false;

    private Song selectedSong;

    public GameObject spelImage1;
    public GameObject spelImage2;
    public GameObject spelImage3;

    public AudioClip buttonEffectClip;
    public GameObject addContentButton;


    private void CreateScriptableObjectsFromLiedjes()
    {
        //VOOR ELKE MAP MET LIEDJE EEN OBJECT AANMAKEN

        //HOOFDMAP = SONGS

        //CREATE SCRIPTABLE OBJECTAND SAVE
        string[] folders = Directory.GetDirectories(Application.dataPath + "/Resources/Liedjes");

        foreach (var folder in folders)
        {
            if (Directory.Exists(folder))
            {
                StartCoroutine(ProcessDirectory(folder));

                //code uitvoeren zet alles om naar een scriptobject en zet om naar een lijst
            }
        }
        UpdateUI();

    }

    public void LoadAddSongsScene()
    {
        SceneManager.LoadScene("AddContent");
    }


    private IEnumerator ProcessDirectory(string folder)
    {
        Song tempSong = ScriptableObject.CreateInstance<Song>();
        tempSong.images = new List<Sprite>();
        string[] files = Directory.GetFiles(folder);
        foreach (var file in files)
        {
            Debug.Log(file);
            if (file.EndsWith(".mp3"))
            {
                //SONG
                using (var www = new WWW(file))
                {
                    yield return www;
                    tempSong.song = www.GetAudioClip();
                }
            }
            else if (file.EndsWith(".png") || file.EndsWith(".jpg"))
            {
                if (file.ToLower().Contains("menuicon"))
                {
                    //tempSong.sprite = IMG2Sprite.LoadNewSprite(file);
                    Texture2D tempTexture2D = IMG2Sprite.LoadTexture(file);

                    tempSong.sprite = IMG2Sprite.ConvertTextureToSprite(tempTexture2D);
                }
                Texture2D tempTexture2DIcon = IMG2Sprite.LoadTexture(file);
                tempSong.images.Add(IMG2Sprite.ConvertTextureToSprite(tempTexture2DIcon));
            }
        }
        songs.Add(tempSong);
        UpdateUI();
    }



    private void Awake()
    {
        Input.multiTouchEnabled = false;
        //EERST SONGS LIST OPBOUWEN DOOR MAPSTRUCTUUR MET LIEDJES AF TE GAAN
        CreateScriptableObjectsFromLiedjes();
        //CreateScriptableObjectsFromLiedjes();

        //audioSource.clip = songs[0].song;
        Debug.Log("");
        //audioSource.Play();

    }

    void Start()
    {
        //UPDATE UI MET EERSTE 2 LIEDJES UIT LIST



    }

    // Update is called once per frame
    void Update()
    {
        if (playGame)
        {
            StartGame(selectedSong);
            if (!audioSource.isPlaying)
            {
                DisableGameUI();
            }
        }

    }

    private void StartGame(Song selectedSong)
    {
        bool started = false;
        if (started == false)
        {
            //SPELVORM
            spelImage1.transform.localPosition = Vector3.Lerp(new Vector2(-1600, 0), new Vector2(1200, 0), Mathf.PingPong(Time.time * 0.2f, 1.0f));
            spelImage2.transform.localPosition = Vector3.Lerp(new Vector2(1600, 200), new Vector2(-1600, 200), Mathf.PingPong(Time.time * 0.2f, 1.0f));
            spelImage3.transform.localPosition = Vector3.Lerp(new Vector2(0, -1200), new Vector2(0, 1200), Mathf.PingPong(Time.time * 0.05f, 1.0f));


            started = true;
        }
    }

    public void DisableGameUI()
    {
        imageLeft.SetActive(true);
        imageRight.SetActive(true);
        buttonLeft.SetActive(true);
        buttonRight.SetActive(true);
        addContentButton.SetActive(true);
        buttonReturnToList.SetActive(false);
        playGame = false;
        spelImage1.SetActive(false);
        spelImage2.SetActive(false);
        spelImage3.SetActive(false);
    }

    public void EnableGameUI()
    {
        imageLeft.SetActive(false);
        imageRight.SetActive(false);
        buttonLeft.SetActive(false);
        buttonRight.SetActive(false);
        addContentButton.SetActive(false);
        buttonReturnToList.SetActive(true);
        spelImage1.SetActive(true);
        spelImage2.SetActive(true);
        spelImage3.SetActive(true);
        playGame = true;
        spelImage1.GetComponent<Image>().sprite = selectedSong.images[0];
        spelImage2.GetComponent<Image>().sprite = selectedSong.images[1];
        spelImage3.GetComponent<Image>().sprite = selectedSong.images[2];
    }

    public void PlaySongLeft()
    {
        Debug.Log("Play Left");

        PlaySound(songs[indexInList].song);
        selectedSong = songs[indexInList];
        EnableGameUI();
    }

    public void PlaySongRight()
    {
        Debug.Log("Play Right");
        PlaySound(songs[indexInList + 1].song);
        selectedSong = songs[indexInList + 1];
        EnableGameUI();

    }


    public void PlaySound(AudioClip clipSong)
    {
        audioSource.clip = clipSong;
        //Debug.Log("Playing " + clipSong.name);
        audioSource.Play();
    }

    public void UpdateUI()
    {
        if (songs.Count > 1)
        {
            GameObject.FindGameObjectWithTag("Image1").GetComponent<Image>().sprite = songs[indexInList].sprite;


            GameObject.FindGameObjectWithTag("Image2").GetComponent<Image>().sprite = songs[indexInList + 1].sprite;
        }
    }


    public int GetLeftPositionInSongsList(Song currentLeftSong)
    {
        return songs.FindIndex(song => song.song == currentLeftSong.song);
    }

    public void GoLeftInList()
    {
        audioSource.clip = buttonEffectClip;
        audioSource.Play();
        Debug.Log("Go left in list");
        if (indexInList == 0 || indexInList == 1)
        {
            //EVEN en index 0
            indexInList = songs.Count - 2;
            UpdateUI();
        }
        else
        {

            indexInList -= 2;
            UpdateUI();
        }
    }

    public void ReturnToList()
    {
        DisableGameUI();
        audioSource.Stop();
    }

    public void GoRightInList()
    {
        audioSource.clip = buttonEffectClip;
        audioSource.Play();
        Debug.Log("Go right in list");
        if (indexInList == songs.Count - 1 || indexInList == songs.Count - 2)
        {
            indexInList = 0;
            UpdateUI();
        }
        else
        {

            indexInList += 2;
            UpdateUI();
        }
    }

}
