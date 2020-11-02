using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   [SerializeField()]
    bool HexSecili = false;
    public static GameManager instance;
   public List<GameObject> YokEdilcekHexlerNormal = new List<GameObject>();
    public List<GameObject> YokEdilcekHexlerOzel = new List<GameObject>();

    public List<GameObject> YokEdilcekHexlerHelper = new List<GameObject>();

    UiManager UiManager;
    [SerializeField()]
    private int YokedilecekHexSayisi = 0;
    [SerializeField()]
    private int YokedilmisHexSayisi = 0;
    private int HaraketSayisi=0;
    public int HaraketLimiti;

    private bool Kaybettin = false;
    private bool YokEdilmeIslemiYapliyor = false;

    public Sprite SpriteSesAcik, SpriteSesKapali;
    public Sprite[] SpriteEgitim;
    private int EgitimIndex=0;
    private void Awake()
    {
        instance = this;
        UiManager = new UiManager();
    }
   
    public void HexSecildi()
    {
        HexSecili = true;
    }
    public void HexBirakildi()
    {
        
        HexSecili = false;
        if (DB.instance.Helper==true)
        {
            YokEdilecekleriKapaHelper();

        }
    }
    public bool Hexsecilimi()
    {
        return HexSecili;
    }
    public bool YokEdilicekmi(GameObject hex)
    {
        if (YokEdilcekHexlerNormal.Contains(hex)|| YokEdilcekHexlerOzel.Contains(hex))
        {
            return true;
        }
        else
        return false;
    }
    public bool YokEdilicekmiHelper(GameObject hex)
    {
        if (YokEdilcekHexlerHelper.Contains(hex) )
        {
            return true;
        }
        else
            return false;
    }
    public void YokEdilcekHexEkleHelper(GameObject g)
    {
        YokEdilcekHexlerHelper.Add(g);
    }
    public void YokEdilcekHexEkleNormal(GameObject g)
    {
        YokEdilcekHexlerNormal.Add(g);
    }
    public void YokEdilcekHexEkleOzel(GameObject g)
    {
        YokEdilcekHexlerOzel.Add(g);
    }

    ///HELPER
    public void YokEdilecekleriGosterHelper()
    {
        for (int i = 0; i < YokEdilcekHexlerHelper.Count; i++)
        {
            GameObject g = YokEdilcekHexlerHelper[i];
            if (g != null)
                g.GetComponent<Hex>().HelperEfektAc();
        }
    }
    public void YokEdilecekleriKapaHelper()
    {
        for (int i = 0; i < YokEdilcekHexlerHelper.Count; i++)
        {
            GameObject g = YokEdilcekHexlerHelper[i];
            if(g!=null)
            g.GetComponent<Hex>().HelperEfektKapa();
        }
        YokEdilcekHexlerHelper.Clear();
    }
    ///HELPER
    public void YokEtmeyeBasla()
    {
        PuanArttir();
        for (int i = 0; i < YokEdilcekHexlerNormal.Count; i++)
        {
            for (int j = i+1; j < YokEdilcekHexlerNormal.Count; j++)
            {
                if (YokEdilcekHexlerNormal[i]== YokEdilcekHexlerNormal[j])
                {
                    YokEdilcekHexlerNormal.RemoveAt(j);
                    j--;
                }
            }
        }
        for (int i = 0; i < YokEdilcekHexlerOzel.Count; i++)
        {
            for (int j = i + 1; j < YokEdilcekHexlerOzel.Count; j++)
            {
                if (YokEdilcekHexlerOzel[i] == YokEdilcekHexlerOzel[j])
                {
                    YokEdilcekHexlerOzel.RemoveAt(j);
                    j--;
                }
            }
        }
        YokedilecekHexSayisi = YokEdilcekHexlerNormal.Count+YokEdilcekHexlerOzel.Count;
        YokedilmisHexSayisi = 0;
        YokEdilmeIslemiYapliyor = true;
    StartCoroutine("YokEtmeSureciNormal");

    }
    public void Kontrol()
    {
        Debug.Log("buraya girebildim");

        GameObject g = GameObject.Find("Harita");
        if (g.transform.childCount==0)
        {
            try
            {
                //burası geçtin yeri
                UiManager.CanvasKazandinAc();
                return;
                
            }
            catch (System.Exception)
            {

               
            }


        }
        else
        {
            Debug.Log(g.transform.childCount);
            for (int i = 0; i < g.transform.childCount; i++)
            {
                if (g.transform.GetChild(i).GetComponent<Hex>().KomsusuVarmi()==false)
                {//kaybettin yeri
                    
                    Kaybettin = true;
                    UiManager.CanvasKaybettinAcNormal();
                    return;
                }
            }
        }

        if (HaraketSayisi >= HaraketLimiti)
        {
            Kaybettin = true;
            UiManager.CanvasKaybettinAcHaraketLimitiAsildi();
        }
    }
    public void HexYokEdildiYokEdilmisSayisiniArttir()
    {
        //particle lara göre çünkü hexler 0.1 sn aralıklarla yok oluyo
        YokedilmisHexSayisi++;
        if (YokedilmisHexSayisi==YokedilecekHexSayisi)
        {
            Kontrol();
            YokEdilmeIslemiYapliyor = false;
        }
    }
    private void PuanArttir()
    {
        HaraketSayisi++;
        UiManager.TxtHareketSayisiGuncelle(HaraketSayisi);
    }
    public int GetLevelNo()
    {

        //index+ 1 (+1 çünkü 1.bölüm  0.index  level 1
        return (SceneManager.GetActiveScene().buildIndex - 2)+1;
    }
    private IEnumerator YokEtmeSureciNormal()
    {
        GameObject g = null;
        if (YokEdilcekHexlerNormal.Count >0)
            g =YokEdilcekHexlerNormal[0];

        if (g != null)
            YokEdilcekHexlerNormal.Remove(g);
       
        if(g!=null)
        g.GetComponent<Hex>().YokOl();
        yield return new WaitForSecondsRealtime(.2f);
        if (YokEdilcekHexlerNormal.Count>0)
        {
            StartCoroutine("YokEtmeSureciNormal");
        }
        else
        {
            if (YokEdilcekHexlerOzel.Count > 0)
            {
                StartCoroutine("YokEtmeSureciOzelEfekt");
            }
            else
            {
                HexBirakildi();


            }
           


        }
    }
    private IEnumerator YokEtmeSureciOzelEfekt()
    {
        for (int i = 0; i < YokEdilcekHexlerOzel.Count; i++)
        {
            GameObject g = YokEdilcekHexlerOzel[i];
            if (g != null)
                g.GetComponent<Hex>().OzelEfektiAc();
        }
        yield return new WaitForSecondsRealtime(.5f);
        for (int i = 0; i < YokEdilcekHexlerOzel.Count; i++)
        {
            GameObject g = YokEdilcekHexlerOzel[i];
            if(g!=null)
           g.GetComponent<Hex>().OzelEfektiKapa();
        }
        StartCoroutine("YokEtmeSureciOzel");
    }
    private IEnumerator YokEtmeSureciOzel()
    {
        GameObject g = YokEdilcekHexlerOzel[0];
        YokEdilcekHexlerOzel.Remove(g);

        if (g != null)
            g.GetComponent<Hex>().YokOl();
        yield return new WaitForSecondsRealtime(.04f);
        if (YokEdilcekHexlerOzel.Count > 0)
        {
            StartCoroutine("YokEtmeSureciOzel");
        }
        else
        {
            HexBirakildi();


        }
    }

    public void BirSonrakiLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1< SceneManager.sceneCountInBuildSettings)
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else
            SceneManager.LoadScene(1);
    }
    public void YenidenOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public bool GetKaybettin()
    {
        return Kaybettin;
    }
    public bool GetYokEdilmeIslemiYapiliyor()
    {
        return YokEdilmeIslemiYapliyor;
    }
    public void EgitimIleri()
    {
        if (EgitimIndex + 1 < SpriteEgitim.Length)
            EgitimIndex++;
    }
    public void EgitimGeri()
    {
        if (EgitimIndex  > 0)
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
    void Start()
    {
        
    }
    /// Oldugunde Space basınca tekli kalanları beyaz gostermek için başlangic
    public void YanlizKalanHexlerGosterAc()
    {
        GameObject g = GameObject.Find("Harita");
        for (int i = 0; i < g.transform.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Hex>().KomsusuVarmi() == false)
            {


                g.transform.GetChild(i).GetComponent<Hex>().OzelEfektiAc();


            }
        }
    }
    public void YanlizKalanHexlerGosterKapa()
    {
        GameObject g = GameObject.Find("Harita");

        for (int i = 0; i < g.transform.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Hex>().KomsusuVarmi() == false)//eğer komşusu yoksa
            {


                g.transform.GetChild(i).GetComponent<Hex>().OzelEfektiKapa();


            }
        }
    }
    /// Oldugunde Space basınca tekli kalanları beyaz gostermek için bitis

    void Update()
    {
        if (Kaybettin&&Input.GetKeyDown(KeyCode.Space))
        {
            YanlizKalanHexlerGosterAc();
            UiManager.CanvasKaybettinKapa();
        }
        else if (Kaybettin && Input.GetKeyUp(KeyCode.Space))
        {
            UiManager.CanvasKaybettinAc();
            YanlizKalanHexlerGosterKapa();

        }
    }
}
