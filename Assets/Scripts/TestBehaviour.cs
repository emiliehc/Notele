using System;
using UnityEngine;
using static ToneLetter;
using static Modifier;
using static Reference;

public class TestBehaviour : MonoBehaviour
{
    private void Update()
    {
        Note note = (C, 4);
        for (var i = 0; i < 48; i++)
        {
            print(((Note)(note + i)).frequency);
        }
    }
}
