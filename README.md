
### Running the tests

To run the unit tests, open the Test Explorer in Visual Studio and click "Run All".

## API Endpoints

### Bingo Game

- `POST /api/bingo`: Starts a new Bingo game.
- `POST /api/bingo/player`: Adds a new player to the Bingo game.
- `POST /api/bingo/player/{id}/card`: Generates a new Bingo card for the specified player.
- `POST /api/bingo/player/{id}/mark`: Marks a number on the specified player's card.
- `POST /api/bingo/call`: Calls the next number in the Bingo game.
- `GET /api/bingo/player/{id}`: Gets the current state of the specified player in the Bingo game.
- `GET /api/bingo/caller`: Gets the current state of the Bingo caller in the game.
- `GET /api/bingo`: Gets the current state of the Bingo game.

### Scrum Poker

- `POST /api/scrum`: Starts a new Scrum Poker game.
- `POST /api/scrum/player`: Adds a new player to the Scrum Poker game.
- `POST /api/scrum/player/{id}/vote`: Submits a vote for the specified player in the Scrum Poker game.
- `GET /api/scrum/player/{id}`: Gets the current state of the specified player in the Scrum Poker game.
- `GET /api/scrum`: Gets the current state of the Scrum Poker game.

## Built With

- C#
- ASP.NET Core
- SignalR
- Flusor  

## Authors

- David Brighton


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
