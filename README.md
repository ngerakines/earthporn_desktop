# Earth Porn Desktop Wallpaper App
Sets the top image on [Earth Porn](http://www.reddit.com/r/earthporn) as your desktop wallpaper.

## Installation

1. Install [.NET Core](https://www.microsoft.com/net/core#windows).
2. Clone the repository.
3. Execute the following commands in the root of the repository:

```
dotnet restore
```

That's it. 

## Running the app
To run the app, execute the following command:

```
dotnet run
```

The output should look something like this:

```
App Started
Talking to Reddit...
Reddit data retrieved: [URL]
Downloading file...
File downloaded to: [Path]
Setting desktop wallpaper...
Done!
```

Feel free to **deploy** the app on your machine, you can follow [this guide](https://docs.microsoft.com/en-us/dotnet/articles/core/deploying/).

## Contributing
All constributions are welcome. Please fork, make your changes and submit a pull request. 