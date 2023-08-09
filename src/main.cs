using System;
using System.IO;


class HZ
{
public static void Main()
	{
		Console.WriteLine("инициализация формул лада");
		Tab.Scales_make_up();
		Console.WriteLine("генерация лада");
		//Tab minor = new Tab(40, "a", 0, 0); // нота, октава, номер лада (см. tab.cs scales[]), номер канала(дорожки)
		Tab minor = new Tab(4, 40, 2);// byte music_size, short bpm, byte channel_num
		minor.Scale_generation ("a", 0, 0);
		minor.Scale_generation ("a", 1, 1);
		Console.WriteLine("правильная версия");
		//minor. Channel_duration_alignment(0);
		//minor. Channel_duration_alignment(1);
		minor.Duration_alignment_of_everything();
		//Wolfish_WAV.generate (minor);
		Console.WriteLine("Конец");
	}
}
