# TombolaTest


This Api uses Docker, Docker Compose, Redis, and the Repository Pattern to provide a scalable, efficient, and maintainable API for managing bean information.

## Docker

Docker is all about containers. Think of it like wrapping up our app and everything it needs (libraries, tools, code, the whole shebang) into neat little packages. This is awesome because:

* **Everything's Consistent:** Docker makes sure the app runs the same way everywhere – whether you're developing it, testing it, or putting it live. No more "but it worked on my machine" headaches!
* **Super Portable:** We can easily move these containers around between different computers or cloud services without any hassle.
* **Keeps Things Separate:** Containers keep apps isolated from each other, so one app's problems don't mess up another.  Makes managing dependencies a breeze.
* **Deployment Made Easy:**  Putting the app live is a snap – just run the Docker container!

## Docker Compose

Docker Compose helps us manage multiple Docker containers at once.  Since our project likely has a few containers (the API itself, Redis):

* **Manages All the Containers:** We define all the containers and how they connect in a single `docker-compose.yml` file.
* **Makes Life Easier:** Compose simplifies starting, stopping, and managing all the containers together. It even makes sure they start in the right order.
* **Configures Everything:** We can set up things like port mappings and environment variables right in the Compose file.

## Redis

Redis is a super-fast in-memory data store. We're probably using it for:

* **Caching:**  Storing coffee bean info (or the stuff people look at most often) in Redis makes things faster.  It takes the load off the main database.
* **Session Management:** If people log in, Redis can store their session data for quick access.
* **Real-time Stuff:** If we have any real-time updates or features, Redis can handle the communication between different parts of the app.

## Repository Pattern

This pattern is like a middleman between how we get data (from the database, etc.) and what the app actually does with the data.  It's great because:

* **Keeps Things Flexible:** We can change how we get data without messing with the main app code.
* **Makes Testing Easy:** We can easily test the app without needing a real database.
* **Keeps Code Organized:** All the data-getting logic lives in one place, making the code cleaner and easier to work with.

## Intructions On How to run the app

Once you clone the project you should be able to run the app from the Run play button "Docker Compose" in you are Visual studio or in a cmd use the "docker-compose up" command

You will need to Run migration

- dotnet ef migrations add "InitialCreate"
- dotnet ef migration update

once the database and tables are created you will need to convert the allbeans json files into sql query to populate the database.