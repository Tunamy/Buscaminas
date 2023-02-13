using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    public GameObject celda;
    public int witdth, height;
    public GameObject[][] map; // matriz

    public int bombsNumber;

    public int puntuacion;
    public TextMeshProUGUI puntuacionTxt;


    public static MapGenerator gen;

    public bool hasPerdido = false;
    public GameObject loseTxt;
    public GameObject winTxt;


    void Start()
    {

        gen = this;
        //iniciamos el mapa creando un gameobject para cada celda

        //map = new GameObject[witdth][]; //hace las celdas de ancho

        //for (int i = 0; i < map.Length; i++)
        //{
        //    map[i] = new GameObject[height]; // hace las celdas de la altura
        //}


        //// bucle que instancia las celdas
        //for (int i = 0; i < witdth; i++)
        //{
        //    for( int j = 0; j < height; j++)
        //    {
        //        map[i][j] = Instantiate(celda,new Vector2(i,j), Quaternion.identity);

        //        map[i][j].GetComponent<Celda>().x = i;
        //        map[i][j].GetComponent<Celda>().y = j;
        //    }
        //}

        ////posiciona la camara en medio del mapa
        //Camera.main.transform.position = new Vector3(((float)witdth/2) - 0.5f, ((float)height/2) -0.5f, -10);


        //// Poner las bombas en el mapa
        //for(int i = 0; i < bombsNumber; i++)
        //{
        //    int x = Random.Range(0, witdth);
        //    int y = Random.Range(0, height);

        //    //map[Random.Range(0, witdth)][Random.Range(0, height)].GetComponent<SpriteRenderer>().color = Color.red;
        //    if (!map[x][y].GetComponent<Celda>().bomb) //si en esa celda no hay bomba
        //    {
        //        map[x][y].GetComponent<Celda>().bomb = true; // la coloca en el otro script en true
        //        //map[x][y].GetComponent<SpriteRenderer>().color = Color.red;
        //    }
        //    else
        //    {
        //        i--; // busca otra
        //    }
        //}
    }


    public void GenerarMapa(int wth, int hei, int bomb)
    {
        witdth = wth;
        height = hei;
        bombsNumber = bomb;

        if(witdth > 9)
            witdth = 9;

        if (height > 9)
            height = 9;

        if (bombsNumber > witdth * height)
            bombsNumber = witdth * height - 1;


        map = new GameObject[witdth][]; //hace las celdas de ancho

        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height]; // hace las celdas de la altura
        }


        // bucle que instancia las celdas
        for (int i = 0; i < witdth; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = Instantiate(celda, new Vector2(i, j), Quaternion.identity);

                map[i][j].GetComponent<Celda>().x = i;
                map[i][j].GetComponent<Celda>().y = j;
            }
        }

        //posiciona la camara en medio del mapa
        Camera.main.transform.position = new Vector3(((float)witdth / 2) - 0.5f, ((float)height / 2) - 0.5f, -10);


        // Poner las bombas en el mapa
        for (int i = 0; i < bombsNumber; i++)
        {
            int x = Random.Range(0, witdth);
            int y = Random.Range(0, height);

            //map[Random.Range(0, witdth)][Random.Range(0, height)].GetComponent<SpriteRenderer>().color = Color.red;
            if (!map[x][y].GetComponent<Celda>().bomb) //si en esa celda no hay bomba
            {
                map[x][y].GetComponent<Celda>().bomb = true; // la coloca en el otro script en true
                //map[x][y].GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                i--; // busca otra
            }
        }

    }

    public int GetBombsAlrededor(int x, int y)
    {
        int contador = 0;

        if( x>0 && y < (height - 1) && map[x-1][ y + 1].GetComponent<Celda>().bomb) //arriba izquierda
            contador++;

        if (y < (height - 1) && map[x][y + 1].GetComponent<Celda>().bomb) //arriba
            contador++;

        if (x < (witdth - 1) && y < (height - 1) && map[x + 1][y + 1].GetComponent<Celda>().bomb) //arribaderecha
            contador++;


        if (x > 0 && map[x - 1][y ].GetComponent<Celda>().bomb) //izquierda
            contador++;

        if (x < (witdth - 1) && map[x +1][y].GetComponent<Celda>().bomb) //derecha
            contador++;


        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Celda>().bomb) //abajo izquierda
            contador++;

        if (y > 0 && map[x][y - 1].GetComponent<Celda>().bomb) //abajo 
            contador++;

        if (x < (witdth - 1) && y > 0 && map[x + 1][y - 1].GetComponent<Celda>().bomb) //abajo derecha
            contador++;




        return contador;
    }



    private void Update()
    {
        puntuacionTxt.text = "Puntuación: " + puntuacion;
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restar()
    {
        GameObject[] celdas = GameObject.FindGameObjectsWithTag("Celda");
        for (int i = celdas.Length - 1; i >= 0; i--)
        {
            Destroy(celdas[i].gameObject);
        }


    }
}
