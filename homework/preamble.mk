CODE = $(filter %.cs, $^) # Expanded when called. Can use := to expand immediately
LIBS = $(addprefix -reference:, $(filter %.dll, $^))
EXES = $(filter %.exe, $^)
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)
TIME = time --output=$@ --append --format "$$timed_var\t%e\t%U"
DMITLIBS = ../../libs

matrix.dll : $(DMITLIBS)/matrix.cs $(DMITLIBS)/vector.cs $(DMITLIBS)/assertions.cs $(DMITLIBS)/extra_constructors.cs $(DMITLIBS)/interfaces.cs
	$(MKLIB)

minimize.dll: ../19-minimization/minimize.cs matrix.dll qrgs.dll hessian.dll; $(MKLIB)
	
hessian.dll: ../19-minimization/hessian.cs matrix.dll qrgs.dll; $(MKLIB)
	
qrgs.dll: ../lineq/qrgs.cs matrix.dll; $(MKLIB)

genlist.dll: ../../exercises/genlist/genlist.cs; $(MKLIB)
