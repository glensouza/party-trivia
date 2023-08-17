# OpenTriviaDbNet
Asynchronous api implementation for Open Trivia Database written in .net standard

The api is instance based making it suitable for injection

Open Trivia Database: https://opentdb.com/

## Install
Available as nuget: https://www.nuget.org/packages/OpenTriviaDbNet/

## Example
```
// create api instance
var api = new OpenTriviaDbNetApi();
```
```
// session token ensures unique questions
await api.CreateSessionTokenAsync();
```
```
// get 10 questions
var questions = await api.GetQuestionsAsync(10);
```
