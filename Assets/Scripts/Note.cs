using System;
using static ToneLetter;
using static Modifier;

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

public enum Modifier : int
{
    Sharp = 1,
    None = 0,
    Flat = -1
}

public sealed record Tone
{
    public ToneLetter toneLetter { get; set; } = default;
    public Modifier modifier { get; set; } = Modifier.None;

    public static implicit operator (ToneLetter, Modifier)(Tone record)
    {
        return (record.toneLetter, record.modifier);
    }

    public static implicit operator Tone((ToneLetter, Modifier) tuple)
    {
        return new Tone {toneLetter = tuple.Item1, modifier = tuple.Item2};
    }

    public static implicit operator Tone(ToneLetter tuple)
    {
        return new Tone {toneLetter = tuple};
    }

    public static implicit operator int(Tone tone)
    {
        return (int) tone.toneLetter + (int) tone.modifier;
    }

    public override string ToString() => toneLetter + modifier switch
    {
        Sharp => "#",
        Flat => "b",
        _ => ""
    };
}

public record Note
{
    public Tone tone { get; set; } = default;
    public int octave { get; set; } = default;

    public static implicit operator (Tone, int)(Note record)
    {
        return (record.tone, record.octave);
    }

    public static implicit operator Note((Tone, int) tuple)
    {
        return new Note {tone = tuple.Item1, octave = tuple.Item2};
    }

    public override string ToString() => tone.ToString() + octave;
    public static implicit operator int(Note note)
    {
        return note.tone + note.octave * 12;
    }

    public const float A4 = 440.0f;
}
