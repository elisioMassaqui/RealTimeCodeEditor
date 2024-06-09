using UnityEngine;

public class MyScript : MonoBehaviour
{
    public string myVariable = "Hello";

    // Outros métodos e lógica do script...

    public int Interpolar(int Real, int Imagine, int Multiplo){

        return Real + (Imagine - Real) * Multiplo;

    }
}
