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