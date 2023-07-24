using System;
using System.IO;


class HZ
{
public static void Main()
	{
		Scale.Make_up();
		Scale minor = new Scale("a", 1, 0);
		Wolfish_WAV.generate (minor);
		Console.WriteLine("Конец");
	}
}
