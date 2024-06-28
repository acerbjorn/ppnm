CODE = $(filter %.cs, $^) # Expanded when called. Can use := to expand immediately
LIBS = $(addprefix -reference:, $(filter %.dll, $^))
EXES = $(filter %.exe, $^)
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)
DMITLIBS = ../libs

.PHONEY: default
default: sinus1.svg wavelet1.svg wavelet2.svg

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

ls_error_conceal.dll: ls_error_conceal.cs matrix.dll qrgs.dll
	$(MKLIB)

matrix.dll : $(DMITLIBS)/matrix.cs $(DMITLIBS)/vector.cs $(DMITLIBS)/assertions.cs $(DMITLIBS)/extra_constructors.cs $(DMITLIBS)/interfaces.cs
	$(MKLIB)

lsfit.dll : ../../homework/11-least-squares/lsfit.cs matrix.dll
	$(MKLIB)

qrgs.dll: ../homework/lineq/qrgs.cs matrix.dll; $(MKLIB)
