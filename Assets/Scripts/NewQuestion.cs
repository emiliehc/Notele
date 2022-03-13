using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Note;
using static Tone;
using static ToneLetter;
using static Modifier;
public class NewQuestion : MonoBehaviour
{
    public static List<Note> Question = List.Of<Note>();

    public static int Attempts;
    // Start is called before the first frame update
    void Start()
    {
        MakeNewQuestion();
        Attempts = 0;
    }

    public void MakeNewQuestion()
    {
        Question = Program.NewRandomQuestion();
        Attempts = 0;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
