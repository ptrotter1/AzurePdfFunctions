# Azure PDF Functions
This is a simple Azure Function that converts posted HTML to a PDF file. It uses PuppeteerSharp to launch a headless 
browser and render the HTML to a PDF file. This project is configured to work on a Linux Consumption Plan.

## Deployment
Install the [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp#install-the-azure-functions-core-tools) and run the following command from the project's root directory:

```bash
func azure functionapp publish <functionappname> --dotnet-isolated
```

## Usage
To use this function, post HTML to the function URL. The function will return a PDF file.

Here's an example using `curl`:
```bash
curl -X POST -H "Content-Type: text/html" -d "<h1>Hello world!</h1><p>This PDF was rendered with Chromium!</p>" <function-url> -o output.pdf
```