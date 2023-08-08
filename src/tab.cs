using System;

public class Tab // нотный стан
{	
	// конструкторы /===================================
	//для 
	public Tab(byte music_size, byte chenalNum) // создаем пустой стан
	{
		this.music_size= music_size;
	}
	
	public Tab(short bpm, string note, byte scale) //генерируем лад
	{
		this.bpm= bpm;
		scale_generation (note, scale);
	}
	//==================================================
	
	// поля /===========================================
	static byte [][] scales= new byte[10][]; // лады
	
	private Tone [] user_tab = new Tone[10]; // пользовательский нотный стан
	public Tone [] User_tab
	{
		get
		{
			return user_tab;
		}	
	}
	
	private float tacts_num;// кол-во тактов
	public float Tacts_num
	{
		get
		{
			return tacts_num;
		}
	}
	
	private int size= 0; // размер массива
	public int Size
	{
		get
		{
			return size;
		}
	}
	
	private short bpm;
	public short Bpm // темп
	{
		get
		{
			return bpm;
		}
	}
	
	private byte music_size= 4; // число четвертых
	public byte Music_size
	{
		get
		{
			return music_size;
		}
	}
	
	//==================================================
	
	// методы /=========================================
	
	public void add_note(string note, byte octave, byte duration, byte music_size) //для сочинения
	{
		if(size== user_tab.Length)
			Array.Resize(ref user_tab, (int)Math.Pow(user_tab.Length, 2)); // перевыделяем память
		user_tab[size] = new Tone(note, octave, duration, music_size);
	}
	
	private void add_note(ref Tone [] tab, Tone note) //для генерации ладов. да здравствует полиморфизм!
	{
		if(size== tab.Length)
			Array.Resize(ref tab, (int)Math.Pow(tab.Length, 2)); // перевыделяем память
		tab[size++] = note;
		Console.WriteLine($"size= {size} Note= {note.Note} Octave= {note.Octave} Freq= {note.Freq} Duration= {note.Duration} Music_base= {note.Music_base}");
	}
	
	public static void Scales_make_up()
	{
		scales[0] = new byte [] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}; // хроматика
		scales[1] = new byte [] {2, 2, 1, 2, 2, 2, 1}; // Ионийский (натуральный мажор)
		scales[2] = new byte [] {2, 1, 2, 2, 2, 1, 2}; // Дорийский
		scales[3] = new byte [] {1, 2, 2, 2, 1, 2, 2}; // Фригийский
		scales[4] = new byte [] {2, 2, 2, 1, 2, 2, 1};// Лидийский
		scales[5] = new byte [] {2, 2, 1, 2, 2, 1, 2}; // Миксолидийский
		scales[6] = new byte [] {2, 1, 2, 2, 1, 2, 2}; // Эолийский (натуральный минор)
		scales[7] = new byte [] {1, 2, 2, 1, 2, 2, 2}; // Локрийский
	
		scales[8] = new byte [] {2, 1, 2, 2, 1, 3, 1}; // гармонический минор
		scales[9] = new byte [] {2, 1, 2, 2, 2, 2, 1}; // мелодический минор
	}
	
	private void fill_up_fourth(int i, float local_duration, float last_fourth_part, ref Tone [] user_right_ver_tab) // дозаполнить четвертую пустыми нотами определенной длительности
	{
		Tone Void_note= new Tone("\\0", 1, user_tab[i-1].Duration, user_tab[i-1].Music_base); // пустая нота нужной длительности и основы
		while (last_fourth_part/local_duration !=  1)
		{
			add_note(ref user_right_ver_tab, Void_note.Copy(true));
			local_duration++;
		}
	}
	
	private int fill_up_tact(int i, ref Tone [] user_right_ver_tab) // дозаполнить четвертую пустыми нотами определенной длительности
	{
		Tone Void_note= new Tone("\\0", 1, 0, user_right_ver_tab[size-1].Music_base); // пустая нота нужной длительности и основы
		while(i++%music_size!= 0)
			add_note(ref user_right_ver_tab, Void_note.Copy(true));
		return i;
	}
	
	private void scale_generation (string note, byte scale)
	{
		Tone semitone = new Tone(note);
		add_note(ref user_tab, semitone.Copy());
		while(true)
		{
			for(int interval_ind = 0; interval_ind < scales[scale].Length; interval_ind++)
			{
				for(int minor_sec_num = 0; minor_sec_num< scales[scale][interval_ind]; minor_sec_num++)
					if(semitone.Next_Note ())
						return; // достигнута самая высокая нота
				add_note(ref user_tab, semitone.Copy());
			}
		}
	}
	
	public void Duration_alignment() // добавляет в пользовательский нотный стан выравнивающие пустые ноты и записывает их в отдельный массив
	{
		//Tone Void_note;  // пустая нота нужной длительности и основы
		Tone [] user_right_ver_tab = new Tone[10]; // пользовательский нотный стан
		int last_size= size; // размер исходного стана
		size= 0;
		float local_duration= 0;
		float fourth_part; // часть четвертой, которую занимает текущая нота
		float last_fourth_part= (float)Math.Pow(user_tab[0].Music_base, user_tab[0].Duration); // часть четвертой, которую занимает предыдущая нота
		add_note(ref user_right_ver_tab, user_tab[0]);
		local_duration++;
		
		for (int i= 1;i< last_size;i++) // идем 
		{
			fourth_part= (float)Math.Pow(user_tab[i].Music_base, user_tab[i].Duration);
			if(fourth_part== last_fourth_part || last_fourth_part < 2)
			{
				if(last_fourth_part/local_duration== 1) // нота полная
				{
					local_duration= 0;
					Console.WriteLine("===================================================");
				}
			}
			else
			{
				fill_up_fourth(i, local_duration, last_fourth_part, ref user_right_ver_tab);
				local_duration= 0;
			}
			add_note(ref user_right_ver_tab, user_tab[i]);
			last_fourth_part= fourth_part;
			local_duration++;
		}
		// если последняя нота не закончила четвертую
		fill_up_fourth(last_size, local_duration, last_fourth_part, ref user_right_ver_tab);
		// если последняя нота не закончила такт
		last_size= fill_up_tact(last_size, ref user_right_ver_tab);
		user_tab= user_right_ver_tab;
		tacts_num= (last_size-1)/(float)music_size;
		Console.WriteLine($"кол-во тактов= {tacts_num}");
	}
	//==================================================
}
