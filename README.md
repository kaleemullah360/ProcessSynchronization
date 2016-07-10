# Part C: ProcessSynchronization:

S sharks and F fish live in an ocean with only ONE sea food-point serving sea-weed having N seats. A Fish can dine at a sea-food point as long as a shark does not arrive there simultaneously. If the shark sees the fish, it must eat the fish. There are N seats at the sea-food point, S sharks and F fish. Our sharks and fish love sea-weed, however, shark does not mind eating fish as well.

### Dependancies:

[Microsoft Visual Studio Express 2013](https://www.microsoft.com/en-pk/download/details.aspx?id=44914)

[.NET Framework 4.5](https://www.microsoft.com/en-pk/download/details.aspx?id=40779) will be required to execute.

### Run Application:

```sh
~Project_Dire\bin\Debug folder contain AOS-A1-P1.exe, input.txt and output.txt
```

## Group Members:

1. Nayyar Ahmed <MSCS14059@ITU.EDU.PK>
2. Tahir Ahmed  <MSCS14059@ITU.EDU.PK>
3. Kaleem Ullah <MSCS14059@ITU.EDU.PK>
4. Rai Rashid   <MSCS14059@ITU.EDU.PK>


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


