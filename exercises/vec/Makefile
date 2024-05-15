VPATH=src
test_lib=../../test_framework

Out.txt : main.exe
	mono main.exe -test_count:1000 > Out.txt

main.exe : main.cs vec.dll AUnit.dll
	mcs -reference:vec.dll -reference:AUnit.dll $<
	
vec.dll : vec.cs
	mcs -target:library $<


AUnit.dll : $(test_lib)/AUnit.cs
	mcs -target:library -out:$@ $^
	#$(MAKE) -C $(test_lib)
	#cp $(test_lib)/$@ $@  

.PHONY: clean
clean: 
	rm *.dll *.exe *.txt
