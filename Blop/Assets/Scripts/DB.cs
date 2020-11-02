using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour
{
    public static DB instance;
    public   int LevelSayisi=16;
    
    public  int OlunanLevel =1;
    public bool Helper = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Yukle();

    }
    void Start()
    {
        
    }
    public void LevelArti()
    {
        if(OlunanLevel+1<=LevelSayisi)
        {
         OlunanLevel++;
            YuksekSkorGirisiYap();
            YuksekSkorGirisiYap();
        }
    }
    public void Kaydet()
    {
        PlayerPrefs.SetInt("Level", OlunanLevel);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("Level", OlunanLevel);
        PlayerPrefs.Save();
    }
    public void Yukle()
    {
        OlunanLevel= PlayerPrefs.GetInt("Level", OlunanLevel);
        OlunanLevel = PlayerPrefs.GetInt("Level", OlunanLevel);
        OlunanLevel = PlayerPrefs.GetInt("Level", OlunanLevel);
        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void YuksekSkorGirisiYap()
    {
        Application.ExternalCall("kongregate.stats.submit","Level",OlunanLevel);
    }
}
