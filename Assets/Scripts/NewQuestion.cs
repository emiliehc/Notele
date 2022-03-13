using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Note;
using static Tone;
using static ToneLetter;
using static Modifier;
using static Program;

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
        print(List.StringOf(Question));
        Attempts = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        var counter = GameObject.FindWithTag("Counter");
        var counterText = counter.GetComponent<TextMeshProUGUI>();
        counterText.text = Attempts.ToString();
        if (!Input.GetKeyDown(KeyCode.C)) return;
        var userResponse = NoteButton.NoteList;
        if (userResponse.IsEmpty())
            return;

        Attempts++;
        var noteButtons = List.Map(GameObject.FindGameObjectsWithTag("Note").ToList(),
            go => go.GetComponent<NoteButton>());
        List.ForEach(noteButtons, btn =>
        {
            if (btn.isEnabled)
                btn.Toggle();
        });
        int comp(Note lhs, Note rhs) => lhs.CompareTo(rhs);
        if (List.Equals(List.Sort(userResponse, comp), List.Sort(Question, comp)))
        {
            // set all to green and generate new question
            void Action(NoteButton btn) => btn.TemporaryStateChangeTo(NoteState.Matches);

            List.ForEach(noteButtons, Action);
            MakeNewQuestion();
            return;
        }
        
        List.ForEach(noteButtons, btn =>
        {
            if (List.Contains(userResponse, btn.note))
            {
                if (List.Contains(Question, btn.note))
                {
                    btn.TemporaryStateChangeTo(NoteState.Matches);
                }
                else
                {
                    btn.TemporaryStateChangeTo(NoteState.DoesNotExist);
                }
            }
        });
    }
}