VAR fish_count = 0
VAR give_fish = false


=== dialog ===
If you want help battling ninjas give me three fishes.
*   Give him three fishes
    { fish_count > 2:
        Thank you give me one minute to eat them.
        Nomnomnom.
        ~ give_fish = true
    - else:
        Ah you don't have three fishes.
    }
*   No.

- ->END