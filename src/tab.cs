using System;

public class Tab // нотный стан
{
	public Tone [] Selected_scale = new Tone[1000]; // выбранный пользователем лад
	
	// формулы ладов .==================================
	static byte [][] scales= new byte[10][]; // лады
	
	public Tab() // создаем массив нот
	{
		string note = "c";
		byte octave = 1;
		byte scale = 5;
		scale_generation (note, octave, scale);
	}
	
	public Tab(string note)
	{
		byte octave = 1;
		byte scale = 5;
		scale_generation (note, octave, scale);
	}
	
	public Tab(string note, byte octave)
	{
		byte scale = 5;
		scale_generation (note, octave, scale);
	}
	
	public Tab(string note, byte octave, byte scale)
	{
		scale_generation (note, octave, scale);
	}

	//==================================================
	
	// поля /===========================================
	private int size= 1; // размер массива
	public int Size
	{
		get
		{
			return size;
		}
	}
	//==================================================
	
	// методы /=========================================
	public static void Scales_make_up()
	{
		scales[0] = new byte []  {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
		scales[1] = new byte []     {2, 2, 1, 2, 2, 2, 1}; // Ионийский (натуральный мажор)
		scales[2] = new byte []     {2, 1, 2, 2, 2, 1, 2}; // Дорийский
		scales[3] = new byte []   {1, 2, 2, 2, 1, 2, 2}; // Фригийский
		scales[4] = new byte []     {2, 2, 2, 1, 2, 2, 1};// Лидийский
		scales[5] = new byte [] {2, 2, 1, 2, 2, 1, 2}; // Миксолидийский
		scales[6] = new byte []      {2, 1, 2, 2, 1, 2, 2}; // Эолийский (натуральный минор)
		scales[7] = new byte []    {1, 2, 2, 1, 2, 2, 2}; // Локрийский
	
		scales [8] = new byte []   {2, 1, 2, 2, 1, 3, 1}; // гармонический минор
		scales [9] = new byte []    {2, 1, 2, 2, 2, 2, 1}; // мелодический минор
	}
	
	private void scale_generation (string note, byte octave, byte scale)
	{
		Selected_scale [0]= new Tone(note);
		Console.WriteLine($"ind= {0} Note= {Selected_scale[0].Note} Octave= {Selected_scale[0].Octave} Freq= {Selected_scale[0].Freq} Duration= {Selected_scale[0].Duration} Music_size= {Selected_scale[0].Music_size}");
		Tone semitone = Selected_scale [0].Copy();
		while(true)
		{
			for(int interval_ind = 0; interval_ind < scales[scale].Length; interval_ind++)
			{
				for(int minor_sec_num = 0; minor_sec_num< scales[scale][interval_ind]; minor_sec_num++)
					if(semitone.Next_Note ())
						return; // достигнута самая высокая нота
				Selected_scale[size]= semitone.Copy();
				size+=1;
				Console.WriteLine($"ind= {size-1} Note= {Selected_scale[size-1].Note} Octave= {Selected_scale[size-1].Octave} Freq= {Selected_scale[size-1].Freq} Duration= {Selected_scale[size-1].Duration} Music_size= {Selected_scale[0].Music_size}");
			}
		}
	}
	//==================================================
}
