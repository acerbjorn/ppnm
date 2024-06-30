# Expects an input_file and output_file to be set in environment when called.
set terminal svg background "white"
set out out_file
set ylabel "Energy"
set xlabel xvar
set title sprintf("Convergence of varying %s",xvar)

plot in_file using 1:2 with lines notitle

