using System;
using static System.Math;

class integral_erf {
    static double Pi = 3.14159265359;
    static void Main() {
        // Dmitris standard-in reader
        var x_list = new genlist<double>();
        var erf_list= new genlist<double>();
        var erfc_list = new genlist<double>();
        var separators = new char[] {' ','\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        do{
                string line=Console.In.ReadLine();
                if(line==null)break; // finish when stdin ends
                if(line[0] == '#') continue; // ignore commented out lines
                string[] words=line.Split(separators, options);
                x_list.add(double.Parse(words[0]));
                erf_list.add(double.Parse(words[1]));
                erfc_list.add(double.Parse(words[2]));
        } while(true);

        
        vector zs = new vector(x_list.to_array());
        vector erf_ref = new vector(erf_list.to_array());
        vector erfc_ref = new vector(erfc_list.to_array());

        vector erfs1 = zs.map(erf);
        vector erfs2 = zs.map(erf_simple);
        vector erfs3 = zs.map(sfuns.erf);
        for(int i=0; i<zs.size; i++) {
            Console.WriteLine($"{zs[i]}\t{erfs1[i]-erf_ref[i]}\t{erfs2[i]-erf_ref[i]}\t{erfs3[i]-erf_ref[i]}");
        }
    }
    public static double erf(double z) {
        if (z<0) return -erf(-z);
        else if (z<1) return integrate.adaptive(
            (double x) => 2/Sqrt(Pi)*Exp(-x*x),
            0, z
        );
        else return 1-2/Sqrt(Pi)*integrate.adaptive(
            (double t) => Exp(-Pow((z+(1-t)/t),2))/t/t,
            0,1
        );
    }
    public static double erf_simple(double z) {
        return integrate.adaptive(
            (double x) => 2/Sqrt(Pi)*Exp(-x*x),
            0, z
        );
    }
}
