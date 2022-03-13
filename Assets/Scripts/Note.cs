using System;
using System.Collections.Generic;
using UnityEngine;
using static ToneLetter;
using static Modifier;
using Notes = System.Collections.Generic.List<Note>;

public enum ToneLetter : int
{
    C = 0,
    D = 2,
    E = 4,
    F = 5,
    G = 7,
    A = 9,
    B = 11
}

public static class ToneLetterExt
{
    public static ToneLetter? MatchToneLetter(this int num)
    {
        var asToneLetter = (ToneLetter) num;
        return asToneLetter switch
        {
            C or D or E or F or G or A or B => asToneLetter,
            < C => MatchToneLetter(num - 12),
            > B => MatchToneLetter(num + 12),
            _ => null
        };
    }
}

public enum Modifier : int
{
    Sharp = 1,
    None = 0,
    Flat = -1
}

public sealed class Tone : IComparable<Tone>
{
    public ToneLetter toneLetter { get; private set; } = default;
    public Modifier modifier { get; private set; } = None;
    public int relativeValue => (int) this;

    public static implicit operator (ToneLetter, Modifier)(Tone record)
        => (record.toneLetter, record.modifier);

    public static implicit operator Tone((ToneLetter, Modifier) tuple)
        => new() {toneLetter = tuple.Item1, modifier = tuple.Item2};

    public static implicit operator Tone(ToneLetter tuple)
        => new() {toneLetter = tuple};

    public static implicit operator int(Tone tone)
        => (int) tone.toneLetter + (int) tone.modifier;

    public static implicit operator Tone(int num)
    {
        var asToneLetter = (ToneLetter) num;
        return asToneLetter switch
        {
            < C => num + 12,
            > B => num - 12,
            _ => num.MatchToneLetter() switch
            {
                { } letter => (letter, None),
                _ => (((Tone)(num - 1)).toneLetter, Sharp)
            }
        };
    }

    public override string ToString() => toneLetter + modifier switch
    {
        Sharp => "#",
        Flat => "b",
        _ => ""
    };

    public void Deconstruct(out ToneLetter _toneLetter, out Modifier _modifier)
    {
        _toneLetter = toneLetter;
        _modifier = modifier;
    }

    public int CompareTo(Tone other) => relativeValue.CompareTo(other.relativeValue);

    public override bool Equals(object obj) => this switch
    {
        { } other => relativeValue == other.relativeValue
    };

    public static bool operator ==(Tone lhs, Tone rhs)
        => lhs != null && lhs.Equals(rhs);

    public static bool operator !=(Tone lhs, Tone rhs) => !(lhs == rhs);

    public override int GetHashCode() => relativeValue.GetHashCode();
}

public sealed class Note : IComparable<Note>
{
    public Tone tone { get; private set; } = default;
    public int octave { get; private set; } = default;
    public int relativeValue => (int) this;

    public static implicit operator (Tone, int)(Note record)
        => (record.tone, record.octave);

    public static implicit operator Note((Tone, int) tuple)
        => new() {tone = tuple.Item1, octave = tuple.Item2};
    
    public static implicit operator Note(int num)
    {
        var octave = num / 12;
        var remain = num % 12;
        return (remain, octave);
    }

    public override string ToString() => tone.ToString() + octave;

    public static implicit operator int(Note note)
    {
        return note.tone + note.octave * 12;
    }

    public void Deconstruct(out Tone _tone, out int _octave)
    {
        _tone = tone;
        _octave = octave;
    }

    public float frequency => this switch
    {
        ((A, None), 4) => 440.0f,
        _ => A4.frequency * Mathf.Pow(stepFactor, this - A4)
    };

    public static readonly Note A4 = (A, 4);
    public static readonly float stepFactor = Mathf.Pow(2, 1.0f / 12.0f);

    public int CompareTo(Note other) => relativeValue.CompareTo(other.relativeValue);

    public override bool Equals(object obj) => this switch
    {
        { } other => relativeValue == other.relativeValue
    };

    public static bool operator ==(Note lhs, Note rhs)
        => lhs != null && lhs.Equals(rhs);

    public static bool operator !=(Note lhs, Note rhs) => !(lhs == rhs);

    public override int GetHashCode() => relativeValue.GetHashCode();
}

public static class Program
{
    public delegate Notes NoteGenerator(Note baseNote);

    private static Notes MajorThird(Note baseNote)
    {
        var ((_, _), _) = baseNote;
        return List.Of(baseNote);
    }
}