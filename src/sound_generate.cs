/*using System;
using System.IO;

public class Wolfish_WAV
{   	
       	// поля /===========================================
       	private static int RIFF = 0x46464952; // указание форматов
	private static int WAVE = 0x45564157; // меня не касается
       	private static int format = 0x20746D66; // туда же
       	private static short formatType = 1; // для линейного квантования, т.е. без сжатия
       	private static int data = 0x61746164; 
       	private static int formatChunkSize = 16; // размеры
       	private static int headerSize = 8; //    для заголовка
       	private static int waveSize = 4; //    меня не касается
       	private static int samplesPerSecond = 44100; // Частота дискретизации. это максимум
	private static short chenalNum = 1; // кол-во каналов
	private static short bitsPerSample = 16; // кол-во бит на один семпл
	private static short ampl = 10000; // громкость
	
	private static short sampleSize;
	private static int bytesPerSecond; // байты за секунду
	
	private static float lengthInSec; // длина в секундах
	private static int samples; // кол-во семплов на файл
        private static int dataChunkSize;
	private static int fileSize;
	
	//private static float tacts_num; // число тактов
	//==================================================
	
	// методы /=========================================
	private static short get_sampleSize()
	{
		return (short)(chenalNum * ((bitsPerSample + 7)/8));
	}
	
	private static int get_bytesPerSecond()
	{
		return samplesPerSecond * sampleSize;
	}
	
	private static int get_samples()
	{
		return samplesPerSecond * (int)lengthInSec;
	}
	
	private static int get_dataChunkSize()
	{
		return samples * chenalNum;
	}
	
	private static int get_fileSize()
	{
		return waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize+ 100000;
	}
	
	private static float get_lengthInSec(Tab tab)
	{
		Console.WriteLine($"трек длинной {tab.Tacts_num / tab.Bpm* 60} сек.");
		Console.WriteLine($"tab.Tacts_num= {tab.Tacts_num} tab.Bpm= {tab.Bpm}");
		return tab.Tacts_num / tab.Bpm * 60;
	}
	
	public static void generate (Tab tab) // инициализация заголовочной части wav файла
	{
		FileStream stream = new FileStream("notes/test.wav", FileMode.Create); // создание файла
		BinaryWriter performer= new BinaryWriter(stream); // создание двоичного композитора

		sampleSize= get_sampleSize();
		bytesPerSecond= get_bytesPerSecond();
		dataChunkSize= get_dataChunkSize();
		fileSize= get_fileSize();
		
		//tacts_num= (uint)get_tacts_num(tab);
		lengthInSec= get_lengthInSec(tab);
		samples= get_samples();
		
		performer.Write(RIFF);
         	performer.Write(fileSize);
         	performer.Write(WAVE);
	        performer.Write(format);
        	performer.Write(formatChunkSize);
        	performer.Write(formatType);
		performer.Write(chenalNum);
		performer.Write(samplesPerSecond);
		performer.Write(bytesPerSecond);
	        performer.Write(sampleSize);
	        performer.Write(bitsPerSample); 
		performer.Write(data);
		performer.Write(dataChunkSize);
		double freq;
		uint samples_per_note; // кол-во семплов на конкретную ноту
		
		for(int note_ind = 0; note_ind < tab.Size; note_ind++)
		{
			for(int chenal_ind= 0; chenal_ind< chenalNum;chenal_ind++) // многоканальная запись
			{
				freq= Math.PI * tab.User_tab[note_ind].Freq / samplesPerSecond;
				samples_per_note= (uint)(samples / (tab.Tacts_num * tab.Music_size*Math.Pow(tab.User_tab[note_ind].Music_base,tab.User_tab[note_ind].Duration)));
				for (int i = 0; i < samples_per_note; i++)
				{
            				short s= (short)(Saw(i, freq) * ampl);
            				performer.Write(s);
				}
			}
		}
		
		performer.Close();
         	stream.Close();		
	}
	
	public static double Sine(int index, double frequency) // синусоидный сигнал
	{
		return Math.Sin(frequency * index);
	}
	
	private static double Saw(int index, double frequency) // пила
	{	
		return 2.0 * (index * frequency - Math.Floor(index * frequency )) -1.0;
	}
	
	private static double Triangle(int index, double frequency) // треугольник
	{
		return 2.0 * Math.Abs (2.0 * (index * frequency - Math.Floor(index * frequency + 0.5))) - 1.0;
	}
	
	private static double Flat(int index, double frequency) 
	{
		if (Math.Sin(frequency * index ) > 0) 
			return 1;
		else 
			return -1;
	}
	//==================================================
}*/
