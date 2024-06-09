using UnityEngine;

public class MyScript : MonoBehaviour
{
    public string myVariable = "Hello";

    // Outros métodos e lógica do script...

    public int Interpolar(int a, int b, int c){
        // int interpolation = a + (b - a) * c;
        return a + (b - a) * c;
    }
}
