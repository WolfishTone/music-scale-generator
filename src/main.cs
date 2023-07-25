using System;
using System.IO;


class HZ
{
public static void Main()
	{
		Console.WriteLine("инициализация формул лада");
		Tab.Scales_make_up();
		Console.WriteLine("генерация лада");
		Tab minor = new Tab("a", 1, 0); // нота, октава, номер лада (см. tab.cs scales[])
		Console.WriteLine("генерация звука");
		Wolfish_WAV.generate (minor);
		Console.WriteLine("Конец");
	}
}
