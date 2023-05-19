# BowlingGame
A system for bowling

The solution consists of 4 projects:
- Core (Domain models, interfaces, services etc.)
- Infrastructure (External dependencies)
- Test (Unit tests)
- Application (Simple console app to test the system)

Design is insprired by "Clean Architecture" and "Domain Driven Design", keeping the domain logic central while having loose coupling to external dependencies.

I'm using the repository pattern with aggregate root to ensure consistency across related objects.

The console appplication is supposed to emulate a stateless bowling lane, handing over scores to the system and displaying the scoreboard. State management and game flow is handled entirely by the database and domain services.

More validation, tests, and error handling would have been nice, but I'm out of time :) 
