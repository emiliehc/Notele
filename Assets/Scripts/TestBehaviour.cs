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
        Tone tone = (C, Sharp);
        var ((letter, modifier), octave) = note;
        var l = List.Of(1, 2, 3);
        var counter = Ref(0);
        var value = !counter;
        
    }
}
