compile:
	dmcs synch.cs

run:
	./synch.exe

clean:
	rm -f *.exe

clean-all: 
	rm -f *.o
	rm -f *.exe

view-project:
	firefox https://github.com/kaleemullah360/ProcessSynchronization &

view-profile:
	firefox https://github.com/kaleemullah360/ProcessSynchronization &

push:
	git add -A
	git commit -m 'updates'
	git push origin master

pull:
	git pull origin master