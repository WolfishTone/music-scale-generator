using System.IO;

/*public class Sound
{
       	// конструкторы /===================================
       	Sound()
       	{
       		FileStream stream = new FileStream("notes/test.wav", FileMode.Create);
		BinaryWriter composer= new BinaryWriter(stream);
		
       	}
       	//==================================================
       	
       	// поля /===========================================
       	private FileStream stream = new FileStream("notes/test.wav", FileMode.Create); // создание файла
	private BinaryWriter composer= new BinaryWriter(stream); // создание двоичного композитора
	
       	private int RIFF = 0x46464952; // указание форматов
	private int WAVE = 0x45564157; // меня не касается
       	private int format = 0x20746D66; // туда же
       	private short formatType = 1; // для линейного квантования, т.е. без сжатия
       	private int data = 0x61746164;
       	
       	private int formatChunkSize = 16; //
       	private int headerSize = 8; // 
       	private int samplesPerSecond = 44100; // Частота дискретизации. это максимум
	private short chenal_num = 1; // кол-во каналов
	private short bitsPerSample = 16; // кол-во бит на один семпл
	private short sampleSize = (short)(chenal_num * ((bitsPerSample + 7)/8));
	private int bytesPerSecond = samplesPerSecond * sampleSize; // байты за секунду
	
	private int waveSize = 4; // пока х/з
	private int lengthInSec = 6; // длина в секундах
	private int samples = 88200 * lengthInSec;
        private int dataChunkSize = samples * chenal_num;
	private int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
	
	private short bpm;
	public short Bpm // темп
	{
		set
		{
			return bpm;
		}
	}
	//==================================================
	
	// методы /=========================================
	private void wav_init () // инициализация заголовочной части wav файла
	{
		composer.Write(RIFF);
         	composer.Write(fileSize);
         	composer.Write(WAVE);
	        composer.Write(format);
        	composer.Write(formatChunkSize);
        	composer.Write(formatType);
		composer.Write(chenal_num); 
		composer.Write(samplesPerSecond);
		composer.Write(bytesPerSecond);
	        composer.Write(sampleSize);
	        composer.Write(bitsPerSample); 
		composer.Write(data);
		composer.Write(dataChunkSize);
	}
	//==================================================
}*/
