im on apify and I'm seeing a number of scrapers.  
the glassdoor scraper automatically puts the job posting in the output - which means that it will be easier to parse the job description data.

the indeed scraper provides a list of jobs and the urls to the jobs. 
 - this means that we'll have to navigate to the page and then target the description by div - which I have done, I believe - 

 <div id="jobDescriptionText" class="jobsearch-jobDescriptionText Jobsearch-JobComponent-description">

 so I think I wanna try the glassdoor one first, since it seems to be both cheaper and requires less steps to find the information.  I will move the example .json into this project folder.

