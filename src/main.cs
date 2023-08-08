using System;
using System.IO;


class HZ
{
public static void Main()
	{
		Console.WriteLine("инициализация формул лада");
		Tab.Scales_make_up();
		Console.WriteLine("генерация лада");
		Tab minor = new Tab(40, "a", 0); // нота, октава, номер лада (см. tab.cs scales[])
		Console.WriteLine("правильная версия");
		minor.Duration_alignment();
		Wolfish_WAV.generate (minor);
		Console.WriteLine("Конец");
	}
}
