using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnaMenu : MonoBehaviour
{
    public Button BtnSes;
    public Text TxtBaslik;
    public Color AltinSarisi;
    public Canvas CanvasLevelSecim;
    public Sprite SpriteGecilenLevel;
    public Sprite SpriteKapaliLevel;
    public Sprite SpriteSesAcik,SpriteSesKapali;
    public Sprite[] SpriteEgitim;
    public Text TxtEgitimDurum;
    public Image ImgEgitim;
    public Canvas CanvasEgitim;
    private int EgitimIndex = 0;
    private void Start()
    {
        LevelAtamasi();
        BtnSesIconSecimiBaslangic();
        ImgEgitim.sprite = GetEgitimSprite();
        TxtEgitimDurumGuncelle();
        StartCoroutine("a");

    }
    IEnumerator a()
    {
        yield return new WaitForSeconds(1f);
        MusicManager.instance.GetComponent<AudioSource>().volume = 1;
        if (MusicManager.instance.GetComponent<AudioSource>().isPlaying==false)
        {
            MusicManager.instance.GetComponent<AudioSource>().Play();
        }
        BtnSesClick();
        BtnSesClick();
    }
    public void LevelAtamasi()
    {
        for (int i = 1; i <= DB.instance.LevelSayisi; i++)
        {
            Button b= GameObject.Find("BtnLevel"+i.ToString()).GetComponent<Button>();
            if (i<=DB.instance.OlunanLevel)
            {
                b.gameObject.GetComponent<Image>().sprite = SpriteGecilenLevel;
                b.onClick
           .AddListener(() => BtnLevelClick());

                b.interactable = true;
                
            }
            else
            {
                b.gameObject.GetComponent<Image>().sprite = SpriteKapaliLevel;
                b.gameObject.GetComponent<Image>().color = Color.black;
                b.interactable = false;

            }
        }
        CanvasLevelSecimMenusuKapa();
    }
    public void BtnLevelClick()
    {
        Button b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        int Levelno = System.Convert.ToInt32(b.name.ToString().Replace("BtnLevel", "").ToString());

        SceneManager.LoadScene((Levelno-1)+2);
    }
    public void BtnLevelOnMauseMove()
    {
    
    }
    public void BtnLevelOnMauseExit()
    {
      
    }
    public void OyunBasligiMauseOver()
    {
        TxtBaslik.color = AltinSarisi;
    }
    public void OyunBasligiMauseExit()
    {
        TxtBaslik.color = Color.black;
    }
    public void OyunBasligiMauseClick()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<HexMenu>().Patla();
        }
    }
    public void PlayButtonClick()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<HexMenu>().Patla();
        }
        CanvasLevelSecimMenusuAc();
    }
    public void CanvasLevelSecimMenusuKapa()
    {
        CanvasLevelSecim.gameObject.SetActive(false);
    }
    private void CanvasLevelSecimMenusuAc()
    {
        
        CanvasLevelSecim.gameObject.SetActive(true);
    }
    public void BtnSesClick()
    {
        if (MusicManager.instance.GetMuzikVarmi())
        {
            MusicManager.instance.MuzikSatatuDegis();
            BtnSes.GetComponent<Image>().sprite = SpriteSesKapali;

        }
        else
        {
            MusicManager.instance.MuzikSatatuDegis();
            BtnSes.GetComponent<Image>().sprite = SpriteSesAcik;
        }
    }
    private void BtnSesIconSecimiBaslangic()
    {
        if (MusicManager.instance.GetMuzikVarmi())
        {
            BtnSes.GetComponent<Image>().sprite = SpriteSesAcik;
        }
        else
        {
            BtnSes.GetComponent<Image>().sprite = SpriteSesKapali;
        }
    }
    //Eğitim
    public void BtnEgitimAcClick()
    {
        if (CanvasEgitim.gameObject.activeSelf == true)
        {
            CanvasEgitimKapa();
        }
        else
        {

            CanvasEgitimAc();
        }
    }
    public void CanvasEgitimAc()
    {
        CanvasEgitim.gameObject.SetActive(true);
    }
    public void CanvasEgitimKapa()
    {
        CanvasEgitim.gameObject.SetActive(false);
    }
    public void BtnEgitimIleriClick()
    {
        EgitimIleri();
        TxtEgitimDurumGuncelle();
        ImgEgitim.sprite = GetEgitimSprite();
    }
    public void BtnEgitimGeriClick()
    {
        EgitimGeri();
        TxtEgitimDurumGuncelle();
        ImgEgitim.sprite = GetEgitimSprite();
    }
    private void TxtEgitimDurumGuncelle()
    {
        TxtEgitimDurum.text = (GetEgitimIndex() + 1) + " / " + SpriteEgitim.Length;
    }
    public void EgitimIleri()
    {
        if (EgitimIndex + 1 < SpriteEgitim.Length)
            EgitimIndex++;

    }
    public void EgitimGeri()
    {
        if (EgitimIndex > 0)
            EgitimIndex--;
    }
    public int GetEgitimIndex()
    {
        return EgitimIndex;
    }
    public Sprite GetEgitimSprite()
    {
        return SpriteEgitim[EgitimIndex];
    }
}
