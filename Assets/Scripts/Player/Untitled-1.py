
def start_game():
    print("Welcome to the Text Adventure Game!")
    print("You find yourself in a mysterious room.")
    print("There are two doors in front of you.")
    print("Which door do you choose? (1 or 2)")

    choice = input("> ")

    if choice == "1":
        room_1()
    elif choice == "2":
        room_2()
    else:
        print("Invalid choice. Please try again.")
        start_game()

def room_1():
    print("You entered room 1.")
    print("There is a key on the table.")
    print("What do you do? (1. Take the key, 2. Leave the key)")

    choice = input("> ")

    if choice == "1":
        print("You took the key.")
        print("You win!")
    elif choice == "2":
        print("You left the key.")
        print("Game over!")
    else:
        print("Invalid choice. Please try again.")
        room_1()

def room_2():
    print("You entered room 2.")
    print("There is a monster in the room!")
    print("What do you do? (1. Fight, 2. Run)")

    choice = input("> ")

    if choice == "1":
        print("You fought the monster.")
        print("You were defeated.")
        print("Game over!")
    elif choice == "2":
        print("You ran away from the monster.")
        print("You win!")
    else:
        print("Invalid choice. Please try again.")
        room_2()

start_game()
