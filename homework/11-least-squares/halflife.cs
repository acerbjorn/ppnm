using static matrix;
using static lsfit;
using System;
using static System.Math;
using System.Collections.Generic;

class halflife {
    static void Main(string[] args) {
        // Should probably read these from file.
        vector times = new vector("1, 2, 3, 4, 6, 9, 10, 13, 15");
        vector activity = new vector("117, ,100, ,88, ,72, ,53, ,29.5, ,25.2, ,15.2, ,11.1");
        vector dA = new vector("6, 5, 4, 4, 4, 3, 3, 2, 2");
        vector dlnA = new vector("6, 5, 4, 4, 4, 3, 3, 2, 2");
        vector ln_act = activity.map(Math.Log);
        for (int i = 0; i<dA.size; i++) {
            dlnA[i] /= activity[i];
        }
        Func<double, double>[] fs = new Func<double, double>[2];
        fs[0] = (double x) => 1.0;
        fs[1] = (double x) => -x;

        lsfit decay_fit = new lsfit(fs, times, ln_act, dlnA);
        // decay_fit.betas.print();

        if (args.Length == 0) {
            decay_fit.covars.print("CoV:", "\t");
            Console.Write(format_halflife(decay_fit.betas, decay_fit.covars));
        } else if (args[0] == "data") {
            List<vector> datas = new List<vector>();
            datas.Add(times);
            datas.Add(activity);
            datas.Add(dA);
            Console.Write(gp_make_table(datas));
        } else if (args[0] == "fit"){
            Console.WriteLine(gp_format_plot_subcommand(decay_fit.betas, decay_fit.covars));
        } else {
            Console.Write("Unrecognized option");
        }
    }
    static string gp_format_plot_subcommand(
        vector betas, matrix covars = null, string numfmt="G3"
    ) {
        string lambda_half = to_halflife(betas[1]).ToString(numfmt);
        string fit_description = gp_format_fitfunc(Exp(betas[0]),betas[1], $"with line linecolor rgb \"red\" title \"\\lambda_½ = {lambda_half} day^{{-1}}\"");
        if (covars != null) {
            fit_description += ", \n";
            fit_description += gp_format_fitfunc(Exp(betas[0]-Sqrt(covars[0,0])), betas[1]+Sqrt(covars[1,1]), "with line dashtype 2 linecolor rgb \"red\" notitle, \n"); 
            fit_description += gp_format_fitfunc(Exp(betas[0]+Sqrt(covars[0,0])), betas[1]-Sqrt(covars[1,1]), "with line dashtype 2 linecolor rgb \"red\" notitle"); 
        }
        return fit_description;
    }
    static string gp_format_fitfunc(double a, double lambda, string line_desc) {
        return $"{a}*exp(-{lambda}*x) {line_desc}";
    }
    static string format_halflife(vector betas, matrix covars, string numfmt="G3") {
        string lambda_half = to_halflife(betas[1]).ToString(numfmt);
        string dlambda_half = hl_uncertainty(betas[1], Math.Sqrt(covars[1,1])).ToString(numfmt); // TODO
        return $"\\lambda_{{1/2}} = {lambda_half}\\pm {dlambda_half} day^{{-1}}";
    }
    static string gp_make_table(List<vector> values, string prefix = "") {
        int columns = values.Count;
        int rows = values[0].size;
        string ans = prefix;
        for (int i = 0; i<rows; i++) {
            for (int j = 0; j<columns; j++) {
                ans += $"{values[j][i]}\t"; 
            }   
            ans += "\n";
        }       
        return ans;
    }
    //static string gp_make_plot_command() {}
    
    static double to_halflife(double lifetime) {
        return Math.Log(2)/lifetime;
    }
    static double hl_uncertainty(double lifetime, double dlifetime) {
        Console.Write($"{dlifetime}\n");
        return Math.Log(2)/Math.Pow(lifetime,2)*dlifetime;
    }
}
