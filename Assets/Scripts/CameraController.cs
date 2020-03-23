using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;
    Animator anima;
    public Cinemachine.CinemachineBrain cinemachine;
    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start () {
        
        anima = GetComponent<Animator>();
	}

    public void shake()
    {
        cinemachine.enabled = false;
        anima.Play("Shake");
    }

    public void activeMovimentCamera()
    {
        cinemachine.enabled = true;
    }
}
