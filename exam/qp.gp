# Expects an input_file and output_file to be set in environment when called.
set terminal svg background "white"
set out out_file

plot \
	in_file using 1:2 index n_i with lines title columnheader	,\
	in_file using 1:3 index n_i with linespoints title columnheader	,\
	in_file using 1:4 index n_i with points title columnheader	,\

