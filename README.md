# Part C: ProcessSynchronization:

S sharks and F fish live in an ocean with only ONE sea food-point serving sea-weed having N seats. A Fish can dine at a sea-food point as long as a shark does not arrive there simultaneously. If the shark sees the fish, it must eat the fish. There are N seats at the sea-food point, S sharks and F fish. Our sharks and fish love sea-weed, however, shark does not mind eating fish as well.

## Dependancies:

This project is created with Mono JIT compiler version 4.2.1 (Debian 4.2.1.102+dfsg2-7ubuntu4).
If you're not aware how to setup this project in Ubuntu OS just do

```sh
bash ./runonce.sh
```
it will install Mono along with its dependancies.

## Group Members:

1. Nayyar Ahmed <MSCS14051@ITU.EDU.PK>
2. Tahir Ahmed  <MSCS14042@ITU.EDU.PK>
3. Kaleem Ullah <MSCS14059@ITU.EDU.PK>
4. Rai Rashid   <MSCS14048@ITU.EDU.PK>

### Run Application:

compile program.

```sh
make compile
```
Then, to run the program do,

```sh
make run
```
for clean-up,

```sh
make clean
```

##Introduction to Problem:

1. Shark and fish should not eat at the same time. i.e. if a shark is occupying a seat at the sea-food point then no fish should be sitting at any seat in the sea-food point. Similarly, if a fish is sitting at a seat in the sea-food point, then no shark should be eating there. This will ensure that the fish is not consumed by the sharks.

2. Only one fish or one shark can sit at a given seat at any particular time.

3. Neither sharks nor fish should starve. A shark or fish that wants to eat should eventually be able to eat. e.g. a solution that permanently prevents all fish from eating would be unacceptable. So would a solution in which sharks were always given priority over fish. When we actually test your solution, each simulated
shark and fish will eat a finite number of times; however, even if the simulation were allowed to run forever, neither sharks nor fish should starve.


4. Your synchronization mechanism should not impose an upper bound on the number of seats that can be occupied simultaneously. i.e. if there are enough animals, it should be possible for them to occupy all available seats simultaneously.
Provide a file synch.c to provide solution to synchronization. Your solution should be implemented entirely in this file.
It can contain six functions, which are invoked by the "shark and fish simulation" created by you.


		sharkb4eating: called each time a shark eats, before it eats
		fishb4eating: called each time a fish eats, before it eats
		sharkafteating: called each time a shark eats, after it eats
		fishafteating: called each time a fish eats, after it eats
		sharkfishsyncinit: called only once, before the shark and fish are created
		sharfishsynccleanup: called only once, after all sharks and fish have finished


