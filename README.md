# vr-capital

## Status
Application is working

## Development
### Editor  
  -  Visual Studio 2015

### Source code 
Please clone the repo to C:\Source

### Test app 
A console application has been provided with a working model of the app 

## Design Descisions

### Dependency Injection 
Autofac used for IOC throughout, in particular using Autofac's keyed IIndex injector facilitating the generic import mechanism

### Sql Server SMO
Microsoft Sql SMO used for dynamic database interaction

## Assumptions
  -  If a column name appears in the file that is not in the db table it should be added with data type nvarchar(max)

## To-dos

  -  Configure database to store data properties, delimiters, extensions and process types 
  -  Add triggers for the data properties in the form of events, schedules etc
  -  Configure app to respond to triggers
  -  Currently adding untyped columns when input column name does not match db column name, mapping should be added
  -  Validation to be added to the SQL injector to parse data points into the format of their column and report errors 
  -  Overall error reporting of the application needs to be reviewed as I was tight for time