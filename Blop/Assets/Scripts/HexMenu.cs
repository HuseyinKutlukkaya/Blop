using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMenu : MonoBehaviour
{
    public GameObject ParticlePatlama;
    bool UstundenGeciyor;
    // Start is called before the first frame update
    void Start()
    {
        UstundenGeciyor = false;
        StartCoroutine("OtomatikYokOlusDongusu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseOver()
    {
        if(UstundenGeciyor==false)
        StartCoroutine("OlmedenOnceYapilcaklar");
    }
    IEnumerator OtomatikYokOlusDongusu()
    {
        float Sure = Random.Range(3f, 15f);
        yield return new WaitForSeconds(Sure);

        if (UstundenGeciyor == false)
        {
            StartCoroutine("OlmedenOnceYapilcaklar");
            StartCoroutine("OtomatikYokOlusDongusu");
        }       
        else
            StartCoroutine("OtomatikYokOlusDongusu");
    }
    IEnumerator OlmedenOnceYapilcaklar()
    {
        UstundenGeciyor = true;
        //anim
        GetComponent<Animator>().Play("HexYokOlus", 0);
        yield return new WaitForSeconds(0.6f);
        GameObject particle = Instantiate(ParticlePatlama, transform.position, Quaternion.identity);
        if (MusicManager.instance.GetMuzikVarmi() == false)
            particle.GetComponent<AudioSource>().volume = 0;
        else
            particle.GetComponent<AudioSource>().volume = 1;

        Destroy(particle, 4);


        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        UstundenGeciyor = false;
    }
    public void Patla()
    {
        if (UstundenGeciyor == false)
            StartCoroutine("OlmedenOnceYapilcaklar");

    }
}
