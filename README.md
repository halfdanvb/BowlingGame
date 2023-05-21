# BowlingGame
A system for bowling

The solution consists of 4 projects:
- Core (Domain models, interfaces, services etc.)
- Infrastructure (External dependencies)
- Test (Unit tests)
- Application (Simple console app to test the system)

Design is insprired by "Clean Architecture" and "Domain Driven Design", keeping the domain logic central while having loose coupling to external dependencies.

I'm using the repository pattern with aggregate root to ensure consistency across related objects.

After the first iteration of the system I found the logic within the GameDomain class to be difficult to read and maintain. Therefore the strategy pattern has been implemented in order to change the logic on runtime depending on wether it's a regular turn or the last turn. 

The console appplication is supposed to emulate a stateless bowling lane, handing over scores to the system and displaying the scoreboard. State management and game flow is handled entirely by the database and domain services.
