using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Celda : MonoBehaviour
{
    public int x, y;
    public bool bomb;

    public TextMeshProUGUI numero;
    

    bool pulsada = true;
    public GameObject bomba;

    private void Update()
    {
        if(MapGenerator.gen.hasPerdido == true || MapGenerator.gen.puntuacion == (MapGenerator.gen.witdth * MapGenerator.gen.height) - MapGenerator.gen.bombsNumber)
        {
            GameObject[] celdas = GameObject.FindGameObjectsWithTag("Celda");
            for (int i = celdas.Length - 1; i >= 0; i--)
            {
                if (celdas[i].GetComponent<Celda>().bomb == true)
                {
                    celdas[i].GetComponent<SpriteRenderer>().color = Color.red;
                    celdas[i].transform.GetChild(1).gameObject.SetActive(true);
                }

            }
        }
    }

    private void OnMouseDown()
    {
        if (MapGenerator.gen.hasPerdido == false)
        {
            if (bomb)
            {
                //si tiene bomba
                GetComponent<SpriteRenderer>().color = Color.red;
                bomba.SetActive(true);
                MapGenerator.gen.hasPerdido = true;
                MapGenerator.gen.loseTxt.SetActive(true);
                

            }
            else
            {
                //si no tiene bomba
                if (pulsada)
                {
                    numero.text = MapGenerator.gen.GetBombsAlrededor(x, y).ToString();
                    MapGenerator.gen.puntuacion++;
                    pulsada = false;
                }

                
            }
        }

        if(MapGenerator.gen.puntuacion == (MapGenerator.gen.witdth * MapGenerator.gen.height) - MapGenerator.gen.bombsNumber)
        {
            MapGenerator.gen.winTxt.SetActive(true);
        }
    }

}
