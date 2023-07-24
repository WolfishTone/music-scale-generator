using System;
using System.IO;

public class Wolfish_WAV
{
       	// конструкторы /===================================
       	//==================================================
       	
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
	
	private static short bpm = 24;
	private static short Bpm // темп
	{
		get
		{
			return bpm;
		}
	}
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
		return waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
	}
	
	private static float get_lengthInSec(Scale scale)
	{
	float notes_sum_duration= 0; // ноты могут иметь разные длительности, поэтому их длительности складываются
		for (int i = 0; i< scale.Size;i++)
		{
			notes_sum_duration+= 1/((float)Math.Pow(scale.Selected_scale[i].Music_size,scale.Selected_scale[i].Duration));
		//	Console.WriteLine($"{scale.Selected_scale[i].Music_size}  {scale.Selected_scale[i].Duration}");
		//	Console.WriteLine($"notes_sum_duration= {notes_sum_duration}");
		}
		Console.WriteLine($"notes_sum_duration= {notes_sum_duration}");
		Console.WriteLine($"{notes_sum_duration / bpm* 60}");
		return notes_sum_duration / bpm * 60;
	}
	
	public static void generate (Scale scale) // инициализация заголовочной части wav файла
	{
		FileStream stream = new FileStream("notes/test.wav", FileMode.Create); // создание файла
		BinaryWriter composer= new BinaryWriter(stream); // создание двоичного композитора

		sampleSize= get_sampleSize();
		bytesPerSecond= get_bytesPerSecond();
		dataChunkSize= get_dataChunkSize();
		fileSize= get_fileSize();
		lengthInSec= get_lengthInSec(scale);
		samples= get_samples();
		
		composer.Write(RIFF);
         	composer.Write(fileSize);
         	composer.Write(WAVE);
	        composer.Write(format);
        	composer.Write(formatChunkSize);
        	composer.Write(formatType);
		composer.Write(chenalNum);
		composer.Write(samplesPerSecond);
		composer.Write(bytesPerSecond);
	        composer.Write(sampleSize);
	        composer.Write(bitsPerSample); 
		composer.Write(data);
		composer.Write(dataChunkSize);
		double freq;
		for(int note_ind = 0; note_ind < scale.Size; note_ind++)
		{
			freq= Math.PI * scale.Selected_scale[note_ind].Freq / samplesPerSecond;
			
			for (int i = 0; i < samples / scale.Size; i++)
			{
            			short s= (short)(Saw(i, freq) * ampl);
            			composer.Write(s);
			}
		}
		composer.Close();
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
}
