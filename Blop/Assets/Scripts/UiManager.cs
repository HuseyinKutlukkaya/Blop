using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager 
{
    Text txtPuan;
    Text TxtEgitimDurum;
    Image ImgEgitim;
    Canvas CanvasEgitim;
    Canvas CanvasKaybettin;
    Canvas CanvasKazandin;
   
    Button BtnSes;
    
    public UiManager()
    {
        GameObject.Find("TxtMax").GetComponent<Text>().text = "Max=" + GameManager.instance.HaraketLimiti ;
        txtPuan          = GameObject.Find("TxtPuan").GetComponent<Text>();
        CanvasKaybettin  = GameObject.Find("CanvasKaybettin").GetComponent<Canvas>();
        CanvasKaybettin.gameObject.SetActive(false);
        CanvasKazandin   = GameObject.Find("CanvasKazandin").GetComponent<Canvas>();
        CanvasKazandin.gameObject.SetActive(false);
        CanvasKazandin.gameObject.transform.Find("BtnBirSonrakiLevel").GetComponent<Button>().onClick
            .AddListener(() => GameManager.instance.BirSonrakiLevel());
        GameObject.Find("TxtOynananLevel").GetComponent<Text>().text= "LEVEL "+GameManager.instance.GetLevelNo().ToString().PadLeft(2,'0');
        CanvasKaybettin.gameObject.transform.Find("BtnYenidenOyna").GetComponent<Button>().onClick
            .AddListener(() => GameManager.instance.YenidenOyna());
        GameObject.Find("BtnYeniden").GetComponent<Button>().onClick
            .AddListener(() => GameManager.instance.YenidenOyna());
        BtnSes = GameObject.Find("BtnSes").GetComponent<Button>();
        BtnSes.onClick
            .AddListener(() => BtnSesClick());
        BtnSes = GameObject.Find("BtnSes").GetComponent<Button>();

        GameObject.Find("BtnHome").GetComponent<Button>().onClick
            .AddListener(() => BtnHomeClick());
        SesIconSecimiBaslangic();

        CanvasEgitim = GameObject.Find("CanvasEgitim").GetComponent<Canvas>();
       

        TxtEgitimDurum = GameObject.Find("TxtEgitimDurum").GetComponent<Text>();
        GameObject.Find("BtnEgitimIleri").GetComponent<Button>().onClick
            .AddListener(() => BtnEgitimIleriClick());
        GameObject.Find("BtnEgitimGeri").GetComponent<Button>().onClick
           .AddListener(() => BtnEgitimGeriClick());
        ImgEgitim= GameObject.Find("ImgEgitim").GetComponent<Image>();
        ImgEgitim.sprite = GameManager.instance.GetEgitimSprite();
        TxtEgitimDurumGuncelle();

       

        GameObject.Find("BtnEgitimAc").GetComponent<Button>().onClick
          .AddListener(() => BtnEgitimAcClick());
        GameObject.Find("BtnEgitimKapa").GetComponent<Button>().onClick
         .AddListener(() => CanvasEgitimKapa());
     
            CanvasEgitimKapa();
        GameObject.Find("BtnHelper").GetComponent<Button>().onClick
           .AddListener(() => BtnHelperClick());
        BtnHelperTxtAta();
    }
    public void BtnHelperClick()
    {
        DB.instance.Helper = !DB.instance.Helper;
        BtnHelperTxtAta();
    }
    private void BtnHelperTxtAta()
    {
        if (DB.instance.Helper==true)
        {
            GameObject.Find("BtnHelper").GetComponentInChildren<Text>().text = "Helper\nON";
        }
        else
        {
            GameObject.Find("BtnHelper").GetComponentInChildren<Text>().text = "Helper\nOFF";
            GameManager.instance.YokEdilecekleriKapaHelper();
        }
    }
    public void BtnEgitimAcClick()
    {
        if (CanvasEgitim.gameObject.activeSelf==true)
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
        GameManager.instance.EgitimIleri();
        TxtEgitimDurumGuncelle();
        ImgEgitim.sprite = GameManager.instance.GetEgitimSprite();
    }
    public void BtnEgitimGeriClick()
    {
        GameManager.instance.EgitimGeri();
        TxtEgitimDurumGuncelle();
        ImgEgitim.sprite = GameManager.instance.GetEgitimSprite();
    }
    private void TxtEgitimDurumGuncelle()
    {
        TxtEgitimDurum.text = (GameManager.instance.GetEgitimIndex() + 1) + " / " + GameManager.instance.SpriteEgitim.Length;
    }
    private void SesIconSecimiBaslangic()
    {
        if (MusicManager.instance.GetMuzikVarmi())
        {
            BtnSes.GetComponent<Image>().sprite = GameManager.instance.SpriteSesAcik;
        }
        else
        {
            BtnSes.GetComponent<Image>().sprite = GameManager.instance.SpriteSesKapali;
        }
    }
    public void TxtHareketSayisiGuncelle(int deger)
    {
        txtPuan.text = "Move = " + (deger);
    }
    public void BtnHomeClick()
    {
        //buraya anamenu indexi
        SceneManager.LoadScene(1);
    }
    public void CanvasKazandinAc()
    {
        CanvasKazandin.gameObject.SetActive(true);
        CanvasKazandin.gameObject.transform.Find("TxtLevelNo").GetComponent<Text>().text
          = GameManager.instance.GetLevelNo().ToString();
        DB.instance.LevelArti();
        DB.instance.Kaydet();
    }
    public void CanvasKaybettinAcHaraketLimitiAsildi()
    {
        
        CanvasKaybettin.gameObject.SetActive(true);
        CanvasKaybettin.gameObject.transform.Find("TxtLevelNo").GetComponent<Text>().text 
            = GameManager.instance.GetLevelNo().ToString();

        CanvasKaybettin.gameObject.transform.Find("TxtAciklama").GetComponent<Text>().text = "You are out of move.";
    }
    public void CanvasKaybettinAcNormal()
    {
        CanvasKaybettin.gameObject.SetActive(true);
        CanvasKaybettin.gameObject.transform.Find("TxtLevelNo").GetComponent<Text>().text
           = GameManager.instance.GetLevelNo().ToString();
        CanvasKaybettin.gameObject.transform.Find("TxtAciklama").GetComponent<Text>().text = "Hold Space for see the puzzle.\nYou have hexagon with no neighbor.";
    }
    public void CanvasKaybettinAc()
    {
        CanvasKaybettin.gameObject.SetActive(true);

    }
    public void CanvasKaybettinKapa()
    {
        CanvasKaybettin.gameObject.SetActive(false);

    }
    public void BtnSesClick()
    {
        if (MusicManager.instance.GetMuzikVarmi())
        {
            MusicManager.instance.MuzikSatatuDegis();
            BtnSes.GetComponent<Image>().sprite = GameManager.instance.SpriteSesKapali;

        }
        else
        {
            MusicManager.instance.MuzikSatatuDegis();
            BtnSes.GetComponent<Image>().sprite = GameManager.instance.SpriteSesAcik;
        }
    }


}
