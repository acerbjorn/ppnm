# Expects an input_file and output_file to be set in environment when called.
set terminal svg background "white"
set out out_file
set logscale xy
set title "Execution time compared to sample rate for a second 440 Hz tone signal"

plot \
	in_file using 1:2 index 0 with linespoints title "Chunked",\
	in_file using 1:2 index 1 with linespoints title "Unchunked",\

