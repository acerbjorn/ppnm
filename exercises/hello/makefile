out.txt : hello.exe
	mono hello.exe > out.txt

hello.exe : hello.cs
	mcs hello.cs

clean :
	rm -f out.txt hello.exe
