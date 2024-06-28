set terminal svg background "white"
set out "test_figure_eight_graph.svg"
set xrange[-2:2]
set yrange[-2:2]
set isotropic

plot \
	"test_figure_eight_out.txt" using 1:($2+1) with lines title "O_1 (shifted up by 1)",\
	"test_figure_eight_out.txt" using 3:4 with lines title      "O_2                  ",\
	"test_figure_eight_out.txt" using 5:($6-1) with lines title "O_3 (shifted down by 1)"
