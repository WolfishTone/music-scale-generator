using System;

public class Tone// нота
{
	static string [] note_names = new string []   {"C",    "C#",   "D",    "D#",   "E",    "F",    "F#",   "G",   "G#",    "A",   "A#",   "B",};
	static float [] note_freqs = new float []    {16.35f, 17.32f, 18.35f, 19.44f, 20.61f, 21.82f, 23.12f, 24.5f, 25.95f,  27.5f, 29.14f, 30.87f}; 
	
	// конструкторы /===================================
	public Tone()
	{
	}
	public Tone(string note)
	{	
		if(is_note(note))
		{
			this.note= note.ToUpper();
			octave= 1;
			freq= get_freq(octave, note);
			duration= 4;
		}
	}
	public Tone(string note, byte octave)
	{
		if(is_note(note) && is_right_octave (octave)) // проверяем название и номер октавы
		{
			this.note= note.ToUpper();
			this.octave= octave;
			freq= get_freq(octave, note);
			duration= 4;
		}
	}
	public Tone(string note, byte octave, byte duration)
	{
		if(is_note(note) && is_right_octave (octave)) // проверяем название и номер октавы
		{
			this.note= note.ToUpper();
			this.octave= octave;
			freq= get_freq(octave, note);
			this.duration= duration;
		}
	}
	
	//==================================================
	
	// поля /===========================================
	private string note; // название ноты
	public string Note
	{
		get
		{
			return note;
		}
	}
	private byte octave; // номер октавы
	public byte Octave
	{
		get
		{
			return octave;
		}
	}
	
	private float freq; // частота
	public float Freq
	{
		get
		{
			return freq;
		}
	}
	private byte duration; // длительность ноты
	public byte Duration
	{
		get
		{
			return duration;
		}
	}
	//==================================================
	
	// методы /=========================================
	private bool is_note(string note) //проверка названия определенной ноты
	{
		int i= 0;
		for(; i < note_names.Length ; i++)
			if(note_names[i] == note.ToUpper())
				break;
		if(i!= note_names.Length)
			return true; // нота существует
		else
			return false; // ноты нет в списке
	}
	
	private bool is_right_octave (byte octave) //проверка номера октавы
	{
		if(octave > 0 && octave < 9)
			return true;
		else
			return false;
	}
	
	private float get_freq (byte octave, string note) //получение частоты определенной ноты
	{
		float freq= note_freqs[note_index(note)];
		return freq * octave; 
	}
	
	private sbyte note_index(string note) // нахождение индекса ноты
	{
		sbyte i = 0;
		for(; i < note_names.Length ; i++)
			if(note_names[i]== note.ToUpper())
				return i;
		return -1;
	}
	
	public bool Next_Note ()
	{
		sbyte note_ind= note_index(note);
		if(note_ind == note_names.Length-1) // если нота G#
		{
			if(this.octave == 8)
			{
				Console.WriteLine("\\0==============");
				this.note= "\\0";
				return true; // самая высокая нота
			}
			else
				this.octave += 1;	
			note_ind= 0;
		}
		else
			note_ind+=1;
		this.note= note_names[note_ind];
		this.freq = get_freq(octave, note_names[note_ind]);
		return false; // успешное завершение
	}
	
	public Tone Copy ()
	{
		Tone copy= new Tone(); 
		copy.note= this.note;
		copy.octave= this.octave;
		copy.freq= this.freq;
		copy.duration= this.duration;
		return copy;
	}
	//==================================================
}
