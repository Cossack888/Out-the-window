INCLUDE Globals.ink
{convo2finished: ->DONE}
{friendlyGreeting==true: Hello}
{hostileGreeting==true: What}
How Are you?
*[Feeling cranky today (END)]  ->ending
*[Fuck you !!! I'm Done(END)] ->ending


===ending===
~convo2finished=true
->DONE 