using System;
using System.IO;

class HZ
{
public static void Main()
	{
		Tone note = new Tone("a#", 7);
		Console.WriteLine($" Note= {note.Note} Octave= {note.Octave} Freq= {note.Freq}");
		
		note.Next_Note ();
		
		Tone copy = note.Copy();
		Console.WriteLine($" Note= {copy.Note} Octave= {copy.Octave} Freq= {copy.Freq}");
		
		Scale minor = new Scale();
		Wolfish_WAV.generate (minor);
		Console.WriteLine("Конец");
	}
}
