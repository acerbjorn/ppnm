set terminal svg background "white"
set out "test_eq_graph.svg"
set xrange[-2:2]
set yrange[-2:2]
set isotropic

plot \
	"test_eq_out.txt" index 0 using (1/$2)*cos($1):(1/$2)*sin($1) with lines title "circular",\
	"test_eq_out.txt" index 1 using (1/$2)*cos($1):(1/$2)*sin($1) with lines title "elliptic",\
	"test_eq_out.txt" index 2 using (1/$2)*cos($1):(1/$2)*sin($1) with lines title "relativistic + elliptic"
