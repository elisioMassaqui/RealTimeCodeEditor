using UnityEngine;

public class MyScript : MonoBehaviour
{
    public string myVariable = "Hello";
    public GameObject bolinhaPrefab;
    public Transform bolinhaPosition;

    public void instanciar()
    {
        Instantiate(bolinhaPrefab, bolinhaPosition);
    }

    // Outros métodos e lógica do script...

    public int Interpolar(int Real, int Imagine, int Multiplo){

        return Real + (Imagine - Real) * Multiplo;

    }
}
