using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Note;
using static Tone;
using static ToneLetter;
using static Modifier;
    

public class NoteButton : MonoBehaviour
{
    //[SerializeField] public ((ToneLetter, Modifier),int) note;
    public ToneLetter toneLetter;
    public Modifier modifier;
    public int octave;
    public bool isEnabled = false;
    public static List<Note> NoteList = List.Of<Note>();
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    public void Toggle()
    {
        if (isEnabled)
        {
            transform.localScale += new Vector3(0.05f, 0.05f);
            NoteList = List.Remove(NoteList, note);
            NoteList = List.Sort(NoteList, (note1, note2) => note1.CompareTo(note2));
        }
        else
        {
            transform.localScale -= new Vector3(0.05f, 0.05f);
            NoteList = NoteList.Append(note);
            NoteList = List.Sort(NoteList, (note1, note2) => note1.CompareTo(note2));
        }

        isEnabled = !isEnabled;
        
    }

    public Note note => ((toneLetter, modifier), octave);

    

    // Update is called once per frame
    // void Update()
    // {
    //     print(List.StringOf(NoteList));
    // }
}
