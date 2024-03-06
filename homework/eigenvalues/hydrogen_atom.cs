using static matrix;
using static EVD;

class hydrogen_atom {
    static void Main(string[] args) {
        double r_max = 10.0;
        double dr = 1e-3;
        foreach(string arg in args) {
            string[] words = arg.Split(":");
            if (words[0] == "-dr") seed = (int)double.Parse(words[1]);
            if (words[0] == "-r_max") max_dim = (int)double.Parse(words[1]);
        }
    }
}
