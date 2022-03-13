using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
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

    private bool canBeToggled = true;
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    public void Toggle()
    {
        if (!canBeToggled) return;
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

    public void TemporaryStateChangeTo(NoteState state)
    {
        StartCoroutine(TemporaryStateChangeTo_aux(state));
    }

    private IEnumerator TemporaryStateChangeTo_aux(NoteState state)
    {
        canBeToggled = false;
        var img = GetComponent<Image>();
        var originalColor = img.color;
        img.color = state switch
        {
            NoteState.DoesNotExist => new Color(0.9f, 0.6f, 0.0f),
            NoteState.Matches => Color.green,
            _ => originalColor
        };
        yield return new WaitForSeconds(3);
        img.color = originalColor;
        canBeToggled = true;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     print(List.StringOf(NoteList));
    // }
}
