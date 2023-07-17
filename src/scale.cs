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
		string note = "c";
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
	private int size= 1;
	public int Size
	{
		get
		{
			return size;
		}
	}
	//==================================================
	
	// методы /=========================================
	private void scale_generation (string note, byte octave, byte scale)
	{
		Selected_scale [0]= new Tone(note);
		Console.WriteLine($"ind= {0} Note= {Selected_scale[0].Note} Octave= {Selected_scale[0].Octave} Freq= {Selected_scale[0].Freq} Duration= {Selected_scale[0].Duration}");
		Tone semitone = Selected_scale [0].Copy();
		while(true)
		{
			Console.WriteLine($"Selected_scale[size-1].Note != \\0 ? {Selected_scale[size-1].Note != "\\0"}");
			for(int interval_ind = 0; interval_ind < 7; interval_ind++)
			{
				for(int minor_sec_num = 0; minor_sec_num< scales[scale, interval_ind]; minor_sec_num++)
					if(semitone.Next_Note ())
						return; // достигнута самая высокая нота
				Selected_scale[size]= semitone.Copy();
				size+=1;
				Console.WriteLine($"ind= {size-1} Note= {Selected_scale[size-1].Note} Octave= {Selected_scale[size-1].Octave} Freq= {Selected_scale[size-1].Freq} Duration= {Selected_scale[size-1].Duration}");
			}
		}
	}
	//==================================================
}
