using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private bool MuzikVar=true;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        MuzikVar = true;
        DontDestroyOnLoad(gameObject);
        StartCoroutine("a");
    }
    IEnumerator a ()
    {

        yield return new WaitForSeconds(0.5f);
        if (SahneGecisi.instance==null)
        {
            StartCoroutine("a");
        }
        else
        {
            GetComponent<AudioSource>().volume = 1;
            MuzikVar = true;
            SahneGecisi.instance.a();
        }
    }
    public void MuzikSatatuDegis()
    {
        MuzikVar = !MuzikVar;
        if (MuzikVar == false)
            GetComponent<AudioSource>().volume = 0;
        else
            GetComponent<AudioSource>().volume = 1;
    }
    public bool GetMuzikVarmi()
    {
        return MuzikVar;
    }
}
