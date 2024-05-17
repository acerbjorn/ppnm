CODE = $(filter %.cs, $^) # Expanded when called. Can use := to expand immediately
LIBS = $(addprefix -reference:, $(filter %.dll, $^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

default: erf.gnuplot.svg fgamma.gnuplot.svg lngamma.gnuplot.svg 
scenic: default
	imv *.svg

erf.gnuplot.svg: Out-erf.txt Makefile
	echo '\
		set terminal svg background "white";\
		set out "$@";\
		set key left;\
		set xlabel "x";\
		set ylabel "erf(x)";\
		set title "Error function";\
		set tics out ;\
		set grid ;\
		plot [-3:3] \
			"$<" with lines notitle,\
			"erf.data.txt" using 1:2 with points pointtype 6 notitle,\
			"erf.data.txt" using (-($$1)):(-($$2)) with points pointtype 6 notitle;\
	' | tee log.gpi | gnuplot 

fgamma.gnuplot.svg: Out-fgamma.txt Out-fact.txt Makefile
	echo '\
		set terminal svg background "white";\
		set out "$@";\
		set yrange [-3:3];\
		set xlabel "x";\
		set ylabel "Γ(x)";\
			set title "Gamma function";\
		set tics out ;\
		set grid ;\
		plot [-10:10] \
			"$<" with lines title "Γ(x)",\
			"Out-fact.txt" with points pointtype 6 title "(x-1)!";\
	' | tee log.gpi | gnuplot 

lngamma.gnuplot.svg: Out-lngamma.txt Out-lnfact.txt Makefile
	echo '\
		set terminal svg background "white";\
		set out "$@";\
		set key left;\
		set xlabel "x";\
		set ylabel "ln(Γ(x))";\
		set title "Logarithmic Gamma function";\
		set tics out ;\
		set grid ;\
		plot [0:20] \
			"$<" with lines title "ln(Γ(x))",\
			"Out-lnfact.txt" with points pointtype 6 title "ln((x-1)!)";\
	' | tee log.gpi | gnuplot 

Out-%.txt: %.exe
		mono $< > $@
		
sfuns.dll: sfuns.cs ; $(MKLIB)
%.exe: %.cs sfuns.dll; $(MKEXE)
