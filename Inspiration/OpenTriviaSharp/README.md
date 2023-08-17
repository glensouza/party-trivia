# OpenTriviaSharp

API wrapper for opentdb.com

[![CodeFactor](https://www.codefactor.io/repository/github/shiroechi/opentriviasharp/badge?style=for-the-badge)](https://www.codefactor.io/repository/github/shiroechi/opentriviasharp)

# Download 

![Nuget](https://img.shields.io/nuget/v/OpenTriviaSharp?style=for-the-badge)

# Feature
- Get randon questions
- Session token to prevent duplicate question

# Example

## Basic Usage

```C#
var client = new OpenTriviaClient();
var questions = await client.GetQuestionAsync(
    amount: 2, // number of question to retrieve ( 1 .. 50)
    type: TriviaType.Multiple, // question type (multiple choice or boolean)
    difficulty: Difficulty.Hard, // difficulty of the question (easy/medium/hard)
    category: Category.General); // category of the question
```

## Using session token

```C#
// create Open triva client 
var client = new OpenTriviaClient();

// request the token
var token = await client.client.RequestTokenAsync();

var questions = await client.GetQuestionAsync(
    amount: 2, // number of question to retrieve ( 1 .. 50)
    type: TriviaType.Multiple, // question type (multiple choice or boolean)
    difficulty: Difficulty.Hard, // difficulty of the question (easy/medium/hard)
    category: Category.General, // category of the question
    sessionToken: token);  // prevent duplicate question
```

Every requested `token` only available for 6 hours, after that you must reset the  `token`. If all question already retrieved with the `token`, you must reset it again or it will throw exception.

## Reset token

```C#
var client = new OpenTriviaClient();

var token = await client.client.RequestTokenAsync();

await client.ResetTokenAsync(token);
```

