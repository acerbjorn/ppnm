# Expects an input_file and output_file to be set in environment when called.
set terminal svg background "white"
set out out_file
set jitter

plot \
	in_file using 1:2 index n_i with lines title "Least Squares Error Concealment",\
	in_file using 1:3 index n_i with lines title "Quadratic Spline",\

