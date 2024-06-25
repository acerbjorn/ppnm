.PHONEY: default
default: linterp_cos.svg qspline_sin.svg

include ../preamble.mk

scenic: *.svg
	imv *.svg

linterp_cos.svg: linterp_test.gp cos_x_linear.txt 
	gnuplot $< 

cos_x_linear.txt: linterp_test.exe
	mono $< >$@

linterp_test.exe: linterp_test.cs splines.dll matrix.dll
	$(MKEXE)

qspline_sin.svg: qspline_sin.gp qspline_sin.txt 
	gnuplot $< 

qspline_sin.txt: qspline_sin.exe
	mono $< >$@

qspline_sin.exe: qspline_sin.cs splines.dll matrix.dll
	$(MKEXE)

splines.dll: splines.cs matrix.dll
	$(MKLIB)


.PHONEY: clean
clean: 
