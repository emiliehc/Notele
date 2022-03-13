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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Toggle()
    {
        if (isEnabled)
        {
            transform.localScale += new Vector3(0.05f, 0.05f);
        }
        else
        {
            transform.localScale -= new Vector3(0.05f, 0.05f);
        }

        isEnabled = !isEnabled;
        
    }

    public Note note => ((toneLetter, modifier), octave);

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
