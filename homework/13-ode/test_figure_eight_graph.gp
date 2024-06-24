set terminal svg background "white"
set out "test_figure_eight_graph.svg"
set xrange[-2:2]
set yrange[-2:2]
set isotropic

plot \
	"test_figure_eight_out.txt" using 1:2 with lines title "circular",\
	# "test_figure_eight_out.txt" using 3:4 with lines title "elliptic",\
	# "test_figure_eight_out.txt" using 5:6 with lines title "relativistic + elliptic"
