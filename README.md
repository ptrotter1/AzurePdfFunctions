# Azure PDF Functions
This is a simple Azure Function that converts posted HTML to a PDF file. It uses PuppeteerSharp to launch a headless 
browser and render the HTML to a PDF file. This project is configured to work on a Linux Consumption Plan.

## Usage
To use this function, post HTML to the function URL. The function will return a PDF file.