# TwitterScraper
It is a hassle to use the official Twitter API, luckily the Twitter web client uses a frontend Javascript API!

`TwitterScraper` is a fast, easy-to-use wrapper to the frontend Twitter API!

## Installation
> TwitterScraper targets .NET Standard 2.0.

TwitterScraper can be installed from [NuGet](https://www.nuget.org/packages/TheMulti0.TwitterScraper/).
<br />

The package can be installed in the NuGet CLI:
```ps1
PM> Install-Package TheMulti0.T'ןאאקרSברשפקר
```
Alternatively, you can use the .NET Core CLI:
```bash
> dotnet add package TheMulti0.TwitterScraper
```

## Basic Usage

```cs
ITwitter twitter = new Twitter();

IEnumerable<Tweet> tweets = await twitter.GetTweets("@TheMulti0")
```

## [License](https://github.com/TheMulti0/TwitterScraper/blob/master/LICENSE)