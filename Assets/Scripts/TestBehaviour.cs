using System;
using UnityEngine;
using static ToneLetter;
using static Modifier;
using static Reference;
using static Program;

public class TestBehaviour : MonoBehaviour
{
    private void Update()
    {
        print(((Note)((C, Flat), 4)) == ((Note)(B, 3)));
    }
}
