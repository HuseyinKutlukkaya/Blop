using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Hex : MonoBehaviour {

	// Our coordinates in the map array
	public int x;
	public int y;
    public Sprite ResimSecili;
    public Sprite ResimNormal;
    public Sprite ResimSag, ResimSol, ResimSagAsagi, ResimSolAsagi, ResimSagYukari, ResimSolYukari;
    public Sprite ResimEfektli;
    [Space(10)]
    [Header("Ozelliklilerin Resimleri")]
    public Sprite ResimEfektSag;
    public Sprite ResimEfektSol;
    public Sprite ResimEfektSagAsagi;
    public Sprite ResimEfektSolAsagi;
    public Sprite ResimEfektSagYukari;
    public Sprite ResimEfektSolYukari;

    public bool EfektSag, EfektSol, EfektSagAsagi, EfektSolAsagi, EfektSagYukari, EfektSolYukari;

    public GameObject ParticlePatlama;
    private bool SeciliHex = false;
    Yonler yon;
    private bool PatlamaNoktasi = false;//RENK İÇİN
    private float OncekiAci = -233;//Helperda Spam Olmaması için fixed update
   
    #region Sonradan Class
    List<List<GameObject>> List = new List<List<GameObject>>() { new List<GameObject>(), new List<GameObject>(), new List<GameObject>() ,
        new List<GameObject>() , new List<GameObject>() , new List<GameObject>() };
    List<List<GameObject>> ListHelper = new List<List<GameObject>>() { new List<GameObject>(), new List<GameObject>(), new List<GameObject>() ,
        new List<GameObject>() , new List<GameObject>() , new List<GameObject>() };
    void HexEkle(int yon, GameObject h)
    {
        List[yon].Add(h);
    }
    void HexEkleHelper(int yon, GameObject h)
    {
        ListHelper[yon].Add(h);
    }
    void HexEkleHelperClear()
    {
        for (int i = 0; i < ListHelper.Count; i++)
        {
            ListHelper[i].Clear();
        }
    }
    void HexleriYokEdilmeListesineEkleHelper()
    {
        int max = 0;

        for (int i = 0; i < ListHelper.Count; i++)
        {
            if (ListHelper[i].Count > max)
            {
                max = ListHelper[i].Count;
            }

        }
        for (int i = 0; i < max; i++)
        {
            for (int j = 0; j < ListHelper.Count; j++)
            {
                if (ListHelper[j].Count - 1 >= i)
                    GameManager.instance.YokEdilcekHexEkleHelper(ListHelper[j][i]);

            }
        }
    }

    void HexleriYokEdilmeListesineEkle()
    {
        int max = 0;
        
        for (int i = 0; i < List.Count; i++)
        {
            if (List[i].Count>max)
            {
                max = List[i].Count;
            }

        }
        for (int i = 0; i < max; i++)
        {
            for (int j = 0; j < List.Count; j++)
            {
                if(List[j].Count-1>=i)
                GameManager.instance.YokEdilcekHexEkleOzel(List[j][i]);

            }
        }
    }
    #endregion
    private void Start()
    {
        BaslangicSpriteAyarlama();
    }
    public enum Yonler
    {    Sagust = 0,
        Sag = 1,
        SagAlt = 2,
        SolAlt = 3,
        Sol = 4,
        SolUst=5
        
    }
    public void SetPatlamaNoktasi()
    {
        PatlamaNoktasi = true;
    }
    void BaslangicSpriteAyarlama()
    {
        

        if (EfektSag)
        {
            
            ResimNormal = ResimEfektSag;

        }
        else if (EfektSagAsagi)
        {
            ResimNormal = ResimEfektSagAsagi;

        }
        else if (EfektSagYukari)
        {
            ResimNormal = ResimEfektSagYukari;

        }
        else if (EfektSol)
        {
            ResimNormal = ResimEfektSol;

        }
        else if (EfektSolAsagi)
        {
            ResimNormal = ResimEfektSolAsagi;

        }
        else if (EfektSolYukari)
        {
            ResimNormal = ResimEfektSolYukari;

        }
        GetComponent<SpriteRenderer>().sprite = ResimNormal;

    }
    public void YokOl()
    {
        StartCoroutine("OlmedenOnceYapilcaklarNormal");
    
    }
    IEnumerator OlmedenOnceYapilcaklarNormal()
    {
        //anim
        GetComponent<Animator>().Play("HexYokOlus",0);
        yield return new WaitForSeconds(0.6f);
       GameObject particle= Instantiate(ParticlePatlama, transform.position, Quaternion.identity);
        if (MusicManager.instance.GetMuzikVarmi() == false)
            particle.GetComponent<AudioSource>().volume = 0;
        else
            particle.GetComponent<AudioSource>().volume = 1;
        Destroy(particle, 4);
        //particle
      
        GameManager.instance.Invoke("HexYokEdildiYokEdilmisSayisiniArttir", 1f);
        
       Destroy(gameObject);
    }
    #region KomsuBulma
    string GetSagYukari(int x,int y )
    {
        int i = y % 2;
        return "Hex_" + (x + i) + "_" + (y + 1);
    }
    string GetSolYukari(int x, int y)
    {
        int i;
        if (y % 2 == 0)
        {
            i = 1;
        }
        else
            i = 0;
        return "Hex_" + (x - i) + "_" + (y + 1);
    }
    string GetSolAsagi(int x, int y)
    {
        int i;
        if (y % 2 == 0)
        {
            i = 1;
        }
        else
            i = 0;
        return "Hex_" + (x - i) + "_" + (y -1);
    }
    string GetSagAsagi(int x, int y)
    {
        int i;
        if (y % 2 == 0)
        {
            i = 0;
        }
        else
            i = 1;

        return "Hex_" + (x + i) + "_" + (y - 1);
    }
    string GetSag(int x, int y)
    {
       

        return "Hex_" + (x + 1) + "_" + (y);
    }
    string GetSol(int x, int y)
    {


        return "Hex_" + (x - 1) + "_" + (y);
    }
    #endregion
    public bool KomsusuVarmi()
    {
        int i = 0;
        GameObject g;
        g = GameObject.Find(GetSagYukari(x, y));
        if (g == null)
        {
            i++;
            if (transform.name == "Hex_3_9")
                Debug.Log("1");

        }
        else
        {
            if (transform.name == "Hex_3_9")
                Debug.Log("ben sagusteki prpsuyum");

        }

        g = GameObject.Find(GetSolYukari(x, y));
        if (g == null)
        {
            i++;
            if (transform.name == "Hex_3_9")
                Debug.Log("2");

        }
        else
        {
            if (transform.name == "Hex_3_9")
                Debug.Log("ben solusteki prpsuyum");

        }

        g = GameObject.Find(GetSolAsagi(x, y));
        if (g == null)
        {
            i++;
            if (transform.name == "Hex_3_9")
                Debug.Log("3");

        }
        else
        {
            if (transform.name == "Hex_3_9")
                Debug.Log("ben solasagı prpsuyum");

        }
        g = GameObject.Find(GetSagAsagi(x, y));
        if (g == null)
        {
            i++;

            if (transform.name == "Hex_3_9")
                Debug.Log("4");
        }
        else
        {
            if (transform.name == "Hex_3_9")
                Debug.Log("ben sagasaagı prpsuyum");

        }
        g = GameObject.Find(GetSag(x, y));
        if (g == null)
        {
            i++;
            if (transform.name == "Hex_3_9")
                Debug.Log("5");

        }
        else
        {
            if (transform.name == "Hex_3_9")
                Debug.Log("ben sag prpsuyum");

        }
        g = GameObject.Find(GetSol(x, y));
        if (g == null)
        {
            i++;
            if (transform.name == "Hex_3_9")
                Debug.Log("6");

        }
        else
        {
            if (transform.name == "Hex_3_9")
                Debug.Log("ben sol prpsuyum");

        }

    
        if (i==6)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(SeciliHex)
            {
                SeciliHex = false;
                GameManager.instance.HexBirakildi();
                HexEkleHelperClear();
                GetComponent<SpriteRenderer>().sprite = ResimNormal;

            }

        }
        if (SeciliHex)
        {
            float Aci = FareyeGoreAciBul();
            if (DB.instance.Helper == true&&Aci!=OncekiAci&& GameManager.instance.GetYokEdilmeIslemiYapiliyor() == false)
            {
                GameManager.instance.YokEdilecekleriKapaHelper();
                HexEkleHelperClear();
                HelperHexleriBul();
            }
            OncekiAci = Aci;
            AciyaGoreGrafikSec(Aci);
          

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GameManager.instance.GetKaybettin() == true)
                {

                    return;
                }
                if (GameManager.instance.GetYokEdilmeIslemiYapiliyor() == true)
                {

                    return;
                }
                if (!KomsusuVarmi())
                {
                 //   belki kaybettin ? yapamazsın
                    return;
                }
                YoneGoreYokEt();
            }
        }

    }
    float FareyeGoreAciBul()
    {
        Vector3 direction = new Vector3(Input.mousePosition.x, Input.mousePosition.y) -
           Camera.main.WorldToScreenPoint(transform.position);

        //use atan2 to get the angle in radians
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        //convert to degrees
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        //angleInDegrees will be in the range (-180,180]. This changes it from (0, 360].
        if (angleInDegrees < 0)
        {
            angleInDegrees += 360;
        }
        return angleInDegrees;
    }
    private void AciyaGoreGrafikSec(float aci)
    {
        if (aci > 0 && aci < 30)
        {
            yon = Yonler.Sag;
            GetComponent<SpriteRenderer>().sprite = ResimSag;

        }
        else if (aci>30&&aci<90)
        {
            yon = Yonler.Sagust;
            GetComponent<SpriteRenderer>().sprite =ResimSagYukari ;

        }
        else if (aci > 90 && aci < 150)
        {
            yon = Yonler.SolUst;
            GetComponent<SpriteRenderer>().sprite = ResimSolYukari;

        }
        else if (aci > 150 && aci < 200)
        {
            yon = Yonler.Sol;

            GetComponent<SpriteRenderer>().sprite = ResimSol;

        }
        else if (aci > 200 && aci < 260)
        {
            yon = Yonler.SolAlt;

            GetComponent<SpriteRenderer>().sprite = ResimSolAsagi;

        }
        else if (aci > 260 && aci < 330)
        {
            yon = Yonler.SagAlt;

            GetComponent<SpriteRenderer>().sprite =  ResimSagAsagi;

        }
        else if (aci > 330 )
        {
            yon = Yonler.Sag;

            GetComponent<SpriteRenderer>().sprite = ResimSag    ;

        }
    }
    public bool OzelHexmi()
    {
        if (EfektSag || EfektSagAsagi || EfektSagYukari || EfektSol || EfektSolAsagi || EfektSolYukari)
            return true;
        else
            return false;

        
    }
    bool efek = false;
    public void EfektUygula()
    {
        efek = false;
        GameObject g;

        g = gameObject;
            while (g != null && (!GameManager.instance.YokEdilicekmi(g) || g == gameObject))
            {
                if (g != gameObject)
            {                     GameManager.instance.YokEdilcekHexEkleOzel(g);
                Debug.Log("GİRİŞ Efekt" + g.name);
            }


            if (EfektSag)
            {
                g = GameObject.Find(GetSag(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSagAsagi)
            {
                g = GameObject.Find(GetSagAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSagYukari)
            {
                g = GameObject.Find(GetSagYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSol)
            {
                g = GameObject.Find(GetSol(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSolAsagi)
            {
                g = GameObject.Find(GetSolAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSolYukari)
            {
                g = GameObject.Find(GetSolYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            if (g != null)
            {
                if (g.GetComponent<Hex>().OzelHexmi())
                {
             

                    g.GetComponent<Hex>().EfektUygula();
                    

                }


            }

        }

        efek = true;

    }
    public bool EfekM()

    {
        return efek;
    }
    public void YansimaYollar()
    {
        
        GameObject g;
        int i = 0;
        foreach (Yonler yon in Enum.GetValues(typeof(Yonler)))
        {
           

            g = gameObject;
            while (g != null&&(!GameManager.instance.YokEdilicekmi(g)||g==gameObject))
            {
                if(g!=gameObject)
                {
                    HexEkle(i, g);
                    //GameManager.instance.YokEdilcekHexEkle(g);
                }
             

                if (yon == Yonler.Sagust)
                {
                    

                    g = GameObject.Find(GetSagYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.Sag)
                {
                  

                    g = GameObject.Find(GetSag(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.SagAlt)
                {
                    


                    g = GameObject.Find(GetSagAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.SolAlt)
                {
                    g = GameObject.Find(GetSolAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.Sol)
                {
                    g = GameObject.Find(GetSol(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.SolUst)
                {
                    g = GameObject.Find(GetSolYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }

                if (g != null)
                {
                    if (g.GetComponent<Hex>().OzelHexmi())
                    {
                        g.GetComponent<Hex>().EfektUygula();
                    }
                  

                }

            }
            i++;
        }
        HexleriYokEdilmeListesineEkle();
    }
    private void YoneGoreYokEt()
    {
        GameManager.instance.YokEdilecekleriKapaHelper();
        GameObject g = gameObject;
        GameObject ensongirilen=g;
            
        while (g != null)
        {
            if (!GameManager.instance.YokEdilicekmi(g))
            {

                ensongirilen = g;
                GameManager.instance.YokEdilcekHexEkleNormal(g);
            }
            else
            {
              
                break;
            }
               
          
          
            
            
            if (yon == Yonler.Sagust)
            {
                g = GameObject.Find(GetSagYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.Sag)
            {
                g = GameObject.Find(GetSag(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.SagAlt)
            {
                g = GameObject.Find(GetSagAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.SolAlt)
            {
                g = GameObject.Find(GetSolAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.Sol)
            {
                g = GameObject.Find(GetSol(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.SolUst)
            {
                g = GameObject.Find(GetSolYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }

            if (g != null)
            {
                if (g.GetComponent<Hex>().OzelHexmi())
                {
                    g.GetComponent<Hex>().EfektUygula();

                 

                }
               

            }
                


        }
        GameManager.instance.YokEdilcekHexEkleOzel(ensongirilen);
        GameManager.instance.YokEdilcekHexlerNormal.Remove(ensongirilen);
        ensongirilen.GetComponent<Hex>().SetPatlamaNoktasi();
        ensongirilen.GetComponent<Hex>().YansimaYollar();
       
        GameManager.instance.YokEtmeyeBasla();
    }
    public void OzelEfektiAc()
    {
        if (PatlamaNoktasi)
            GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<SpriteRenderer>().sprite = ResimEfektli;
    }
    public void OzelEfektiKapa()
    {
        GetComponent<SpriteRenderer>().sprite = ResimNormal;
    }
    private void OnMouseDown()
    {
        if (GameManager.instance.GetKaybettin()==true)
        {
            
            return;
        }
        if (GameManager.instance.GetYokEdilmeIslemiYapiliyor() == true)
        {

            return;
        }
        if (!KomsusuVarmi())
        {
            //belki  kaybettin ? yapamazsın
            return;
        }
        if (SeciliHex||GameManager.instance.Hexsecilimi())
        {
            return;
        }
        GameManager.instance.HexSecildi();
        SeciliHex = true;
        GetComponent<SpriteRenderer>().sprite = ResimSecili;

    }
    #region Helper
    private void HelperHexleriBul()
    {
        YoneGoreYokEtHelper();
    }
    private void YoneGoreYokEtHelper()
    {
        GameObject g = gameObject;
        GameObject ensongirilen = g;

        while (g != null)
        {
            if (!GameManager.instance.YokEdilicekmiHelper(g))
            {

                ensongirilen = g;
                GameManager.instance.YokEdilcekHexEkleHelper(g);
            }
            else
            {

                break;
            }





            if (yon == Yonler.Sagust)
            {
                g = GameObject.Find(GetSagYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.Sag)
            {
                g = GameObject.Find(GetSag(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.SagAlt)
            {
                g = GameObject.Find(GetSagAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.SolAlt)
            {
                g = GameObject.Find(GetSolAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.Sol)
            {
                g = GameObject.Find(GetSol(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (yon == Yonler.SolUst)
            {
                g = GameObject.Find(GetSolYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }

            if (g != null)
            {
                if (g.GetComponent<Hex>().OzelHexmi())
                {
                    g.GetComponent<Hex>().EfektUygulaHelper();



                }


            }



        }
      
        ensongirilen.GetComponent<Hex>().YansimaYollarHelper();

        GameManager.instance.YokEdilecekleriGosterHelper();
    }
    public void EfektUygulaHelper()
    {
        efek = false;
        GameObject g;

        g = gameObject;
        while (g != null && (!GameManager.instance.YokEdilicekmiHelper(g) || g == gameObject))
        {
            if (g != gameObject)
            {
                GameManager.instance.YokEdilcekHexEkleHelper(g);
                Debug.Log("GİRİŞ Efekt" + g.name);
            }


            if (EfektSag)
            {
                g = GameObject.Find(GetSag(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSagAsagi)
            {
                g = GameObject.Find(GetSagAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSagYukari)
            {
                g = GameObject.Find(GetSagYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSol)
            {
                g = GameObject.Find(GetSol(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSolAsagi)
            {
                g = GameObject.Find(GetSolAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            else if (EfektSolYukari)
            {
                g = GameObject.Find(GetSolYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

            }
            if (g != null)
            {
                if (g.GetComponent<Hex>().OzelHexmi())
                {


                    g.GetComponent<Hex>().EfektUygulaHelper();


                }


            }

        }

        efek = true;

    }
    public void YansimaYollarHelper()
    {
        Debug.Log("yansioz mk");

        GameObject g;
        int i = 0;
        foreach (Yonler yon in Enum.GetValues(typeof(Yonler)))
        {


            g = gameObject;
            while (g != null && (!GameManager.instance.YokEdilicekmiHelper(g) || g == gameObject))
            {
                if (g != gameObject)
                {

                    HexEkleHelper(i, g);
                }


                if (yon == Yonler.Sagust)
                {


                    g = GameObject.Find(GetSagYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.Sag)
                {


                    g = GameObject.Find(GetSag(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.SagAlt)
                {



                    g = GameObject.Find(GetSagAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.SolAlt)
                {
                    g = GameObject.Find(GetSolAsagi(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.Sol)
                {
                    g = GameObject.Find(GetSol(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }
                else if (yon == Yonler.SolUst)
                {
                    g = GameObject.Find(GetSolYukari(g.GetComponent<Hex>().x, g.GetComponent<Hex>().y));

                }

                if (g != null)
                {
                    if (g.GetComponent<Hex>().OzelHexmi())
                    {
                        g.GetComponent<Hex>().EfektUygulaHelper();
                    }


                }

            }
            i++;
        }
        HexleriYokEdilmeListesineEkleHelper();
    }
    public void HelperEfektAc()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<SpriteRenderer>().sprite = ResimEfektli;
    }
    public void HelperEfektKapa()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().sprite = ResimNormal;
    }
    #endregion
}
