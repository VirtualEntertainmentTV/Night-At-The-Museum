using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitch : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Load Scene Index")]
    private int sceneIndex;

   // private AudioManagement audioManagement;

    private void Awake()
    {
        //audioManagement = AudioManagement.instance;
    }
    void OnTriggerEnter(Collider other)
    {
       // audioManagement.PlaySound(SoundEffects.Scene);
        SceneManager.LoadScene(sceneIndex);

    }



} // class
