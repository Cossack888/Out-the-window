INCLUDE Globals.ink
Welcome to my story.
I can not tell you how much it means to me that you came. 
*[ I did not want to come] --> sad 
*[ Yeah it is nice to meet you] --> happy

===sad===
I wish you hadn't said that. 
~hostileGreeting=true
->nextKnot

===happy===
It makes me happy that you think so.
~friendlyGreeting=true
-> nextKnot

===nextKnot===
{friendlyGreeting: Well since we are so cordial I want to let you in on a little secret} {hostileGreeting: You do not trust me yet? That will change...} You see I have made it my life's work to help the poor and unfortunate amongst us. I aim to start with you.
*[END]
->DONE