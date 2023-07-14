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
		
		Scale chromatic = new Scale();
		
		
		FileStream stream = new FileStream("notes/test.wav", FileMode.Create);
		BinaryWriter composer= new BinaryWriter(stream);
		
		int RIFF = 0x46464952; // указание форматов
         	int WAVE = 0x45564157; // меня не касается
         	int format = 0x20746D66; // туда же
         	short formatType = 1; // для линейного квантования, т.е. без сжатия
         	int data = 0x61746164;
         	
         	int formatChunkSize = 16; //
         	int headerSize = 8; // 
         	int samplesPerSecond = 44100; // Частота дискретизации. это максимум
		short chenal_num = 1; // кол-во каналов
		short bitsPerSample = 16; // кол-во бит на один семпл
		short sampleSize = (short)(chenal_num * ((bitsPerSample + 7)/8));
		int bytesPerSecond = samplesPerSecond * sampleSize; // байты за секунду
		
		int waveSize = 4; // пока х/з
		int lengthInSec = 6;
		int samples = 88200 * lengthInSec;
         	int dataChunkSize = samples * chenal_num;
		int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
		
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
		
		double aNatural = 220.0; // частота
		double ampl = 10000;
		double perfect = 1.5;
		double concert = 1.498307077;
		double freq = aNatural * perfect;
		
		/*for (int i = 0; i < samples / 4; i++)
		{
			double t = (double)i / (double)samplesPerSecond;
			short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
            		composer.Write(s);
            	}*/
            	
            	freq = aNatural * concert;
		for (int i = 0; i < samples / 4; i++)
		{
			double t = (double)i / (double)samplesPerSecond;
			short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
			composer.Write(s);
		}
         
		for (int i = 0; i < samples / 4; i++)
		{
            		double t = (double)i / (double)samplesPerSecond;
            		short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * perfect * 2.0 * Math.PI)));
			composer.Write(s);
		}
		for (int i = 0; i < samples / 4; i++)
		{
			double t = (double)i / (double)samplesPerSecond;
			short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * concert * 2.0 * Math.PI)));
			composer.Write(s);
		}
		
		composer.Close();
		stream.Close();
		Console.WriteLine("Конец");
	}
}
