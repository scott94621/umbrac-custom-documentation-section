
## Write documentation

To write documentation, go to the Documentation folder in the project root. Here you can add Markdown, HTML and plain text files. The folder structure will be rendered as the section tree in the Umbraco backoffice. To add a folder description, add a README.md file to the folder and this will be shown when the folder is clicked. 

To edit the Documentation dashboard, find the file at ~\App_Plugins\customDocumentation\Dashboard\cDocDashboard.html. 

Adding custom css can be done at ~\App_Plugins\customDocumentation\css\. The new files need to be included in the package.manifest in ~\App_Plugins\customDocumentation otherwise the css won't be used in the browser. Adding JavaScript files follows the same pattern, just add the files and include them and it should be good to go. 
