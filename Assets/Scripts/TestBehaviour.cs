
using UnityEngine;
using static ToneLetter;
using static Modifier;


public class TestBehaviour : MonoBehaviour
{
    private void Update()
    {
        Note note = (C, 4);
        print(note.frequency);
    }
}
