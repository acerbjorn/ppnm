using static System.Console;
using static System.Math;

class main {
    static int Main(string[] args) {
        double[] nums = new double[0];
        foreach (string arg in args) {
            string[] words = arg.Split(":");
            if (words[0] == "-numbers") {
                nums = parse_csv(words[1]);
            }
        }
        WriteLine($"x\tCos(x)\tSin(x)");
        foreach (double num in nums){
            WriteLine($"{num:N2}\t{Cos(num):N4}\t{Sin(num):N4}"); 
        }
        return 0;
    }
    
    static double[] parse_csv(string nums) {
        string[] split_nums = nums.Split(",");
        double[] output = new double[split_nums.Length];
        for (int i=0; i<split_nums.Length; i++){
            output[i] = double.Parse(split_nums[i]);
        }
        return output; 
    }
}
