using static System.Math;
using static System.Console;
using static sfuns;

class math {
	static void Main() {
		double sqrt2=Sqrt(2.0);
		Write($"sqrt2^2 = {sqrt2*sqrt2} (should equal 2)\n");
		double fifthrt2 = Pow(2.0, 0.2);
		Write($"2^(1/5)^5 = {fifthrt2*fifthrt2*fifthrt2*fifthrt2*fifthrt2}\n");
		double etopi = Pow(E,PI);
		Write($"e^pi = {etopi}\n");
		Write("refval:23.1406926328\n");
		double pitoe = Pow(PI,E);
		Write($"pi^e = {pitoe}\n");
		Write("refval:22.4591577184\n");
		Write("\n-------GAMMA FUNCTIONS-------\n");
		for (int i=1; i<=10; i++) {
			GammaTest(i);
		}
		Write("\n-----LOG GAMMA FUNCTIONS------\n");
		for (int i=1; i<=10; i++) {
			LnGammaTest(i);
		}
	}
	static void GammaTest(double x) {
		Write($"Using sfuns \tΓ({x})=\t{fgamma(x)}\n");
		Write($"Using ({x}-1)!\tΓ({x})=\t{Fact((int)x-1)}\n");
	}
	static void LnGammaTest(double x) {
		Write($"Using sfuns \tln(Γ({x}))=\t{lngamma(x)}\n");
		Write($"Using ({x}-1)!\tln(Γ({x}))=\t{Log(Fact((int)x-1))}\n");
	}
}
