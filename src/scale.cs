using System;

public class Scale // лад
{
	public Tone [] Selected_scale = new Tone[97]; // выбранный пользователем лад
	
	// формулы ладов .==================================
	static byte [,] scales= 
	{
		{2, 2, 1, 2, 2, 2, 1}, // Ионийский (натуральный мажор)
		{2, 1, 2, 2, 2, 1, 2}, // Дорийский
		{1, 2, 2, 2, 1, 2, 2}, // Фригийский
		{2, 2, 2, 1, 2, 2, 1},// Лидийский
		{2, 2, 1, 2, 2, 1, 2}, // Миксолидийский
		{2, 1, 2, 2, 1, 2, 2}, // Эолийский (натуральный минор)
		{1, 2, 2, 1, 2, 2, 2}, // Локрийский
	
		{2, 1, 2, 2, 1, 3, 1}, // гармонический минор
		{2, 1, 2, 2, 2, 2, 1}  // мелодический минор
	};
	// конструкторы /===================================
	public Scale() // создаем массив нот
	{
		string note = "A";
		byte octave = 1;
		byte scale = 5;
		scale_generation (note, octave, scale);
	}
	
	public Scale(string note)
	{
		byte octave = 1;
		byte scale = 5;
		scale_generation (note, octave, scale);
	}
	
	public Scale(string note, byte octave)
	{
		byte scale = 5;
		scale_generation (note, octave, scale);
	}
	
	public Scale(string note, byte octave, byte scale)
	{
		scale_generation (note, octave, scale);
	}
	
	
	//==================================================
	
	// поля /===========================================
	//==================================================
	
	// методы /=========================================
	private void scale_generation (string note, byte octave, byte scale)
	{
		Selected_scale [0]= new Tone("A");
		Console.WriteLine($"ind= {0} Note= {Selected_scale[0].Note} Octave= {Selected_scale[0].Octave} Freq= {Selected_scale[0].Freq} Duration= {Selected_scale[0].Duration}");
		
		int note_ind = 1;// индекс в массиве лада
		Tone semitone = Selected_scale [0].Copy();
		while(Selected_scale[note_ind-1].Note != "\\0")
			for(int interval_ind = 0; interval_ind < 7; interval_ind++)
			{
				for(int minor_sec_num = 0; minor_sec_num< scales[scale, interval_ind]; minor_sec_num++)
					semitone.Next_Note ();
				Selected_scale[note_ind]= semitone.Copy();
				note_ind+=1;
				Console.WriteLine($"ind= {note_ind-1} Note= {Selected_scale[note_ind-1].Note} Octave= {Selected_scale[note_ind-1].Octave} Freq= {Selected_scale[note_ind-1].Freq} Duration= {Selected_scale[note_ind-1].Duration}");
			}
	}
	//==================================================
}
