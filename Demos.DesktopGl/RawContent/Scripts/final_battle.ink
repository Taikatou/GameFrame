VAR victory = false
VAR enemy_health = 100

This is a fight between men petty words will be left out of this.
The dojo master attacks you arm what do you do?
->standard_options
The dojo master lies defeated on the ground.
You find a key on the floor.
~ victory = true
- ->END

===standard_options===
+   Aim for his leg
    You roll and try to cut his leg you leave him with a scratch.
    ~enemy_health = enemy_health - 20
    { enemy_health > 0:
        ->back_against_wall
    }
+   Kick him.
    You push him back before he can attack you.
    ~enemy_health = enemy_health - 5
    { enemy_health > 0:
        ->distance_between
    }
+   Spit at him.
    The dojo master is disgusted at you, however you have distracted him
    ->distracted
- ->END

===back_against_wall===
    Your back is against the wall what do you do?
    +   Jump at him swinging your sword.
        He pulls back after getting a small cut on his wrist
        ~enemy_health = enemy_health - 10
        { enemy_health > 0:
            ->distance_between
        }
    +   Dodge to the left predicting he attacks.
        His attack misses you.
        An he is caught un-aware.
        ->caught_unaware
    +   Run at him trying to get closer.
        ->caught_unaware
- ->DONE

===distance_between===
    You have some difference between the two of you what will you do?
    +   Jump and attack
        You cut his chest leaving him injured.
        ~enemy_health = enemy_health - 10
        { enemy_health > 0:
            ->standard_options
        }
    +   Charge towards him
        ->caught_unaware
    +   Close distance
        ->standard_options
- ->DONE

===distracted===
    The dojo master is distracted.
    +   Charge in and attack.
        You make a big cut across his arm.
        ~enemy_health = enemy_health - 30
        { enemy_health > 0:
            The dojo master takes a step back
            ->standard_options
        }
    +   Kick him onto floor
        { enemy_health > 20:
            The dojo master avoids your kick ad takes a step back.
            ->standard_options
        - else:
            ~enemy_health = 0
        }
- ->DONE

===caught_unaware===
    The have caught the dojo master unaware.
    +   Use dragon sword smash on dojo master
        ~enemy_health = enemy_health - 30
        { enemy_health > 0:
            The dojo master recoils with pain
            ->distance_between
        }
    +   Stab and close distance between you two.
        You leave a small cut on his leg.
        ~enemy_health = enemy_health - 15
        { enemy_health > 0:
            You have closed in on the dojo master
            ->standard_options
        }
- ->DONE