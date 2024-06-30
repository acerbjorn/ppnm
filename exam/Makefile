CODE = $(filter %.cs, $^) # Expanded when called. Can use := to expand immediately
LIBS = $(addprefix -reference:, $(filter %.dll, $^))
EXES = $(filter %.exe, $^)
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)
TIME = time --output=$@ --append --quiet --format "$$timed_var\t%e\t%U\t%x"
DMITLIBS = ../libs

.PHONEY: default
default: sinus1.svg wavelet1.svg wavelet2.svg comp_wavelet.svg

scenic: default
	imv *.svg

sinus1.svg: Out.txt qp.gp
	gnuplot -e "out_file='$@'; in_file='$<'; n_i=0;title='f(x)=cos(x)*sin(2x)';" qp.gp

wavelet1.svg: Out.txt qp.gp
	gnuplot -e "out_file='$@'; in_file='$<'; n_i=1;title='f(x)=cos(x)*sin(2x)';" qp.gp

wavelet2.svg: Out.txt qp.gp
	gnuplot -e "out_file='$@'; in_file='$<'; n_i=2;title='f(x)=cos(x)*sin(2x)';" qp.gp

Out.txt: lsec_test.exe
	mono $< > $@

lsec_test.exe: lsec_test.cs ls_error_conceal.dll matrix.dll
	$(MKEXE)
	
comp_wavelet.svg: comparison.txt err_plot.gp
	gnuplot -e "out_file='$@'; in_file='$<'; n_i=0;title='Error on wavelets in the range [-10:10] with 0.1 between points';" err_plot.gp

comparison.txt: spline_vs_lsec.exe
	mono $< > $@

spline_vs_lsec.exe: spline_vs_lsec.cs ls_error_conceal.dll matrix.dll splines.dll
	$(MKEXE)

timed.svg: split_time.txt time.gp
	gnuplot -e "out_file='$@'; in_file='$<';" time.gp
	
split_time.txt: split_test.exe
	rm -f $@
	for timed_var in $$(seq 100 1000 44100); do\
		 $(TIME) mono $< -sample_rate:$$timed_var;\
	done
	echo >> $@
	echo >> $@
	for timed_var in $$(seq 100 100 3100); do\
		 $(TIME) mono $< -no_chunk -sample_rate:$$timed_var;\
	done
	

Out_split.txt: split_test.exe
	mono $< > $@

split_test.exe: split_test.cs ls_error_conceal.dll matrix.dll
	$(MKEXE)
	
ls_error_conceal.dll: ls_error_conceal.cs matrix.dll qrgs.dll
	$(MKLIB)

matrix.dll : $(DMITLIBS)/matrix.cs $(DMITLIBS)/vector.cs $(DMITLIBS)/assertions.cs $(DMITLIBS)/extra_constructors.cs $(DMITLIBS)/interfaces.cs
	$(MKLIB)

lsfit.dll : ../../homework/11-least-squares/lsfit.cs matrix.dll
	$(MKLIB)

qrgs.dll: ../homework/lineq/qrgs.cs matrix.dll; $(MKLIB)
splines.dll: ../homework/12-splines/splines.cs matrix.dll; $(MKLIB)
