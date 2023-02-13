using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TMP_InputField w;
    public TMP_InputField h;
    public TMP_InputField bombs;

    public GameObject menu;
    public GameObject juego;

    public Button botonplay;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        juego.SetActive(false);
        botonplay.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!String.IsNullOrEmpty(w.text) && !String.IsNullOrEmpty(h.text) && !String.IsNullOrEmpty(bombs.text))
        {
            if (Int32.Parse(w.text) * Int32.Parse(h.text) > Int32.Parse(bombs.text))
            botonplay.interactable = true;
        }
    }

     public void PulsarPlay() 
    {
        MapGenerator.gen.GenerarMapa(Int32.Parse(w.text), Int32.Parse(h.text), Int32.Parse(bombs.text));
        menu.SetActive(false);
        juego.SetActive(true);
        MapGenerator.gen.hasPerdido = false;
        MapGenerator.gen.loseTxt.SetActive(false);
        MapGenerator.gen.winTxt.SetActive(false);

    }

}
