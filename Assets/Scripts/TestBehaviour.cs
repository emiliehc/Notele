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
        var l = List.Of<Note>((A, 4), (C, 4));
        //print(NewRandomQuestion());
        print(List.StringOf(NewRandomQuestion()));
    }
}
