# Custom documentation

This is an Umbraco package where project developers can easily add documentation to write down what a specific thing does in the application. It is meant to help editors, translators and other users read what the intended use for something was. 

The package supports:

- Markdown
- HTML and CSS
- Plain text 

## Installation

Download the package from [Github](https://github.com/scott94621/umbraco-custom-documentation-section) or get it from [Our Umbraco](). 

This package uses Markdig version 0.17.0 to convert Markdown to Html. Downloading it is required for this package to function. Find it here on [nuget](https://www.nuget.org/packages/Markdig/).

## Usage

### Write documentation

To write documentation, go to the Documentation folder in the project root. Here you can add Markdown, HTML and plain text files. The folder structure will be rendered as the section tree in the Umbraco backoffice. To add a folder description, add a README.md file to the folder and this will be shown when the folder is clicked. 

To edit the Documentation dashboard, find the file at ~\App_Plugins\customDocumentation\Dashboard\cDocDashboard.html. 

Adding custom css can be done at ~\App_Plugins\customDocumentation\css\. The new files need to be included in the package.manifest in ~\App_Plugins\customDocumentation otherwise the css won't be used in the browser. Adding JavaScript files follows the same pattern, just add the files and include them and it should be good to go. 

## Future

* Test versions 7 and 8

* Publish?
	* Add basic umbraco languages

* Add dashboard for editing
* Add possibility of editing folder names in umbraco
* Add possibility for adding folders and children of three types in section
* Write unit tests for the classes
* Document the classes
* Change the name of the documentation folder
* Allow other types to be READMEs.

