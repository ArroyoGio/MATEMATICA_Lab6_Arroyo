using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] public int cantidad;

    public void AumentarPuntos()
    {
        cantidad++;
    }
}
