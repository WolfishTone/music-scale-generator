using System;

public class Tab // нотный стан
{	
	// конструкторы /===================================
	public Tab(byte music_size, short bpm, byte channel_num)
	{
		this.music_size= music_size;
		this.bpm= bpm;
		user_tab_make_up();
	}
	//==================================================
	
	// поля /===========================================
	static byte [][] scales= new byte[10][]; // лады
	
	private Tone [][] user_tab; // пользовательский нотный стан зубчатый
	public Tone [][] User_tab
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
	
	private int [] size;
	public int [] Size
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
	
	private byte channel_num= 2; // кол-во каналов
	public byte Channel_num
	{
		get
		{
			return channel_num;
		}
	}
	
	//==================================================
	
	// методы /=========================================
	
	public void add_note(ref Tone [][] tab, string note, byte octave, byte duration, byte music_size, byte channel_ind) //для сочинения
	{
		if(size[channel_ind]== tab[channel_ind].GetLength(0))
			Array.Resize(ref tab[channel_ind], (int)Math.Pow(tab[channel_ind].GetLength(0), 2)); // перевыделяем память
		user_tab[channel_ind][size[channel_ind]++] = new Tone(note, octave, duration, music_size);
	}
	
	private void add_note(ref Tone [][] tab, Tone note, byte channel_ind) //для генерации ладов. да здравствует полиморфизм!
	{
		if(size[channel_ind]== tab[channel_ind].GetLength(0))
			Array.Resize(ref tab[channel_ind], (int)Math.Pow(tab[channel_ind].GetLength(0), 2)); // перевыделяем память
		tab[channel_ind][size[channel_ind]++] = note;
		Console.WriteLine($"size[{channel_ind}]= {size[channel_ind]} Note= {note.Note} Octave= {note.Octave} Freq= {note.Freq} Duration= {note.Duration} Music_base= {note.Music_base}");
	}
	
	private void add_note(ref Tone [] tab, Tone note, byte channel_ind) //для генерации ладов. да здравствует полиморфизм!
	{
		if(size[channel_ind]== tab.Length)
			Array.Resize(ref tab, (int)Math.Pow(tab.Length, 2)); // перевыделяем память
		tab[size[channel_ind]++] = note;
		Console.WriteLine($"size[{channel_ind}]= {size[channel_ind]} Note= {note.Note} Octave= {note.Octave} Freq= {note.Freq} Duration= {note.Duration} Music_base= {note.Music_base}");
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
	
	private void user_tab_make_up() // первоначальное определение пользовательского стана
	{
		user_tab= new Tone [channel_num][];
		size= new int[channel_num];
		for(int channel_ind= 0; channel_ind < channel_num; channel_ind++)
		{
			user_tab[channel_ind]= new Tone [10];
			Console.WriteLine($"channel_ind= {channel_ind}");
		}
		Console.WriteLine("выделилось");
	}
	
	private void fill_up_fourth(int i, float local_duration, float last_fourth_part, ref Tone [] user_right_ver_tab, byte channel_ind) // дозаполнить четвертую пустыми нотами определенной длительности
	{
		Tone Void_note= new Tone("\\0", 1, user_tab[channel_ind][i-1].Duration, user_tab[channel_ind][i-1].Music_base); // пустая нота нужной длительности и основы
		while (last_fourth_part/local_duration !=  1)
		{
			add_note(ref user_right_ver_tab, Void_note.Copy(true), channel_ind);
			local_duration++;
		}
	}
		
	private int fill_up_tact(int i, ref Tone [] user_right_ver_tab, byte channel_ind) // дозаполнить четвертую пустыми нотами определенной длительности
	{
		Tone Void_note= new Tone("\\0", 1, 0, user_right_ver_tab[size[channel_ind]-1].Music_base); // пустая нота нужной длительности и основы
		while(i++%music_size!= 0)
			add_note(ref user_right_ver_tab, Void_note.Copy(true), channel_ind);
		return i;
	}
	
	public void Scale_generation (string note, byte scale, byte channel_ind)
	{
		Tone semitone = new Tone(note);
		add_note(ref user_tab, semitone.Copy(), channel_ind);// 0- индекс канала
		while(true)
		{
			for(int interval_ind = 0; interval_ind < scales[scale].Length; interval_ind++)
			{
				for(int minor_sec_num = 0; minor_sec_num< scales[scale][interval_ind]; minor_sec_num++)
					if(semitone.Next_Note ())
						return; // достигнута самая высокая нота
				add_note(ref user_tab, semitone.Copy(), channel_ind);
			}
		}
	}
	
	public void Channel_duration_alignment(byte channel_ind) // выравнивает ноты и такты в выбранном канале
	{
		//Tone Void_note;  // пустая нота нужной длительности и основы
		Tone [] user_right_ver_tab = new Tone[10]; // пользовательский нотный стан
		int last_size= size[channel_ind]; // размер исходного стана
		size[channel_ind]= 0;
		float local_duration= 0;
		float fourth_part; // часть четвертой, которую занимает текущая нота
		float last_fourth_part= (float)Math.Pow(user_tab[channel_ind][0].Music_base, user_tab[channel_ind][0].Duration); // часть четвертой, которую занимает предыдущая нота
		add_note(ref user_right_ver_tab, user_tab[channel_ind][0], channel_ind);
		local_duration++;
		
		for (int i= 1;i< last_size;i++) // идем по стану
		{
			fourth_part= (float)Math.Pow(user_tab[channel_ind][i].Music_base, user_tab[channel_ind][i].Duration);
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
				fill_up_fourth(i, local_duration, last_fourth_part, ref user_right_ver_tab, channel_ind);
				local_duration= 0;
			}
			add_note(ref user_right_ver_tab, user_tab[channel_ind][i], channel_ind);
			last_fourth_part= fourth_part;
			local_duration++;
		}
		// если последняя нота не закончила четвертую
		fill_up_fourth(last_size, local_duration, last_fourth_part, ref user_right_ver_tab, channel_ind);
		// если последняя нота не закончила такт
		last_size= fill_up_tact(last_size, ref user_right_ver_tab, channel_ind);
		user_tab[channel_ind]= user_right_ver_tab;
		tacts_num= (last_size-1)/(float)music_size;
		Console.WriteLine($"кол-во тактов= {tacts_num}");
	}
	
	public void Duration_alignment_of_everything()
	{
		for(byte i= 0; i< channel_num;i++)
			if(size[i]!= 0)
				Channel_duration_alignment(i);
	}
	//==================================================
}
