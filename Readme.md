# Jobtimize

#### By: Jannon Sielaff


## Technologies Used

* C#
* OpenAI's API
* Newtonsoft.json
* .Net
* OpenXML

## Description

This is an automatic Cover Letter and Custom Resume creation script.  It will take a job description, send it to chatGPT and then have chatGPT write both a cover letter and update a resume based on (a) the overlap in skills between the user as outlined in ExistingSkills.cs and the job description, and (b) the github projects as defined in the GithubProject.cs file and the dictionary of GithubProjects in the Program.cs file.

## Pre-Requisites

1. You are going to need to parse data in a particular way.  I used Octoparse - specifically their linkedin job posting scraper.  This program is designed in such a way to be able to parse their json specifically.

2. Once you have scraped the data you need, you will need to manipulate the strings so that no forbidden characters exist in either the Job_title, Company, and Location fields since they are what is used to automatically populate the files created from the api call/scraped data

3. You are going to need an api key from openAI.  You will use their "https://api.openai.com/v1/completions" endpoint.  You will also need your own API key.  Be sure to store this in a .env file so that you don't give your api key to the whole world.  Once you have set up your api call to openai, you are ready to run the program.
## Setup Instructions 

1. Clone this repository.
2. Open your terminal (e.g., Terminal or GitBash) and navigate to this project's production directory called "/jobtimize.solution/".
3. In your terminal or GitBash, type dotnet update or dotnet restore to install whatever dependencies you may need to install.
4. Then type dotnet run - this will start the program.


## Known Bugs

* NA


## License 

MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

* Copyright (c) _2023_ _Jannon Sielaff_# Getting Started with Create React App

