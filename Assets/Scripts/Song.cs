using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public Sprite sprite;
    public AudioClip song;
    public List<Sprite> images;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
