using static System.Console;
using static System.Math;
using static complex;
using static cmath;

class complex_tests {
	static void Main() {
		complex minus_one = new complex(-1, 0);
		complex i = new complex(0, 1);
		complex pi = new complex(3.14159265359, 0);
		double Pi = 3.14159265359;
		complex target = new complex(0,1);

		complex sqrtmone = cmath.sqrt(minus_one);
		WriteLine($"sqrt(-1)\t={sqrtmone:G3}");
		if (sqrtmone.approx(i)) {
			WriteLine($"This is approx i={i:G3}");
		}
		Write("\n");
		
		complex sqrti = cmath.sqrt(i);
		double sqrttwo = Sqrt(2);
		target = new complex(1/sqrttwo, 1/sqrttwo);
		WriteLine($"sqrt(i)\t={sqrti:G3}");
		if (sqrti.approx(target)) {
			WriteLine($"This is approx ={target:G3}");
		}
		Write("\n");
		
		complex ei = cmath.exp(i);
		target = new complex(Cos(1), Sin(1));
		WriteLine($"e^i\t={ei:G3}");
		if (ei.approx(target)) {
			WriteLine($"This is approx ={target:G3}");
		}
		Write("\n");

		target = new complex(-1,0);
		complex eipi = cmath.exp(i*pi);
		WriteLine($"sqrt(e^(iπ))\t={eipi:G3}");
		if (eipi.approx(target)) {
			WriteLine($"This is approx ={target:G3}");
		}
		Write("\n");

		target = new complex(Exp(-Pi/2),0);
		complex ii = cmath.pow(i,i);
		WriteLine($"i^i\t={ii:G3}");
		if (ii.approx(target)) {
			WriteLine($"This is approx ={target:G3}");
		}
		Write("\n");

		target = i*pi/2;
		complex lni = cmath.log(i);
		WriteLine($"ln(i)\t={lni:G3}");
		if (lni.approx(target)) {
			WriteLine($"This is approx ={target:G3}");
		}
		Write("\n");

		target = -i*Sinh(-Pi);
		complex sinipi = cmath.sin(i*pi);
		WriteLine($"sin(iπ)\t={sinipi:G3}");
		if (sinipi.approx(target)) {
			WriteLine($"This is approx ={target:G3}");
		}
		Write("\n");
	}
}
