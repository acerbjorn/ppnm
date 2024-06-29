# Expects an input_file and output_file to be set in environment when called.
set terminal svg background "white"
set out out_file
set jitter
set logscale y

plot \
	in_file using 1:3 index n_i with dots title "Least Squares Error Concealment",\
	in_file using ($1+0.002):4 index n_i with dots title "Quadratic Spline",\

