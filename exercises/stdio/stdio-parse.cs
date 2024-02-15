using static System.Console;
using static System.Math;
class main {
	static int Main() {
		char[] split_delimiters = {' ','\t','\n'};
		var split_options = System.StringSplitOptions.RemoveEmptyEntries;
		Error.WriteLine("x\tSin(x)\tCos(x)");
		for( string line = ReadLine(); line != null; line = ReadLine() ){
			var numbers = line.Split(split_delimiters,split_options);
			foreach(var number in numbers){
				double x = double.Parse(number);
				Error.WriteLine($"{x:N4}\t{Sin(x):N4}\t{Cos(x):N4}");
                }
        }
		return 0;
	}
}
