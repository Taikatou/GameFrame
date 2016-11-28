VAR x_pos = 0
VAR y_pos = 0
VAR story_over = false
Taikatou.
Today is the day are you ready?
*   Yes I am born ready.
    Perfect you won't mind starting straight away then.
    ->describe_mission
*   Uh...
    You will be fine, I was nervous before my final ninja exam too
    ->describe_mission
*   Ready for what?
    Today is the day you will finally become a fully qualified ninja.
    However in order to do so you must pass one last test.
    ->describe_mission
- ->END

===describe_mission===
~ x_pos = 7
~ y_pos = 5
travel west out in to the forest, we need someone to collect the princess.
I know this will be hard for you seeing as this is your first mission.
However we have no other choice, because of the war you are the only fully trained ninja left in the village.
~ story_over = true
- ->DONE