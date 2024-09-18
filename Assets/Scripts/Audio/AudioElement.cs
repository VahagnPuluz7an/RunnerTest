using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioElement : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public AudioSource Source => source;
}
