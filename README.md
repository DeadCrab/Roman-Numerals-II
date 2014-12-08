Roman-Numerals-II
=================
Second crack at http://agilekatas.co.uk/katas/romannumerals-kata.html


To Do
-----
Suspect the structure of the CharacterSet classes could be greatly simplified


Things Learned
-------------- 
- right click -> Extract To Method
- Using Visual Studio with Git
- Using class level privates can help with structure of code. Not sure if this is good practise as methods are not stand alone
- Exponents and the like
- Throw your own custom exceptions even if the exception is a base one that you have wrapped in your custom exception - makes it clear the exception has occurred in your class (http://stackoverflow.com/questions/4761216/c-throwing-custom-exception-best-practices)


Things To Improve
-----------------
1) Define your exceptions early on. Means:
i) You don't forget to do it later
ii) You communicate your intentions clearly

2) Got lost in code in middle. Why? 
	- Coding Monday mornings to Wednesday mornings - always took a while to get headspace back to right place on Monday mornings
	- Continued suspicion that I overcomplicate things. Is this to do with diving in before having a full grasp of the problem?
		- Started with unit tests but there were not enough.
		- Missed rule about only being allowed to subtract from next two chars in sequence
		- Probably made loop too complicated in first instance