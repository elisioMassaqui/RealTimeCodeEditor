using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MyScript : MonoBehaviour
{
    public string myVariable = "Hello";
    public GameObject bolinhaPrefab;
    public Transform bolinhaLocal;

    void Update(){
        if (Input.GetKeyUp(KeyCode.I))
        {
            instanciarBolinha();
        }
    }
    public void instanciarBolinha()
    {
        Instantiate(bolinhaPrefab, bolinhaLocal.localPosition, bolinhaLocal.localRotation);
    }

    // Outros métodos e lógica do script...

    public int Interpolar(int Real, int Imagine, int Multiplo){

        return Real + (Imagine - Real) * Multiplo;

    }
}
