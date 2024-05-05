using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    public Material[] materials; // Array para almacenar los materiales
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = materials[Random.Range(0, materials.Length)]; // Asignar un material aleatorio del array
    }
}
